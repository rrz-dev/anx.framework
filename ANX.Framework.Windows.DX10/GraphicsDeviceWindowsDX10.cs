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

namespace ANX.Framework.Windows.DX10
{
    public class GraphicsDeviceWindowsDX10 : INativeGraphicsDevice
		{
			#region Constants
			private const float ColorMultiplier = 1f / 255f;
			#endregion

        private Device device;
        private SwapChain swapChain; 
        private RenderTargetView renderView;
        private SharpDX.Direct3D10.Texture2D backBuffer;
        internal Effect_DX10 currentEffect;
        private VertexBuffer currentVertexBuffer;
        private IndexBuffer currentIndexBuffer;
        private SharpDX.Direct3D10.Viewport currentViewport;

        public GraphicsDeviceWindowsDX10(PresentationParameters presentationParameters)
        {
            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, new Rational(60, 1), Format.R8G8B8A8_UNorm),
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

            // New RenderTargetView from the backbuffer
            backBuffer = SharpDX.Direct3D10.Texture2D.FromSwapChain<SharpDX.Direct3D10.Texture2D>(swapChain, 0);
            renderView = new RenderTargetView(device, backBuffer);

            currentViewport = new SharpDX.Direct3D10.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);
        }

				#region Clear
				private uint lastClearColor;
				private SharpDX.Color4 clearColor;
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
					device.ClearRenderTargetView(renderView, clearColor);
				}
				#endregion

				public void Present()
        {
            swapChain.Present(0, PresentFlags.None);
        }

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

        public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
        {
            SharpDX.Direct3D10.EffectPass pass; SharpDX.Direct3D10.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            // Prepare All the stages
            device.InputAssembler.PrimitiveTopology = FormatConverter.Translate(primitiveType);
            device.Rasterizer.SetViewports(currentViewport);

            device.OutputMerger.SetTargets(this.renderView);

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
            device.OutputMerger.SetTargets(this.renderView);

            for (int i = 0; i < technique.Description.PassCount; ++i)
            {
                pass.Apply();
                device.Draw(primitiveCount, vertexOffset);
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

        public void SetVertexBuffer(VertexBuffer vertexBuffer, int vertexOffset)
        {
            if (vertexBuffer == null)
            {
                throw new ArgumentNullException("vertexBuffer");
            }

            this.currentVertexBuffer = vertexBuffer;

            VertexBuffer_DX10 nativeVertexBuffer = vertexBuffer.NativeVertexBuffer as VertexBuffer_DX10;

            if (nativeVertexBuffer != null)
            {
                device.InputAssembler.SetVertexBuffers(0, new SharpDX.Direct3D10.VertexBufferBinding(nativeVertexBuffer.NativeBuffer, vertexBuffer.VertexDeclaration.VertexStride, vertexOffset));
            }
            else
            {
                throw new Exception("couldn't fetch native DirectX10 VertexBuffer");
            }
        }

        public void SetVertexBuffer(VertexBuffer vertexBuffer)
        {
            SetVertexBuffer(vertexBuffer, 0);
        }

        public void SetVertexBuffers(Graphics.VertexBufferBinding[] vertexBuffers)
        {
            throw new NotImplementedException();            
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
    }
}
