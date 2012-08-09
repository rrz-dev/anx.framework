#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
    public class Keyboard : IKeyboard
    {
        private IntPtr windowHandle;

        public IntPtr WindowHandle
        {
            get
            {
                return this.windowHandle;
            }
            set
            {
                this.windowHandle = value;
            }
        }

        public Framework.Input.KeyboardState GetState()
        {
            throw new NotImplementedException();
        }

        public Framework.Input.KeyboardState GetState(Framework.PlayerIndex playerIndex)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
