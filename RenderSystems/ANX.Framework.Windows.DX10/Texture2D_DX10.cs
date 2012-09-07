using System;
using System.IO;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class Texture2D_DX10 : BaseTexture2D<Dx10.Texture2D>, INativeTexture2D
	{
		#region Public
		public override int Width
		{
			get
			{
				return NativeTexture != null ? NativeTexture.Description.Width : 0;
			}
		}

		public override int Height
		{
			get
			{
				return NativeTexture != null ? NativeTexture.Description.Height : 0;
			}
		}

		protected internal Dx10.ShaderResourceView NativeShaderResourceView { get; protected set; }
		#endregion

		#region Constructor
		internal Texture2D_DX10(GraphicsDevice graphicsDevice, SurfaceFormat surfaceFormat)
			: base(graphicsDevice, surfaceFormat)
		{
		}

		public Texture2D_DX10(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
			: base(graphicsDevice, surfaceFormat)
		{
			if (mipCount > 1)
				throw new Exception("creating textures with mip map not yet implemented");

			Dx10.Texture2DDescription description = new Dx10.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = mipCount,
				ArraySize = mipCount,
				Format = BaseFormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = Dx10.ResourceUsage.Dynamic,
				BindFlags = Dx10.BindFlags.ShaderResource,
				CpuAccessFlags = Dx10.CpuAccessFlags.Write,
				OptionFlags = Dx10.ResourceOptionFlags.None,
			};

			Dx10.Device device = (graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10).NativeDevice;
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

		#region Dispose
		public override void Dispose()
		{
			if (NativeShaderResourceView != null)
			{
				NativeShaderResourceView.Dispose();
				NativeShaderResourceView = null;
			}

			base.Dispose();
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
		protected override IntPtr MapWrite()
		{
			tempSubresource = Dx10.Texture2D.CalculateSubResourceIndex(0, 0, 1);
			DataRectangle rect = NativeTexture.Map(tempSubresource, Dx10.MapMode.WriteDiscard, Dx10.MapFlags.None);
			pitch = rect.Pitch;
			return rect.DataPointer;
		}
		#endregion

		#region MapRead
		protected override IntPtr MapRead()
		{
			tempSubresource = Dx10.Texture2D.CalculateSubResourceIndex(0, 0, 1);
			DataRectangle rect = NativeTexture.Map(tempSubresource, Dx10.MapMode.Read, Dx10.MapFlags.None);
			pitch = rect.Pitch;
			return rect.DataPointer;
		}
		#endregion

		#region Unmap
		protected override void Unmap()
		{
			NativeTexture.Unmap(tempSubresource);
		}
		#endregion
	}
}
