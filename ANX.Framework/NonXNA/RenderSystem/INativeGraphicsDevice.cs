using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public interface INativeGraphicsDevice : IDisposable
	{
		void Clear(ClearOptions options, Vector4 color, float depth, int stencil);

		void Present();

		void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount, IndexBuffer indexBuffer);

		void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
			int startIndex, int primitiveCount, int instanceCount, IndexBuffer indexBuffer);

		void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
			Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType;

		void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount,
			VertexDeclaration vertexDeclaration) where T : struct, IVertexType;

		void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount);

#if XNAEXT
		void SetConstantBuffer(int slot, ConstantBuffer constantBuffer);
#endif

		void SetVertexBuffers(VertexBufferBinding[] vertexBuffers);

		void SetIndexBuffer(IndexBuffer indexBuffer);

		void SetViewport(Viewport viewport);

		void SetRenderTargets(params RenderTargetBinding[] renderTargets);

		void GetBackBufferData<T>(Nullable<Rectangle> rect, T[] data, int startIndex, int elementCount) where T : struct;
		void GetBackBufferData<T>(T[] data) where T : struct;
		void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct;

		void ResizeBuffers(PresentationParameters presentationParameters);

		bool VSync { get; set; }
        Rectangle ScissorRectangle { get; set; }
	}
}
