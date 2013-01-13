using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Core;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.ModernUI
{
    [PercentageComplete(80)]
    [TestState(TestStateAttribute.TestState.InProgress)]
    [Developer("rene87")]
    public class Keyboard : IKeyboard
    {
        private IntPtr windowHandle;
        KeyboardState _state;
        public IntPtr WindowHandle
        {
            get { return windowHandle; }
            set
            {
                if (windowHandle != value)
                {
                    windowHandle = value;
                }
            }
        }

        public Keyboard()
        {
            CoreWindow.GetForCurrentThread().KeyDown += Keyboard_KeyDown;
            CoreWindow.GetForCurrentThread().KeyUp += Keyboard_KeyUp;
            _state = new KeyboardState(Keys.None);
            _state.RemovePressedKey(Keys.None);
        }

        void Keyboard_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            var key = FormatConverter.Translate(args.VirtualKey);

            _state.RemovePressedKey(key);

        }

        void Keyboard_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            var key = FormatConverter.Translate(args.VirtualKey);
            if (key != Keys.None)
            {
                _state.AddPressedKey(key);
            }
        }

        public KeyboardState GetState()
        {

            return _state;
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
