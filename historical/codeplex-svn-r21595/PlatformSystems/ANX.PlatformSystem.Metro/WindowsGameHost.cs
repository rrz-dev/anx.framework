using System;
using ANX.Framework;
using Windows.ApplicationModel.Core;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
	public class WindowsGameHost : GameHost, IFrameworkViewSource
	{
		public static WindowsGameHost Instance
		{
			get;
			private set;
		}

		#region Private
		private Game game;
		private WindowsGameWindow gameWindow;
		internal bool ExitRequested
		{
			get;
			private set;
		}
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
			: base(setGame)
		{
			Instance = this;
			game = setGame;
			gameWindow = new WindowsGameWindow(this);
		}
		#endregion

		#region Run
		public override void Run()
		{
			CoreApplication.Run(this);
		}
		#endregion

		internal void InvokeOnIdle()
		{
			OnIdle();
		}

		#region Exit
		public override void Exit()
		{
			ExitRequested = true;
		}
		#endregion

		#region CreateView
		public IFrameworkView CreateView()
		{
			return gameWindow;
		}
		#endregion
	}
}
