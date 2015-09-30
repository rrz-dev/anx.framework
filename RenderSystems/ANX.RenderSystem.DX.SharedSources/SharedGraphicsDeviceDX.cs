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
namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public partial class GraphicsDeviceDX
	{
		#region Private
		protected SharpDX.DXGI.SwapChain swapChain;
		protected VertexBufferBinding[] currentVertexBuffer;
        protected int currentVertexBufferCount;
		protected IndexBuffer currentIndexBuffer;
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

		#region GetBackBufferData (TODO)
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
	}
}
