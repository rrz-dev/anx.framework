using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using System;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.ModernUI
{
    [PercentageComplete(0)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("rene87")]
    class Keyboard : IKeyboard
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

        public KeyboardState GetState()
        {
            throw new NotImplementedException();
        }

        public KeyboardState GetState(PlayerIndex playerIndex)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
