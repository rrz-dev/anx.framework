using System;
using ANX.BaseDirectX;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
	public class RenderTarget2D_DX11 : Texture2D_DX11, INativeRenderTarget2D, INativeTexture2D
	{
		public RenderTarget2D_DX11(GraphicsDevice graphics, int width, int height, bool mipMap, SurfaceFormat surfaceFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
			: base(graphics, surfaceFormat)
		{
			if (mipMap)
				throw new NotImplementedException("creating RenderTargets with mip map not yet implemented");

			var description = new SharpDX.Direct3D11.Texture2DDescription()
			{
				Width = width,
				Height = height,
				MipLevels = 1,
				ArraySize = 1,
				Format = BaseFormatConverter.Translate(surfaceFormat),
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				Usage = SharpDX.Direct3D11.ResourceUsage.Default,
				BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | SharpDX.Direct3D11.BindFlags.RenderTarget,
				CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
				OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None,
			};

			SharpDX.Direct3D11.DeviceContext device = (GraphicsDevice.NativeDevice as GraphicsDeviceWindowsDX11).NativeDevice;
			NativeTexture = new SharpDX.Direct3D11.Texture2D(device.Device, description);
			NativeShaderResourceView = new SharpDX.Direct3D11.ShaderResourceView(device.Device, NativeTexture);
		}
	}
}
