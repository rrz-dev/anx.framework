#region Using Statements
using System;
using SharpDX.DXGI;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D10;
using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Device = SharpDX.Direct3D10.Device;
using Rectangle = ANX.Framework.Rectangle;
using Vector4 = ANX.Framework.Vector4;
using VertexBufferBinding = ANX.Framework.Graphics.VertexBufferBinding;
using Viewport = ANX.Framework.Graphics.Viewport;

namespace ANX.RenderSystem.Windows.DX10
{
    public class GraphicsDeviceWindowsDX10 : INativeGraphicsDevice
	{
		#region Constants
		private const float ColorMultiplier = 1f / 255f;
		#endregion

        #region Interop
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner 
            public int Top;         // y position of upper-left corner 
            public int Right;       // x position of lower-right corner 
            public int Bottom;      // y position of lower-right corner 
        } 

        #endregion

        #region Private Members
        private Device device;
        private SwapChain swapChain; 
        private RenderTargetView renderView;
        private RenderTargetView[] renderTargetView = new RenderTargetView[1];
        private DepthStencilView depthStencilView;
        private SharpDX.Direct3D10.Texture2D depthStencilBuffer;
        private SharpDX.Direct3D10.Texture2D backBuffer;
        internal Effect_DX10 currentEffect;
        private VertexBuffer currentVertexBuffer;
        private IndexBuffer currentIndexBuffer;
        private SharpDX.Direct3D10.Viewport currentViewport;
        private uint lastClearColor;
        private SharpDX.Color4 clearColor;
        private bool vSyncEnabled;

		#endregion // Private Members

		internal Device NativeDevice
		{
			get
			{
				return this.device;
			}
		}

        public GraphicsDeviceWindowsDX10(PresentationParameters presentationParameters)
        {
            this.vSyncEnabled = true;

            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, new Rational(60, 1), FormatConverter.Translate(presentationParameters.BackBufferFormat)),
                IsWindowed = true,
                OutputHandle = presentationParameters.DeviceWindowHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create Device and SwapChain
#if DIRECTX_DEBUG_LAYER
            // http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
            Device.CreateWithSwapChain(SharpDX.Direct3D10.DriverType.Hardware, DeviceCreationFlags.Debug, desc, out device, out swapChain);
#else
            Device.CreateWithSwapChain(SharpDX.Direct3D10.DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
#endif

            // Ignore all windows events
            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(presentationParameters.DeviceWindowHandle, WindowAssociationFlags.IgnoreAll);

            ResizeRenderWindow(presentationParameters);

            // New RenderTargetView from the backbuffer
            backBuffer = SharpDX.Direct3D10.Texture2D.FromSwapChain<SharpDX.Direct3D10.Texture2D>(swapChain, 0);
            renderView = new RenderTargetView(device, backBuffer);

            currentViewport = new SharpDX.Direct3D10.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);

            //
            // create the depth stencil buffer
            //
            Format depthFormat = FormatConverter.Translate(presentationParameters.DepthStencilFormat);
            if (depthFormat != Format.Unknown)
            {
                CreateDepthStencilBuffer(depthFormat);
            }
        }

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
            this.depthStencilBuffer = new SharpDX.Direct3D10.Texture2D(device, depthStencilTextureDesc);

            this.depthStencilView = new DepthStencilView(device, this.depthStencilBuffer);

            Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, Vector4.Zero, 1.0f, 0);  // this workaround is working but maybe not the best solution to issue #472
        }

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
                    device.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else if ((options | ClearOptions.Stencil) == options)
                {
                    device.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else
                {
                    device.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth, depth, (byte)stencil);
                }
            }
        }

		#endregion

        #region Present
        public void Present()
        {
            swapChain.Present(this.vSyncEnabled ? 1 : 0, PresentFlags.None);
        }

        #endregion // Present

        #region DrawPrimitives & DrawIndexedPrimitives
        public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
        {
            SharpDX.Direct3D10.EffectPass pass; SharpDX.Direct3D10.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            // Prepare All the stages
            device.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);

            device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
                device.DrawIndexed(CalculateVertexCount(primitiveType, primitiveCount), startIndex, baseVertex);
            }
        }

        public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
        {
            SharpDX.Direct3D10.EffectPass pass; SharpDX.Direct3D10.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            // Prepare All the stages
            device.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);

            device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
                device.Draw(primitiveCount, vertexOffset);
            }
        }

        #endregion // DrawPrimitives & DrawIndexedPrimitives

        #region DrawInstancedPrimitives
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount)
        {
            device.DrawIndexedInstanced(numVertices, instanceCount, startIndex, baseVertex, 0);
        }

        #endregion // DrawInstancedPrimitives

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration, IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;
            VertexBuffer_DX10 vb10 = new VertexBuffer_DX10(this.device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

            SharpDX.Direct3D10.VertexBufferBinding nativeVertexBufferBindings = new SharpDX.Direct3D10.VertexBufferBinding(vb10.NativeBuffer, vertexDeclaration.VertexStride, 0);

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

            IndexBuffer_DX10 idx10 = new IndexBuffer_DX10(this.device, indexFormat, indexCount, BufferUsage.None);
            if (indexData.GetType()  == typeof(Int16[]))
            {
                idx10.SetData<short>(null, (short[])indexData);
            }
            else
            {
                idx10.SetData<int>(null, (int[])indexData);
            }

            DrawIndexedPrimitives(primitiveType, 0, vertexOffset, numVertices, indexOffset, primitiveCount);
        }

        #endregion // DrawUserIndexedPrimitives<T>

        #region DrawUserPrimitives<T>
        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            VertexBuffer_DX10 vb10 = new VertexBuffer_DX10(this.device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb10.SetData<T>(null, vertexData);

            SharpDX.Direct3D10.VertexBufferBinding nativeVertexBufferBindings = new SharpDX.Direct3D10.VertexBufferBinding(vb10.NativeBuffer, vertexDeclaration.VertexStride, 0);

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

            SharpDX.Direct3D10.EffectPass pass; SharpDX.Direct3D10.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            var layout = CreateInputLayout(device, passSignature, vertexDeclaration);

            device.InputAssembler.InputLayout = layout;
            // Prepare All the stages
            device.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);

            //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
                device.Draw(primitiveCount, vertexOffset);
            }
        }

        #endregion // DrawUserPrimitives<T>

        private void SetupEffectForDraw(out SharpDX.Direct3D10.EffectPass pass, out SharpDX.Direct3D10.EffectTechnique technique,
			out ShaderBytecode passSignature)
        {
            // get the current effect
            //TODO: check for null and throw exception
            Effect_DX10 effect = this.currentEffect;
			
            // get the input semantic of the current effect / technique that is used
            // TODO: check for null's and throw exceptions
			// TODO: get the correct pass index!
			technique = effect.GetCurrentTechnique().NativeTechnique;
            pass = technique.GetPassByIndex(0);
            passSignature = pass.Description.Signature;
        }

        private void SetupInputLayout(ShaderBytecode passSignature)
        {
            // get the VertexDeclaration from current VertexBuffer to create input layout for the input assembler
            
            if (currentVertexBuffer == null)
                throw new ArgumentNullException("passSignature");

            VertexDeclaration vertexDeclaration = currentVertexBuffer.VertexDeclaration;
            var layout = CreateInputLayout(device, passSignature, vertexDeclaration);

            device.InputAssembler.InputLayout = layout;
        }

        private int CalculateVertexCount(PrimitiveType type, int primitiveCount)
        {
            if (type == PrimitiveType.TriangleList)
            {
                return primitiveCount * 3;
            }
            else if (type == PrimitiveType.LineList)
            {
                return primitiveCount * 2;
            }
            else if (type == PrimitiveType.LineStrip)
            {
                return primitiveCount + 1;
            }
            else if (type == PrimitiveType.TriangleStrip)
            {
                return primitiveCount + 2;
            }
            else
            {
                throw new NotImplementedException("couldn't calculate vertex count for PrimitiveType '" + type.ToString() + "'");
            }
        }

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
                device.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer, FormatConverter.Translate(indexBuffer.IndexElementSize), 0);
            }
            else
            {
                throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
            }
        }

#if XNAEXT
        public void SetConstantBuffer(int slot, ANX.Framework.Graphics.ConstantBuffer constantBuffer)
        {
            if (constantBuffer == null)
            {
                throw new ArgumentNullException("constantBuffer");
            }

            throw new NotImplementedException();
        }
#endif

        public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
        {
            if (vertexBuffers == null)
            {
                throw new ArgumentNullException("vertexBuffers");
            }

            this.currentVertexBuffer = vertexBuffers[0].VertexBuffer;   //TODO: hmmmmm, not nice :-)

            SharpDX.Direct3D10.VertexBufferBinding[] nativeVertexBufferBindings =
				new SharpDX.Direct3D10.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                var nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as VertexBuffer_DX10;

                if (nativeVertexBuffer != null)
                {
                    nativeVertexBufferBindings[i] = new SharpDX.Direct3D10.VertexBufferBinding(nativeVertexBuffer.NativeBuffer,
						anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride, anxVertexBufferBinding.VertexOffset);
                }
                else
                {
                    throw new Exception("couldn't fetch native DirectX10 VertexBuffer");
                }
            }

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);
        }

        public void SetViewport(Viewport viewport)
        {
            this.currentViewport = new SharpDX.Direct3D10.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height,
				viewport.MinDepth, viewport.MaxDepth);
        }

        /// <summary>
        /// This method creates a InputLayout which is needed by DirectX 10 for rendering primitives.
		/// The VertexDeclaration of ANX/XNA needs to be mapped to the DirectX 10 types.
        /// </summary>
        private InputLayout CreateInputLayout(Device device, ShaderBytecode passSignature, VertexDeclaration vertexDeclaration)
        {
            VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();
            int elementCount = vertexElements.Length;
            InputElement[] inputElements = new InputElement[elementCount];

            for (int i = 0; i < elementCount; i++)
                inputElements[i] = CreateInputElementFromVertexElement(vertexElements[i]);

            return new InputLayout(device, passSignature, inputElements);
        }

        private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement)
        {
            string elementName = FormatConverter.Translate(ref vertexElement);
			Format elementFormat = ConvertVertexElementFormat(vertexElement.VertexElementFormat);
            return new InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
        }

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

                device.OutputMerger.SetRenderTargets(1, new RenderTargetView[] { this.renderView }, this.depthStencilView);
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

                    this.renderTargetView = new RenderTargetView[renderTargetCount];
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

                        renderTargetView[i] = new RenderTargetView(device, ((Texture2D_DX10)nativeRenderTarget).NativeShaderResourceView.Resource);
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

		#region GetBackBufferData
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

                backBuffer = SharpDX.Direct3D10.Texture2D.FromSwapChain<SharpDX.Direct3D10.Texture2D>(swapChain, 0);
                renderView = new RenderTargetView(device, backBuffer);

                currentViewport = new SharpDX.Direct3D10.Viewport(0, 0, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight);

                // create the depth stencil buffer
                Format depthFormat = FormatConverter.Translate(presentationParameters.DepthStencilFormat);
                if (depthFormat != Format.Unknown)
                {
                    CreateDepthStencilBuffer(depthFormat);
                }
            }

            ResizeRenderWindow(presentationParameters);
        }
		#endregion

		#region ResizeRenderWindow
		private void ResizeRenderWindow(PresentationParameters presentationParameters)
        {
            RECT windowRect;
            RECT clientRect;
			if (GetWindowRect(presentationParameters.DeviceWindowHandle, out windowRect) &&
				GetClientRect(presentationParameters.DeviceWindowHandle, out clientRect))
			{
				int width = presentationParameters.BackBufferWidth + ((windowRect.Right - windowRect.Left) - clientRect.Right);
				int height = presentationParameters.BackBufferHeight + ((windowRect.Bottom - windowRect.Top) - clientRect.Bottom);

				SetWindowPos(presentationParameters.DeviceWindowHandle, IntPtr.Zero, windowRect.Left, windowRect.Top, width,
					height, 0);
			}
        }
		#endregion

        public bool VSync
        {
            get
            {
                return this.vSyncEnabled;
            }
            set
            {
                this.vSyncEnabled = value;
            }
        }

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
