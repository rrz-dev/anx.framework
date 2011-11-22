#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using System.IO;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;
using SharpDX.DXGI;
using ANX.Framework.NonXNA.RenderSystem;

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

namespace ANX.Framework.Windows.DX10
{
    public class Creator : IRenderSystemCreator
    {
        public string Name
        {
            get { return "DirectX10"; }
        }

        public GameHost CreateGameHost(Game game)
        {
            return new WindowsGameHost(game);
        }

        public INativeGraphicsDevice CreateGraphicsDevice(PresentationParameters presentationParameters)
        {
            return new GraphicsDeviceWindowsDX10(presentationParameters);
        }

        public INativeBuffer CreateIndexBuffer(GraphicsDevice graphics, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            return new IndexBuffer_DX10(graphics, size, indexCount, usage);
        }

        public INativeBuffer CreateVertexBuffer(GraphicsDevice graphics, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new VertexBuffer_DX10(graphics, vertexDeclaration, vertexCount, usage);
        }

        public INativeEffect CreateEffect(GraphicsDevice graphics, ANX.Framework.Graphics.Effect managedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
        {
            Effect_DX10 effect = new Effect_DX10(graphics, managedEffect, vertexShaderByteCode, pixelShaderByteCode);

            return effect;
        }

        public INativeEffect CreateEffect(GraphicsDevice graphics, ANX.Framework.Graphics.Effect managedEffect, System.IO.Stream byteCode)
        {
            Effect_DX10 effect = new Effect_DX10(graphics, managedEffect, byteCode);

            return effect;
        }

        public Texture2D CreateTexture(GraphicsDevice graphics, string fileName)
        {
            //TODO: implement
            throw new NotImplementedException();

            //GraphicsDeviceWindowsDX10 graphicsDX10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
            //SharpDX.Direct3D10.Texture2D nativeTexture = SharpDX.Direct3D10.Texture2D.FromFile<SharpDX.Direct3D10.Texture2D>(graphicsDX10.NativeDevice, fileName);
            //Texture2D_DX10 texture = new Texture2D_DX10(graphics, nativeTexture.Description.Width, nativeTexture.Description.Height);
            //texture.NativeTexture = nativeTexture;

            //return texture;
        }

        public INativeBlendState CreateBlendState()
        {
            return new BlendState_DX10();
        }

        public INativeRasterizerState CreateRasterizerState()
        {
            return new RasterizerState_DX10();
        }

        public INativeDepthStencilState CreateDepthStencilState()
        {
            return new DepthStencilState_DX10();
        }

        public INativeSamplerState CreateSamplerState()
        {
            return new SamplerState_DX10();
        }

        public byte[] GetShaderByteCode(PreDefinedShader type)
        {
            if (type == PreDefinedShader.SpriteBatch)
            {
                return ShaderByteCode.SpriteBatchByteCode;
            }

            throw new NotImplementedException("ByteCode for '" + type.ToString() + "' is not yet available");
        }

        public void RegisterCreator(AddInSystemFactory factory)
        {
            factory.AddCreator(this);
        }


        public System.Collections.ObjectModel.ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
        {
            SharpDX.DXGI.Factory factory = new Factory();

            List<GraphicsAdapter> adapterList = new List<GraphicsAdapter>();
            DisplayModeCollection displayModeCollection = new DisplayModeCollection();

            for (int i = 0; i < factory.GetAdapterCount(); i++)
            {
                using (Adapter adapter = factory.GetAdapter(i))
                {
                    GraphicsAdapter ga = new GraphicsAdapter();
                    //ga.CurrentDisplayMode = ;
                    //ga.Description = ;
                    ga.DeviceId = adapter.Description.DeviceId;
                    ga.DeviceName = adapter.Description.Description;
                    ga.IsDefaultAdapter = i == 0; //TODO: how to set default adapter?
                    //ga.IsWideScreen = ;
                    //ga.MonitorHandle = ;
                    ga.Revision = adapter.Description.Revision;
                    ga.SubSystemId = adapter.Description.SubsystemId;
                    //ga.SupportedDisplayModes = ;
                    ga.VendorId = adapter.Description.VendorId;

                    using (Output adapterOutput = adapter.GetOutput(0))
                    {
                        foreach (ModeDescription modeDescription in adapterOutput.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced))
                        {
                            DisplayMode displayMode = new DisplayMode()
                            {
                                Format = FormatConverter.Translate(modeDescription.Format),
                                Width = modeDescription.Width,
                                Height = modeDescription.Height,
                                AspectRatio = (float)modeDescription.Width / (float)modeDescription.Height,
                                TitleSafeArea = new Rectangle(0, 0, modeDescription.Width, modeDescription.Height), //TODO: calculate this for real
                            };

                            displayModeCollection[displayMode.Format] = new DisplayMode[] { displayMode };
                        }
                    }

                    ga.SupportedDisplayModes = displayModeCollection;

                    adapterList.Add(ga);
                }
            }

            factory.Dispose();

            return new System.Collections.ObjectModel.ReadOnlyCollection<GraphicsAdapter>(adapterList);
        }

        public INativeTexture2D CreateTexture(GraphicsDevice graphics, SurfaceFormat surfaceFormat, int width, int height, int mipCount)
        {
            return new Texture2D_DX10(graphics, width, height, surfaceFormat, mipCount);
        }
    }
}
