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

		#region RegisterCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			factory.AddCreator(this);
		}
		#endregion

		#region IRenderSystemCreator Member

		public INativeGraphicsDevice CreateGraphicsDevice(
			PresentationParameters presentationParameters)
		{
			return new PsVitaGraphicsDevice(presentationParameters);
		}

		public INativeTexture2D CreateTexture(GraphicsDevice graphics,
			SurfaceFormat surfaceFormat, int width, int height, int mipCount)
		{
			throw new NotImplementedException();
		}

		public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics,
			int width, int height, bool mipMap, SurfaceFormat preferredFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount,
			RenderTargetUsage usage)
		{
			throw new NotImplementedException();
		}

		public INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics,
			IndexBuffer managedBuffer, IndexElementSize size, int indexCount,
			BufferUsage usage)
		{
			throw new NotImplementedException();
		}

		public INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics,
			VertexBuffer managedBuffer, VertexDeclaration vertexDeclaration,
			int vertexCount, BufferUsage usage)
		{
			throw new NotImplementedException();
		}

		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream byteCode)
		{
			throw new NotImplementedException();
		}

		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			throw new NotImplementedException();
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

		public ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
