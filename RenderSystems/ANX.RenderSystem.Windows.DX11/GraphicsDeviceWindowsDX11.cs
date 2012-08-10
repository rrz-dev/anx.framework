#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using ANX.Framework;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;
using Rectangle = ANX.Framework.Rectangle;
using Vector4 = ANX.Framework.Vector4;
using VertexBufferBinding = ANX.Framework.Graphics.VertexBufferBinding;
using Viewport = ANX.Framework.Graphics.Viewport;

namespace ANX.RenderSystem.Windows.DX11
{
    public class GraphicsDeviceWindowsDX11 : INativeGraphicsDevice
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
        private DeviceContext deviceContext;
        private SwapChain swapChain;
        private RenderTargetView renderView;
        private RenderTargetView[] renderTargetView = new RenderTargetView[1];
        private DepthStencilView depthStencilView;
        private SharpDX.Direct3D11.Texture2D depthStencilBuffer;
        private SharpDX.Direct3D11.Texture2D backBuffer;
        internal Effect_DX11 currentEffect;
        private VertexBuffer currentVertexBuffer;
        private IndexBuffer currentIndexBuffer;
        private SharpDX.Direct3D11.Viewport currentViewport;
        private uint lastClearColor;
        private SharpDX.Color4 clearColor;
        private bool vSyncEnabled;

        #endregion // Private Members

        public GraphicsDeviceWindowsDX11(PresentationParameters presentationParameters)
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
            Device dxDevice;

#if DIRECTX_DEBUG_LAYER
            // http://msdn.microsoft.com/en-us/library/windows/desktop/bb205068(v=vs.85).aspx
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.Debug, desc, out dxDevice, out swapChain);
#else
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out dxDevice, out swapChain);
#endif
            this.deviceContext = dxDevice.ImmediateContext;

            // Ignore all windows events
            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(presentationParameters.DeviceWindowHandle, WindowAssociationFlags.IgnoreAll);

            ResizeRenderWindow(presentationParameters);

            // New RenderTargetView from the backbuffer
            backBuffer = SharpDX.Direct3D11.Texture2D.FromSwapChain<SharpDX.Direct3D11.Texture2D>(swapChain, 0);
            renderView = new RenderTargetView(deviceContext.Device, backBuffer);

            currentViewport = new SharpDX.Direct3D11.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);

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
            this.depthStencilBuffer = new SharpDX.Direct3D11.Texture2D(deviceContext.Device, depthStencilTextureDesc);

            this.depthStencilView = new DepthStencilView(deviceContext.Device, this.depthStencilBuffer);

            Clear(ClearOptions.DepthBuffer | ClearOptions.Stencil, ANX.Framework.Vector4.Zero, 1.0f, 0);  //TODO: this workaround is working but maybe not the best solution to issue #472
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
                this.deviceContext.ClearRenderTargetView(this.renderView, this.clearColor);
            }
            else
            {
                for (int i = 0; i < this.renderTargetView.Length; i++)
                {
                    if (this.renderTargetView[i] == null)
                    {
                        break;
                    }

                    this.deviceContext.ClearRenderTargetView(this.renderTargetView[i], this.clearColor);
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
                {
                    this.deviceContext.ClearRenderTargetView(this.renderView, this.clearColor);
                }
                else
                {
                    for (int i = 0; i < this.renderTargetView.Length; i++)
                    {
                        if (this.renderTargetView[i] == null)
                        {
                            break;
                        }

                        this.deviceContext.ClearRenderTargetView(this.renderTargetView[i], this.clearColor);
                    }
                }
            }

            if (this.depthStencilView != null)
            {
                if ((options | ClearOptions.Stencil | ClearOptions.DepthBuffer) == options)
                {
                    // Clear the stencil buffer
                    deviceContext.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else if ((options | ClearOptions.Stencil) == options)
                {
                    deviceContext.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Stencil, depth, (byte)stencil);
                }
                else
                {
                    deviceContext.ClearDepthStencilView(this.depthStencilView, DepthStencilClearFlags.Depth, depth, (byte)stencil);
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
            SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            // Prepare All the stages
            deviceContext.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            deviceContext.Rasterizer.SetViewports(currentViewport);

            deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply(deviceContext);
                deviceContext.DrawIndexed(CalculateVertexCount(primitiveType, primitiveCount), startIndex, baseVertex);
            }
        }

        public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
        {
            SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            // Prepare All the stages
            deviceContext.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            deviceContext.Rasterizer.SetViewports(currentViewport);

            deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply(deviceContext);
                deviceContext.Draw(primitiveCount, vertexOffset);
            }
        }

        #endregion // DrawPrimitives & DrawIndexedPrimitives

        #region DrawInstancedPrimitives
        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, int instanceCount)
        {
            deviceContext.DrawIndexedInstanced(numVertices, instanceCount, startIndex, baseVertex, 0);
        }

        #endregion // DrawInstancedPrimitives

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration, IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;
            VertexBuffer_DX11 vb11 = new VertexBuffer_DX11(this.deviceContext.Device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb11.SetData<T>(null, vertexData);

            SharpDX.Direct3D11.VertexBufferBinding nativeVertexBufferBindings = new SharpDX.Direct3D11.VertexBufferBinding(vb11.NativeBuffer, vertexDeclaration.VertexStride, 0);

            deviceContext.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

            IndexBuffer_DX11 idx10 = new IndexBuffer_DX11(this.deviceContext.Device, indexFormat, indexCount, BufferUsage.None);
            if (indexData.GetType() == typeof(Int16[]))
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
            VertexBuffer_DX11 vb11 = new VertexBuffer_DX11(this.deviceContext.Device, vertexDeclaration, vertexCount, BufferUsage.None);
            vb11.SetData<T>(null, vertexData);

            SharpDX.Direct3D11.VertexBufferBinding nativeVertexBufferBindings = new SharpDX.Direct3D11.VertexBufferBinding(vb11.NativeBuffer, vertexDeclaration.VertexStride, 0);

            deviceContext.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);

            SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            var layout = CreateInputLayout(deviceContext.Device, passSignature, vertexDeclaration);

            deviceContext.InputAssembler.InputLayout = layout;
            // Prepare All the stages
            deviceContext.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            deviceContext.Rasterizer.SetViewports(currentViewport);

            //device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply(deviceContext);
                deviceContext.Draw(primitiveCount, vertexOffset);
            }
        }

        #endregion // DrawUserPrimitives<T>

        internal DeviceContext NativeDevice
        {
            get
            {
                return this.deviceContext;
            }
        }

        private void SetupEffectForDraw(out SharpDX.Direct3D11.EffectPass pass, out SharpDX.Direct3D11.EffectTechnique technique, out ShaderBytecode passSignature)
        {
            // get the current effect
            //TODO: check for null and throw exception
            Effect_DX11 effect = this.currentEffect;

            // get the input semantic of the current effect / technique that is used
            //TODO: check for null's and throw exceptions
            technique = effect.NativeEffect.GetTechniqueByIndex(0);
            pass = technique.GetPassByIndex(0);
            passSignature = pass.Description.Signature;
        }

        private void SetupInputLayout(ShaderBytecode passSignature)
        {
            // get the VertexDeclaration from current VertexBuffer to create input layout for the input assembler
            //TODO: check for null and throw exception
            VertexDeclaration vertexDeclaration = currentVertexBuffer.VertexDeclaration;
            var layout = CreateInputLayout(deviceContext.Device, passSignature, vertexDeclaration);

            deviceContext.InputAssembler.InputLayout = layout;
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

            IndexBuffer_DX11 nativeIndexBuffer = indexBuffer.NativeIndexBuffer as IndexBuffer_DX11;

            if (nativeIndexBuffer != null)
            {
                deviceContext.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer, FormatConverter.Translate(indexBuffer.IndexElementSize), 0);
            }
            else
            {
                throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
            }
        }

        public void SetVertexBuffers(ANX.Framework.Graphics.VertexBufferBinding[] vertexBuffers)
        {
            if (vertexBuffers == null)
            {
                throw new ArgumentNullException("vertexBuffers");
            }

            this.currentVertexBuffer = vertexBuffers[0].VertexBuffer;   //TODO: hmmmmm, not nice :-)

            SharpDX.Direct3D11.VertexBufferBinding[] nativeVertexBufferBindings = new SharpDX.Direct3D11.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                VertexBuffer_DX11 nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as VertexBuffer_DX11;

                if (nativeVertexBuffer != null)
                {
                    nativeVertexBufferBindings[i] = new SharpDX.Direct3D11.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride, anxVertexBufferBinding.VertexOffset);
                }
                else
                {
                    throw new Exception("couldn't fetch native DirectX10 VertexBuffer");
                }
            }

            deviceContext.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);
        }

        public void SetViewport(ANX.Framework.Graphics.Viewport viewport)
        {
            this.currentViewport = new SharpDX.Direct3D11.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
        }

        /// <summary>
        /// This method creates a InputLayout which is needed by DirectX 10 for rendering primitives. The VertexDeclaration of ANX/XNA needs to be mapped
        /// to the DirectX 10 types. This is what this method is for.
        /// </summary>
        private InputLayout CreateInputLayout(Device device, ShaderBytecode passSignature, VertexDeclaration vertexDeclaration)
        {
            VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();
            int elementCount = vertexElements.Length;
            InputElement[] inputElements = new InputElement[elementCount];

            for (int i = 0; i < elementCount; i++)
            {
                inputElements[i] = CreateInputElementFromVertexElement(vertexElements[i]);
            }

            // Layout from VertexShader input signature
            return new InputLayout(device, passSignature, inputElements);
        }

        private InputElement CreateInputElementFromVertexElement(VertexElement vertexElement)
        {
            string elementName = FormatConverter.Translate(vertexElement.VertexElementUsage);

            Format elementFormat;
            switch (vertexElement.VertexElementFormat)
            {
                case VertexElementFormat.Vector2:
                    elementFormat = Format.R32G32_Float;
                    break;
                case VertexElementFormat.Vector3:
                    elementFormat = Format.R32G32B32_Float;
                    break;
                case VertexElementFormat.Vector4:
                    elementFormat = Format.R32G32B32A32_Float;
                    break;
                case VertexElementFormat.Color:
                    elementFormat = Format.R8G8B8A8_UNorm;
                    break;
                default:
                    throw new Exception("can't map '" + vertexElement.VertexElementFormat.ToString() + "' to DXGI.Format in DirectX10 RenderSystem CreateInputElementFromVertexElement");
            }

            return new InputElement(elementName, vertexElement.UsageIndex, elementFormat, vertexElement.Offset, 0);
        }

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
                deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
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

                        renderTargetView[i] = new RenderTargetView(deviceContext.Device, ((Texture2D_DX11)nativeRenderTarget).NativeShaderResourceView.Resource);
                    }
                }

                //deviceContext.OutputMerger.SetRenderTargets(renderTargetCount, renderTargetView, this.depthStencilView);
                deviceContext.OutputMerger.SetTargets(this.depthStencilView, this.renderTargetView);
            }
        }

        public void GetBackBufferData<T>(ANX.Framework.Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
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

        public void ResizeBuffers(PresentationParameters presentationParameters)
        {
            if (swapChain != null)
            {
                renderView.Dispose();
                backBuffer.Dispose();

                //TODO: handle format

                swapChain.ResizeBuffers(swapChain.Description.BufferCount, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, Format.R8G8B8A8_UNorm, swapChain.Description.Flags);

                backBuffer = SharpDX.Direct3D11.Texture2D.FromSwapChain<SharpDX.Direct3D11.Texture2D>(swapChain, 0);
                renderView = new RenderTargetView(deviceContext.Device, backBuffer);

                currentViewport = new SharpDX.Direct3D11.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);

                //
                // create the depth stencil buffer
                //
                Format depthFormat = FormatConverter.Translate(presentationParameters.DepthStencilFormat);
                if (depthFormat != Format.Unknown)
                {
                    CreateDepthStencilBuffer(depthFormat);
                }
            }

            ResizeRenderWindow(presentationParameters);
        }

        private void ResizeRenderWindow(PresentationParameters presentationParameters)
        {
            RECT windowRect;
            RECT clientRect;
            if (GetWindowRect(presentationParameters.DeviceWindowHandle, out windowRect) &&
                GetClientRect(presentationParameters.DeviceWindowHandle, out clientRect))
            {
                int width = presentationParameters.BackBufferWidth + ((windowRect.Right - windowRect.Left) - clientRect.Right);
                int height = presentationParameters.BackBufferHeight + ((windowRect.Bottom - windowRect.Top) - clientRect.Bottom);

                SetWindowPos(presentationParameters.DeviceWindowHandle, IntPtr.Zero, windowRect.Left, windowRect.Top, width, height, 0);
            }
        }

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
    }
}
