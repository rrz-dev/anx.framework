using System;
using System.Windows.Forms;
using ANX.Framework;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace WindowsFormsEditor
{
	public partial class Form1 : Form
	{
		private GraphicsDevice device;

		public Form1()
		{
			InitializeComponent();
		}

		public void Initialize()
		{
			device = new GraphicsDevice(
				GraphicsAdapter.DefaultAdapter,
				GraphicsProfile.HiDef,
				new PresentationParameters
				{
					BackBufferWidth = panel1.Width,
					BackBufferHeight = panel1.Height,
					BackBufferFormat = SurfaceFormat.Color,
					DeviceWindowHandle = panel1.Handle,
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
