using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;

namespace ANX.InputSystem.Windows.XInput
{
    class Mouse:IMouse
    {
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

        public MouseState GetState()
        {
            throw new NotImplementedException();
        }

        public void SetPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
