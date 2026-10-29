using System;
using System.IO;
using System.Runtime.InteropServices;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class Texture2D_Metro : INativeTexture2D
	{
		#region Private
		protected bool useRenderTexture;
		protected Dx11.Texture2D NativeTextureStaging;
		protected internal Dx11.Texture2D NativeTexture;
        protected internal Dx11.ShaderResourceView NativeShaderResourceView;
		protected int formatSize;
		protected SurfaceFormat surfaceFormat;
		protected GraphicsDevice graphicsDevice;
		private int mipCount;
		#endregion

		#region Public
		public int Width
		{
			get
			{
				return NativeTexture != null ? NativeTexture.Description.Width : 0;
			}
		}

		public int Height
		{
			get
			{
				return NativeTexture != null ? NativeTexture.Description.Height : 0;
			}
		}
		#endregion

		#region Constructor
		protected Texture2D_Metro(GraphicsDevice graphicsDevice)
		{
			this.graphicsDevice = graphicsDevice;
		}

		public Texture2D_Metro(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
		{
			this.mipCount = mipCount;
			useRenderTexture = mipCount > 1;
			this.graphicsDevice = graphicsDevice;
			this.surfaceFormat = surfaceFormat;

			GraphicsDeviceWindowsMetro graphicsMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = graphicsMetro.NativeDevice.NativeDevice;

			if (useRenderTexture)
			{
				var descriptionStaging = new Dx11.Texture2DDescription()
				{
					Width = width,
					Height = height,
					MipLevels = mipCount,
					ArraySize = mipCount,
					Format = FormatConverter.Translate(surfaceFormat),
					SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
					Usage = Dx11.ResourceUsage.Staging,
					CpuAccessFlags = Dx11.CpuAccessFlags.Write,
				};
				NativeTextureStaging = new Dx11.Texture2D(device, descriptionStaging);
			}

			var description = new Dx11.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = mipCount,
				ArraySize = mipCount,
				Format = FormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = useRenderTexture ? Dx11.ResourceUsage.Default : Dx11.ResourceUsage.Dynamic,
				BindFlags = Dx11.BindFlags.ShaderResource,
				CpuAccessFlags = useRenderTexture ? Dx11.CpuAccessFlags.None : Dx11.CpuAccessFlags.Write,
			};
			this.NativeTexture = new Dx11.Texture2D(device, description);
			this.NativeShaderResourceView = new Dx11.ShaderResourceView(device, this.NativeTexture);

			// description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
			// more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

			this.formatSize = FormatConverter.GetSurfaceFormatSize(surfaceFormat);
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return NativeTexture.NativePointer.ToInt32();
		}
		#endregion

		#region SetData
		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data, int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes, T[] data, int startIndex, int elementCount)
			where T : struct
		{
			//TODO: handle offsetInBytes parameter
			//TODO: handle startIndex parameter
			//TODO: handle elementCount parameter

			unsafe
			{
				GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
				byte* colorData = (byte*)handle.AddrOfPinnedObject();

				switch (surfaceFormat)
				{
					case SurfaceFormat.Color:
						SetDataColor(0, offsetInBytes, colorData, startIndex, elementCount);
						return;

					case SurfaceFormat.Dxt1:
					case SurfaceFormat.Dxt3:
					case SurfaceFormat.Dxt5:
						SetDataDxt(0, offsetInBytes, colorData, startIndex, elementCount, data.Length);
						return;
				}

				handle.Free();
			}

			throw new Exception(String.Format("creating textures of format {0} not yet implemented...", surfaceFormat));
		}

		public void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			//TODO: handle rect parameter
			if (rect != null)
				throw new Exception("Texture2D SetData with rectangle is not yet implemented!");

			unsafe
			{
				GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
				byte* colorData = (byte*)handle.AddrOfPinnedObject();

				switch (surfaceFormat)
				{
					case SurfaceFormat.Color:
						SetDataColor(level, 0, colorData, startIndex, elementCount);
						return;

					case SurfaceFormat.Dxt1:
					case SurfaceFormat.Dxt3:
					case SurfaceFormat.Dxt5:
						SetDataDxt(level, 0, colorData, startIndex, elementCount, data.Length);
						return;
				}

				handle.Free();
			}

			throw new Exception(String.Format("creating textures of format {0} not yet implemented...", surfaceFormat));
		}
		#endregion

		#region SetDataColor
		private unsafe void SetDataColor(int level, int offsetInBytes, byte* colorData, int startIndex, int elementCount)
		{
			int mipmapWidth = Math.Max(Width >> level, 1);
			int mipmapHeight = Math.Max(Height >> level, 1);

			int subresource = Dx11.Texture2D.CalculateSubResourceIndex(level, 0, mipCount);
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			SharpDX.DataBox rectangle = NativeDxDevice.Current.MapSubresource(texture, subresource);
			
			int srcIndex = 0;
			byte* pTexels = (byte*)rectangle.DataPointer;
			for (int row = 0; row < mipmapHeight; row++)
			{
				int rowStart = row * rectangle.RowPitch;

				for (int col = 0; col < mipmapWidth; col++)
				{
					int colStart = rowStart + (col * formatSize);
					pTexels[colStart++] = colorData[srcIndex++];
					pTexels[colStart++] = colorData[srcIndex++];
					pTexels[colStart++] = colorData[srcIndex++];
					pTexels[colStart++] = colorData[srcIndex++];
				}
			}

			NativeDxDevice.Current.UnmapSubresource(texture, subresource);
			if (useRenderTexture)
				NativeDxDevice.Current.NativeContext.CopyResource(NativeTextureStaging, NativeTexture);
		}
		#endregion

		#region SetDataDxt
		private unsafe void SetDataDxt(int level, int offsetInBytes, byte* colorData, int startIndex, int elementCount,
			int dataLength)
		{
			int mipmapWidth = Math.Max(Width >> level, 1);
			int mipmapHeight = Math.Max(Height >> level, 1);

			int w = (mipmapWidth + 3) >> 2;
			int h = (mipmapHeight + 3) >> 2;
			formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

			int subresource = Dx11.Texture2D.CalculateSubResourceIndex(level, 0, mipCount);
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			SharpDX.DataBox rectangle = NativeDxDevice.Current.MapSubresource(texture, subresource);

			var ds = new SharpDX.DataStream(rectangle.DataPointer, mipmapWidth * mipmapHeight * 4 * 2, true, true);
			int col = 0;
			int index = 0; // startIndex
			int count = dataLength; // elementCount
			int actWidth = w * formatSize;

			for (int i = 0; i < h; i++)
			{
				ds.Position = (i * rectangle.RowPitch) + (col * formatSize);
				if (count <= 0)
					break;
				else if (count < actWidth)
				{
					for (int idx = index; idx < index + count; idx++)
						ds.WriteByte(colorData[idx]);
					break;
				}

				for (int idx = index; idx < index + actWidth; idx++)
					ds.WriteByte(colorData[idx]);

				index += actWidth;
				count -= actWidth;
			}

			NativeDxDevice.Current.UnmapSubresource(texture, subresource);
			if (useRenderTexture)
				NativeDxDevice.Current.NativeContext.CopyResource(NativeTextureStaging, NativeTexture);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (NativeShaderResourceView != null)
			{
				NativeShaderResourceView.Dispose();
				NativeShaderResourceView = null;
			}

			if (NativeTexture != null)
			{
				NativeTexture.Dispose();
				NativeTexture = null;
			}

			if (NativeTextureStaging != null)
			{
				NativeTextureStaging.Dispose();
				NativeTextureStaging = null;
			}
		}
		#endregion

		#region SaveAsJpeg (TODO)
		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SaveAsPng (TODO)
		public void SaveAsPng(Stream stream, int width, int height)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetData (TODO)
		public void GetData<T>(int level, Rectangle? rect, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
