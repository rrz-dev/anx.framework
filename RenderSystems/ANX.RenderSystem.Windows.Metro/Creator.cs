using System;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class Creator : IRenderSystemCreator
	{
		#region Public
		public string Name
		{
			get
			{
				return "Metro";
			}
		}

		public int Priority
		{
			get
			{
				return 10;
			}
		}

		public bool IsSupported
		{
			get
			{
				return OSInformation.GetName() == PlatformName.Windows8;
			}
		}
		#endregion

		#region CreateGraphicsDevice
		public INativeGraphicsDevice CreateGraphicsDevice(
			PresentationParameters presentationParameters)
		{
			return new GraphicsDeviceWindowsMetro(presentationParameters);
		}
		#endregion

		#region CreateIndexBuffer
		public INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics,
			IndexBuffer managedBuffer, IndexElementSize size, int indexCount,
			BufferUsage usage)
		{
			return new IndexBuffer_Metro(graphics, size, indexCount, usage);
		}
		#endregion

		#region CreateVertexBuffer
		public INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics,
			VertexBuffer managedBuffer, VertexDeclaration vertexDeclaration,
			int vertexCount, BufferUsage usage)
		{
			return new VertexBuffer_Metro(graphics, vertexDeclaration, vertexCount, usage);
		}
		#endregion

#if XNAEXT
        #region CreateConstantBuffer
        public INativeConstantBuffer CreateConstantBuffer(GraphicsDevice graphics, ConstantBuffer managedBuffer, BufferUsage usage)
        {
            throw new NotImplementedException();
        }
        #endregion
#endif

        #region CreateEffect
        public INativeEffect CreateEffect(GraphicsDevice graphics,
			Effect managedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			return new Effect_Metro(graphics, managedEffect,
				vertexShaderByteCode, pixelShaderByteCode);
		}
		#endregion

		#region CreateEffect
		public INativeEffect CreateEffect(GraphicsDevice graphics,
			Effect managedEffect, System.IO.Stream byteCode)
		{
			return new Effect_Metro(graphics, managedEffect, byteCode);
		}
		#endregion

		#region CreateTexture (TODO)
		public Texture2D CreateTexture(GraphicsDevice graphics, string fileName)
		{
			//TODO: implement
			throw new NotImplementedException();

			//GraphicsDeviceWindowsDX10 graphicsDX10 = graphics.NativeDevice as GraphicsDeviceWindowsDX10;
			//Dx11.Texture2D nativeTexture = Dx11.Texture2D.FromFile<Dx11.Texture2D>(graphicsDX10.NativeDevice, fileName);
			//Texture2D_DX10 texture = new Texture2D_DX10(graphics, nativeTexture.Description.Width, nativeTexture.Description.Height, FormatConverter.Translate(nativeTexture.Description.Format), nativeTexture.Description.MipLevels);
			//texture.NativeTexture = nativeTexture;

			//return texture;
		}
		#endregion

		#region CreateTexture
		public INativeTexture2D CreateTexture(GraphicsDevice graphics,
			SurfaceFormat surfaceFormat, int width, int height, int mipCount)
		{
			return new Texture2D_Metro(graphics, width, height, surfaceFormat, mipCount);
		}
		#endregion

		#region CreateBlendState
		public INativeBlendState CreateBlendState()
		{
			return new BlendState_Metro();
		}
		#endregion

		#region CreateRasterizerState
		public INativeRasterizerState CreateRasterizerState()
		{
			return new RasterizerState_Metro();
		}
		#endregion

		#region CreateDepthStencilState
		public INativeDepthStencilState CreateDepthStencilState()
		{
			return new DepthStencilState_Metro();
		}
		#endregion

		#region CreateSamplerState
		public INativeSamplerState CreateSamplerState()
		{
			return new SamplerState_Metro();
		}
		#endregion

		#region GetShaderByteCode
		public byte[] GetShaderByteCode(PreDefinedShader type)
		{
			if (type == PreDefinedShader.SpriteBatch)
			{
				return ShaderByteCode.SpriteBatchByteCode;
			}
			else if (type == PreDefinedShader.DualTextureEffect)
			{
				return ShaderByteCode.DualTextureEffectByteCode;
			}
			else if (type == PreDefinedShader.AlphaTestEffect)
			{
				return ShaderByteCode.AlphaTestEffectByteCode;
			}
			else if (type == PreDefinedShader.EnvironmentMapEffect)
			{
				return ShaderByteCode.EnvironmentMapEffectByteCode;
			}
			/* TODO
			else if (type == PreDefinedShader.BasicEffect)
			{
				return ShaderByteCode.BasicEffectByteCode;
			}
			else if (type == PreDefinedShader.SkinnedEffect)
			{
				return ShaderByteCode.SkinnedEffectByteCode;
			}
			*/
			throw new NotImplementedException("ByteCode for '" + type + "' is not yet available");
		}
		#endregion

        public EffectSourceLanguage GetStockShaderSourceLanguage
        {
            get
            {
                return EffectSourceLanguage.HLSL_FX;
            }
        }

		#region RegisterCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			factory.AddCreator(this);
		}
		#endregion

		#region GetAdapterList (TODO)
		public ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
		{
			return new ReadOnlyCollection<GraphicsAdapter>(new GraphicsAdapter[]
			{
				new GraphicsAdapter()
				{
					IsDefaultAdapter = true
				}
			});
			/*
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

				return new System.Collections.ObjectModel.ReadOnlyCollection<GraphicsAdapter>(adapterList);*/
		}
		#endregion

		#region CreateRenderTarget
		public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics,
			int width, int height, bool mipMap, SurfaceFormat preferredFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
		{
			return new RenderTarget2D_Metro(graphics, width, height, mipMap,
				preferredFormat, preferredDepthFormat, preferredMultiSampleCount, usage);
		}
		#endregion

		#region CreateOcclusionQuery (TODO)
		public IOcclusionQuery CreateOcclusionQuery()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region IsLanguageSupported
		public bool IsLanguageSupported(EffectSourceLanguage sourceLanguage)
		{
			return sourceLanguage == EffectSourceLanguage.HLSL_FX || sourceLanguage == EffectSourceLanguage.HLSL;
		}
		#endregion

		#region SetTextureSampler (TODO)
		public void SetTextureSampler(int index, Texture value)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
