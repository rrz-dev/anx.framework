//#define DIRECTX_DEBUG_LAYER
using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using SharpDX.DXGI;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.BaseDirectX
{
	public abstract class BaseGraphicsDevice<S>
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
		protected S nativeDevice;
		protected uint lastClearColor;
		protected SharpDX.Color4 clearColor;
		protected SharpDX.DXGI.SwapChain swapChain;
		protected VertexBuffer currentVertexBuffer;
		protected IndexBuffer currentIndexBuffer;
		#endregion

		#region Public
		public bool VSync { get; set; }

		public S NativeDevice
		{
			get
			{
				return nativeDevice;
			}
		}
		#endregion

		#region Constructor
		protected BaseGraphicsDevice(PresentationParameters presentationParameters)
		{
			VSync = true;

			CreateDevice(presentationParameters);
			MakeWindowAssociationAndResize(presentationParameters);
			CreateRenderView();

			// create the depth stencil buffer
			SharpDX.DXGI.Format depthFormat = BaseFormatConverter.Translate(presentationParameters.DepthStencilFormat);
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

		protected abstract void SetViewport(int x, int y, int width, int height, float minDepth, float maxDepth);
		#endregion

		#region ResizeBuffers
		public void ResizeBuffers(PresentationParameters presentationParameters)
		{
			if (swapChain != null)
			{
				DisposeRenderView();

				Format colorFormat = BaseFormatConverter.Translate(presentationParameters.BackBufferFormat);
				swapChain.ResizeBuffers(swapChain.Description.BufferCount, presentationParameters.BackBufferWidth,
					presentationParameters.BackBufferHeight, colorFormat, swapChain.Description.Flags);

				CreateRenderView();

				SetViewport(0, 0, presentationParameters.BackBufferWidth, presentationParameters.BackBufferHeight, 0f, 1f);

				// create the depth stencil buffer
				Format depthFormat = BaseFormatConverter.Translate(presentationParameters.DepthStencilFormat);
				if (depthFormat != Format.Unknown)
					CreateDepthStencilBuffer(depthFormat);
			}

			WindowHelper.ResizeRenderWindow(presentationParameters);
		}
		#endregion

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

		protected abstract void CreateDevice(PresentationParameters presentationParameters);
		protected abstract void CreateRenderView();
		protected abstract void CreateDepthStencilBuffer(SharpDX.DXGI.Format depthFormat);
		protected abstract void DisposeRenderView();
	}
}
