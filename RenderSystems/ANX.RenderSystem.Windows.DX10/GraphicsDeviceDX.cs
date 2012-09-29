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
        #region Private
        private Dx10.Device nativeDevice;
		private Dx10.RenderTargetView renderView;
		private Dx10.RenderTargetView[] renderTargetView = new Dx10.RenderTargetView[1];
		private Dx10.DepthStencilView depthStencilView;
        private Dx10.Texture2D depthStencilBuffer;
        private Dx10.Texture2D backBuffer;
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

			// http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
			var flags = IsDebugMode ? Dx10.DeviceCreationFlags.Debug : Dx10.DeviceCreationFlags.None;
			Dx10.Device.CreateWithSwapChain(Dx10.DriverType.Hardware, flags, desc, out nativeDevice, out swapChain);
		}
		#endregion

		#region CreateRenderView
		protected void CreateRenderView()
		{
			backBuffer = Dx10.Texture2D.FromSwapChain<Dx10.Texture2D>(swapChain, 0);
			renderView = new Dx10.RenderTargetView(nativeDevice, backBuffer);
            nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
		}
		#endregion

		#region CreateDepthStencilBuffer
		protected void CreateDepthStencilBuffer(Format depthFormat, int width, int height, bool setAndClearTarget)
        {
            if (this.depthStencilBuffer != null &&
                this.depthStencilBuffer.Description.Format == depthFormat &&
                this.depthStencilBuffer.Description.Width == width &&
                this.depthStencilBuffer.Description.Height == height)
            {
                // a DepthStencilBuffer with the right format and the right size already exists -> nothing to do
                return;
            }

            if (depthFormat == Format.Unknown)
            {
                // no DepthStencilBuffer to create... Old one was disposed already...
                return;
            }

			var depthStencilViewDesc = new Dx10.DepthStencilViewDescription()
            {
                Format = depthFormat,
            };

			var depthStencilTextureDesc = new Dx10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = 1,
                ArraySize = 1,
                Format = depthFormat,
                SampleDescription = new SampleDescription(1, 0),
				Usage = Dx10.ResourceUsage.Default,
				BindFlags = Dx10.BindFlags.DepthStencil,
				CpuAccessFlags = Dx10.CpuAccessFlags.None,
				OptionFlags = Dx10.ResourceOptionFlags.None
            };
			this.depthStencilBuffer = new Dx10.Texture2D(nativeDevice, depthStencilTextureDesc);

			this.depthStencilView = new Dx10.DepthStencilView(nativeDevice, this.depthStencilBuffer);

            if (setAndClearTarget)
            {
                nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

                // this workaround is working but maybe not the best solution to issue #472
                Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Vector4.Zero, 1.0f, 0);
            }
        }
		#endregion

		#region Clear
		public void Clear(ref Color color)
		{
			UpdateClearColorIfNeeded(ref color);

            if (this.renderTargetView[0] == null)
				nativeDevice.ClearRenderTargetView(this.renderView, this.clearColor);
            else
            {
                for (int i = 0; i < this.renderTargetView.Length; i++)
                {
                    if (this.renderTargetView[i] == null)
                    {
                        break;
                    }

					nativeDevice.ClearRenderTargetView(this.renderTargetView[i], this.clearColor);
                }
            }
		}

        public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
        {
            if ((options & ClearOptions.Target) == ClearOptions.Target)
            {
                // Clear a RenderTarget (or BackBuffer)

                this.clearColor.Red = color.X;
                this.clearColor.Green = color.Y;
                this.clearColor.Blue = color.Z;
                this.clearColor.Alpha = color.W;
                this.lastClearColor = 0;

                if (this.renderTargetView[0] == null)
                {
					nativeDevice.ClearRenderTargetView(this.renderView, this.clearColor);
                }
                else
                {
                    for (int i = 0; i < this.renderTargetView.Length; i++)
                    {
                        if (this.renderTargetView[i] == null)
                            break;

						nativeDevice.ClearRenderTargetView(this.renderTargetView[i], this.clearColor);
                    }
                }
            }

            if (this.depthStencilView != null)
            {
                if ((options | ClearOptions.Stencil | ClearOptions.DepthBuffer) == options)
                {
                    // Clear the stencil buffer
					nativeDevice.ClearDepthStencilView(this.depthStencilView, Dx10.DepthStencilClearFlags.Depth |
						Dx10.DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else if ((options | ClearOptions.Stencil) == options)
                {
					nativeDevice.ClearDepthStencilView(this.depthStencilView, Dx10.DepthStencilClearFlags.Stencil, depth,
						(byte)stencil);
                }
                else
                {
					nativeDevice.ClearDepthStencilView(this.depthStencilView, Dx10.DepthStencilClearFlags.Depth, depth,
						(byte)stencil);
                }
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
			//nativeDevice.Rasterizer.SetViewports(currentViewport);
			//nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

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
			//nativeDevice.Rasterizer.SetViewports(currentViewport);
			//nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
			{
				technique.GetPassByIndex(i).Apply();
				nativeDevice.Draw(vertexCount, vertexOffset);
            }

			nativeDevice.InputAssembler.InputLayout.Dispose();
			nativeDevice.InputAssembler.InputLayout = null;
        }
        #endregion

        #region DrawInstancedPrimitives
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount, int instanceCount, IndexBuffer indexBuffer)
        {
            Dx10.EffectTechnique technique = SetupEffectForDraw();
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
                technique.GetPassByIndex(i).Apply();
                nativeDevice.DrawIndexedInstanced(vertexCount, instanceCount, startIndex, baseVertex, 0);
            }

            nativeDevice.InputAssembler.InputLayout.Dispose();
            nativeDevice.InputAssembler.InputLayout = null;
        }
        #endregion

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;

            VertexBuffer vertexBuffer = new VertexBuffer(vertexDeclaration.GraphicsDevice, vertexDeclaration, vertexCount, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertexData);
            this.SetVertexBuffers(new[] { new Framework.Graphics.VertexBufferBinding(vertexBuffer, vertexOffset) });

            IndexBuffer indexBuffer = new IndexBuffer(vertexDeclaration.GraphicsDevice, indexFormat, indexCount, BufferUsage.WriteOnly);
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
        #endregion

        #region DrawUserPrimitives<T>
        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
			DxVertexBuffer vb10 = new DxVertexBuffer(nativeDevice, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

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
			//nativeDevice.Rasterizer.SetViewports(currentViewport);
            //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                technique.GetPassByIndex(i).Apply();
				nativeDevice.Draw(vertexCount, vertexOffset);
            }

			nativeDevice.InputAssembler.InputLayout.Dispose();
			nativeDevice.InputAssembler.InputLayout = null;
        }
        #endregion

		#region SetupEffectForDraw
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
		#endregion

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
            nativeDevice.Rasterizer.SetViewports(new Dx10.Viewport(x, y, width, height, minDepth, maxDepth));
		}

        protected void SetViewport(params Dx10.Viewport[] viewports)
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
            if (renderTargets == null)
            {
                // reset the RenderTarget to backbuffer
                CreateDepthStencilBuffer(this.depthStencilBuffer.Description.Format, this.backBuffer.Description.Width, this.backBuffer.Description.Height, false);
				nativeDevice.OutputMerger.SetRenderTargets(1, new Dx10.RenderTargetView[] { this.renderView }, this.depthStencilView);  //TODO: necessary?
				nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
                nativeDevice.Rasterizer.SetViewports(new Dx10.Viewport(0, 0, this.backBuffer.Description.Width, this.backBuffer.Description.Height));

                // dispose the old views
                for (int i = 0; i < renderTargetView.Length; i++)
                {
                    if (renderTargetView[i] != null)
                    {
                        renderTargetView[i].Dispose();
                        renderTargetView[i] = null;
                    }
                }
            }
            else
            {
                int renderTargetCount = renderTargets.Length;
                RenderTargetView[] renderTargetsToDelete = new RenderTargetView[renderTargetView.Length];
                Array.Copy(renderTargetView, renderTargetsToDelete, renderTargetView.Length);
                Dx10.Viewport[] rtViewports = new Dx10.Viewport[renderTargetCount];

                if (this.renderTargetView.Length != renderTargetCount)
                {
					this.renderTargetView = new Dx10.RenderTargetView[renderTargetCount];
                }

                int width = this.backBuffer.Description.Width;
                int height = this.backBuffer.Description.Height;

                for (int i = 0; i < renderTargetCount; i++)
                {
                    RenderTarget2D renderTarget = renderTargets[i].RenderTarget as RenderTarget2D;

                    //TODO: check if all render Targets have the same size
                    width = renderTarget.Width;
                    height = renderTarget.Height;

                    if (renderTarget != null)
                    {
                        RenderTarget2D_DX10 nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX10;

                        if (renderTargetView[i] != null)
                        {
                            renderTargetView[i].Dispose();
                        }

						renderTargetView[i] = new Dx10.RenderTargetView(nativeDevice, ((DxTexture2D)nativeRenderTarget).NativeShaderResourceView.Resource);
                        rtViewports[i] = new Dx10.Viewport(0, 0, width, height);
                    }
                }

                CreateDepthStencilBuffer(this.depthStencilBuffer.Description.Format, width, height, false);

				nativeDevice.OutputMerger.SetRenderTargets(renderTargetCount, renderTargetView, this.depthStencilView);

                nativeDevice.Rasterizer.SetViewports(rtViewports);

                // free the old render target views...
                for (int i = 0; i < renderTargetsToDelete.Length; i++)
                {
                    if (renderTargetsToDelete[i] != null)
                    {
                        renderTargetsToDelete[i].Dispose();
                        renderTargetsToDelete[i] = null;
                    }
                }
            }
        }
		#endregion

		#region DisposeRenderView
		protected void DisposeRenderView()
		{
			renderView.Dispose();
			renderView = null;

			backBuffer.Dispose();
			backBuffer = null;
		}
		#endregion

		#region Dispose
		public void Dispose()
        {
            for (int i = 0; i < renderTargetView.Length; i++)
            {
                if (renderTargetView[i] != null)
                {
                    renderTargetView[i].Dispose();
                    renderTargetView[i] = null;
                }
            }

            if (swapChain != null)
            {
				DisposeRenderView();

                swapChain.Dispose();
                swapChain = null;
            }

            if (this.depthStencilView != null)
            {
                this.depthStencilBuffer.Dispose();
                this.depthStencilBuffer = null;

                this.depthStencilView.Dispose();
                this.depthStencilView = null;
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
	}
}
