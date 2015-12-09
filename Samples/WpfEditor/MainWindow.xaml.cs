using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace WpfEditor
{
	public partial class MainWindow
	{
		private GraphicsDevice device;
	    private readonly ThreadStart emptyThreadStart;

		public MainWindow()
		{
            //AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "OpenGL3");
            AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "DirectX10");

			InitializeComponent();
		    emptyThreadStart = delegate { };
		}

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef,
                new PresentationParameters
                {
                    BackBufferWidth = GamePanel.Width,
                    BackBufferHeight = GamePanel.Height,
                    BackBufferFormat = SurfaceFormat.Color,
                    DeviceWindowHandle = new WindowHandle(GamePanel.Handle),
                    PresentationInterval = PresentInterval.Default,
                });
        }

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			while (IsVisible && IsActive)
			{
				if (Application.Current != null)
					Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, emptyThreadStart);

				Tick();
			}
		}

		public void Tick()
		{
			device.Clear(ClearOptions.Target, Color.CornflowerBlue, 0f, 0);

			device.Present();
		}
	}
}
