using System.Windows.Forms;
using ANX.Framework;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Linux
{
	public class LinuxGameHost : GameHost
	{
		#region Private
		private Game game;
		private LinuxGameWindow gameWindow;
		private bool isQuitting;
		#endregion

		#region Public
		public override GameWindow Window
		{
			get
			{
				return gameWindow;
			}
		}
		#endregion

		#region Constructor
		public LinuxGameHost(Game setGame)
			: base(setGame)
		{
			isQuitting = false;
			game = setGame;

			Logger.Info("creating a new GameWindow");
			gameWindow = new LinuxGameWindow();

			InputDeviceFactory.Instance.WindowHandle = this.gameWindow.Handle;

			Logger.Info("hook up GameWindow events");
			gameWindow.Activated += delegate
			{
				OnActivated();
			};
			gameWindow.Deactivated += delegate
			{
				OnDeactivated();
			};
			LinuxGameWindow.Form.FormClosing += delegate
			{
				isQuitting = true;
			};
		}
		#endregion

		#region Run
		public override void Run()
		{
			LinuxGameWindow.Form.Show();
			while (isQuitting == false)
			{
				Application.DoEvents();
				base.OnIdle();
			}
			gameWindow.Close();
		}
		#endregion

		#region Exit
		public override void Exit()
		{
			isQuitting = true;
		}
		#endregion
	}
}
