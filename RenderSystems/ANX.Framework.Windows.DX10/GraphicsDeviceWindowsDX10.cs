#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.D3DCompiler;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;
using ANX.Framework.Graphics;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

using Device = SharpDX.Direct3D10.Device;
using Buffer = SharpDX.Direct3D10.Buffer;
using System.Runtime.InteropServices;

namespace ANX.Framework.Windows.DX10
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
        private RenderTargetView renderTargetView;
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

			this.device.ClearRenderTargetView(this.renderTargetView != null ? this.renderTargetView : this.renderView, this.clearColor);
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

                this.device.ClearRenderTargetView(this.renderTargetView != null ? this.renderTargetView : this.renderView, this.clearColor);
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
            throw new NotImplementedException();
        }

        #endregion // DrawInstancedPrimitives

        #region DrawUserIndexedPrimitives<T>
        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices, Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration, IndexElementSize indexFormat) where T : struct, IVertexType
        {
            throw new NotImplementedException();
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

            DrawPrimitives(primitiveType, vertexOffset, primitiveCount);
        }

        #endregion // DrawUserPrimitives<T>

        internal Device NativeDevice
        {
            get
            {
                return this.device;
            }
        }

        private void SetupEffectForDraw(out SharpDX.Direct3D10.EffectPass pass, out SharpDX.Direct3D10.EffectTechnique technique, out ShaderBytecode passSignature)
        {
            // get the current effect
            //TODO: check for null and throw exception
            Effect_DX10 effect = this.currentEffect;

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

        public void SetVertexBuffers(Graphics.VertexBufferBinding[] vertexBuffers)
        {
            if (vertexBuffers == null)
            {
                throw new ArgumentNullException("vertexBuffers");
            }

            this.currentVertexBuffer = vertexBuffers[0].VertexBuffer;   //TODO: hmmmmm, not nice :-)

            SharpDX.Direct3D10.VertexBufferBinding[] nativeVertexBufferBindings = new SharpDX.Direct3D10.VertexBufferBinding[vertexBuffers.Length];
            for (int i = 0; i < vertexBuffers.Length; i++)
            {
                ANX.Framework.Graphics.VertexBufferBinding anxVertexBufferBinding = vertexBuffers[i];
                VertexBuffer_DX10 nativeVertexBuffer = anxVertexBufferBinding.VertexBuffer.NativeVertexBuffer as VertexBuffer_DX10;

                if (nativeVertexBuffer != null)
                {
                    nativeVertexBufferBindings[i] = new SharpDX.Direct3D10.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, anxVertexBufferBinding.VertexBuffer.VertexDeclaration.VertexStride, anxVertexBufferBinding.VertexOffset);
                }
                else
                {
                    throw new Exception("couldn't fetch native DirectX10 VertexBuffer");
                }
            }

            device.InputAssembler.SetVertexBuffers(0, nativeVertexBufferBindings);
        }

        public void SetViewport(Graphics.Viewport viewport)
        {
            this.currentViewport = new SharpDX.Direct3D10.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
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
                if (renderTargetView != null)
                {
                    renderTargetView.Dispose();
                    renderTargetView = null;
                }

                device.OutputMerger.SetRenderTargets(1, new RenderTargetView[] { this.renderView }, this.depthStencilView);
                device.OutputMerger.SetTargets(this.depthStencilView, this.renderView);
            }
            else
            {
                if (renderTargets.Length == 1)
                {
                    RenderTarget2D renderTarget = renderTargets[0].RenderTarget as RenderTarget2D;
                    RenderTarget2D_DX10 nativeRenderTarget = renderTarget.NativeRenderTarget as RenderTarget2D_DX10;
                    if (renderTarget != null)
                    {
                        if (renderTargetView != null)
                        {
                            renderTargetView.Dispose();
                            renderTargetView = null;
                        }
                        this.renderTargetView = new RenderTargetView(device, ((Texture2D_DX10)nativeRenderTarget).NativeShaderResourceView.Resource);
                        DepthStencilView depthStencilView = null;
                        device.OutputMerger.SetRenderTargets(1,new RenderTargetView[] { this.renderTargetView }, depthStencilView);
                    }
                }
                else
                {
                    throw new NotImplementedException("handling of multiple RenderTargets are not yet implemented");
                }
            }
        }

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

        public void ResizeBuffers(PresentationParameters presentationParameters)
        {
            if (swapChain != null)
            {
                renderView.Dispose();
                backBuffer.Dispose();

                //TODO: handle format

                swapChain.ResizeBuffers(swapChain.Description.BufferCount, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, Format.R8G8B8A8_UNorm, (int)swapChain.Description.Flags);

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
            if (renderTargetView != null)
            {
                renderTargetView.Dispose();
                renderTargetView = null;
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
