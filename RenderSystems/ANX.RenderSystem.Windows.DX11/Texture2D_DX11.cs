using System;
using System.IO;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX;
using Dx11 = SharpDX.Direct3D11;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class Texture2D_DX11 : BaseTexture2D<Dx11.Texture2D>, INativeTexture2D
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

		protected internal Dx11.ShaderResourceView NativeShaderResourceView { get; protected set; }
		#endregion

		#region Constructor
		internal Texture2D_DX11(GraphicsDevice graphicsDevice, SurfaceFormat surfaceFormat)
			: base(graphicsDevice, surfaceFormat)
		{
		}

		public Texture2D_DX11(GraphicsDevice graphicsDevice, int width, int height, SurfaceFormat surfaceFormat, int mipCount)
			: base(graphicsDevice, surfaceFormat)
		{
			if (mipCount > 1)
				throw new Exception("creating textures with mip map not yet implemented");
			
			var description = new Dx11.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = mipCount,
				ArraySize = mipCount,
				Format = BaseFormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = Dx11.ResourceUsage.Dynamic,
				BindFlags = Dx11.BindFlags.ShaderResource,
				CpuAccessFlags = Dx11.CpuAccessFlags.Write,
				OptionFlags = Dx11.ResourceOptionFlags.None,
			};

			Dx11.DeviceContext context = (graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11).NativeDevice;
			NativeTexture = new Dx11.Texture2D(context.Device, description);
			NativeShaderResourceView = new Dx11.ShaderResourceView(context.Device, NativeTexture);
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
			Dx11.DeviceContext context = (GraphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11).NativeDevice;
			tempSubresource = Dx11.Texture2D.CalculateSubResourceIndex(0, 0, 1);
			DataBox box = context.MapSubresource(NativeTexture, tempSubresource, Dx11.MapMode.WriteDiscard, Dx11.MapFlags.None);
			pitch = box.RowPitch;
			return box.DataPointer;
		}
		#endregion

		#region MapRead
		protected override IntPtr MapRead()
		{
			Dx11.DeviceContext context = (GraphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11).NativeDevice;
			tempSubresource = Dx11.Texture2D.CalculateSubResourceIndex(0, 0, 1);
			DataBox box = context.MapSubresource(NativeTexture, tempSubresource, Dx11.MapMode.Read, Dx11.MapFlags.None);
			pitch = box.RowPitch;
			return box.DataPointer;
		}
		#endregion

		#region Unmap
		protected override void Unmap()
		{
			Dx11.DeviceContext context = (GraphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11).NativeDevice;
			context.UnmapSubresource(NativeTexture, tempSubresource);
		}
		#endregion
	}
}
