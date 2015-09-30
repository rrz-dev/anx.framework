#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;
using SharpDX.DXGI;

namespace ANX.RenderSystem.Windows.DX10
{
    public class RenderTarget2D_DX10 : DxTexture2D, INativeRenderTarget2D, INativeTexture2D
	{
#if DEBUG
        static int depthStencilCount = 0;
        static int renderTargetCount = 0;
#endif

        Dx10.Texture2D depthStencil;

        public Dx10.RenderTargetView RenderTargetView
        {
            get;
            protected set;
        }

        public Dx10.DepthStencilView DepthStencilView
        {
            get;
            protected set;
        }

        public RenderTarget2D_DX10(GraphicsDeviceDX graphics, Dx10.Texture2D texture, DepthFormat depthFormat)
            : base(graphics, texture)
        {
            Dx10.Device device = graphics.NativeDevice;

            NativeTexture = texture;
#if DEBUG
            NativeTexture.DebugName = "RenderTarget_" + renderTargetCount++;
#endif
            RenderTargetView = new Dx10.RenderTargetView(device, NativeTexture);
#if DEBUG
            RenderTargetView.DebugName = NativeTexture.DebugName + "_RenderTargetView";
#endif

            CreateDepthStencil(graphics, depthFormat, texture.Description.Width, texture.Description.Height);
        }

		#region Constructor
        public RenderTarget2D_DX10(GraphicsDeviceDX graphics, int width, int height, bool mipMap, SurfaceFormat surfaceFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
            : base(graphics)
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

            Dx10.Device device = graphics.NativeDevice;
			var texture = new Dx10.Texture2D(device, description);
#if DEBUG
            texture.DebugName = "RenderTarget_" + renderTargetCount++;
#endif
            this.Width = width;
            this.Height = height;

            NativeTexture = texture;
            RenderTargetView = new Dx10.RenderTargetView(device, NativeTexture);
#if DEBUG
            RenderTargetView.DebugName = NativeTexture.DebugName + "_RenderTargetView";
#endif

            CreateDepthStencil(graphics, preferredDepthFormat, width, height);
		}
		#endregion

        private void CreateDepthStencil(GraphicsDeviceDX graphics, DepthFormat depthFormat, int width, int height)
        {
            //TODO: As we can only ever have 1 active DepthStencil and it's currently not possible to access it from the outside,
            //we could share the depth stencils as long as they have the correct size and format.

            if (depthFormat == DepthFormat.None)
                return;

            var dxDepthFormat = DxFormatConverter.Translate(depthFormat);
            var depthStencilViewDesc = new Dx10.DepthStencilViewDescription()
            {
                Format = dxDepthFormat,
            };

            var depthStencilTextureDesc = new Dx10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
                Format = dxDepthFormat,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Dx10.ResourceUsage.Default,
                BindFlags = Dx10.BindFlags.DepthStencil,
                CpuAccessFlags = Dx10.CpuAccessFlags.None,
                OptionFlags = Dx10.ResourceOptionFlags.None
            };
            this.depthStencil = new Dx10.Texture2D(graphics.NativeDevice, depthStencilTextureDesc);
            this.DepthStencilView = new Dx10.DepthStencilView(graphics.NativeDevice, this.depthStencil);
#if DEBUG
            this.depthStencil.DebugName = "DepthStencil_" + depthStencilCount++;
            this.DepthStencilView.DebugName = this.depthStencil.DebugName + "_View";
#endif
        }

        protected override void Dispose(bool managed)
        {
            base.Dispose(managed);

            if (RenderTargetView != null)
            {
                RenderTargetView.Dispose();
            }
            if (DepthStencilView != null)
            {
                DepthStencilView.Dispose();
            }
        }
	}
}
