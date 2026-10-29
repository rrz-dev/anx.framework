using ANX.Framework;
using ANX.Framework.NonXNA;
using Sce.PlayStation.Core.Environment;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.PsVita
{
	public class PsVitaGameHost : GameHost
	{
		#region Private
		private Game game;
		private PsVitaGameWindow gameWindow;
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
		public PsVitaGameHost(Game setGame)
			: base(setGame)
		{
			isQuitting = false;
			game = setGame;

			Logger.Info("creating a new GameWindow");
			gameWindow = new PsVitaGameWindow();

			// TODO
			//InputDeviceFactory.Instance.WindowHandle = this.gameWindow.Handle;

			Logger.Info("hook up GameWindow events");
			gameWindow.Activated += delegate
			{
				OnActivated();
			};
			gameWindow.Deactivated += delegate
			{
				OnDeactivated();
			};
			// TODO
			//PsVitaGameWindow.Form.FormClosing += delegate
			//{
			//  isQuitting = true;
			//};
		}
		#endregion

		#region Run
		public override void Run()
		{
			while (isQuitting == false)
			{
				SystemEvents.CheckEvents();
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
