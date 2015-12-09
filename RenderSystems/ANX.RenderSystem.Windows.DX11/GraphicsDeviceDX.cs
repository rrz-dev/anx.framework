#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Device = SharpDX.Direct3D11.Device;
using Dx11 = SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
{
    public partial class GraphicsDeviceDX : INativeGraphicsDevice
    {
        #region Private
#if DEBUG
        static int graphicsDeviceCount = 0;
        static int swapChainCount = 0;
#endif

        private Dx11.DeviceContext nativeDevice;
        private Dx11.RenderTargetView[] renderTargetView = new RenderTargetView[1];
        private Dx11.DepthStencilView[] depthStencilView = new DepthStencilView[1];
        private RenderTarget2D_DX11 backBuffer;
        internal EffectPass_DX11 currentPass;
		#endregion

		#region CreateDevice
		protected void CreateDevice(PresentationParameters presentationParameters)
		{
			var desc = new SwapChainDescription()
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight, new Rational(60, 1),
					DxFormatConverter.Translate(presentationParameters.BackBufferFormat)),
				IsWindowed = true,
				OutputHandle = presentationParameters.DeviceWindowHandle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput | Usage.ShaderInput
			};

			// Create Device and SwapChain
			Device dxDevice;

#if DEBUG
            var flags = DeviceCreationFlags.Debug;
#else
            var flags = DeviceCreationFlags.None;
#endif
            var driverType = DriverType.Hardware;
            if (GraphicsAdapter.UseReferenceDevice)
                driverType = DriverType.Reference;
            else if (GraphicsAdapter.UseNullDevice)
                driverType = DriverType.Null;

            // http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
            Device.CreateWithSwapChain(driverType, flags, desc, out dxDevice, out swapChain);

            nativeDevice = dxDevice.ImmediateContext;
#if DEBUG
            nativeDevice.DebugName = "GraphicsDevice_" + graphicsDeviceCount++;
            swapChain.DebugName = "SwapChain_" + swapChainCount++;
#endif
		}
		#endregion

		#region CreateRenderView
        protected void CreateRenderView(PresentationParameters presentationParameters)
        {
            backBuffer = new RenderTarget2D_DX11(this, Dx11.Texture2D.FromSwapChain<Dx11.Texture2D>(swapChain, 0), presentationParameters.DepthStencilFormat);
            this.SetRenderTargets();
        }
		#endregion

		#region Clear
        public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
        {
            if ((options & ClearOptions.Target) == ClearOptions.Target)
            {
                // Clear a RenderTarget (or BackBuffer)
                var clearColor = new SharpDX.Color4(color.X, color.Y, color.Z, color.W);

                foreach (var renderTargetView in this.renderTargetView)
                {
                    nativeDevice.ClearRenderTargetView(renderTargetView, clearColor);
                }
            }

            Dx11.DepthStencilClearFlags clearFlags;
            if ((options | ClearOptions.Stencil | ClearOptions.DepthBuffer) == options)
            {
                clearFlags = Dx11.DepthStencilClearFlags.Depth | Dx11.DepthStencilClearFlags.Stencil;
            }
            else if ((options | ClearOptions.Stencil) == options)
            {
                clearFlags = Dx11.DepthStencilClearFlags.Stencil;
            }
            else
            {
                clearFlags = Dx11.DepthStencilClearFlags.Depth;
            }

            foreach (var depthStencilView in this.depthStencilView)
            {
                nativeDevice.ClearDepthStencilView(depthStencilView, clearFlags, depth, (byte)stencil);
            }
        }

        #endregion

        #region Present
        public void Present()
        {
			swapChain.Present(VSync ? 1 : 0, PresentFlags.None);
        }

        public void Present(Rectangle? sourceRectangle, Rectangle? destinationRectangle, WindowHandle overrideWindowHandle)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void SetupDraw(PrimitiveType primitiveType)
        {
            var inputLayout = this.inputLayoutManager.GetInputLayout(NativeDevice.Device, currentPass.Signature, this.currentVertexBuffer);

            nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
            if (currentInputLayout != inputLayout)
            {
                nativeDevice.InputAssembler.InputLayout = inputLayout;
                currentInputLayout = inputLayout;
            }
        }

		#region SetVertexBuffers
		public void SetVertexBuffers(ANX.Framework.Graphics.VertexBufferBinding[] vertexBuffers)
        {
            if (vertexBuffers == null)
                throw new ArgumentNullException("vertexBuffers");

            this.currentVertexBufferCount = vertexBuffers.Length;

            if (this.currentVertexBuffer == null || this.currentVertexBuffer.Length < currentVertexBufferCount)
            {
                this.currentVertexBuffer = new ANX.Framework.Graphics.VertexBufferBinding[currentVertexBufferCount];
            }

            for (int i = 0; i < this.currentVertexBufferCount; i++)
            {
                this.currentVertexBuffer[i] = vertexBuffers[i].VertexBuffer;
            }

            var nativeVertexBufferBindings = new Dx11.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                var nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as DxVertexBuffer;

                if (nativeVertexBuffer != null)
                {
                    int vertexStride = anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride;
                    nativeVertexBufferBindings[i] = new Dx11.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, vertexStride, anxVertexBufferBinding.VertexOffset * vertexStride);
                }
                else
                {
                    throw new Exception("couldn't fetch native DirectX10 VertexBuffer");
                }
            }

			nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);
		}
		#endregion

		#region SetViewport
		protected void SetViewport(int x, int y, int width, int height, float minDepth, float maxDepth)
		{
            nativeDevice.Rasterizer.SetViewport(new SharpDX.Viewport(x, y, width, height, minDepth, maxDepth));
		}

        protected void SetViewport(params SharpDX.ViewportF[] viewports)
        {
            nativeDevice.Rasterizer.SetViewports(viewports);
        }
        #endregion

		#region CreateInputElementFromVertexElement
        private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement, int slot)
        {
            return CreateInputElementFromVertexElement(vertexElement, 0, slot);
        }

		private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement, int instanceFrequency, int slot)
		{
			string elementName = DxFormatConverter.Translate(ref vertexElement);
			Format elementFormat = DxFormatConverter.ConvertVertexElementFormat(vertexElement.VertexElementFormat);
			return new InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, slot, instanceFrequency == 0 ? InputClassification.PerVertexData : InputClassification.PerInstanceData, instanceFrequency);
		}
		#endregion

		#region SetRenderTargets
        public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
        {
            if (renderTargets == null || renderTargets.Length == 0)
            {
                this.renderTargetView = new RenderTargetView[] { this.backBuffer.RenderTargetView };
                this.depthStencilView = new DepthStencilView[] { this.backBuffer.DepthStencilView };

                nativeDevice.OutputMerger.ResetTargets();
                //To correctly unset renderTargets, the amount of given rendertargetViews must be max(#previousRenderTargets, #newRenderTargets),
                //otherwise the old ones at the slots stay bound. For us it means, we try to unbind every possible previous slot.
                nativeDevice.OutputMerger.SetRenderTargets(this.backBuffer.DepthStencilView, this.backBuffer.RenderTargetView);
                nativeDevice.Rasterizer.SetViewport(new SharpDX.ViewportF(0, 0, this.backBuffer.Width, this.backBuffer.Height));
            }
            else
            {
                int renderTargetCount = renderTargets.Length;
                var rtViewports = new SharpDX.ViewportF[renderTargetCount];
                this.renderTargetView = new RenderTargetView[renderTargetCount];
                this.depthStencilView = new DepthStencilView[renderTargetCount];

                var firstRenderTarget = renderTargets[0].RenderTarget as RenderTarget2D;
                for (int i = 1; i < renderTargetCount; i++)
                {
                    var renderTarget = renderTargets[i].RenderTarget as RenderTarget2D;
                    if (renderTarget.Width != firstRenderTarget.Width || renderTarget.Height != firstRenderTarget.Height || renderTarget.MultiSampleCount != firstRenderTarget.MultiSampleCount)
                        throw new ArgumentException("The render targets don't match");
                }

                for (int i = 0; i < renderTargetCount; i++)
                {
                    RenderTarget2D renderTarget = renderTargets[i].RenderTarget as RenderTarget2D;
                    var nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX11;

                    this.renderTargetView[i] = nativeRenderTarget.RenderTargetView;
                    this.depthStencilView[i] = nativeRenderTarget.DepthStencilView;
                    rtViewports[i] = new SharpDX.ViewportF(0, 0, renderTarget.Width, renderTarget.Height);
                }

                nativeDevice.OutputMerger.ResetTargets();

                nativeDevice.OutputMerger.SetRenderTargets(this.depthStencilView[0], renderTargetView);
                nativeDevice.Rasterizer.SetViewports(rtViewports);
            }
        }
		#endregion

        protected void DisposeBackBuffer()
        {
            if (backBuffer != null)
            {
                nativeDevice.OutputMerger.ResetTargets();

                backBuffer.Dispose();
                backBuffer = null;
            }
        }

        internal DeviceContext NativeDevice
        {
            get
            {
                return this.nativeDevice;
            }
        }

        public Rectangle ScissorRectangle
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
