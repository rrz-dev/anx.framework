#region Using Statements
using System;
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

namespace ANX.RenderSystem.Windows.DX11
{
    public partial class GraphicsDeviceDX : INativeGraphicsDevice
    {
        #region Private
        private DeviceContext nativeDevice;
        private RenderTargetView renderView;
        private RenderTargetView[] renderTargetView = new RenderTargetView[1];
        private DepthStencilView depthStencilView;
        private SharpDX.Direct3D11.Texture2D depthStencilBuffer;
        private SharpDX.Direct3D11.Texture2D backBuffer;
        internal EffectDX currentEffect;
        private SharpDX.Direct3D11.Viewport currentViewport;
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

			// http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
			var flags = IsDebugMode ? DeviceCreationFlags.Debug : DeviceCreationFlags.None;
			Device.CreateWithSwapChain(DriverType.Hardware, flags, desc, out dxDevice, out swapChain);
			nativeDevice = dxDevice.ImmediateContext;
		}
		#endregion

		#region CreateRenderView
		protected void CreateRenderView()
		{
			backBuffer = SharpDX.Direct3D11.Texture2D.FromSwapChain<SharpDX.Direct3D11.Texture2D>(swapChain, 0);
			renderView = new RenderTargetView(nativeDevice.Device, backBuffer);
		}
		#endregion

		#region CreateDepthStencilBuffer
		protected void CreateDepthStencilBuffer(Format depthFormat)
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

            DepthStencilViewDescription depthStencilViewDesc = new DepthStencilViewDescription()
            {
                Format = depthFormat,
            };

            Texture2DDescription depthStencilTextureDesc = new Texture2DDescription()
            {
                Width = this.backBuffer.Description.Width,
                Height = this.backBuffer.Description.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = depthFormat,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };
			this.depthStencilBuffer = new SharpDX.Direct3D11.Texture2D(nativeDevice.Device, depthStencilTextureDesc);

			this.depthStencilView = new DepthStencilView(nativeDevice.Device, this.depthStencilBuffer);

            Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, ANX.Framework.Vector4.Zero, 1.0f, 0);  //TODO: this workaround is working but maybe not the best solution to issue #472
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

        public void Clear(ClearOptions options, ANX.Framework.Vector4 color, float depth, int stencil)
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
					nativeDevice.ClearRenderTargetView(this.renderView, this.clearColor);
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
					nativeDevice.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth |
						DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else if ((options | ClearOptions.Stencil) == options)
                {
					nativeDevice.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Stencil, depth,
						(byte)stencil);
                }
                else
                {
					nativeDevice.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth, depth, (byte)stencil);
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
		public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex,
					int numVertices, int startIndex, int primitiveCount)
		{
			SharpDX.Direct3D11.EffectPass pass;
			SharpDX.Direct3D11.EffectTechnique technique;
			ShaderBytecode passSignature;
			SetupEffectForDraw(out pass, out technique, out passSignature);

			var layout = SetupInputLayout(passSignature);

			// Prepare All the stages
			nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
			nativeDevice.Rasterizer.SetViewports(currentViewport);

			nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

			for (int i = 0; i < technique.Description.PassCount; ++i)
			{
				pass.Apply(nativeDevice);
				nativeDevice.DrawIndexed(DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount), startIndex,
					baseVertex);
			}

			layout.Dispose();
			layout = null;
		}
		#endregion

		#region DrawPrimitives
		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
		{
			SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
			SetupEffectForDraw(out pass, out technique, out passSignature);

			var layout = SetupInputLayout(passSignature);

			// Prepare All the stages
			nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
			nativeDevice.Rasterizer.SetViewports(currentViewport);

			nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

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
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount)
        {
			nativeDevice.DrawIndexedInstanced(numVertices, instanceCount, startIndex, baseVertex, 0);
        }

        #endregion // DrawInstancedPrimitives

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;
			var vb11 = new DxVertexBuffer(nativeDevice.Device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb11.SetData<T>(null, vertexData);

            var nativeVertexBufferBindings = new SharpDX.Direct3D11.VertexBufferBinding(vb11.NativeBuffer,
				vertexDeclaration.VertexStride, 0);

			nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

			DxIndexBuffer idx10 = new DxIndexBuffer(nativeDevice.Device, indexFormat, indexCount, BufferUsage.None);
            if (indexData.GetType() == typeof(Int16[]))
                idx10.SetData<short>(null, (short[])indexData);
            else
                idx10.SetData<int>(null, (int[])indexData);

            DrawIndexedPrimitives(primitiveType, 0, vertexOffset, numVertices, indexOffset, primitiveCount);
        }

        #endregion // DrawUserIndexedPrimitives<T>

        #region DrawUserPrimitives<T>
        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
			DxVertexBuffer vb11 = new DxVertexBuffer(nativeDevice.Device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb11.SetData<T>(null, vertexData);

            SharpDX.Direct3D11.VertexBufferBinding nativeVertexBufferBindings = new SharpDX.Direct3D11.VertexBufferBinding(vb11.NativeBuffer, vertexDeclaration.VertexStride, 0);

			nativeDevice.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

            SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

			var layout = CreateInputLayout(nativeDevice.Device, passSignature, vertexDeclaration);

			nativeDevice.InputAssembler.InputLayout = layout;
            // Prepare All the stages
			nativeDevice.InputAssembler.PrimitiveTopology = DxFormatConverter.Translate(primitiveType);
			nativeDevice.Rasterizer.SetViewports(currentViewport);

            //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
				pass.Apply(nativeDevice);
				nativeDevice.Draw(primitiveCount, vertexOffset);
			}

			layout.Dispose();
			layout = null;
        }

        #endregion // DrawUserPrimitives<T>

		#region SetupEffectForDraw
		private void SetupEffectForDraw(out SharpDX.Direct3D11.EffectPass pass, out SharpDX.Direct3D11.EffectTechnique technique,
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
		#endregion

		#region SetupInputLayout
		private InputLayout SetupInputLayout(ShaderBytecode passSignature)
        {
            // get the VertexDeclaration from current VertexBuffer to create input layout for the input assembler
            //TODO: check for null and throw exception
            VertexDeclaration vertexDeclaration = currentVertexBuffer.VertexDeclaration;
			var layout = CreateInputLayout(nativeDevice.Device, passSignature, vertexDeclaration);

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

            this.currentVertexBuffer = vertexBuffers[0].VertexBuffer;   //TODO: hmmmmm, not nice :-)

            var nativeVertexBufferBindings = new SharpDX.Direct3D11.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                var nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as DxVertexBuffer;

                if (nativeVertexBuffer != null)
                {
                    nativeVertexBufferBindings[i] = new SharpDX.Direct3D11.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride, anxVertexBufferBinding.VertexOffset);
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
			currentViewport = new SharpDX.Direct3D11.Viewport(x, y, width, height, minDepth, maxDepth);
		}
		#endregion

		#region CreateInputLayout
		private InputLayout CreateInputLayout(Device device, ShaderBytecode passSignature, VertexDeclaration vertexDeclaration)
        {
            VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();
            int elementCount = vertexElements.Length;
            InputElement[] inputElements = new InputElement[elementCount];

            for (int i = 0; i < elementCount; i++)
                inputElements[i] = CreateInputElementFromVertexElement(vertexElements[i]);

            // Layout from VertexShader input signature
            return new InputLayout(device, passSignature, inputElements);
		}
		#endregion

		#region CreateInputElementFromVertexElement
		private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement)
		{
			string elementName = DxFormatConverter.Translate(ref vertexElement);
			Format elementFormat = DxFormatConverter.ConvertVertexElementFormat(vertexElement.VertexElementFormat);
			return new InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
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

                //deviceContext.OutputMerger.SetRenderTargets(1, new RenderTargetView[] { this.renderView }, this.depthStencilView);
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

                    this.renderTargetView = new RenderTargetView[renderTargetCount];
                }

                for (int i = 0; i < renderTargetCount; i++)
                {
                    RenderTarget2D renderTarget = renderTargets[i].RenderTarget as RenderTarget2D;
                    if (renderTarget != null)
                    {
                        RenderTarget2D_DX11 nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX11;

                        if (renderTargetView[i] != null)
                        {
                            renderTargetView[i].Dispose();
                        }

						renderTargetView[i] = new RenderTargetView(nativeDevice.Device,
							((DxTexture2D)nativeRenderTarget).NativeShaderResourceView.Resource);
                    }
                }

                //deviceContext.OutputMerger.SetRenderTargets(renderTargetCount, renderTargetView, this.depthStencilView);
				nativeDevice.OutputMerger.SetTargets(this.depthStencilView, this.renderTargetView);
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

        internal DeviceContext NativeDevice
        {
            get
            {
                return this.nativeDevice;
            }
        }
    }
}
