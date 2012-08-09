#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX.Direct3D11;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Windows.Metro
{
    public class RenderTarget2D_Metro : Texture2D_Metro, INativeRenderTarget2D, INativeTexture2D
    {
        #region Private Members

        #endregion // Private Members

        public RenderTarget2D_Metro(GraphicsDevice graphics, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
            : base(graphics)
        {
            if (mipMap)
            {
                throw new NotImplementedException("creating RenderTargets with mip map not yet implemented");
            }

            this.surfaceFormat = surfaceFormat;

            GraphicsDeviceWindowsMetro graphicsMetro = graphicsDevice.NativeDevice as GraphicsDeviceWindowsMetro;
            SharpDX.Direct3D11.DeviceContext device = graphicsMetro.NativeDevice;

            SharpDX.Direct3D11.Texture2DDescription description = new SharpDX.Direct3D11.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
                Format = FormatConverter.Translate(preferredFormat),
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.Direct3D11.ResourceUsage.Default,
                BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | SharpDX.Direct3D11.BindFlags.RenderTarget,
                CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
                OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None,
            };
            this.nativeTexture = new SharpDX.Direct3D11.Texture2D(graphicsMetro.NativeDevice.Device, description);
            this.nativeShaderResourceView = new SharpDX.Direct3D11.ShaderResourceView(graphicsMetro.NativeDevice.Device, this.nativeTexture);

            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

            this.formatSize = FormatConverter.FormatSize(surfaceFormat);
        }

    }
}
