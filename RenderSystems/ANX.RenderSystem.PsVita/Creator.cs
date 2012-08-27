using System;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class Creator : IRenderSystemCreator
	{
		#region Public
		public string Name
		{
			get
			{
				return "RenderSystem.PsVita";
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
				return OSInformation.GetName() == PlatformName.PSVita;
			}
		}
		#endregion

		#region CreateGraphicsDevice
		public INativeGraphicsDevice CreateGraphicsDevice(
			PresentationParameters presentationParameters)
		{
			return new PsVitaGraphicsDevice(presentationParameters);
		}
		#endregion

		#region CreateIndexBuffer
		public INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics,
			IndexBuffer managedBuffer, IndexElementSize size, int indexCount,
			BufferUsage usage)
		{
			return new PsVitaIndexBuffer(managedBuffer, size, indexCount, usage);
		}
		#endregion

		#region CreateVertexBuffer
		public INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics,
			VertexBuffer managedBuffer, VertexDeclaration vertexDeclaration,
			int vertexCount, BufferUsage usage)
		{
			return new PsVitaVertexBuffer(managedBuffer, vertexDeclaration,
				vertexCount, usage);
		}
		#endregion

		#region CreateTexture
		public INativeTexture2D CreateTexture(GraphicsDevice graphics,
			SurfaceFormat surfaceFormat, int width, int height, int mipCount)
		{
			return new PsVitaTexture2D(surfaceFormat, width, height, mipCount);
		}
		#endregion

		#region CreateRenderTarget
		public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics,
			int width, int height, bool mipMap, SurfaceFormat preferredFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount,
			RenderTargetUsage usage)
		{
			return new PsVitaRenderTarget2D();
		}
		#endregion

		#region CreateEffect
		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream byteCode)
		{
			return new PsVitaEffect(managedEffect, byteCode);
		}
		
		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			return new PsVitaEffect(managedEffect, vertexShaderByteCode,
				pixelShaderByteCode);
		}
		#endregion

		#region CreateBlendState
		public INativeBlendState CreateBlendState()
		{
			return new PsVitaBlendState();
		}
		#endregion

		#region CreateRasterizerState
		public INativeRasterizerState CreateRasterizerState()
		{
			return new PsVitaRasterizerState();
		}
		#endregion

		#region CreateDepthStencilState
		public INativeDepthStencilState CreateDepthStencilState()
		{
			return new PsVitaDepthStencilState();
		}
		#endregion

		#region CreateSamplerState
		public INativeSamplerState CreateSamplerState()
		{
			return new PsVitaSamplerState();
		}
		#endregion

		public byte[] GetShaderByteCode(PreDefinedShader type)
		{
			throw new NotImplementedException();
		}

		public ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
		{
			throw new NotImplementedException();
		}

        public bool IsLanguageSupported(EffectSourceLanguage sourceLanguage)
        {
            //TODO: implement supported sourceLanguages
            return false;
        }

	}
}
