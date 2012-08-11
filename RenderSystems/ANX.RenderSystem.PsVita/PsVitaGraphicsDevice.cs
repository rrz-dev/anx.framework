using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Sce.PlayStation.Core.Graphics;

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaGraphicsDevice : INativeGraphicsDevice
	{
		private GraphicsContext context;
		private uint? lastClearColor;

		public bool VSync
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public PsVitaGraphicsDevice(PresentationParameters presentationParameters)
		{
			// TODO
			context = new GraphicsContext(presentationParameters.BackBufferWidth,
				presentationParameters.BackBufferHeight, PixelFormat.Rgba,
				PixelFormat.Depth24Stencil8, MultiSampleMode.None);
		}

		#region INativeGraphicsDevice Member

		#region Clear
		public void Clear(ref Color color)
		{
			uint newClearColor = color.PackedValue;
			if (lastClearColor.HasValue == false ||
				lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				context.SetClearColor(color.R, color.G, color.B, color.A);
			}

			context.Clear(ClearMask.Color | ClearMask.Depth);
		}

		public void Clear(ClearOptions options, Vector4 color, float depth, int stencil)
		{
			context.SetClearColor(color.X, color.Y, color.Z, color.W);

			ClearMask mask = ClearMask.None;
			if ((options | ClearOptions.Target) == options)
			{
				mask |= ClearMask.Color;
			}
			if ((options | ClearOptions.Stencil) == options)
			{
				mask |= ClearMask.Stencil;
			}
			if ((options | ClearOptions.DepthBuffer) == options)
			{
				mask |= ClearMask.Depth;
			}

			context.SetClearDepth(depth);
			context.SetClearStencil(stencil);
			context.Clear(mask);
		}
		#endregion

		#region Present
		public void Present()
		{
			context.SwapBuffers();
		}
		#endregion

		public void DrawIndexedPrimitives(PrimitiveType primitiveType,
			int baseVertex, int minVertexIndex, int numVertices, int startIndex,
			int primitiveCount)
		{
			throw new NotImplementedException();
		}

		public void DrawInstancedPrimitives(PrimitiveType primitiveType,
			int baseVertex, int minVertexIndex, int numVertices, int startIndex,
			int primitiveCount, int instanceCount)
		{
			throw new NotImplementedException();
		}

		public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType,
			T[] vertexData, int vertexOffset, int numVertices, Array indexData,
			int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
			IndexElementSize indexFormat) where T : struct, IVertexType
		{
			throw new NotImplementedException();
		}

		public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData,
			int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration)
			where T : struct, IVertexType
		{
			throw new NotImplementedException();
		}

		public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset,
			int primitiveCount)
		{
			throw new NotImplementedException();
		}

		public void SetVertexBuffers(VertexBufferBinding[] vertexBuffers)
		{
			throw new NotImplementedException();
		}

		public void SetIndexBuffer(IndexBuffer indexBuffer)
		{
			throw new NotImplementedException();
		}

		#region SetViewport
		public void SetViewport(Viewport viewport)
		{
			context.SetViewport(viewport.X, viewport.Y, viewport.Width, viewport.Height);
		}
		#endregion

		public void SetRenderTargets(params RenderTargetBinding[] renderTargets)
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex,
			int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount)
			where T : struct
		{
			throw new NotImplementedException();
		}

		public void ResizeBuffers(PresentationParameters presentationParameters)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			context.Dispose();
			context = null;
		}
		#endregion
	}
}
