//#define DIRECTX_DEBUG_LAYER

#region Using Statements
using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using SharpDX.DXGI;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
using SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
using SharpDX.Direct3D11;

namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public partial class GraphicsDeviceDX
	{
		#region Private
		protected SharpDX.DXGI.SwapChain swapChain;
        protected Framework.Graphics.VertexBufferBinding[] currentVertexBuffer = new Framework.Graphics.VertexBufferBinding[0];
        protected int currentVertexBufferCount;
		protected IndexBuffer currentIndexBuffer;
        private InputLayoutManager inputLayoutManager = new InputLayoutManager();
        private InputLayout currentInputLayout = null;
		#endregion

		#region Public
		public bool VSync { get; set; }

		#endregion

		#region Constructor
		public GraphicsDeviceDX(PresentationParameters presentationParameters)
		{
			VSync = true;

			CreateDevice(presentationParameters);
			MakeWindowAssociationAndResize(presentationParameters);
			CreateRenderView(presentationParameters);
		}
		#endregion

		#region MakeWindowAssociationAndResize
		protected void MakeWindowAssociationAndResize(PresentationParameters presentationParameters)
		{
			// Ignore all windows events
			var factory = swapChain.GetParent<SharpDX.DXGI.Factory>();
			factory.MakeWindowAssociation(presentationParameters.DeviceWindowHandle,
				SharpDX.DXGI.WindowAssociationFlags.IgnoreAll);

			WindowHelper.ResizeRenderWindow(presentationParameters);

			SetViewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, 0f, 1f);
		}
		#endregion

		#region SetViewport
		public void SetViewport(Viewport viewport)
		{
			SetViewport(viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth);
		}

		#endregion

		#region ResizeBuffers
		public void ResizeBuffers(PresentationParameters presentationParameters)
		{
			if (swapChain != null)
			{
				DisposeBackBuffer();

				Format colorFormat = DxFormatConverter.Translate(presentationParameters.BackBufferFormat);
				swapChain.ResizeBuffers(swapChain.Description.BufferCount, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight, colorFormat, swapChain.Description.Flags);

				CreateRenderView(presentationParameters);

				SetViewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, 0f, 1f);
			}

			WindowHelper.ResizeRenderWindow(presentationParameters);
		}
		#endregion

        public void DrawIndexedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices, int startIndex, int primitiveCount)
        {
            if (primitiveCount <= 0) throw new ArgumentOutOfRangeException("primitiveCount is less than or equal to zero. When drawing, at least one primitive must be drawn.");
            if (this.currentVertexBufferCount == 0) throw new InvalidOperationException("you have to set a valid vertex buffer before drawing.");

            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            SetupDraw(primitiveType);

            nativeDevice.DrawIndexed(vertexCount, startIndex, baseVertex);
        }

        public void DrawPrimitives(PrimitiveType primitiveType, int vertexOffset, int primitiveCount)
        {
            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            SetupDraw(primitiveType);

            nativeDevice.Draw(vertexCount, vertexOffset);
        }

        public void DrawInstancedPrimitives(PrimitiveType primitiveType, int baseVertex, int minVertexIndex, int numVertices,
            int startIndex, int primitiveCount, int instanceCount)
        {
            int vertexCount = DxFormatConverter.CalculateVertexCount(primitiveType, primitiveCount);

            SetupDraw(primitiveType);

            nativeDevice.DrawIndexedInstanced(vertexCount, instanceCount, startIndex, baseVertex, 0);
        }

        public void DrawUserIndexedPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int numVertices,
            Array indexData, int indexOffset, int primitiveCount, VertexDeclaration vertexDeclaration,
            IndexElementSize indexFormat) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            int indexCount = indexData.Length;

            using (var vertexBuffer = new DynamicVertexBuffer(vertexDeclaration.GraphicsDevice, vertexDeclaration, vertexCount, BufferUsage.WriteOnly))
            using (var indexBuffer = new DynamicIndexBuffer(vertexDeclaration.GraphicsDevice, indexFormat, indexCount, BufferUsage.WriteOnly))
            {
                vertexBuffer.SetData(vertexData);
                this.SetVertexBuffers(new[] { new Framework.Graphics.VertexBufferBinding(vertexBuffer, vertexOffset) });

                if (indexData.GetType() == typeof(Int16[]))
                {
                    indexBuffer.SetData<short>((short[])indexData);
                }
                else
                {
                    indexBuffer.SetData<int>((int[])indexData);
                }
                this.IndexBuffer = indexBuffer;

                DrawIndexedPrimitives(primitiveType, 0, vertexOffset, numVertices, indexOffset, primitiveCount);
            }
        }

        public void DrawUserPrimitives<T>(PrimitiveType primitiveType, T[] vertexData, int vertexOffset, int primitiveCount, VertexDeclaration vertexDeclaration) where T : struct, IVertexType
        {
            int vertexCount = vertexData.Length;
            //TODO: use a shared vertexBuffer, instead of creating one on every call.
            using (var vertexBuffer = new DynamicVertexBuffer(vertexDeclaration.GraphicsDevice, vertexDeclaration, vertexCount, BufferUsage.WriteOnly))
            {
                vertexBuffer.SetData(vertexData);
                this.SetVertexBuffers(new[] { new Framework.Graphics.VertexBufferBinding(vertexBuffer, vertexOffset) });

                SetupDraw(primitiveType);

                nativeDevice.Draw(vertexCount, vertexOffset);
            }
        }

		#region GetBackBufferData
		public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
            this.backBuffer.GetData(0, rect, data, startIndex, elementCount);
		}

		public void GetBackBufferData<T>(T[] data) where T : struct
		{
            this.backBuffer.GetData(data);
		}

		public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
            this.backBuffer.GetData(data, startIndex, elementCount);
		}
		#endregion

        public IndexBuffer IndexBuffer
        {
            get
            {
                return this.currentIndexBuffer;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("indexBuffer");

                if (this.currentIndexBuffer != value)
                {
                    this.currentIndexBuffer = value;

                    DxIndexBuffer nativeIndexBuffer = value.NativeIndexBuffer as DxIndexBuffer;

                    if (nativeIndexBuffer != null)
                    {
                        nativeDevice.InputAssembler.SetIndexBuffer(nativeIndexBuffer.NativeBuffer, DxFormatConverter.Translate(value.IndexElementSize), 0);
                    }
                    else
                    {
                        throw new Exception("couldn't fetch native DirectX10 IndexBuffer");
                    }
                }
            }
        }

#if XNAEXT
		#region SetConstantBuffer (TODO)
        public void SetConstantBuffer(int slot, ConstantBuffer constantBuffer)
        {
            if (constantBuffer == null)
                throw new ArgumentNullException("constantBuffer");

            throw new NotImplementedException();
        }
		#endregion
#endif

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool diposeManaged)
        {
            if (swapChain != null)
            {
                DisposeBackBuffer();

                swapChain.Dispose();
                swapChain = null;
            }

            if (inputLayoutManager != null)
            {
                inputLayoutManager.Dispose();
                inputLayoutManager = null;
            }
            //TODO: dispose everything else
        }
	}
}
