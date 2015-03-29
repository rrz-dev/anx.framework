using System.Windows.Forms;
using ANX.Framework;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Windows
{
	public class WindowsGameHost : GameHost
	{
		#region Private
		private Game game;
		private WindowsGameWindow gameWindow;
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
		public WindowsGameHost(Game setGame)
			: base()
		{
			isQuitting = false;
			game = setGame;

			Logger.Info("creating a new GameWindow");
			gameWindow = new WindowsGameWindow();

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
			WindowsGameWindow.Form.FormClosing += delegate
			{
				isQuitting = true;
                OnExiting();
			};
		}
		#endregion

		#region Run
		public override void Run()
		{
			WindowsGameWindow.Form.Show();
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
            OnExiting();
		}
		#endregion
	}
}
