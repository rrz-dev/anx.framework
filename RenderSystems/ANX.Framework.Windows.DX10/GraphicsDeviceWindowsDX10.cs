using System;
using ANX.BaseDirectX;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class GraphicsDeviceWindowsDX10 : BaseGraphicsDevice<Dx10.Device>, INativeGraphicsDevice
	{
        #region Private
		private Dx10.RenderTargetView renderView;
		private Dx10.RenderTargetView[] renderTargetView = new Dx10.RenderTargetView[1];
		private Dx10.DepthStencilView depthStencilView;
        private Dx10.Texture2D depthStencilBuffer;
        private Dx10.Texture2D backBuffer;
        internal Effect_DX10 currentEffect;
        private Dx10.Viewport currentViewport;
		#endregion

		#region Constructor
		public GraphicsDeviceWindowsDX10(PresentationParameters presentationParameters)
			: base(presentationParameters)
        {
        }
		#endregion

		#region CreateDevice
		protected override void CreateDevice(PresentationParameters presentationParameters)
		{
			var desc = new SwapChainDescription()
			{
				BufferCount = 1,
				ModeDescription = new ModeDescription(presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight, new Rational(60, 1),
					BaseFormatConverter.Translate(presentationParameters.BackBufferFormat)),
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
		protected override void CreateRenderView()
		{
			backBuffer = Dx10.Texture2D.FromSwapChain<Dx10.Texture2D>(swapChain, 0);
			renderView = new Dx10.RenderTargetView(nativeDevice, backBuffer);
		}
		#endregion

		#region CreateDepthStencilBuffer
		protected override void CreateDepthStencilBuffer(Format depthFormat)
        {
            if (this.depthStencilBuffer != null &&
                this.depthStencilBuffer.Description.Format == depthFormat &&
                this.depthStencilBuffer.Description.Width == this.backBuffer.Description.Width &&
                this.depthStencilBuffer.Description.Height == this.backBuffer.Description.Height)
            {
                // a DepthStencilBuffer with the right format and the right size already exists -> nothing to do
                return;
            }

            if (this.depthStencilView != null)
            {
                this.depthStencilView.Dispose();
                this.depthStencilView = null;
            }

            if (this.depthStencilBuffer != null)
            {
                this.depthStencilBuffer.Dispose();
                this.depthStencilBuffer = null;
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
                Width = this.backBuffer.Description.Width,
                Height = this.backBuffer.Description.Height,
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

			// this workaround is working but maybe not the best solution to issue #472
            Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Vector4.Zero, 1.0f, 0);
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
        public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount)
		{
			Dx10.EffectTechnique technique = SetupEffectForDraw();
			int vertexCount = BaseFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

			nativeDevice.InputAssembler.PrimitiveTopology = BaseFormatConverter.Translate(primitiveType);
			nativeDevice.Rasterizer.SetViewports(currentViewport);
			nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

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

			nativeDevice.InputAssembler.PrimitiveTopology = BaseFormatConverter.Translate(primitiveType);
			nativeDevice.Rasterizer.SetViewports(currentViewport);
			nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
			{
				technique.GetPassByIndex(i).Apply();
				nativeDevice.Draw(primitiveCount, vertexOffset);
            }

			nativeDevice.InputAssembler.InputLayout.Dispose();
			nativeDevice.InputAssembler.InputLayout = null;
        }
        #endregion

        #region DrawInstancedPrimitives
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount, int instanceCount)
        {
			nativeDevice.DrawIndexedInstanced(numVertices, instanceCount, startIndex, baseVertex, 0);
        }
        #endregion

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;
			VertexBuffer_DX10 vb10 = new VertexBuffer_DX10(nativeDevice, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

            Dx10.VertexBufferBinding nativeVertexBufferBindings = new Dx10.VertexBufferBinding(vb10.NativeBuffer,
				vertexDeclaration.VertexStride, 0);

			nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

			IndexBuffer_DX10 idx10 = new IndexBuffer_DX10(nativeDevice, indexFormat, indexCount, BufferUsage.None);
            if (indexData.GetType()  == typeof(Int16[]))
                idx10.SetData<short>(null, (short[])indexData);
            else
                idx10.SetData<int>(null, (int[])indexData);

            DrawIndexedPrimitives(primitiveType, 0, vertexOffset, numVertices, indexOffset, primitiveCount);
        }
        #endregion

        #region DrawUserPrimitives<T>
        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount,
			VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
			VertexBuffer_DX10 vb10 = new VertexBuffer_DX10(nativeDevice, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

            Dx10.VertexBufferBinding nativeVertexBufferBindings = new Dx10.VertexBufferBinding(vb10.NativeBuffer,
				vertexDeclaration.VertexStride, 0);

			nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

			//TODO: check for currentEffect null and throw exception
			// TODO: check for null's and throw exceptions
			// TODO: get the correct pass index!
			var technique = currentEffect.GetCurrentTechnique().NativeTechnique;
			var pass = technique.GetPassByIndex(0);
			var layout = CreateInputLayout(nativeDevice, pass.Description.Signature, vertexDeclaration);

			nativeDevice.InputAssembler.InputLayout = layout;
            // Prepare All the stages
			nativeDevice.InputAssembler.PrimitiveTopology = BaseFormatConverter.Translate(primitiveType);
			nativeDevice.Rasterizer.SetViewports(currentViewport);

            //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
				nativeDevice.Draw(primitiveCount, vertexOffset);
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
		private void SetupInputLayout(ShaderBytecode passSignature)
        {
            if (currentVertexBuffer == null)
                throw new ArgumentNullException("passSignature");

            VertexDeclaration vertexDeclaration = currentVertexBuffer.VertexDeclaration;
			var layout = CreateInputLayout(nativeDevice, passSignature, vertexDeclaration);

			nativeDevice.InputAssembler.InputLayout = layout;
        }
		#endregion
		
		#region SetIndexBuffer
        public void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            if (indexBuffer == null)
                throw new ArgumentNullException("indexBuffer");

            this.currentIndexBuffer = indexBuffer;

            IndexBuffer_DX10 nativeIndexBuffer = indexBuffer.NativeIndexBuffer as IndexBuffer_DX10;

            if (nativeIndexBuffer != null)
            {
				nativeDevice.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer,
					BaseFormatConverter.Translate(indexBuffer.IndexElementSize), 0);
            }
            else
            {
                throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
            }
        }
		#endregion

		#region SetVertexBuffers
		public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
        {
            if (vertexBuffers == null)
                throw new ArgumentNullException("vertexBuffers");

            this.currentVertexBuffer = vertexBuffers[0].VertexBuffer;   //TODO: hmmmmm, not nice :-)

            Dx10.VertexBufferBinding[] nativeVertexBufferBindings =
				new Dx10.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                var nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as VertexBuffer_DX10;

                if (nativeVertexBuffer != null)
                {
                    nativeVertexBufferBindings[i] = new Dx10.VertexBufferBinding(nativeVertexBuffer.NativeBuffer,
						anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride, anxVertexBufferBinding.VertexOffset);
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
		protected override void SetViewport(int x, int y, int width, int height, float minDepth, float maxDepth)
		{
			currentViewport = new Dx10.Viewport(x, y, width, height, minDepth, maxDepth);
		}
		#endregion

		#region CreateInputLayout
		/// <summary>
        /// This method creates a InputLayout which is needed by DirectX 10 for rendering primitives.
		/// The VertexDeclaration of ANX/XNA needs to be mapped to the DirectX 10 types.
        /// </summary>
		private Dx10.InputLayout CreateInputLayout(Dx10.Device device, ShaderBytecode passSignature,
			VertexDeclaration vertexDeclaration)
        {
            VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();
            int elementCount = vertexElements.Length;
			var inputElements = new Dx10.InputElement[elementCount];

            for (int i = 0; i < elementCount; i++)
                inputElements[i] = CreateInputElementFromVertexElement(vertexElements[i]);

			return new Dx10.InputLayout(device, passSignature, inputElements);
		}
		#endregion

		#region CreateInputElementFromVertexElement
		private Dx10.InputElement CreateInputElementFromVertexElement(VertexElement vertexElement)
        {
			string elementName = BaseFormatConverter.Translate(ref vertexElement);
			Format elementFormat = BaseFormatConverter.ConvertVertexElementFormat(vertexElement.VertexElementFormat);
			return new Dx10.InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
		}
		#endregion

		#region SetRenderTargets
		public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
        {
            if (renderTargets == null)
            {
                // reset the RenderTarget to backbuffer
                for (int i = 0; i < renderTargetView.Length; i++)
                {
                    if (renderTargetView[i] != null)
                    {
                        renderTargetView[i].Dispose();
                        renderTargetView[i] = null;
                    }
                }

				nativeDevice.OutputMerger.SetRenderTargets(1, new Dx10.RenderTargetView[] { this.renderView },
					this.depthStencilView);
				nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
            }
            else
            {
                int renderTargetCount = renderTargets.Length;
                if (this.renderTargetView.Length != renderTargetCount)
                {
                    for (int i = 0; i < renderTargetView.Length; i++)
                    {
                        if (renderTargetView[i] != null)
                        {
                            renderTargetView[i].Dispose();
                            renderTargetView[i] = null;
                        }
                    }

					this.renderTargetView = new Dx10.RenderTargetView[renderTargetCount];
                }

                for (int i = 0; i < renderTargetCount; i++)
                {
                    RenderTarget2D renderTarget = renderTargets[i].RenderTarget as RenderTarget2D;
                    if (renderTarget != null)
                    {
                        RenderTarget2D_DX10 nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX10;

                        if (renderTargetView[i] != null)
                            renderTargetView[i].Dispose();

						renderTargetView[i] = new Dx10.RenderTargetView(nativeDevice,
							((Texture2D_DX10)nativeRenderTarget).NativeShaderResourceView.Resource);
                    }
                }

				nativeDevice.OutputMerger.SetRenderTargets(renderTargetCount, renderTargetView, this.depthStencilView);
				nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderTargetView);

                //if (renderTargets.Length == 1)
                //{
                //    RenderTarget2D renderTarget = renderTargets[0].RenderTarget as RenderTarget2D;
                //    RenderTarget2D_DX10 nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX10;
                //    if (renderTarget != null)
                //    {
                //        if (renderTargetView != null)
                //        {
                //            renderTargetView.Dispose();
                //            renderTargetView = null;
                //        }
                //        this.renderTargetView = new RenderTargetView(device, ((Texture2D_DX10)nativeRenderTarget).NativeShaderResourceView.Resource);
                //        DepthStencilView depthStencilView = null;
                //        device.OutputMerger.SetRenderTargets(1, new RenderTargetView[] { this.renderTargetView }, depthStencilView);
                //    }
                //}
                //else
                //{
                //    throw new NotImplementedException("handling of multiple RenderTargets are not yet implemented");
                //}
            }
        }
		#endregion

		#region DisposeRenderView
		protected override void DisposeRenderView()
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
	}
}
