using System;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using ANX.Framework.NonXNA.PlatformSystem;
using ANX.PlatformSystem.Metro;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using PresentationParameters = ANX.Framework.Graphics.PresentationParameters;

namespace ANX.RenderSystem.Windows.Metro
{
	internal class SwapChainMetro : IDisposable
	{
		#region Private
		private SwapChain1 swapChain;
		private NativeDxDevice graphicsDevice;
		private PresentationParameters presentationParameters;
		private PresentParameters presentParameters;
		#endregion

		#region Constructor
		public SwapChainMetro(NativeDxDevice setGraphicsDevice,
			PresentationParameters presentationParameters)
		{
			graphicsDevice = setGraphicsDevice;
			this.presentationParameters = presentationParameters;

			presentParameters = new PresentParameters();
		}
		#endregion

		#region CreateTexture
		public Texture2D CreateTexture()
		{
			return Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
		}
		#endregion

		#region ResizeOrCreate
		public void ResizeOrCreate(PresentationParameters presentationParameters)
		{
			this.presentationParameters = presentationParameters;

			if (swapChain != null)
				Resize();
			else
				Create();
		}
		#endregion

		#region Resize
		private void Resize()
		{
			swapChain.ResizeBuffers(2, presentationParameters.BackBufferWidth,
				presentationParameters.BackBufferHeight,
				FormatConverter.Translate(presentationParameters.BackBufferFormat),
				SwapChainFlags.None);
		}
		#endregion

		#region Create
		private void Create()
		{
			var desc = CreateSwapChainDescription();

			using (var dxgiDevice2 = graphicsDevice.NativeDevice.QueryInterface<Device2>())
			{
				var dxgiAdapter = dxgiDevice2.Adapter;
				var dxgiFactory2 = dxgiAdapter.GetParent<Factory2>();

                //TODO: try to remove the direct dependency to the platform system
				WindowsGameWindow gameWindow = (WindowsGameHost.Instance.Window as WindowsGameWindow);
				var comWindow = new ComObject(gameWindow.Form);

				swapChain = dxgiFactory2.CreateSwapChainForCoreWindow(graphicsDevice.NativeDevice,
					comWindow, ref desc, null);
				dxgiDevice2.MaximumFrameLatency = 1;
			}
		}
		#endregion

		#region CreateSwapChainDescription
		private SwapChainDescription1 CreateSwapChainDescription()
		{
			return new SwapChainDescription1()
			{
				Width = presentationParameters.BackBufferWidth,
				Height = presentationParameters.BackBufferHeight,
				Format = FormatConverter.Translate(presentationParameters.BackBufferFormat),
				Stereo = false,
				SampleDescription = new SampleDescription(1, 0),
				Usage = Usage.RenderTargetOutput,
				BufferCount = 2,
				Scaling = Scaling.None,
				SwapEffect = SwapEffect.FlipSequential,
			};
		}
		#endregion

		#region Present
		public void Present(int interval)
		{
			if (swapChain == null)
				ResizeOrCreate(presentationParameters);

			swapChain.Present(interval, PresentFlags.None, presentParameters);
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			if (swapChain != null)
			{
				swapChain.Dispose();
				swapChain = null;
			}
		}
		#endregion
	}
}
