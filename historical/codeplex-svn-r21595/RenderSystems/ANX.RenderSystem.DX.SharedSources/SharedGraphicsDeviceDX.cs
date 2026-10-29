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
		#region Constants
		protected const float ColorMultiplier = 1f / 255f;

		protected const bool IsDebugMode =
#if DIRECTX_DEBUG_LAYER
			true;
#else
			false;
#endif
		#endregion

		#region Private
		protected uint lastClearColor;
		protected SharpDX.Color4 clearColor;
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
			CreateRenderView();

			// create the depth stencil buffer
			SharpDX.DXGI.Format depthFormat = DxFormatConverter.Translate(presentationParameters.DepthStencilFormat);
			if (depthFormat != SharpDX.DXGI.Format.Unknown)
				CreateDepthStencilBuffer(depthFormat);
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

		#region UpdateClearColorIfNeeded
		protected void UpdateClearColorIfNeeded(ref Color color)
		{
			uint newClearColor = color.PackedValue;
			if (lastClearColor != newClearColor)
			{
				lastClearColor = newClearColor;
				clearColor.Red = color.R * ColorMultiplier;
				clearColor.Green = color.G * ColorMultiplier;
				clearColor.Blue = color.B * ColorMultiplier;
				clearColor.Alpha = color.A * ColorMultiplier;
			}
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
				DisposeRenderView();

				Format colorFormat = DxFormatConverter.Translate(presentationParameters.BackBufferFormat);
				swapChain.ResizeBuffers(swapChain.Description.BufferCount, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight, colorFormat, swapChain.Description.Flags);

				CreateRenderView();

				SetViewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, 0f, 1f);

				// create the depth stencil buffer
				Format depthFormat = DxFormatConverter.Translate(presentationParameters.DepthStencilFormat);
				if (depthFormat != Format.Unknown)
					CreateDepthStencilBuffer(depthFormat);
			}

			WindowHelper.ResizeRenderWindow(presentationParameters);
		}
		#endregion

        protected void CreateDepthStencilBuffer(Format depthFormat)
        {
            CreateDepthStencilBuffer(depthFormat, this.backBuffer.Description.Width, this.backBuffer.Description.Height, true);
        }

		#region GetBackBufferData (TODO)
		public void GetBackBufferData<T>(Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data) where T : struct
		{
			throw new NotImplementedException();
		}

		public void GetBackBufferData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			throw new NotImplementedException();
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
