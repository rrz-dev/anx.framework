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
        private KeyboardState _state;
        private KeyboardState _emptyState;
        public WindowHandle WindowHandle
        {
            get;
            set;
        }

        public Keyboard()
        {
            //CoreWindow.GetForCurrentThread is null
            CoreWindow.GetForCurrentThread().KeyDown += Keyboard_KeyDown;
            CoreWindow.GetForCurrentThread().KeyUp += Keyboard_KeyUp;
            _state = new KeyboardState(new Keys[0]);
            _emptyState =new KeyboardState(new Keys[0]);
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

            return new KeyboardState(_state.GetPressedKeys());
        }

        public KeyboardState GetState(PlayerIndex playerIndex)
        {
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    return GetState();
                default:
                    return _emptyState;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
