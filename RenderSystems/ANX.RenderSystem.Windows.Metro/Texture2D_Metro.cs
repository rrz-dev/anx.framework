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
		protected internal Dx11.Texture2D NativeTexture;
        protected internal Dx11.ShaderResourceView NativeShaderResourceView;
		protected int formatSize;
		protected SurfaceFormat surfaceFormat;
		protected GraphicsDevice graphicsDevice;
		#endregion

		#region Public
		public int Width
		{
			get
			{
				return NativeTexture != null ?
					NativeTexture.Description.Width :
					0;
			}
		}

		public int Height
		{
			get
			{
				return NativeTexture != null ?
					NativeTexture.Description.Height :
					0;
			}
		}
		#endregion

		#region Constructor
		protected Texture2D_Metro(GraphicsDevice graphicsDevice)
		{
			this.graphicsDevice = graphicsDevice;
		}

		public Texture2D_Metro(GraphicsDevice graphicsDevice, int width, int height,
			SurfaceFormat surfaceFormat, int mipCount)
		{
			if (mipCount > 1)
			{
				throw new Exception("creating textures with mip map not yet implemented");
			}

			this.graphicsDevice = graphicsDevice;
			this.surfaceFormat = surfaceFormat;

			GraphicsDeviceWindowsMetro graphicsMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
			var device = graphicsMetro.NativeDevice.NativeDevice;

			var description = new Dx11.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = mipCount,
				ArraySize = mipCount,
				Format = FormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = Dx11.ResourceUsage.Dynamic,
				BindFlags = Dx11.BindFlags.ShaderResource,
				CpuAccessFlags = Dx11.CpuAccessFlags.Write,
				OptionFlags = Dx11.ResourceOptionFlags.None,
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
		public void SetData<T>(int level, Rectangle? rect, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data)
			where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, 0, data.Length);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, T[] data,
			int startIndex, int elementCount) where T : struct
		{
			SetData<T>(graphicsDevice, 0, data, startIndex, elementCount);
		}

		public void SetData<T>(GraphicsDevice graphicsDevice, int offsetInBytes,
			T[] data, int startIndex, int elementCount) where T : struct
		{
			//TODO: handle offsetInBytes parameter
			//TODO: handle startIndex parameter
			//TODO: handle elementCount parameter
			
			if (this.surfaceFormat == SurfaceFormat.Color)
			{
				int subresource = Dx11.Texture2D.CalculateSubResourceIndex(0, 0, 1);
				SharpDX.DataBox rectangle = NativeDxDevice.Current.MapSubresource(NativeTexture, subresource);
				int rowPitch = rectangle.RowPitch;

				unsafe
				{
					GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
					byte* colorData = (byte*)handle.AddrOfPinnedObject();

					byte* pTexels = (byte*)rectangle.DataPointer;
					int srcIndex = 0;

					for (int row = 0; row < Height; row++)
					{
						int rowStart = row * rowPitch;

						for (int col = 0; col < Width; col++)
						{
							int colStart = col * formatSize;
							pTexels[rowStart + colStart + 0] = colorData[srcIndex++];
							pTexels[rowStart + colStart + 1] = colorData[srcIndex++];
							pTexels[rowStart + colStart + 2] = colorData[srcIndex++];
							pTexels[rowStart + colStart + 3] = colorData[srcIndex++];
						}
					}

					handle.Free();
				}

				NativeDxDevice.Current.UnmapSubresource(NativeTexture, subresource);
			}
			else if (surfaceFormat == SurfaceFormat.Dxt5 || surfaceFormat == SurfaceFormat.Dxt3 || surfaceFormat == SurfaceFormat.Dxt1)
			{
				unsafe
				{
					GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
					byte* colorData = (byte*)handle.AddrOfPinnedObject();

					int w = (Width + 3) >> 2;
					int h = (Height + 3) >> 2;
					formatSize = (surfaceFormat == SurfaceFormat.Dxt1) ? 8 : 16;

					int subresource = Dx11.Texture2D.CalculateSubResourceIndex(0, 0, 1);
					SharpDX.DataBox rectangle =
						NativeDxDevice.Current.MapSubresource(NativeTexture, subresource);
					SharpDX.DataStream ds = new SharpDX.DataStream(rectangle.DataPointer, Width * Height * 4 * 2, true, true);
					int pitch = rectangle.RowPitch;
					int col = 0;
					int index = 0; // startIndex
					int count = data.Length; // elementCount
					int actWidth = w * formatSize;

					for (int i = 0; i < h; i++)
					{
						ds.Position = (i * pitch) + (col * formatSize);
						if (count <= 0)
						{
							break;
						}
						else if (count < actWidth)
						{
							for (int idx = index; idx < index + count; idx++)
							{
								ds.WriteByte(colorData[idx]);
							}
							break;
						}

						for (int idx = index; idx < index + actWidth; idx++)
						{
							ds.WriteByte(colorData[idx]);
						}

						index += actWidth;
						count -= actWidth;
					}

					handle.Free();

					NativeDxDevice.Current.UnmapSubresource(NativeTexture, subresource);
				}
			}
			else
			{
				throw new Exception(string.Format("creating textures of format {0} not yet implemented...", surfaceFormat.ToString()));
			}
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
