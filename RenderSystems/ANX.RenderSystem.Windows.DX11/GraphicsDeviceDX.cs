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
        //Restricted to 8 from DirectX side.
        const int MAX_RENDER_TARGETS = 8;

        private Dx11.DeviceContext nativeDevice;
        private Dx11.RenderTargetView[] renderTargetView = new RenderTargetView[MAX_RENDER_TARGETS];
        private Dx11.DepthStencilView[] depthStencilView = new DepthStencilView[MAX_RENDER_TARGETS];
        private RenderTarget2D_DX11 backBuffer;
        internal EffectDX currentEffect;
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
				Usage = Usage.RenderTargetOutput
			};

			// Create Device and SwapChain
			Device dxDevice;

#if DEBUG
            var flags = DeviceCreationFlags.Debug;
#else
            var flags = DeviceCreationFlags.None;
#endif
            // http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
            Device.CreateWithSwapChain(DriverType.Hardware, flags, desc, out dxDevice, out swapChain);
#if DEBUG
            nativeDevice.DebugName = "GraphicsDevice_" + graphicsDeviceCount++;
            swapChain.DebugName = "SwapChain_" + swapChainCount++;
#endif

			nativeDevice = dxDevice.ImmediateContext;
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

                for (int i = 0; i < MAX_RENDER_TARGETS; i++)
                {
                    if (this.renderTargetView[i] == null)
                        break;

                    nativeDevice.ClearRenderTargetView(this.renderTargetView[i], clearColor);
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

            for (int i = 0; i < MAX_RENDER_TARGETS; i++)
            {
                if (this.depthStencilView[i] == null)
                    break;

                nativeDevice.ClearDepthStencilView(this.depthStencilView[i], clearFlags, depth, (byte)stencil);
            }
        }

        #endregion

        #region Present
        public void Present()
        {
			swapChain.Present(VSync ? 1 : 0, PresentFlags.None);
        }
        #endregion

		#region DrawIndexedPrimitives
        public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, IndexBuffer indexBuffer)
		{
            if (primitiveCount <= 0) throw new ArgumentOutOfRangeException("primitiveCount is less than or equal to zero. When drawing, at least one primitive must be drawn.");
            if (this.currentVertexBuffer == null || this.currentVertexBufferCount <= 0) throw new InvalidOperationException("you have to set a valid vertex buffer before drawing.");

            Dx11.EffectTechnique technique = SetupEffectForDraw();
            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
            //nativeDevice.Rasterizer.SetViewports(currentViewport);
            //nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            if (indexBuffer != null)
            {
                SetIndexBuffer(indexBuffer);
            }

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                technique.GetPassByIndex(i).Apply(nativeDevice);
                nativeDevice.DrawIndexed(vertexCount, startIndex, baseVertex);
            }

            nativeDevice.InputAssembler.InputLayout.Dispose();
            nativeDevice.InputAssembler.InputLayout = null;
		}
		#endregion

		#region DrawPrimitives
		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
		{
			Dx11.EffectPass pass; Dx11.EffectTechnique technique; ShaderBytecode passSignature;
			SetupEffectForDraw(out pass, out technique, out passSignature);

			var layout = SetupInputLayout(passSignature);

			// Prepare All the stages
			nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
			//nativeDevice.Rasterizer.SetViewports(currentViewport);
			//nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

			for (int i = 0; i < technique.Description.PassCount; ++i)
			{
				pass.Apply(nativeDevice);
				nativeDevice.Draw(primitiveCount, vertexOffset);
			}

			layout.Dispose();
			layout = null;
		}
        #endregion

        #region DrawInstancedPrimitives
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount, IndexBuffer indexBuffer)
        {
            Dx11.EffectTechnique technique = SetupEffectForDraw();
            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
            //nativeDevice.Rasterizer.SetViewports(currentViewport);
            //nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            if (indexBuffer != null)
            {
                SetIndexBuffer(indexBuffer);
            }

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                technique.GetPassByIndex(i).Apply(nativeDevice);
                nativeDevice.DrawIndexedInstanced(vertexCount, instanceCount, startIndex, baseVertex, 0);
            }

            nativeDevice.InputAssembler.InputLayout.Dispose();
            nativeDevice.InputAssembler.InputLayout = null;
        }

        #endregion // DrawInstancedPrimitives

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;

            using (VertexBuffer vertexBuffer = new VertexBuffer(vertexDeclaration.GraphicsDevice, vertexDeclaration, vertexCount, BufferUsage.WriteOnly))
            using (IndexBuffer indexBuffer = new IndexBuffer(vertexDeclaration.GraphicsDevice, indexFormat, indexCount, BufferUsage.WriteOnly))
            {
                vertexBuffer.SetData(vertexData);
                this.SetVertexBuffers(new[] { new Framework.Graphics.VertexBufferBinding(vertexBuffer, vertexOffset) });

                if (indexData.GetType() == typeof(Int16[]))
                {
                    indexBuffer.SetData<short>((short[])indexData);
                }
                else
                {
                    indexBuffer.SetData<int>((int[])indexData);
                }

                DrawIndexedPrimitives(primitiveType, 0, vertexOffset, numVertices, indexOffset, primitiveCount, indexBuffer);
            }
        }

        #endregion // DrawUserIndexedPrimitives<T>

        #region DrawUserPrimitives<T>
        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            using (DxVertexBuffer vb11 = new DxVertexBuffer(this, vertexDeclaration, vertexCount, BufferUsage.WriteOnly, dynamic: true))
            {
                vb11.SetData<T>(vertexData);

                Dx11.VertexBufferBinding nativeVertexBufferBindings = new Dx11.VertexBufferBinding(vb11.NativeBuffer, vertexDeclaration.VertexStride, 0);

                nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

                Dx11.EffectPass pass; Dx11.EffectTechnique technique; ShaderBytecode passSignature;
                SetupEffectForDraw(out pass, out technique, out passSignature);

                var layout = CreateInputLayout(nativeDevice.Device, passSignature, vertexDeclaration);

                nativeDevice.InputAssembler.InputLayout = layout;
                // Prepare All the stages
                nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
                //nativeDevice.Rasterizer.SetViewports(currentViewport);
                //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

                for (int i = 0; i < technique.Description.PassCount; ++i)
                {
                    pass.Apply(nativeDevice);
                    nativeDevice.Draw(primitiveCount, vertexOffset);
                }

                layout.Dispose();
                layout = null;
            }
        }

        #endregion // DrawUserPrimitives<T>

		#region SetupEffectForDraw
		private void SetupEffectForDraw(out Dx11.EffectPass pass, out Dx11.EffectTechnique technique,
			out ShaderBytecode passSignature)
        {
            // get the current effect
            //TODO: check for null and throw exception
            EffectDX effect = this.currentEffect;

            // get the input semantic of the current effect / technique that is used
			//TODO: check for null's and throw exceptions
			// TODO: get the correct pass index!
			technique = effect.GetCurrentTechnique().NativeTechnique;
            pass = technique.GetPassByIndex(0);
            passSignature = pass.Description.Signature;
        }

        private Dx11.EffectTechnique SetupEffectForDraw()
        {
            //TODO: check for currentEffect null and throw exception
            // TODO: check for null's and throw exceptions
            // TODO: get the correct pass index!
            var technique = currentEffect.GetCurrentTechnique().NativeTechnique;
            var pass = technique.GetPassByIndex(0);
            SetupInputLayout(pass.Description.Signature);

            return technique;
        }
		#endregion

		#region SetupInputLayout
		private InputLayout SetupInputLayout(ShaderBytecode passSignature)
        {
            // get the VertexDeclaration from current VertexBuffer to create input layout for the input assembler
			var layout = CreateInputLayout(nativeDevice.Device, passSignature, currentVertexBuffer);

			nativeDevice.InputAssembler.InputLayout = layout;
			return layout;
        }
		#endregion

		#region SetIndexBuffer
		public void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");

            this.currentIndexBuffer = indexBuffer;
            DxIndexBuffer nativeIndexBuffer = indexBuffer.NativeIndexBuffer as DxIndexBuffer;

            if (nativeIndexBuffer != null)
            {
				nativeDevice.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer,
					DxFormatConverter.Translate(indexBuffer.IndexElementSize), 0);
            }
            else
                throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
        }
		#endregion

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

		#region CreateInputLayout
        private InputLayout CreateInputLayout(Device device, ShaderBytecode passSignature, params VertexDeclaration[] vertexDeclaration)
        {
            if (device == null) throw new ArgumentNullException("device");
            if (passSignature == null) throw new ArgumentNullException("passSignature");
            if (vertexDeclaration == null) throw new ArgumentNullException("vertexDeclaration");

            //TODO: try to get rid of the list
            List<InputElement> inputElements = new List<InputElement>();
            foreach (VertexDeclaration decl in vertexDeclaration)
            {
                foreach (VertexElement vertexElement in decl.GetVertexElements())
                {
                    inputElements.Add(CreateInputElementFromVertexElement(vertexElement, 0));
                }
            }

            return new InputLayout(device, passSignature, inputElements.ToArray());
        }
        
        private InputLayout CreateInputLayout(Device device, ShaderBytecode passSignature, params ANX.Framework.Graphics.VertexBufferBinding[] vertexBufferBindings)
        {
            if (device == null) throw new ArgumentNullException("device");
            if (passSignature == null) throw new ArgumentNullException("passSignature");
            if (vertexBufferBindings == null) throw new ArgumentNullException("vertexBufferBindings");

            //TODO: try to get rid of the list
            List<InputElement> inputElements = new List<InputElement>();
            int slot = 0;
            foreach (ANX.Framework.Graphics.VertexBufferBinding binding in vertexBufferBindings)
            {
                foreach (VertexElement vertexElement in binding.VertexBuffer.VertexDeclaration.GetVertexElements())
                {
                    inputElements.Add(CreateInputElementFromVertexElement(vertexElement, binding.InstanceFrequency, slot));
                }
                slot++;
            }

            // Layout from VertexShader input signature
            return new InputLayout(device, passSignature, inputElements.ToArray());
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
                this.renderTargetView[0] = this.backBuffer.RenderTargetView;
                this.depthStencilView[0] = this.backBuffer.DepthStencilView;

                for (int i = 1; i < MAX_RENDER_TARGETS; i++)
                {
                    this.renderTargetView[i] = null;
                    this.depthStencilView[i] = null;
                }

                //To correctly unset renderTargets, the amount of given rendertargetViews must be max(#previousRenderTargets, #newRenderTargets),
                //otherwise the old ones at the slots stay bound. For us it means, we try to unbind every possible previous slot.
                nativeDevice.OutputMerger.SetRenderTargets(this.backBuffer.DepthStencilView, this.renderTargetView);
                nativeDevice.Rasterizer.SetViewport(new SharpDX.ViewportF(0, 0, this.backBuffer.Width, this.backBuffer.Height));
            }
            else
            {
                int renderTargetCount = renderTargets.Length;
                if (renderTargetCount > MAX_RENDER_TARGETS)
                    throw new NotSupportedException(string.Format("{0} render targets are not supported. The maximum is {1}.", renderTargetCount, MAX_RENDER_TARGETS));

                var rtViewports = new SharpDX.ViewportF[renderTargetCount];

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

                    renderTargetView[i] = nativeRenderTarget.RenderTargetView;
                    depthStencilView[i] = nativeRenderTarget.DepthStencilView;
                    rtViewports[i] = new SharpDX.ViewportF(0, 0, renderTarget.Width, renderTarget.Height);
                }

                for (int i = renderTargetCount; i < MAX_RENDER_TARGETS; i++)
                {
                    this.renderTargetView[i] = null;
                    this.depthStencilView[i] = null;
                }

                nativeDevice.OutputMerger.SetRenderTargets(this.depthStencilView[0], renderTargetView);
                nativeDevice.Rasterizer.SetViewports(rtViewports);
            }
        }
		#endregion

        protected void DisposeBackBuffer()
        {
            if (backBuffer != null)
            {
                for (int i = 0; i < MAX_RENDER_TARGETS; i++)
                {
                    this.renderTargetView[i] = null;
                    this.depthStencilView[i] = null;
                }

                nativeDevice.OutputMerger.SetRenderTargets(null, this.renderTargetView);

                backBuffer.Dispose();
                backBuffer = null;
            }
        }

		#region Dispose
		public void Dispose()
        {
            if (swapChain != null)
            {
				DisposeBackBuffer();

                swapChain.Dispose();
                swapChain = null;
            }

            //TODO: dispose everything else
        }
		#endregion

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
