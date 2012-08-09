using System.Windows;
using ANX.Framework;
using ANX.Framework.Graphics;
using System.Windows.Interop;
using System;
using System.Windows.Threading;
using System.Threading;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace WpfEditor
{
	public partial class MainWindow : Window
	{
		private GraphicsDevice device;

		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			Initialize();

			while (IsVisible)
			{
				if (Application.Current != null)
				{
					Application.Current.Dispatcher.Invoke(
						DispatcherPriority.Background, new ThreadStart(delegate { }));
				}

				Tick();
			}
		}

		public void Initialize()
		{
			device = new GraphicsDevice(
				GraphicsAdapter.DefaultAdapter,
				GraphicsProfile.HiDef,
				new PresentationParameters
				{
					BackBufferWidth = (int)GamePanel.Width,
					BackBufferHeight = (int)GamePanel.Height,
					BackBufferFormat = SurfaceFormat.Color,
					DeviceWindowHandle = GamePanel.Handle,
					PresentationInterval = PresentInterval.Default,
				});
		}

		public void Tick()
		{
			device.Clear(ClearOptions.Target, Color.CornflowerBlue, 0f, 0);

			device.Present();
		}
	}
}
