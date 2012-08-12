using System;
using SharpDX;
using SharpDX.Direct3D;
using Windows.Foundation;
using Windows.UI.Core;
using Dx11 = SharpDX.Direct3D11;
using PresentationParameters = ANX.Framework.Graphics.PresentationParameters;

namespace ANX.RenderSystem.Windows.Metro
{
	public class NativeDxDevice : IDisposable
	{
		private FeatureLevel featureLevel;
		private Dx11.RenderTargetView renderTargetView;
		private Dx11.DepthStencilView depthStencilView;
		private SwapChainMetro swapChain;

		internal Dx11.Device1 NativeDevice
		{
			get;
			private set;
		}

		internal Dx11.DeviceContext1 NativeContext
		{
			get;
			private set;
		}

		public Rect RenderTargetBounds
		{
			get;
			protected set;
		}

		public NativeDxDevice(CoreWindow setWindow, PresentationParameters presentationParameters)
		{
			swapChain = new SwapChainMetro(setWindow, this, presentationParameters);

			var creationFlags = Dx11.DeviceCreationFlags.VideoSupport | Dx11.DeviceCreationFlags.BgraSupport;
#if DEBUG
			creationFlags |= Dx11.DeviceCreationFlags.Debug;
#endif
			using (var defaultDevice = new Dx11.Device(DriverType.Hardware, creationFlags))
			{
				NativeDevice = defaultDevice.QueryInterface<Dx11.Device1>();
			}
			featureLevel = NativeDevice.FeatureLevel;

			NativeContext = NativeDevice.ImmediateContext.QueryInterface<Dx11.DeviceContext1>();
		}

		public void Resize(PresentationParameters presentationParameters)
		{
			if (renderTargetView != null)
			{
				renderTargetView.Dispose();
				renderTargetView = null;
			}
			if (depthStencilView != null)
			{
				depthStencilView.Dispose();
				depthStencilView = null;
			}

			swapChain.ResizeOrCreate(presentationParameters);

			using (var backBuffer = swapChain.CreateTexture())
			{
				renderTargetView = new Dx11.RenderTargetView(NativeDevice, backBuffer);

				var backBufferDesc = backBuffer.Description;
				RenderTargetBounds = new Rect(0, 0, backBufferDesc.Width, backBufferDesc.Height);
			}

			using (var depthBuffer = new Dx11.Texture2D(NativeDevice, new Dx11.Texture2DDescription()
			{
				Format = SharpDX.DXGI.Format.D24_UNorm_S8_UInt,
				ArraySize = 1,
				MipLevels = 1,
				Width = (int)RenderTargetBounds.Width,
				Height = (int)RenderTargetBounds.Height,
				SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
				BindFlags = Dx11.BindFlags.DepthStencil,
			}))
			{
				depthStencilView = new Dx11.DepthStencilView(NativeDevice, depthBuffer, new Dx11.DepthStencilViewDescription()
				{
					Dimension = Dx11.DepthStencilViewDimension.Texture2D
				});
			}

			var viewport = new Dx11.Viewport((float)RenderTargetBounds.X, (float)RenderTargetBounds.Y,
					(float)RenderTargetBounds.Width, (float)RenderTargetBounds.Height, 0.0f, 1.0f);

			NativeContext.Rasterizer.SetViewports(viewport);

			NativeContext.OutputMerger.SetTargets(depthStencilView, renderTargetView);
		}

		public void ClearDepthAndStencil(Dx11.DepthStencilClearFlags flags, float depth, byte stencil)
		{
			NativeContext.ClearDepthStencilView(depthStencilView, flags, depth, stencil);
		}

		public void Clear(Color4 color)
		{
			NativeContext.ClearRenderTargetView(renderTargetView, color);
		}

		public void Present(int interval)
		{
			swapChain.Present(interval);
		}

		public void Dispose()
		{
			if (swapChain != null)
			{
				swapChain.Dispose();
				swapChain = null;
			}

			if (NativeDevice != null)
			{
				NativeDevice.Dispose();
				NativeDevice = null;
			}

			if (NativeContext != null)
			{
				NativeContext.Dispose();
				NativeContext.Dispose();
			}
		}
	}
}
