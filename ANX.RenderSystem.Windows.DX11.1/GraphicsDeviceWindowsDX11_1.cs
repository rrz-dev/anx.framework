#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using SharpDX.Direct3D11;
using ANX.Framework;
using ANX.Framework.Graphics;
using SharpDX.D3DCompiler;

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

using Device = SharpDX.Direct3D11.Device;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace ANX.RenderSystem.Windows.DX11_1
{
    public class GraphicsDeviceWindowsDX11_1 : INativeGraphicsDevice
		{
        private Device device;
        private SwapChain swapChain; 
        private RenderTargetView renderView;
        private SharpDX.Direct3D11.Texture2D backBuffer;
        internal Effect_DX11_1 currentEffect;
        private VertexBuffer currentVertexBuffer;
        private IndexBuffer currentIndexBuffer;
        private SharpDX.Direct3D11.Viewport currentViewport;

        public GraphicsDeviceWindowsDX11_1(PresentationParameters presentationParameters)
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
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out device, out swapChain);
#endif

            // Ignore all windows events
            Factory factory = swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(presentationParameters.DeviceWindowHandle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            backBuffer = SharpDX.Direct3D11.Texture2D.FromSwapChain<SharpDX.Direct3D11.Texture2D>(swapChain, 0);
            renderView = new RenderTargetView(device, backBuffer);

            currentViewport = new SharpDX.Direct3D11.Viewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight);
        }

        public void Clear(ref Color color)
        {
            throw new NotImplementedException();
            //device.ClearRenderTargetView(renderView, new SharpDX.Color4(color.A / 255f, color.R / 255f, color.G / 255f, color.B / 255f));
        }

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

        private void SetupEffectForDraw(out SharpDX.Direct3D11.EffectPass pass, out SharpDX.Direct3D11.EffectTechnique technique, out ShaderBytecode passSignature)
        {
            // get the current effect
            //TODO: check for null and throw exception
            Effect_DX11_1 effect = this.currentEffect;

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

            throw new NotImplementedException();

            //device.InputAssembler.InputLayout = layout;
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
            SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            throw new NotImplementedException();

            // Prepare All the stages
            //device.InputAssembler.PrimitiveTopology = Translate(primitiveType);
            //device.Rasterizer.SetViewports(currentViewport);

            //device.OutputMerger.SetTargets(this.renderView);

            //for (int i = 0; i < technique.Description.PassCount; ++i)
            //{
            //    pass.Apply();
            //    device.DrawIndexed(CalculateVertexCount(primitiveType, primitiveCount), startIndex, baseVertex);
            //}
        }

        public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
        {
            SharpDX.Direct3D11.EffectPass pass; SharpDX.Direct3D11.EffectTechnique technique; ShaderBytecode passSignature;
            SetupEffectForDraw(out pass, out technique, out passSignature);

            SetupInputLayout(passSignature);

            throw new NotImplementedException();

            // Prepare All the stages
            //device.InputAssembler.PrimitiveTopology = Translate(primitiveType);
            //device.Rasterizer.SetViewports(currentViewport);
            //device.OutputMerger.SetTargets(this.renderView);

            //for (int i = 0; i < technique.Description.PassCount; ++i)
            //{
            //    pass.Apply();
            //    device.Draw(primitiveCount, vertexOffset);
            //}
        }

        public void SetIndexBuffer(IndexBuffer indexBuffer)
        {
            if (indexBuffer == null)
            {
                throw new ArgumentNullException("indexBuffer");
            }

            this.currentIndexBuffer = indexBuffer;

            throw new NotImplementedException();

            //IndexBuffer_DX11 nativeIndexBuffer = indexBuffer.NativeIndexBuffer as IndexBuffer_DX11;

            //if (nativeIndexBuffer != null)
            //{
            //    device.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer, Translate(indexBuffer.IndexElementSize), 0);
            //}
            //else
            //{
            //    throw new Exception("couldn't fetch native DirectX11.1 IndexBuffer");
            //}
        }

        public void SetVertexBuffers(ANX.Framework.Graphics.VertexBufferBinding[] vertexBuffers)
        {
            throw new NotImplementedException();
        }

        public void SetViewport(ANX.Framework.Graphics.Viewport viewport)
        {
            this.currentViewport = new SharpDX.Direct3D11.Viewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
        }

        public void ApplyStateObject(ANX.Framework.Graphics.BlendState blendState)
        {
            throw new NotImplementedException();

            //BlendStateDescription description = new BlendStateDescription();
            //description.AlphaBlendOperation = Translate(blendState.AlphaBlendFunction);
            //description.BlendOperation = Translate(blendState.ColorBlendFunction);
            //description.DestinationAlphaBlend = Translate(blendState.AlphaDestinationBlend);
            //description.DestinationBlend = Translate(blendState.ColorDestinationBlend);

            //for (int i = 0; i < 4; i++)
            //{
            //    description.IsBlendEnabled[i] = true;
            //}

            //description.RenderTargetWriteMask[0] = Translate(blendState.ColorWriteChannels);
            //description.RenderTargetWriteMask[1] = Translate(blendState.ColorWriteChannels1);
            //description.RenderTargetWriteMask[2] = Translate(blendState.ColorWriteChannels2);
            //description.RenderTargetWriteMask[3] = Translate(blendState.ColorWriteChannels3);

            //description.SourceAlphaBlend = Translate(blendState.AlphaSourceBlend);
            //description.SourceBlend = Translate(blendState.ColorSourceBlend);

            //SharpDX.Direct3D11.BlendState nativeBlendState = new SharpDX.Direct3D11.BlendState(device, description);

            //Vector4 tempVector = blendState.BlendFactor.ToVector4();
            //SharpDX.Color4 blendFactor = new Color4(tempVector.X, tempVector.Y, tempVector.Z, tempVector.W);
            //device.OutputMerger.SetBlendState(nativeBlendState, blendFactor, blendState.MultiSampleMask);
        }

        public void ApplyStateObject(ANX.Framework.Graphics.RasterizerState rasterizerState)
        {
            RasterizerStateDescription description = new RasterizerStateDescription();
            description.CullMode = Translate(rasterizerState.CullMode);
            description.DepthBias = (int)rasterizerState.DepthBias;     //TODO: this looks wrong!!!
            description.IsScissorEnabled = rasterizerState.ScissorTestEnable;
            description.SlopeScaledDepthBias = rasterizerState.SlopeScaleDepthBias;
            description.IsMultisampleEnabled = rasterizerState.MultiSampleAntiAlias;
            description.FillMode = Translate(rasterizerState.FillMode);
            
            description.IsAntialiasedLineEnabled = false;               //TODO: this should be ok

            SharpDX.Direct3D11.RasterizerState nativeRasterizerState = new SharpDX.Direct3D11.RasterizerState(device, description);

            //device.Rasterizer.State = nativeRasterizerState;
            throw new NotImplementedException();
        }

        public void ApplyStateObject(ANX.Framework.Graphics.DepthStencilState depthStencilState)
        {
            DepthStencilStateDescription description = new DepthStencilStateDescription();
            description.IsStencilEnabled = depthStencilState.StencilEnable;
            description.IsDepthEnabled = depthStencilState.DepthBufferEnable;
            description.DepthComparison = Translate(depthStencilState.DepthBufferFunction);

            //TODO: more to implement

            SharpDX.Direct3D11.DepthStencilState nativeDepthStencilState = new SharpDX.Direct3D11.DepthStencilState(device, description);
            //device.OutputMerger.SetDepthStencilState(nativeDepthStencilState, depthStencilState.ReferenceStencil);
            throw new NotImplementedException();
        }

        public void ApplyStateObject(int slot, ANX.Framework.Graphics.SamplerState samplerState)
        {
            SamplerStateDescription description = new SamplerStateDescription();
            description.AddressU = Translate(samplerState.AddressU);
            description.AddressV = Translate(samplerState.AddressV);
            description.AddressW = Translate(samplerState.AddressW);
            description.Filter = Translate(samplerState.Filter);
            description.MaximumAnisotropy = samplerState.MaxAnisotropy;
            description.MaximumLod = samplerState.MaxMipLevel;      //TODO: is this correct?
            description.MipLodBias = samplerState.MipMapLevelOfDetailBias;

            SharpDX.Direct3D11.SamplerState nativeSamplerState = new SharpDX.Direct3D11.SamplerState(device, description);
            //device.PixelShader.SetSampler(slot, nativeSamplerState);
            throw new NotImplementedException();
        }

        private Filter Translate(TextureFilter filter)
        {
            switch (filter)
            {
                case TextureFilter.Anisotropic:
                    return Filter.Anisotropic;
                case TextureFilter.Linear:
                    return Filter.MinMagMipLinear;
                case TextureFilter.LinearMipPoint:
                    return Filter.MinMagMipPoint;
                case TextureFilter.MinLinearMagPointMipLinear:
                    return Filter.MinLinearMagPointMipLinear;
                case TextureFilter.MinLinearMagPointMipPoint:
                    return Filter.MinLinearMagMipPoint;
                case TextureFilter.MinPointMagLinearMipLinear:
                    return Filter.MinPointMagMipLinear;
                case TextureFilter.MinPointMagLinearMipPoint:
                    return Filter.MinPointMagLinearMipPoint;
                case TextureFilter.Point:
                    return Filter.MinMagMipPoint;
                case TextureFilter.PointMipLinear:
                    return Filter.MinMagPointMipLinear;
            }

            throw new NotImplementedException();
        }

        private SharpDX.Direct3D11.TextureAddressMode Translate(ANX.Framework.Graphics.TextureAddressMode addressMode)
        {
            switch (addressMode)
            {
                case ANX.Framework.Graphics.TextureAddressMode.Clamp:
                    return SharpDX.Direct3D11.TextureAddressMode.Clamp;
                case ANX.Framework.Graphics.TextureAddressMode.Mirror:
                    return SharpDX.Direct3D11.TextureAddressMode.Mirror;
                case ANX.Framework.Graphics.TextureAddressMode.Wrap:
                    return SharpDX.Direct3D11.TextureAddressMode.Wrap;
            }

            return SharpDX.Direct3D11.TextureAddressMode.Clamp;
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
            string elementName = Translate(vertexElement.VertexElementUsage);

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

        private PrimitiveTopology Translate(PrimitiveType primitiveType)
        {
            switch (primitiveType)
            {
                case PrimitiveType.LineList:
                    return PrimitiveTopology.LineList;
                case PrimitiveType.LineStrip:
                    return PrimitiveTopology.LineStrip;
                case PrimitiveType.TriangleList:
                    return PrimitiveTopology.TriangleList;
                case PrimitiveType.TriangleStrip:
                    return PrimitiveTopology.TriangleStrip;
                default:
                    throw new InvalidOperationException("unknown PrimitiveType: " + primitiveType.ToString());
            }
        }

        private SharpDX.DXGI.Format Translate(IndexElementSize indexElementSize)
        {
            switch (indexElementSize)
            {
                case IndexElementSize.SixteenBits:
                    return Format.R16_UInt;
                case IndexElementSize.ThirtyTwoBits:
                    return Format.R32_UInt;
                default:
                    throw new InvalidOperationException("unknown IndexElementSize: " + indexElementSize.ToString());
            }
        }

        private string Translate(VertexElementUsage usage)
        {
            //TODO: map the other Usages
            if (usage == VertexElementUsage.TextureCoordinate)
            {
                return "TEXCOORD";
            }
            else
            {
                return usage.ToString().ToUpperInvariant();
            }
        }

        private ColorWriteMaskFlags Translate(ColorWriteChannels colorWriteChannels)
        {
            switch (colorWriteChannels)
            {
                case ColorWriteChannels.All:
                    return ColorWriteMaskFlags.All;
                case ColorWriteChannels.Alpha:
                    return ColorWriteMaskFlags.Alpha;
                case ColorWriteChannels.Blue:
                    return ColorWriteMaskFlags.Blue;
                case ColorWriteChannels.Green:
                    return ColorWriteMaskFlags.Green;
                case ColorWriteChannels.Red:
                    return ColorWriteMaskFlags.Red;
            }

            return 0;
        }

        private BlendOption Translate(Blend blend)
        {
            switch (blend)
            {
                case Blend.BlendFactor:
                    return BlendOption.BlendFactor;
                case Blend.DestinationAlpha:
                    return BlendOption.DestinationAlpha;
                case Blend.DestinationColor:
                    return BlendOption.DestinationColor;
                case Blend.InverseBlendFactor:
                    return BlendOption.InverseBlendFactor;
                case Blend.InverseDestinationAlpha:
                    return BlendOption.InverseDestinationAlpha;
                case Blend.InverseDestinationColor:
                    return BlendOption.InverseDestinationColor;
                case Blend.InverseSourceAlpha:
                    return BlendOption.InverseSourceAlpha;
                case Blend.InverseSourceColor:
                    return BlendOption.InverseSourceColor;
                case Blend.One:
                    return BlendOption.One;
                case Blend.SourceAlpha:
                    return BlendOption.SourceAlpha;
                case Blend.SourceAlphaSaturation:
                    return BlendOption.SourceAlphaSaturate;
                case Blend.SourceColor:
                    return BlendOption.SourceColor;
                case Blend.Zero:
                    return BlendOption.Zero;
            }

            return BlendOption.One;
        }

        private BlendOperation Translate(BlendFunction blendFunction)
        {
            switch (blendFunction)
            {
                case BlendFunction.Add:
                    return BlendOperation.Add;
                case BlendFunction.Max:
                    return BlendOperation.Maximum;
                case BlendFunction.Min:
                    return BlendOperation.Minimum;
                case BlendFunction.ReverseSubtract:
                    return BlendOperation.ReverseSubtract;
                case BlendFunction.Subtract:
                    return BlendOperation.Subtract;
            }

            return BlendOperation.Add;
        }

        private SharpDX.Direct3D11.FillMode Translate(ANX.Framework.Graphics.FillMode fillMode)
        {
            if (fillMode == ANX.Framework.Graphics.FillMode.WireFrame)
            {
                return SharpDX.Direct3D11.FillMode.Wireframe;
            }
            else
            {
                return SharpDX.Direct3D11.FillMode.Solid;
            }
        }

        private SharpDX.Direct3D11.CullMode Translate(ANX.Framework.Graphics.CullMode cullMode)
        {
            if (cullMode == ANX.Framework.Graphics.CullMode.CullClockwiseFace)
            {
                return SharpDX.Direct3D11.CullMode.Front;
            }
            else if (cullMode == ANX.Framework.Graphics.CullMode.CullCounterClockwiseFace)
            {
                return SharpDX.Direct3D11.CullMode.Back;
            }
            else
            {
                return SharpDX.Direct3D11.CullMode.None;
            }
        }

        private Comparison Translate(CompareFunction compareFunction)
        {
            switch (compareFunction)
            {
                case CompareFunction.Always:
                    return Comparison.Always;
                case CompareFunction.Equal:
                    return Comparison.Equal;
                case CompareFunction.Greater:
                    return Comparison.Greater;
                case CompareFunction.GreaterEqual:
                    return Comparison.GreaterEqual;
                case CompareFunction.Less:
                    return Comparison.Less;
                case CompareFunction.LessEqual:
                    return Comparison.LessEqual;
                case CompareFunction.Never:
                    return Comparison.Never;
                case CompareFunction.NotEqual:
                    return Comparison.NotEqual;
            }

            return Comparison.Always;
        }



        public void SetRenderTarget(Framework.Graphics.RenderTarget2D renderTarget)
        {
            throw new NotImplementedException();
        }

        public void SetRenderTarget(Framework.Graphics.RenderTargetCube renderTarget, Framework.Graphics.CubeMapFace cubeMapFace)
        {
            throw new NotImplementedException();
        }

        public void SetRenderTargets(params Framework.Graphics.RenderTargetBinding[] renderTargets)
        {
            throw new NotImplementedException();
        }
    }
}
