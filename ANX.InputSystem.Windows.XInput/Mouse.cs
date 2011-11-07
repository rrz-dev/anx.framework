#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;
using SharpDX.DirectInput;
using System.Runtime.InteropServices;
using ANX.Framework;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

using MouseX = SharpDX.DirectInput.Mouse;

namespace ANX.InputSystem.Windows.XInput
{
    class Mouse:IMouse
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll")]
        static extern void SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);

        private DirectInput directInput;
        private MouseX mouse;
 
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
                ScreenToClient(WindowHandle, ref currentPosition;
            }
            SetCursorPos(currentPosition.X, currentPosition.Y);
        }
    }
}
