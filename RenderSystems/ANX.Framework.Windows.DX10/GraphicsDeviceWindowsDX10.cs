//#define DIRECTX_DEBUG_LAYER
using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.DX10.Helpers;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;
using Dx10 = SharpDX.Direct3D10;
using ANX.BaseDirectX;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class GraphicsDeviceWindowsDX10 : INativeGraphicsDevice
	{
		#region Constants
		private const float ColorMultiplier = 1f / 255f;
		#endregion

        #region Private
		private Dx10.Device device;
        private SwapChain swapChain;
		private Dx10.RenderTargetView renderView;
		private Dx10.RenderTargetView[] renderTargetView = new Dx10.RenderTargetView[1];
		private Dx10.DepthStencilView depthStencilView;
        private Dx10.Texture2D depthStencilBuffer;
        private Dx10.Texture2D backBuffer;
        internal Effect_DX10 currentEffect;
        private VertexBuffer currentVertexBuffer;
        private IndexBuffer currentIndexBuffer;
        private Dx10.Viewport currentViewport;
        private uint lastClearColor;
        private SharpDX.Color4 clearColor;
		#endregion

		#region Public
		internal Dx10.Device NativeDevice
		{
			get
			{
				return this.device;
			}
		}

		public bool VSync { get; set; }
		#endregion

		#region Constructor
		public GraphicsDeviceWindowsDX10(PresentationParameters presentationParameters)
        {
			VSync = true;

            // SwapChain description
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

            // Create Device and SwapChain
#if DIRECTX_DEBUG_LAYER
            // http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
            Dx10.Device.CreateWithSwapChain(Dx10.DriverType.Hardware, Dx10.DeviceCreationFlags.Debug, desc, out device,
				out swapChain);
#else
			Dx10.Device.CreateWithSwapChain(Dx10.DriverType.Hardware, Dx10.DeviceCreationFlags.None, desc, out device,
				out swapChain);
#endif

            // Ignore all windows events
            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(presentationParameters.DeviceWindowHandle, WindowAssociationFlags.IgnoreAll);

			WindowHelper.ResizeRenderWindow(presentationParameters);

            // New RenderTargetView from the backbuffer
            backBuffer = Dx10.Texture2D.FromSwapChain<Dx10.Texture2D>(swapChain, 0);
			renderView = new Dx10.RenderTargetView(device, backBuffer);

            currentViewport = new Dx10.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);

            //
            // create the depth stencil buffer
            //
			Format depthFormat = BaseFormatConverter.Translate(presentationParameters.DepthStencilFormat);
            if (depthFormat != Format.Unknown)
                CreateDepthStencilBuffer(depthFormat);
        }
		#endregion

		#region CreateDepthStencilBuffer
		private void CreateDepthStencilBuffer(Format depthFormat)
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
            this.depthStencilBuffer = new Dx10.Texture2D(device, depthStencilTextureDesc);

			this.depthStencilView = new Dx10.DepthStencilView(device, this.depthStencilBuffer);

			// this workaround is working but maybe not the best solution to issue #472
            Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Vector4.Zero, 1.0f, 0);
        }
		#endregion

		#region Clear
		public void Clear(ref Color color)
		{
			uint newClearColor = color.PackedValue;
			if (lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				clearColor.Red = color.R * ColorMultiplier;
				clearColor.Green = color.G * ColorMultiplier;
				clearColor.Blue = color.B * ColorMultiplier;
				clearColor.Alpha = color.A * ColorMultiplier;
			}

            if (this.renderTargetView[0] == null)
            {
                this.device.ClearRenderTargetView(this.renderView, this.clearColor);
            }
            else
            {
                for (int i = 0; i < this.renderTargetView.Length; i++)
                {
                    if (this.renderTargetView[i] == null)
                    {
                        break;
                    }

                    this.device.ClearRenderTargetView(this.renderTargetView[i], this.clearColor);
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
                    this.device.ClearRenderTargetView(this.renderView, this.clearColor);
                }
                else
                {
                    for (int i = 0; i < this.renderTargetView.Length; i++)
                    {
                        if (this.renderTargetView[i] == null)
                        {
                            break;
                        }

                        this.device.ClearRenderTargetView(this.renderTargetView[i], this.clearColor);
                    }
                }
            }

            if (this.depthStencilView != null)
            {
                if ((options | ClearOptions.Stencil | ClearOptions.DepthBuffer) == options)
                {
                    // Clear the stencil buffer
					device.ClearDepthStencilView(this.depthStencilView, Dx10.DepthStencilClearFlags.Depth |
						Dx10.DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else if ((options | ClearOptions.Stencil) == options)
                {
					device.ClearDepthStencilView(this.depthStencilView, Dx10.DepthStencilClearFlags.Stencil, depth,
						(byte)stencil);
                }
                else
                {
					device.ClearDepthStencilView(this.depthStencilView, Dx10.DepthStencilClearFlags.Depth, depth, (byte)stencil);
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

			device.InputAssembler.PrimitiveTopology = BaseFormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);
            device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
			{
                technique.GetPassByIndex(i).Apply();
				device.DrawIndexed(vertexCount, startIndex, baseVertex);
			}

			device.InputAssembler.InputLayout.Dispose();
			device.InputAssembler.InputLayout = null;
		}
		#endregion

		#region DrawPrimitives
		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
		{
			Dx10.EffectTechnique technique = SetupEffectForDraw();

			device.InputAssembler.PrimitiveTopology = BaseFormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);
            device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
			{
				technique.GetPassByIndex(i).Apply();
                device.Draw(primitiveCount, vertexOffset);
            }

			device.InputAssembler.InputLayout.Dispose();
			device.InputAssembler.InputLayout = null;
        }
        #endregion

        #region DrawInstancedPrimitives
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount, int instanceCount)
        {
            device.DrawIndexedInstanced(numVertices, instanceCount, startIndex, baseVertex, 0);
        }
        #endregion

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;
            VertexBuffer_DX10 vb10 = new VertexBuffer_DX10(this.device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

            Dx10.VertexBufferBinding nativeVertexBufferBindings = new Dx10.VertexBufferBinding(vb10.NativeBuffer,
				vertexDeclaration.VertexStride, 0);

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

            IndexBuffer_DX10 idx10 = new IndexBuffer_DX10(this.device, indexFormat, indexCount, BufferUsage.None);
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
            VertexBuffer_DX10 vb10 = new VertexBuffer_DX10(this.device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

            Dx10.VertexBufferBinding nativeVertexBufferBindings = new Dx10.VertexBufferBinding(vb10.NativeBuffer,
				vertexDeclaration.VertexStride, 0);

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

			//TODO: check for currentEffect null and throw exception
			// TODO: check for null's and throw exceptions
			// TODO: get the correct pass index!
			var technique = currentEffect.GetCurrentTechnique().NativeTechnique;
			var pass = technique.GetPassByIndex(0);
			var layout = CreateInputLayout(device, pass.Description.Signature, vertexDeclaration);

            device.InputAssembler.InputLayout = layout;
            // Prepare All the stages
			device.InputAssembler.PrimitiveTopology = BaseFormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);

            //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
                device.Draw(primitiveCount, vertexOffset);
            }

			device.InputAssembler.InputLayout.Dispose();
			device.InputAssembler.InputLayout = null;
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
            var layout = CreateInputLayout(device, passSignature, vertexDeclaration);

            device.InputAssembler.InputLayout = layout;
        }
		#endregion
		
		#region SetIndexBuffer
        public void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            if (indexBuffer == null)
            {
                throw new ArgumentNullException("indexBuffer");
            }

            this.currentIndexBuffer = indexBuffer;

            IndexBuffer_DX10 nativeIndexBuffer = indexBuffer.NativeIndexBuffer as IndexBuffer_DX10;

            if (nativeIndexBuffer != null)
            {
				device.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer,
					BaseFormatConverter.Translate(indexBuffer.IndexElementSize), 0);
            }
            else
            {
                throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
            }
        }
		#endregion

#if XNAEXT
		#region SetConstantBuffer
        public void SetConstantBuffer(int slot, ANX.Framework.Graphics.ConstantBuffer constantBuffer)
        {
            if (constantBuffer == null)
                throw new ArgumentNullException("constantBuffer");

            throw new NotImplementedException();
        }
		#endregion
#endif

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

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);
		}
		#endregion

		#region SetViewport
		public void SetViewport(Viewport viewport)
        {
            this.currentViewport = new Dx10.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height,
				viewport.MinDepth, viewport.MaxDepth);
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
			Format elementFormat = ConvertVertexElementFormat(vertexElement.VertexElementFormat);
			return new Dx10.InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
		}
		#endregion

		#region ConvertVertexElementFormat
		private Format ConvertVertexElementFormat(VertexElementFormat format)
		{
			switch (format)
			{
				case VertexElementFormat.Vector2:
					return Format.R32G32_Float;
				case VertexElementFormat.Vector3:
					return Format.R32G32B32_Float;
				case VertexElementFormat.Vector4:
					return Format.R32G32B32A32_Float;
				case VertexElementFormat.Color:
					return Format.R8G8B8A8_UNorm;
				case VertexElementFormat.Single:
					return Format.R32_Float;
					// TODO: validate
				case VertexElementFormat.Short2:
					return Format.R16G16_SInt;
				case VertexElementFormat.Short4:
					return Format.R16G16B16A16_SInt;
			}

			throw new Exception("Can't map '" + format + "' to DXGI.Format in Dx10 CreateInputElementFromVertexElement.");
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

				device.OutputMerger.SetRenderTargets(1, new Dx10.RenderTargetView[] { this.renderView }, this.depthStencilView);
                device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
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
                        {
                            renderTargetView[i].Dispose();
                        }

						renderTargetView[i] = new Dx10.RenderTargetView(device, ((Texture2D_DX10)nativeRenderTarget).NativeShaderResourceView.Resource);
                    }
                }

                device.OutputMerger.SetRenderTargets(renderTargetCount, renderTargetView, this.depthStencilView);
                device.OutputMerger.SetTargets(this.depthStencilView, this.renderTargetView);

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

		#region GetBackBufferData (TODO)
		public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }

        public void GetBackBufferData<T>(T[] data) where T : struct
        {
            throw new NotImplementedException();
        }

        public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            throw new NotImplementedException();
        }
		#endregion

		#region ResizeBuffers
		public void ResizeBuffers(PresentationParameters presentationParameters)
        {
            if (swapChain != null)
            {
                renderView.Dispose();
                backBuffer.Dispose();

                //TODO: handle format

                swapChain.ResizeBuffers(swapChain.Description.BufferCount, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight, Format.R8G8B8A8_UNorm, swapChain.Description.Flags);

                backBuffer = Dx10.Texture2D.FromSwapChain<Dx10.Texture2D>(swapChain, 0);
				renderView = new Dx10.RenderTargetView(device, backBuffer);

                currentViewport = new Dx10.Viewport(0, 0, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight);

                // create the depth stencil buffer
				Format depthFormat = BaseFormatConverter.Translate(presentationParameters.DepthStencilFormat);
                if (depthFormat != Format.Unknown)
                    CreateDepthStencilBuffer(depthFormat);
            }

			WindowHelper.ResizeRenderWindow(presentationParameters);
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
                renderView.Dispose();
                renderView = null;

                backBuffer.Dispose();
                backBuffer = null;

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
