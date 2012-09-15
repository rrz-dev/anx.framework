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

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
	public partial class DxTexture2D : INativeTexture2D
	{
        private Dx10.Texture2D NativeTextureStaging;
        
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

		internal Dx10.ShaderResourceView NativeShaderResourceView { get; set; }

        internal Dx10.Texture2D NativeTexture { get; set; }

		#region Constructor
		internal DxTexture2D(GraphicsDevice graphicsDevice, SurfaceFormat surfaceFormat)
			: this(graphicsDevice, surfaceFormat, 1)
		{
		}

		public DxTexture2D(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
			: this(graphicsDevice, surfaceFormat, mipCount)
		{
			Dx10.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice;

			if (useRenderTexture)
			{
				var descriptionStaging = new Dx10.Texture2DDescription()
				{
					Width = width,
					Height = height,
					MipLevels = mipCount,
					ArraySize = mipCount,
					Format = DxFormatConverter.Translate(surfaceFormat),
					SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
					Usage = Dx10.ResourceUsage.Staging,
					CpuAccessFlags = Dx10.CpuAccessFlags.Write,
				};
				NativeTextureStaging = new Dx10.Texture2D(device, descriptionStaging);
			}

			var description = new Dx10.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = mipCount,
				ArraySize = mipCount,
				Format = DxFormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = useRenderTexture ? Dx10.ResourceUsage.Default : Dx10.ResourceUsage.Dynamic,
				BindFlags = Dx10.BindFlags.ShaderResource,
				CpuAccessFlags = useRenderTexture ? Dx10.CpuAccessFlags.None : Dx10.CpuAccessFlags.Write,
				OptionFlags = Dx10.ResourceOptionFlags.None,
			};

			NativeTexture = new Dx10.Texture2D(device, description);
			NativeShaderResourceView = new Dx10.ShaderResourceView(device, NativeTexture);
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
			Dx10.Texture2D.ToStream(NativeTexture, Dx10.ImageFileFormat.Jpg, stream);
		}
		#endregion

		#region SaveAsPng (TODO)
		public void SaveAsPng(Stream stream, int width, int height)
		{
			// TODO: handle width and height?
			Dx10.Texture2D.ToStream(NativeTexture, Dx10.ImageFileFormat.Png, stream);
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
			tempSubresource = Dx10.Texture2D.CalculateSubResourceIndex(level, 0, mipCount);
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			DataRectangle rect = texture.Map(tempSubresource, useRenderTexture ? Dx10.MapMode.Write : Dx10.MapMode.WriteDiscard,
				Dx10.MapFlags.None);
			pitch = rect.Pitch;
			return rect.DataPointer;
		}
		#endregion

		#region MapRead
		protected IntPtr MapRead(int level)
		{
			tempSubresource = Dx10.Texture2D.CalculateSubResourceIndex(level, 0, mipCount);
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			DataRectangle rect = texture.Map(tempSubresource, Dx10.MapMode.Read, Dx10.MapFlags.None);
			pitch = rect.Pitch;
			return rect.DataPointer;
		}
		#endregion

		#region Unmap
		protected void Unmap()
		{
			var texture = useRenderTexture ? NativeTextureStaging : NativeTexture;
			texture.Unmap(tempSubresource);
			if (useRenderTexture)
				texture.Device.CopyResource(NativeTextureStaging, NativeTexture);
		}
		#endregion
	}
}
