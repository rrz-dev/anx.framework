#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
	public partial class DxTexture2D : INativeTexture2D
	{
        private Dx11.Texture2D NativeTextureStaging;

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

		protected internal Dx11.ShaderResourceView NativeShaderResourceView { get; protected set; }

        internal Dx11.Texture2D NativeTexture { get; set; }

		#region Constructor
		internal DxTexture2D(GraphicsDevice graphicsDevice, SurfaceFormat surfaceFormat)
			: this(graphicsDevice, surfaceFormat, 1)
		{
		}

		public DxTexture2D(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
			: this(graphicsDevice, surfaceFormat, mipCount)
		{
			Dx11.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice.Device;
			var sampleDescription = new SharpDX.DXGI.SampleDescription(1, 0);

			if (useRenderTexture)
			{
				var descriptionStaging = new Dx11.Texture2DDescription()
				{
					Width = width,
					Height = height,
					MipLevels = mipCount,
					ArraySize = mipCount,
					Format = DxFormatConverter.Translate(surfaceFormat),
					SampleDescription = sampleDescription,
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
				Format = DxFormatConverter.Translate(surfaceFormat),
				SampleDescription = sampleDescription,
				Usage = useRenderTexture ? Dx11.ResourceUsage.Default : Dx11.ResourceUsage.Dynamic,
				BindFlags = Dx11.BindFlags.ShaderResource,
				CpuAccessFlags = useRenderTexture ? Dx11.CpuAccessFlags.None : Dx11.CpuAccessFlags.Write,
			};

			NativeTexture = new Dx11.Texture2D(device, description);
			NativeShaderResourceView = new Dx11.ShaderResourceView(device, NativeTexture);
		}
		#endregion

		#region GetHashCode
		public override int GetHashCode()
		{
			return NativeTexture.NativePointer.ToInt32();
		}
		#endregion

		#region SaveAsJpeg (TODO)
		public void SaveAsJpeg(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx11.Texture2D.ToStream(NativeTexture.Device.ImmediateContext, NativeTexture, Dx11.ImageFileFormat.Jpg, stream);
		}
		#endregion

		#region SaveAsPng (TODO)
		public void SaveAsPng(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx11.Texture2D.ToStream(NativeTexture.Device.ImmediateContext, NativeTexture, Dx11.ImageFileFormat.Png, stream);
		}
		#endregion

		#region GetData (TODO)
		public void GetData<T>(T[] data) where T : struct
		{
			GetData(data, 0, data.Length);
		}

		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetData<T>(int level, Framework.Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}
		#endregion

		#region MapWrite
		protected IntPtr MapWrite(int level)
		{
			Dx11.DeviceContext context = (GraphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice;
			tempSubresource = Dx11.Texture2D.CalculateSubResourceIndex(level, 0, mipCount);
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			DataBox box = context.MapSubresource(texture, tempSubresource,
				useRenderTexture ? Dx11.MapMode.Write : Dx11.MapMode.WriteDiscard, Dx11.MapFlags.None);
			pitch = box.RowPitch;
			return box.DataPointer;
		}
		#endregion

		#region MapRead
		protected IntPtr MapRead(int level)
		{
			Dx11.DeviceContext context = (GraphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice;
			tempSubresource = Dx11.Texture2D.CalculateSubResourceIndex(level, 0, mipCount);
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			DataBox box = context.MapSubresource(texture, tempSubresource, Dx11.MapMode.Read, Dx11.MapFlags.None);
			pitch = box.RowPitch;
			return box.DataPointer;
		}
		#endregion

		#region Unmap
		protected void Unmap()
		{
			Dx11.DeviceContext context = (GraphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice;
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			context.UnmapSubresource(texture, tempSubresource);

			if(useRenderTexture)
				context.CopyResource(NativeTextureStaging, NativeTexture);
		}
		#endregion
	}
}
