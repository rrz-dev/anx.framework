#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
    public class RenderTarget2D_DX10 : DxTexture2D, INativeRenderTarget2D, INativeTexture2D
	{
		#region Constructor
		public RenderTarget2D_DX10(GraphicsDevice graphics, int width, int height, bool mipMap, SurfaceFormat surfaceFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
            : base(graphics, surfaceFormat)
        {
            if (mipMap)
                throw new NotImplementedException("creating RenderTargets with mip map not yet implemented");
			
            var description = new Dx10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
				Format = DxFormatConverter.Translate(surfaceFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = Dx10.ResourceUsage.Default,
                BindFlags = Dx10.BindFlags.ShaderResource | Dx10.BindFlags.RenderTarget,
                CpuAccessFlags = Dx10.CpuAccessFlags.None,
                OptionFlags = Dx10.ResourceOptionFlags.None,
            };

			Dx10.Device device = (GraphicsDevice.NativeDevice as GraphicsDeviceDX).NativeDevice;
			NativeTexture = new Dx10.Texture2D(device, description);
			NativeShaderResourceView = new Dx10.ShaderResourceView(device, NativeTexture);
		}
		#endregion
	}
}
