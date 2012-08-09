using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public interface IRenderSystemCreator : ICreator
	{
		INativeGraphicsDevice CreateGraphicsDevice(
			PresentationParameters presentationParameters);

		INativeTexture2D CreateTexture(GraphicsDevice graphics,
			SurfaceFormat surfaceFormat, int width, int height, int mipCount);

		INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics,
			int width, int height, bool mipMap, SurfaceFormat preferredFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount,
			RenderTargetUsage usage);

		INativeIndexBuffer CreateIndexBuffer(GraphicsDevice graphics,
			IndexBuffer managedBuffer, IndexElementSize size, int indexCount,
			BufferUsage usage);

		INativeVertexBuffer CreateVertexBuffer(GraphicsDevice graphics,
			VertexBuffer managedBuffer, VertexDeclaration vertexDeclaration,
			int vertexCount, BufferUsage usage);

		INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream byteCode);
		INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode);

		INativeBlendState CreateBlendState();
		INativeRasterizerState CreateRasterizerState();
		INativeDepthStencilState CreateDepthStencilState();
		INativeSamplerState CreateSamplerState();

		byte[] GetShaderByteCode(PreDefinedShader type);

		ReadOnlyCollection<GraphicsAdapter> GetAdapterList();
	}
}
