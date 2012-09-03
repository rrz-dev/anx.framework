#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;
using SharpDX.DXGI;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class Creator : IRenderSystemCreator
	{
		public string Name
		{
			get { return "DirectX10"; }
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
				return OSInformation.IsWindows;
			}
		}

		public INativeGraphicsDevice CreateGraphicsDevice(PresentationParameters presentationParameters)
		{
			PreventSystemChange();
			return new GraphicsDeviceWindowsDX10(presentationParameters);
		}

		public INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics, IndexBuffer managedBuffer, IndexElementSize size,
			int indexCount, BufferUsage usage)
		{
			PreventSystemChange();
			return new IndexBuffer_DX10(graphics, size, indexCount, usage);
		}

		public INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics, VertexBuffer managedBuffer,
			VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			PreventSystemChange();

            return new VertexBuffer_DX10(graphics, vertexDeclaration, vertexCount, usage);
		}

#if XNAEXT
        #region CreateConstantBuffer
        public INativeConstantBuffer CreateConstantBuffer(GraphicsDevice graphics, ConstantBuffer managedBuffer, BufferUsage usage)
        {
            PreventSystemChange();

            throw new NotImplementedException();
        }
        #endregion
#endif

        public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect, Stream vertexShaderByteCode,
			Stream pixelShaderByteCode)
		{
			PreventSystemChange();

			return new Effect_DX10(graphics, managedEffect, vertexShaderByteCode, pixelShaderByteCode);
		}

		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect, Stream byteCode)
		{
			PreventSystemChange();

			return new Effect_DX10(graphics, managedEffect, byteCode);
		}

		public Texture2D CreateTexture(GraphicsDevice graphics, string fileName)
		{
			PreventSystemChange();

			//TODO: implement
			throw new NotImplementedException();

			//GraphicsDeviceWindowsDX10 graphicsDX10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
			//SharpDX.Direct3D10.Texture2D nativeTexture = SharpDX.Direct3D10.Texture2D.FromFile<SharpDX.Direct3D10.Texture2D>(graphicsDX10.NativeDevice, fileName);
			//Texture2D_DX10 texture = new Texture2D_DX10(graphics, nativeTexture.Description.Width, nativeTexture.Description.Height, FormatConverter.Translate(nativeTexture.Description.Format), nativeTexture.Description.MipLevels);
			//texture.NativeTexture = nativeTexture;

			//return texture;
		}

		public INativeBlendState CreateBlendState()
		{
			PreventSystemChange();
			return new BlendState_DX10();
		}

		public INativeRasterizerState CreateRasterizerState()
		{
			PreventSystemChange();
			return new RasterizerState_DX10();
		}

		public INativeDepthStencilState CreateDepthStencilState()
		{
			PreventSystemChange();
			return new DepthStencilState_DX10();
		}

		public INativeSamplerState CreateSamplerState()
		{
			PreventSystemChange();
			return new SamplerState_DX10();
		}

		public byte[] GetShaderByteCode(PreDefinedShader type)
		{
			PreventSystemChange();

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

			throw new NotImplementedException("ByteCode for '" + type + "' is not yet available");
		}

        public EffectSourceLanguage GetStockShaderSourceLanguage
        {
            get
            {
                return EffectSourceLanguage.HLSL_FX;
            }
        }

		public ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
		{
			PreventSystemChange();

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

					using (Output adapterOutput = adapter.Outputs[0])
					{
						var modeList = adapterOutput.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);
						foreach (ModeDescription modeDescription in modeList)
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

			return new ReadOnlyCollection<GraphicsAdapter>(adapterList);
		}

		public INativeTexture2D CreateTexture(GraphicsDevice graphics, SurfaceFormat surfaceFormat, int width, int height,
			int mipCount)
		{
			PreventSystemChange();

			return new Texture2D_DX10(graphics, width, height, surfaceFormat, mipCount);
		}

		public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics, int width, int height, bool mipMap,
			SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
		{
			PreventSystemChange();

			return new RenderTarget2D_DX10(graphics, width, height, mipMap, preferredFormat, preferredDepthFormat,
				preferredMultiSampleCount, usage);
		}

		#region PreventSystemChange
		private void PreventSystemChange()
		{
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.RenderSystem);
		}
		#endregion

        public bool IsLanguageSupported(EffectSourceLanguage sourceLanguage)
        {
            return sourceLanguage == EffectSourceLanguage.HLSL_FX || sourceLanguage == EffectSourceLanguage.HLSL;
        }

		#region CreateOcclusionQuery (TODO)
		public IOcclusionQuery CreateOcclusionQuery()
		{
			throw new NotImplementedException();
		}
		#endregion
    }
}
