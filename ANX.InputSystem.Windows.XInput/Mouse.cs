using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;
using SharpDX.DirectInput;
using MouseX = SharpDX.DirectInput.Mouse;


namespace ANX.InputSystem.Windows.XInput
{
    class Mouse:IMouse
    {
        private DirectInput directInput;
        private MouseX mouse;
        public IntPtr WindowHandle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Mouse()
        {
            this.directInput = new DirectInput();
            this.mouse = new MouseX(this.directInput);
            this.mouse.Acquire();
        }

        public ANX.Framework.Input.MouseState GetState()
        {
            var state = this.mouse.GetCurrentState();
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
            throw new NotImplementedException();
        }
    }
}
