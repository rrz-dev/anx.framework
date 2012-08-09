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

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Windows.Metro
{
    public class Creator : IRenderSystemCreator
    {
        public string Name
        {
            get { return "Metro"; }
        }

        public int Priority
        {
            get { return 10; }
        }

        public bool IsSupported
        {
            get 
            {
                //TODO: this is just a very basic version of test for support
                //TODO: return AddInSystemFactory.Instance.OperatingSystem.Platform == PlatformID.Win32NT; 
                throw new NotImplementedException();
            }
        }
			
        public INativeGraphicsDevice CreateGraphicsDevice(PresentationParameters presentationParameters)
        {
            return new GraphicsDeviceWindowsMetro(presentationParameters);
        }

		public INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics,
			IndexBuffer managedBuffer, IndexElementSize size, int indexCount, BufferUsage usage)
        {
            return new IndexBuffer_Metro(graphics, size, indexCount, usage);
        }

				public INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics,
					VertexBuffer managedBuffer, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
        {
            return new VertexBuffer_Metro(graphics, vertexDeclaration, vertexCount, usage);
        }

        public INativeEffect CreateEffect(GraphicsDevice graphics, ANX.Framework.Graphics.Effect managedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
        {
            Effect_Metro effect = new Effect_Metro(graphics, managedEffect, vertexShaderByteCode, pixelShaderByteCode);

            return effect;
        }

        public INativeEffect CreateEffect(GraphicsDevice graphics, ANX.Framework.Graphics.Effect managedEffect, System.IO.Stream byteCode)
        {
            Effect_Metro effect = new Effect_Metro(graphics, managedEffect, byteCode);

            return effect;
        }

        public Texture2D CreateTexture(GraphicsDevice graphics, string fileName)
        {
            //TODO: implement
            throw new NotImplementedException();

            //GraphicsDeviceWindowsDX10 graphicsDX10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
            //SharpDX.Direct3D11.Texture2D nativeTexture = SharpDX.Direct3D11.Texture2D.FromFile<SharpDX.Direct3D11.Texture2D>(graphicsDX10.NativeDevice, fileName);
            //Texture2D_DX10 texture = new Texture2D_DX10(graphics, nativeTexture.Description.Width, nativeTexture.Description.Height, FormatConverter.Translate(nativeTexture.Description.Format), nativeTexture.Description.MipLevels);
            //texture.NativeTexture = nativeTexture;

            //return texture;
        }

        public INativeBlendState CreateBlendState()
        {
            return new BlendState_Metro();
        }

        public INativeRasterizerState CreateRasterizerState()
        {
            return new RasterizerState_Metro();
        }

        public INativeDepthStencilState CreateDepthStencilState()
        {
            return new DepthStencilState_Metro();
        }

        public INativeSamplerState CreateSamplerState()
        {
            return new SamplerState_Metro();
        }

        public byte[] GetShaderByteCode(PreDefinedShader type)
        {
            if (type == PreDefinedShader.SpriteBatch)
            {
                return ShaderByteCode.SpriteBatchByteCode;
            }
            else if (type == PreDefinedShader.AlphaTestEffect)
            {
                return ShaderByteCode.AlphaTestEffectByteCode;
            }
            else if (type == PreDefinedShader.BasicEffect)
            {
                return ShaderByteCode.BasicEffectByteCode;
            }
            else if (type == PreDefinedShader.DualTextureEffect)
            {
                return ShaderByteCode.DualTextureEffectByteCode;
            }
            else if (type == PreDefinedShader.EnvironmentMapEffect)
            {
                return ShaderByteCode.EnvironmentMapEffectByteCode;
            }
            else if (type == PreDefinedShader.SkinnedEffect)
            {
                return ShaderByteCode.SkinnedEffectByteCode;
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
            return new Texture2D_Metro(graphics, width, height, surfaceFormat, mipCount);
        }

        public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
        {
            return new RenderTarget2D_Metro(graphics, width, height, mipMap, preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage);
        }
    }
}
