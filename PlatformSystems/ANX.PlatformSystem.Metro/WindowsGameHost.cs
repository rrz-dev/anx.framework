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
        private Game game;
        private Func<Game> gameCreationHandler;

        public static WindowsGameHost Instance
        {
            get;
            private set;
        }

        public Game Game
        {
            get { return game; }
        }

        #region Private
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
        public WindowsGameHost(Func<Game> gameCreationHandler)
            : base()
        {
            if (gameCreationHandler == null)
                throw new ArgumentNullException("gameCreationHandler");

            this.gameCreationHandler = gameCreationHandler;
            Instance = this;
            gameWindow = new WindowsGameWindow(this);
        }
        #endregion

        #region Run
        public override void Run()
        {
            gameWindow.MessageLoop();
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

        public IFrameworkView CreateView()
        {
            this.game = this.gameCreationHandler();
            return gameWindow;
        }
    }
}
