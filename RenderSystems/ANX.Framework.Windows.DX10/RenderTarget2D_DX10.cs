#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX.Direct3D10;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class RenderTarget2D_DX10 : Texture2D_DX10, INativeRenderTarget2D, INativeTexture2D
    {
        #region Private Members

        #endregion // Private Members

        public RenderTarget2D_DX10(GraphicsDevice graphics, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
            : base(graphics)
        {
            if (mipMap)
            {
                throw new NotImplementedException("creating RenderTargets with mip map not yet implemented");
            }

            this.surfaceFormat = surfaceFormat;

            GraphicsDeviceWindowsDX10 graphicsDX10 = graphicsDevice.NativeDevice as GraphicsDeviceWindowsDX10;
            SharpDX.Direct3D10.Device device = graphicsDX10.NativeDevice;

            SharpDX.Direct3D10.Texture2DDescription description = new SharpDX.Direct3D10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
                Format = FormatConverter.Translate(preferredFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.Direct3D10.ResourceUsage.Default,
                BindFlags = SharpDX.Direct3D10.BindFlags.ShaderResource | SharpDX.Direct3D10.BindFlags.RenderTarget,
                CpuAccessFlags = SharpDX.Direct3D10.CpuAccessFlags.None,
                OptionFlags = SharpDX.Direct3D10.ResourceOptionFlags.None,
            };
            this.nativeTexture = new SharpDX.Direct3D10.Texture2D(graphicsDX10.NativeDevice, description);
            this.nativeShaderResourceView = new SharpDX.Direct3D10.ShaderResourceView(graphicsDX10.NativeDevice, this.nativeTexture);

            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

            this.formatSize = FormatConverter.FormatSize(surfaceFormat);
        }

    }
}
