#region Using Statements
using System;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;
using SharpDX.DirectInput;
using System.Runtime.InteropServices;
using ANX.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using MouseX = SharpDX.DirectInput.Mouse;

namespace ANX.InputDevices.Windows.XInput
{
    class Mouse : IMouse
    {
        #region Interop
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        static extern void SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        #endregion // Interop

        #region Private Members
        private DirectInput directInput;
        private MouseX mouse;

        #endregion // Private Members

        public IntPtr WindowHandle
        {
            get;
            set;
        }

        public Mouse()
        {
            this.directInput = new DirectInput();
            this.mouse = new MouseX(this.directInput);
            this.mouse.Properties.AxisMode = DeviceAxisMode.Absolute;
            this.mouse.Acquire();
        }

        public ANX.Framework.Input.MouseState GetState()
        {
            var state = this.mouse.GetCurrentState();

            Point cursorPos = new Point();
            GetCursorPos(ref cursorPos);
            if (WindowHandle != IntPtr.Zero)
            {
                ScreenToClient(WindowHandle, ref cursorPos);
            }

            state.X = cursorPos.X;
            state.Y = cursorPos.Y;

            ButtonState left = new ButtonState();
            ButtonState middle = new ButtonState();
            ButtonState right = new ButtonState();
            ButtonState x1 = new ButtonState();
            ButtonState x2 = new ButtonState();
            if(state.Buttons[0]){left=ButtonState.Pressed;}
            if(state.Buttons[1]){middle=ButtonState.Pressed;}
            if(state.Buttons[2]){right=ButtonState.Pressed;}
            if(state.Buttons[3]){x1=ButtonState.Pressed;}
            if(state.Buttons[4]){x2=ButtonState.Pressed;}
            return new ANX.Framework.Input.MouseState(state.X,state.Y,state.Z,left,middle,right,x1,x2);
        }

        public void SetPosition(int x, int y)
        {
            Point currentPosition = new Point(x, y);
            GetCursorPos(ref currentPosition);
            if (WindowHandle != IntPtr.Zero)
            {
                ScreenToClient(WindowHandle, ref currentPosition);
            }
            SetCursorPos(currentPosition.X, currentPosition.Y);
        }
    }
}
