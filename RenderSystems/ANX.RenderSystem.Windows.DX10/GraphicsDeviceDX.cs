#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;
using SharpDX.Direct3D10;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
	public partial class GraphicsDeviceDX : INativeGraphicsDevice
	{
#if DEBUG
        static int graphicsDeviceCount = 0;
        static int swapChainCount = 0;
#endif
        //Restricted to 8 from DirectX side.
        const int MAX_RENDER_TARGETS = 8;

        #region Private
        private Dx10.Device nativeDevice;
        private Dx10.RenderTargetView[] renderTargetView = new RenderTargetView[MAX_RENDER_TARGETS];
        private Dx10.DepthStencilView[] depthStencilView = new DepthStencilView[MAX_RENDER_TARGETS];
        private RenderTarget2D_DX10 backBuffer;
        internal EffectDX currentEffect;
		#endregion

		#region CreateDevice
		protected void CreateDevice(PresentationParameters presentationParameters)
		{
			var desc = new SwapChainDescription()
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(
                    presentationParameters.BackBufferWidth,
                    presentationParameters.BackBufferHeight,
                    new Rational(60, 1),
                    DxFormatConverter.Translate(presentationParameters.BackBufferFormat)
                ),
				IsWindowed = true,
				OutputHandle = presentationParameters.DeviceWindowHandle,
				SampleDescription = new SampleDescription(1, 0),
				SwapEffect = SwapEffect.Discard,
				Usage = Usage.RenderTargetOutput | Usage.ShaderInput,
			};

			// http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
#if DEBUG
			var flags = Dx10.DeviceCreationFlags.Debug;
#else
            var flags = Dx10.DeviceCreationFlags.None;
#endif
			Dx10.Device.CreateWithSwapChain(Dx10.DriverType.Hardware, flags, desc, out nativeDevice, out swapChain);
#if DEBUG
            nativeDevice.DebugName = "GraphicsDevice_" + graphicsDeviceCount++;
            swapChain.DebugName = "SwapChain_" + swapChainCount++;
#endif

		}
		#endregion

		#region CreateRenderView
		protected void CreateRenderView(PresentationParameters presentationParameters)
		{
            backBuffer = new RenderTarget2D_DX10(this, Dx10.Texture2D.FromSwapChain<Dx10.Texture2D>(swapChain, 0), presentationParameters.DepthStencilFormat);
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

            Dx10.DepthStencilClearFlags clearFlags;
            if ((options | ClearOptions.Stencil | ClearOptions.DepthBuffer) == options)
            {
                clearFlags = Dx10.DepthStencilClearFlags.Depth | Dx10.DepthStencilClearFlags.Stencil;
            }
            else if ((options | ClearOptions.Stencil) == options)
            {
                clearFlags = Dx10.DepthStencilClearFlags.Stencil;
            }
            else
            {
                clearFlags = Dx10.DepthStencilClearFlags.Depth;
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

			Dx10.EffectTechnique technique = SetupEffectForDraw();
			int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);

            if (indexBuffer != null)
            {
                SetIndexBuffer(indexBuffer);
            }

            for (int i = 0; i < technique.Description.PassCount; ++i)
			{
                technique.GetPassByIndex(i).Apply();
				nativeDevice.DrawIndexed(vertexCount, startIndex, baseVertex);
			}

            nativeDevice.InputAssembler.InputLayout.Dispose();
            nativeDevice.InputAssembler.InputLayout = null;
		}
		#endregion

		#region DrawPrimitives
		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
		{
			Dx10.EffectTechnique technique = SetupEffectForDraw();
            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

			nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);

            for (int i = 0; i < technique.Description.PassCount; ++i)
			{
				technique.GetPassByIndex(i).Apply();
				nativeDevice.Draw(vertexCount, vertexOffset);
            }

			nativeDevice.InputAssembler.InputLayout.Dispose();
			nativeDevice.InputAssembler.InputLayout = null;
        }
        #endregion

        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount, int instanceCount, IndexBuffer indexBuffer)
        {
            Dx10.EffectTechnique technique = SetupEffectForDraw();
            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);

            if (indexBuffer != null)
            {
                SetIndexBuffer(indexBuffer);
            }

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                technique.GetPassByIndex(i).Apply();
                nativeDevice.DrawIndexedInstanced(vertexCount, instanceCount, startIndex, baseVertex, 0);
            }

            nativeDevice.InputAssembler.InputLayout.Dispose();
            nativeDevice.InputAssembler.InputLayout = null;
        }

        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;

            using (var vertexBuffer = new DynamicVertexBuffer(vertexDeclaration.GraphicsDevice, vertexDeclaration, vertexCount, BufferUsage.WriteOnly))
            using (var indexBuffer = new DynamicIndexBuffer(vertexDeclaration.GraphicsDevice, indexFormat, indexCount, BufferUsage.WriteOnly))
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

        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            //TODO: use a shared vertexBuffer, instead of creating one on every call.
            using (DxVertexBuffer vb10 = new DxVertexBuffer(nativeDevice, vertexDeclaration, vertexCount, BufferUsage.WriteOnly, dynamic: true))
            {
                vb10.SetData<T>(vertexData);

                Dx10.VertexBufferBinding nativeVertexBufferBindings = new Dx10.VertexBufferBinding(vb10.NativeBuffer, vertexDeclaration.VertexStride, 0);

                nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

                //TODO: check for currentEffect null and throw exception
                // TODO: check for null's and throw exceptions
                // TODO: get the correct pass index!
                var technique = currentEffect.GetCurrentTechnique().NativeTechnique;
                var pass = technique.GetPassByIndex(0);
                var layout = CreateInputLayout(nativeDevice, pass.Description.Signature, vertexDeclaration);

                nativeDevice.InputAssembler.InputLayout = layout;
                // Prepare All the stages
                nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);

                for (int i = 0; i < technique.Description.PassCount; ++i)
                {
                    technique.GetPassByIndex(i).Apply();
                    nativeDevice.Draw(vertexCount, vertexOffset);
                }

                nativeDevice.InputAssembler.InputLayout.Dispose();
                nativeDevice.InputAssembler.InputLayout = null;
            }
        }

		private Dx10.EffectTechnique SetupEffectForDraw()
        {
			//TODO: check for currentEffect null and throw exception
            // TODO: check for null's and throw exceptions
			// TODO: get the correct pass index!
			var technique = currentEffect.GetCurrentTechnique().NativeTechnique;
			var pass = technique.GetPassByIndex(0);
			SetupInputLayout(pass.Description.Signature);

			return technique;
        }

		#region SetupInputLayout
        private InputLayout SetupInputLayout(ShaderBytecode passSignature)
        {
            // get the VertexDeclaration from current VertexBuffer to create input layout for the input assembler
            var layout = CreateInputLayout(nativeDevice, passSignature, currentVertexBuffer);

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
				nativeDevice.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer, DxFormatConverter.Translate(indexBuffer.IndexElementSize), 0);
            }
            else
            {
                throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
            }
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
                this.currentVertexBuffer[i] = vertexBuffers[i];
            }

            Dx10.VertexBufferBinding[] nativeVertexBufferBindings = new Dx10.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                var nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as DxVertexBuffer;

                if (nativeVertexBuffer != null)
                {
                    int vertexStride = anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride;
                    nativeVertexBufferBindings[i] = new Dx10.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, vertexStride, anxVertexBufferBinding.VertexOffset * vertexStride);
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
            nativeDevice.Rasterizer.SetViewports(new SharpDX.Viewport(x, y, width, height, minDepth, maxDepth));
		}

        protected void SetViewport(params SharpDX.Viewport[] viewports)
        {
            nativeDevice.Rasterizer.SetViewports(viewports);
        }
        #endregion

        #region CreateInputLayout
        /// <summary>
        /// This method creates a InputLayout which is needed by DirectX 10 for rendering primitives.
        /// The VertexDeclaration of ANX/XNA needs to be mapped to the DirectX 10 types.
        /// </summary>
        private Dx10.InputLayout CreateInputLayout(Dx10.Device device, ShaderBytecode passSignature, params VertexDeclaration[] vertexDeclaration)
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

			return new Dx10.InputLayout(device, passSignature, inputElements.ToArray());
		}

        private Dx10.InputLayout CreateInputLayout(Dx10.Device device, ShaderBytecode passSignature, params ANX.Framework.Graphics.VertexBufferBinding[] vertexBufferBindings)
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
                nativeDevice.OutputMerger.SetRenderTargets(MAX_RENDER_TARGETS, this.renderTargetView, this.backBuffer.DepthStencilView);
                nativeDevice.Rasterizer.SetViewports(new SharpDX.Viewport(0, 0, this.backBuffer.Width, this.backBuffer.Height));
            }
            else
            {
                int renderTargetCount = renderTargets.Length;
                if (renderTargetCount > MAX_RENDER_TARGETS)
                    throw new NotSupportedException(string.Format("{0} render targets are not supported. The maximum is {1}.", renderTargetCount, MAX_RENDER_TARGETS));

                SharpDX.Viewport[] rtViewports = new SharpDX.Viewport[renderTargetCount];

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
                    RenderTarget2D_DX10 nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX10;

                    renderTargetView[i] = nativeRenderTarget.RenderTargetView;
                    depthStencilView[i] = nativeRenderTarget.DepthStencilView;
                    rtViewports[i] = new SharpDX.Viewport(0, 0, renderTarget.Width, renderTarget.Height);
                }

                for (int i = renderTargetCount; i < MAX_RENDER_TARGETS; i++)
                {
                    this.renderTargetView[i] = null;
                    this.depthStencilView[i] = null;
                }

                nativeDevice.OutputMerger.SetRenderTargets(MAX_RENDER_TARGETS, renderTargetView, this.depthStencilView[0]);
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

                nativeDevice.OutputMerger.SetRenderTargets(MAX_RENDER_TARGETS, this.renderTargetView, null);

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

        internal Dx10.Device NativeDevice
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
