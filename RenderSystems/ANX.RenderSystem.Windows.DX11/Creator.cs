#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using System.IO;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;
using ANX.Framework;

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

namespace ANX.RenderSystem.Windows.DX11
{
    public class Creator : IRenderSystemCreator
    {

        public void RegisterCreator(AddInSystemFactory factory)
        {
            factory.AddCreator(this);
        }

        public string Name
        {
            get { return "DirectX11"; }
        }

        public GameHost CreateGameHost(Game game)
        {
            throw new NotImplementedException();
            //return new WindowsGameHost(game);
        }

        public INativeGraphicsDevice CreateGraphicsDevice(PresentationParameters presentationParameters)
        {
            throw new NotImplementedException();
            //return new GraphicsDeviceWindowsDX11_1(presentationParameters);
        }

        public INativeBuffer CreateIndexBuffer(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            throw new NotImplementedException();
            //return new IndexBuffer_DX 11(graphics, size, indexCount, usage);
        }

        public INativeBuffer CreateVertexBuffer(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new VertexBuffer_DX11(graphics, vertexDeclaration, vertexCount, usage);
        }

        public INativeEffect CreateEffect(GraphicsDevice graphics, Effect effect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
        {
            throw new NotImplementedException();
            //return new Effect_DX11(graphics, vertexShaderByteCode, pixelShaderByteCode);
        }

        public INativeEffect CreateEffect(GraphicsDevice graphics, Effect effect, System.IO.Stream byteCode)
        {
            throw new NotImplementedException();
            //return new Effect_DX11(graphics, byteCode);
        }

        public Texture2D CreateTexture(GraphicsDevice graphics, string fileName)
        {
            throw new NotImplementedException();

            //GraphicsDeviceWindowsDX10 graphicsDX10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
            //SharpDX.Direct3D10.Texture2D nativeTexture = SharpDX.Direct3D10.Texture2D.FromFile<SharpDX.Direct3D10.Texture2D>(graphicsDX10.NativeDevice, fileName);
            //Texture2D_DX10 texture = new Texture2D_DX10(graphics, nativeTexture.Description.Width, nativeTexture.Description.Height);
            //texture.NativeTexture = nativeTexture;

            //return texture;
        }

        public Texture2D CreateTexture(GraphicsDevice graphics, SurfaceFormat surfaceFormat, int width, int height, int mipCount, byte[] colorData)
        {
            throw new NotImplementedException();

/*
            if (mipCount > 1)
            {
                throw new Exception("creating textures with mip map not yet implemented");
            }

            GraphicsDeviceWindowsDX10 graphicsDX10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
            SharpDX.Direct3D10.Device device = graphicsDX10.NativeDevice;
            Texture2D_DX10 texture = new Texture2D_DX10(graphics, width, height);

            SharpDX.Direct3D10.Texture2DDescription description = new SharpDX.Direct3D10.Texture2DDescription()
            {
                Width = width,
                Height = height,
                MipLevels = mipCount,
                ArraySize = mipCount,
                Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.Direct3D10.ResourceUsage.Dynamic,
                BindFlags = SharpDX.Direct3D10.BindFlags.ShaderResource,
                CpuAccessFlags = SharpDX.Direct3D10.CpuAccessFlags.Write,
            };
            texture.NativeTexture = new SharpDX.Direct3D10.Texture2D(graphicsDX10.NativeDevice, description);

            // description of texture formats of DX10: http://msdn.microsoft.com/en-us/library/bb694531(v=VS.85).aspx
            // more helpfull information on DX10 textures: http://msdn.microsoft.com/en-us/library/windows/desktop/bb205131(v=vs.85).aspx

            if (surfaceFormat == SurfaceFormat.Color)
            {
                int subresource = SharpDX.Direct3D10.Texture2D.CalculateSubresourceIndex(0, 0, 1);
                SharpDX.DataRectangle rectangle = texture.NativeTexture.Map(subresource, SharpDX.Direct3D10.MapMode.WriteDiscard, SharpDX.Direct3D10.MapFlags.None);
                int rowPitch = rectangle.Pitch;

                unsafe
                {
                    byte* pTexels = (byte*)rectangle.DataPointer;
                    int srcIndex = 0;

                    for (int row = 0; row < height; row++)
                    {
                        int rowStart = row * rowPitch;

                        for (int col = 0; col < width; col++)
                        {
                            int colStart = col * 4;
                            pTexels[rowStart + colStart + 0] = colorData[srcIndex++];
                            pTexels[rowStart + colStart + 1] = colorData[srcIndex++];
                            pTexels[rowStart + colStart + 2] = colorData[srcIndex++];
                            pTexels[rowStart + colStart + 3] = colorData[srcIndex++];
                        }
                    }
                }

                texture.NativeTexture.Unmap(subresource);
            }
            else 
            {
                throw new Exception(string.Format("creating textures of format {0} not yet implemented...", surfaceFormat.ToString()));
            }

            return texture;
*/
        }


        public INativeBlendState CreateBlendState()
        {
            throw new NotImplementedException();
        }


        public INativeRasterizerState CreateRasterizerState()
        {
            throw new NotImplementedException();
        }


        public INativeDepthStencilState CreateDepthStencilState()
        {
            throw new NotImplementedException();
        }


        public INativeSamplerState CreateSamplerState()
        {
            throw new NotImplementedException();
        }


        public byte[] GetShaderByteCode(PreDefinedShader type)
        {
            throw new NotImplementedException();
        }


        public System.Collections.ObjectModel.ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
        {
            throw new NotImplementedException();
        }


        public Framework.NonXNA.RenderSystem.INativeTexture2D CreateTexture(GraphicsDevice graphics, SurfaceFormat surfaceFormat, int width, int height, int mipCount)
        {
            throw new NotImplementedException();
        }
    }
}
