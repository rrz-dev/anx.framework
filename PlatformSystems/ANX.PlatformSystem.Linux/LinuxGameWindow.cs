using System;
using System.Windows.Forms;
using ANX.Framework;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.PlatformSystem.Linux
{
    internal class LinuxGameWindow : GameWindow
    {
        #region Public
        #region Form
        internal static Form Form
        {
            get;
            private set;
        }
        #endregion

        #region Handle
        public override WindowHandle Handle
        {
            get
            {
                return new WindowHandle(Form.Handle);
            }
        }
        #endregion

        #region IsMinimized
        internal override bool IsMinimized
        {
            get
            {
                return Form.WindowState == FormWindowState.Minimized;
            }
        }
        #endregion

        #region ScreenDeviceName
        public override string ScreenDeviceName
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region AllowUserResizing
        public override bool AllowUserResizing
        {
            get
            {
                return Form.FormBorderStyle == FormBorderStyle.Sizable;
            }
            set
            {
                Form.FormBorderStyle = value ?
                    FormBorderStyle.Sizable :
                    FormBorderStyle.Fixed3D;
            }
        }
        #endregion

        #region ClientBounds
        public override Rectangle ClientBounds
        {
            get
            {
                System.Drawing.Rectangle rect = Form.ClientRectangle;
                return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            }
        }
        #endregion

        #region CurrentOrientation
        public override DisplayOrientation CurrentOrientation
        {
            get
            {
                return DisplayOrientation.Default;
            }
        }
        #endregion
        #endregion

        #region Constructor
        internal LinuxGameWindow()
        {
            Form = new Form()
            {
                Text = "ANX Framework",
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.Fixed3D,
                ClientSize = new System.Drawing.Size(
                    GraphicsDeviceManager.DefaultBackBufferWidth,
                    GraphicsDeviceManager.DefaultBackBufferHeight),
            };

            Form.Activated += delegate { base.OnActivated(); };
            Form.Deactivate += delegate { base.OnDeactivated(); };
        }
        #endregion

        #region Close
        public void Close()
        {
            if (Form != null)
            {
                Form.Close();
                Form.Dispose();
                Form = null;
            }
        }
        #endregion

        #region SetTitle
        protected override void SetTitle(string title)
        {
            Form.Text = title;
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
    }
}
