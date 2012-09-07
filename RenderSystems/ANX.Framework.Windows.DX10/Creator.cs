using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;
using ANX.RenderSystem.Windows.DX10.Helpers;
using SharpDX.DXGI;
using ANX.BaseDirectX;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class Creator : IRenderSystemCreator
	{
		#region Public
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

		public EffectSourceLanguage GetStockShaderSourceLanguage
		{
			get
			{
				return EffectSourceLanguage.HLSL_FX;
			}
		}
		#endregion

		#region CreateGraphicsDevice
		public INativeGraphicsDevice CreateGraphicsDevice(PresentationParameters presentationParameters)
		{
			PreventSystemChange();
			return new GraphicsDeviceWindowsDX10(presentationParameters);
		}
		#endregion

		#region CreateIndexBuffer
		public INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics, IndexBuffer managedBuffer, IndexElementSize size,
			int indexCount, BufferUsage usage)
		{
			PreventSystemChange();
			return new IndexBuffer_DX10(graphics, size, indexCount, usage);
		}
		#endregion

		#region CreateVertexBuffer
		public INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics, VertexBuffer managedBuffer,
			VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage usage)
		{
			PreventSystemChange();
			return new VertexBuffer_DX10(graphics, vertexDeclaration, vertexCount, usage);
		}
		#endregion

#if XNAEXT
		#region CreateConstantBuffer
		public INativeConstantBuffer CreateConstantBuffer(GraphicsDevice graphics, ConstantBuffer managedBuffer, BufferUsage usage)
		{
			PreventSystemChange();

			throw new NotImplementedException();
		}
		#endregion
#endif

		#region CreateEffect
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
		#endregion

		#region CreateTexture
		public INativeTexture2D CreateTexture(GraphicsDevice graphics, SurfaceFormat surfaceFormat, int width, int height,
			int mipCount)
		{
			PreventSystemChange();
			return new Texture2D_DX10(graphics, width, height, surfaceFormat, mipCount);
		}
		#endregion

		#region CreateBlendState
		public INativeBlendState CreateBlendState()
		{
			PreventSystemChange();
			return new BlendState_DX10();
		}
		#endregion

		#region CreateRasterizerState
		public INativeRasterizerState CreateRasterizerState()
		{
			PreventSystemChange();
			return new RasterizerState_DX10();
		}
		#endregion

		#region CreateDepthStencilState
		public INativeDepthStencilState CreateDepthStencilState()
		{
			PreventSystemChange();
			return new DepthStencilState_DX10();
		}
		#endregion

		#region CreateSamplerState
		public INativeSamplerState CreateSamplerState()
		{
			PreventSystemChange();
			return new SamplerState_DX10();
		}
		#endregion

		#region GetShaderByteCode
		public byte[] GetShaderByteCode(PreDefinedShader type)
		{
			PreventSystemChange();
			switch (type)
			{
				case PreDefinedShader.SpriteBatch:
					return ShaderByteCode.SpriteBatchByteCode;
				case PreDefinedShader.AlphaTestEffect:
					return ShaderByteCode.AlphaTestEffectByteCode;
				case PreDefinedShader.BasicEffect:
					return ShaderByteCode.BasicEffectByteCode;
				case PreDefinedShader.DualTextureEffect:
					return ShaderByteCode.DualTextureEffectByteCode;
				case PreDefinedShader.EnvironmentMapEffect:
					return ShaderByteCode.EnvironmentMapEffectByteCode;
				case PreDefinedShader.SkinnedEffect:
					return ShaderByteCode.SkinnedEffectByteCode;
			}

			throw new NotImplementedException("ByteCode for '" + type + "' is not yet available");
		}
		#endregion

		#region GetAdapterList
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
					var ga = new GraphicsAdapter();
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
								Format = BaseFormatConverter.Translate(modeDescription.Format),
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
		#endregion

		#region CreateRenderTarget
		public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics, int width, int height, bool mipMap,
			SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount,
			RenderTargetUsage usage)
		{
			PreventSystemChange();
			return new RenderTarget2D_DX10(graphics, width, height, mipMap, preferredFormat, preferredDepthFormat,
				preferredMultiSampleCount, usage);
		}
		#endregion

		#region PreventSystemChange
		private void PreventSystemChange()
		{
			AddInSystemFactory.Instance.PreventSystemChange(AddInType.RenderSystem);
		}
		#endregion

		#region IsLanguageSupported
		public bool IsLanguageSupported(EffectSourceLanguage sourceLanguage)
		{
			return sourceLanguage == EffectSourceLanguage.HLSL_FX || sourceLanguage == EffectSourceLanguage.HLSL;
		}
		#endregion

		#region CreateOcclusionQuery (TODO)
		public IOcclusionQuery CreateOcclusionQuery()
		{
			PreventSystemChange();
			throw new NotImplementedException();
		}
		#endregion

		#region SetTextureSampler (TODO)
		public void SetTextureSampler(int index, Texture value)
		{
			PreventSystemChange();
			throw new NotImplementedException();
		}
		#endregion
	}
}
