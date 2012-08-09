using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Input;
using Windows.ApplicationModel.Infrastructure;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
    public class WindowsGameHost : GameHost, IViewProviderFactory
    {
        private Game game;
        private WindowsGameWindow gameWindow;
        private bool exitRequested;

        public WindowsGameHost(Game game)
            : base(game)
        {
            this.game = game;
            this.gameWindow = new WindowsGameWindow();
        }

        public IViewProvider CreateViewProvider()
        {
            return gameWindow;
        }

        public override void Run()
        {
            //Windows.ApplicationModel.Core.CoreApplication.Run(this);
            throw new NotImplementedException();
        }

        public override GameWindow Window
        {
            get 
            { 
                return this.gameWindow; 
            }
        }

        public override void Exit()
        {
            this.exitRequested = true;
        }
    }

    //public class WindowsGameHost : GameHost, IViewProvider
    //{
    //    private Game game;
    //    private WindowsGameWindow gameWindow;
    //    private bool exitRequested;

    //    public WindowsGameHost(Game game)
    //        : base(game)
    //    {
    //        this.game = game;
    //        //this.LockThreadToProcessor();
    //        this.gameWindow = new WindowsGameWindow();
    //        Mouse.WindowHandle = this.gameWindow.Handle;        //TODO: find a way to initialize all InputSystems with one Handle
    //        Keyboard.WindowHandle = this.gameWindow.Handle;
    //        //TouchPanel.WindowHandle = this.gameWindow.Handle;
    //        //this.gameWindow.IsMouseVisible = game.IsMouseVisible;
    //        this.gameWindow.Activated += new EventHandler<EventArgs>(this.GameWindowActivated);
    //        this.gameWindow.Deactivated += new EventHandler<EventArgs>(this.GameWindowDeactivated);
    //        //this.gameWindow.Suspend += new EventHandler<EventArgs>(this.GameWindowSuspend);
    //        //this.gameWindow.Resume += new EventHandler<EventArgs>(this.GameWindowResume);
        
    //    }

    //    public override void Run()
    //    {
    //        Application.Idle += new EventHandler(this.ApplicationIdle);
    //        Application.Run(this.gameWindow.Form);
    //        Application.Idle -= this.ApplicationIdle;
    //    }

    //    public void RunOneFrame()
    //    {
    //        //this.gameWindow.Tick();
    //        base.OnIdle();
    //    }

    //    public override GameWindow Window
    //    {
    //        get 
    //        { 
    //            return this.gameWindow; 
    //        }
    //    }

    //    public override void Exit()
    //    {
    //        this.exitRequested = true;
    //    }

    //    private void GameWindowActivated(object sender, EventArgs e)
    //    {
    //        base.OnActivated();
    //    }

    //    private void GameWindowDeactivated(object sender, EventArgs e)
    //    {
    //        base.OnDeactivated();
    //    }

    //    private void ApplicationIdle(object sender, EventArgs e)
    //    {
    //        NativeMethods.Message message;
    //        while (!NativeMethods.PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
    //        {
    //            if (this.exitRequested)
    //            {
    //                this.gameWindow.Close();
    //            }
    //            else
    //            {
    //                this.RunOneFrame();
    //            }
    //        }
    //    }
    //}
}
