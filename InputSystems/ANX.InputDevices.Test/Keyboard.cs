using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Test
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
