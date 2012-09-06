using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.RenderSystem.Windows.DX10.Helpers;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class RenderTarget2D_DX10 : Texture2D_DX10, INativeRenderTarget2D, INativeTexture2D
	{
		#region Constructor
		public RenderTarget2D_DX10(GraphicsDevice graphics, int width, int height, bool mipMap, SurfaceFormat preferredFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
            : base(graphics)
        {
            if (mipMap)
                throw new NotImplementedException("creating RenderTargets with mip map not yet implemented");

            this.surfaceFormat = surfaceFormat;

            GraphicsDeviceWindowsDX10 graphicsDX10 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10;
            Dx10.Device device = graphicsDX10.NativeDevice;

            var description = new Dx10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
                Format = FormatConverter.Translate(preferredFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = Dx10.ResourceUsage.Default,
                BindFlags = Dx10.BindFlags.ShaderResource | Dx10.BindFlags.RenderTarget,
                CpuAccessFlags = Dx10.CpuAccessFlags.None,
                OptionFlags = Dx10.ResourceOptionFlags.None,
            };
            nativeTexture = new Dx10.Texture2D(graphicsDX10.NativeDevice, description);
            nativeShaderResourceView = new Dx10.ShaderResourceView(graphicsDX10.NativeDevice, nativeTexture);

            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

            formatSize = FormatConverter.FormatSize(surfaceFormat);
		}
		#endregion
	}
}
