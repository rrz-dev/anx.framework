using System;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Infrastructure;
using Windows.UI.Core;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Metro
{
    internal class WindowsGameWindow : GameWindow, IViewProvider
    {
        #region Private Members
        private CoreWindow gameWindow;

        #endregion // Private Members

        internal WindowsGameWindow()
        {
            //this.gameWindow = new RenderForm("ANX.Framework");

            //this.gameWindow.Width = 800;
            //this.gameWindow.Height = 480;

            //this.gameWindow.MaximizeBox = false;
            //this.gameWindow.FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        public void Initialize(CoreWindow window, CoreApplicationView applicationView)
        {
            this.gameWindow = window;
        }

        public void Load(string entryPoint)
        {

        }

        public void Run()
        {
            System.Diagnostics.Debugger.Break();
        }

        public void Uninitialize()
        {

        }

        public void Close()
        {
            if (gameWindow != null)
            {
                gameWindow.Close();
            }
        }

        public CoreWindow Form
        {
            get
            {
                return gameWindow;
            }
        }

        public override IntPtr Handle
        {
            get 
            { 
                return IntPtr.Zero; 
            }
        }

        public override bool IsMinimized
        {
            get 
            {
                //TODO: return gameWindow.WindowState == FormWindowState.Minimized;
                return false;
            }
        }

        public override void BeginScreenDeviceChange(bool willBeFullScreen)
        {
            throw new NotImplementedException();
        }

        public override void EndScreenDeviceChange(string screenDeviceName, int clientWidth, int clientHeight)
        {
            throw new NotImplementedException();
        }

        protected override void SetTitle(string title)
        {
            //TODO: this.gameWindow.Text = title;
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
                //TODO: cache this to prevent four castings on every access
                //TODO: check if double type bounds are really castable to int
                return new Rectangle((int)this.gameWindow.Bounds.Left, 
                                     (int)this.gameWindow.Bounds.Top, 
                                     (int)this.gameWindow.Bounds.Width, 
                                     (int)this.gameWindow.Bounds.Height);
            }
        }

        public override string ScreenDeviceName
        {
            get { throw new NotImplementedException(); }
        }

        public override DisplayOrientation CurrentOrientation
        {
            get { throw new NotImplementedException(); }
        }
    }
}
