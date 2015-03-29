using System;
using ANX.Framework;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Display;
using Windows.UI.Core;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
    public class WindowsGameWindow : GameWindow, IFrameworkView
    {
        #region Private
        private WindowsGameHost gameHost;
        private CoreWindow gameWindow;
        private float dpi;
        private Rectangle clientBounds;
        #endregion

        #region Public
        public CoreWindow Form
        {
            get
            {
                return gameWindow;
            }
        }

        public override WindowHandle Handle
        {
            get
            {
                return new WindowHandle(gameWindow);
            }
        }

        internal override bool IsMinimized
        {
            get
            {
                //TODO: return gameWindow.WindowState == FormWindowState.Minimized;
                return false;
            }
        }

        public override string ScreenDeviceName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DisplayOrientation CurrentOrientation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool AllowUserResizing
        {
            get
            {
                //return gameWindow.FormBorderStyle == FormBorderStyle.Sizable;
                return false;
            }
            set
            {
                throw new NotSupportedException("AllowUserResizing can not be changed in RenderSystem Metro");
            }
        }

        public override Rectangle ClientBounds
        {
            get
            {
                return clientBounds;
            }
        }
        #endregion

        #region Constructor
        internal WindowsGameWindow(WindowsGameHost setGameHost)
        {
            gameHost = setGameHost;
            //TODO: Hook up activate and deactivate events
        }
        #endregion

        #region Close
        public void Close()
        {
            if (gameWindow != null)
            {
                gameWindow.Close();
            }
        }
        #endregion

        #region BeginScreenDeviceChange
        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region EndScreenDeviceChange
        public override void EndScreenDeviceChange(string screenDeviceName,
            int clientWidth, int clientHeight)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SetTitle
        protected override void SetTitle(string title)
        {
            //TODO: this.gameWindow.Text = title;
        }
        #endregion

        #region SetDpiIfNeeded
        private void SetDpiIfNeeded(float setDpi)
        {
            if (dpi != setDpi)
            {
                dpi = setDpi;
                SizeChanged();
            }
        }
        #endregion

        #region SetWindow
        public void SetWindow(CoreWindow window)
        {
            gameWindow = window;

            window.SizeChanged += delegate
            {
                SizeChanged();
            };

            SetDpiIfNeeded(DisplayInformation.GetForCurrentView().LogicalDpi);

            InputDeviceFactory.Instance.WindowHandle = this.Handle;
        }
        #endregion

        private void SizeChanged()
        {
            clientBounds = new Rectangle(
                (int)gameWindow.Bounds.Left, (int)gameWindow.Bounds.Top,
                (int)gameWindow.Bounds.Width, (int)gameWindow.Bounds.Height);

            OnClientSizeChanged();
        }

        //Gets called through run of the game class.
        internal void MessageLoop()
        {
            while (gameHost.ExitRequested == false)
            {
                gameWindow.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
                gameHost.InvokeOnIdle();
            }
        }

        #region Run
        //Gets called through run of the core application.
        //Before we can get to the MessageLoop we have to do some initialization here.
        public void Run()
        {
            DisplayInformation.GetForCurrentView().DpiChanged += delegate
            {
                SetDpiIfNeeded(DisplayInformation.GetForCurrentView().LogicalDpi);
            };
            gameWindow.PointerCursor = new CoreCursor(CoreCursorType.Arrow, 0);
            gameWindow.Activate();

            gameHost.Game.Run();
        }
        #endregion

        public void Initialize(CoreApplicationView applicationView)
        {
            
        }

        public void Load(string entryPoint)
        {
            
        }

        public void Uninitialize()
        {
            
        }
    }
}
