#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using Input = OpenTK.Input;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
    public class Keyboard : IKeyboard
    {
        public WindowHandle WindowHandle
        {
            get;
            set;
        }

        public Framework.Input.KeyboardState GetState()
        {
            return Input.Keyboard.GetState().ToAnx();
        }

        public Framework.Input.KeyboardState GetState(Framework.PlayerIndex playerIndex)
        {
            return Input.Keyboard.GetState((int)playerIndex).ToAnx();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
