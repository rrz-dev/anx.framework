#region Using Statements
using System;
using ANX.Framework.NonXNA;
using SharpDX.DirectInput;
using DXKeyboard = SharpDX.DirectInput.Keyboard;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
    public class Keyboard : IKeyboard
    {
        #region Private Members
        private DirectInput directInput;
        private DXKeyboard nativeKeyboard;
        private KeyboardState nativeState;

        #endregion // Private Members

        public IntPtr WindowHandle
        {
            get;
            set;
        }

        public Keyboard()
        {
            this.nativeState = new KeyboardState();
        }

        public Framework.Input.KeyboardState GetState(Framework.PlayerIndex playerIndex)
        {
            //TODO: prevent new

            // only available on XBox, behaviour regarding MSDN: empty keystate

            return new Framework.Input.KeyboardState(new Framework.Input.Keys[0]);
        }

        public Framework.Input.KeyboardState GetState()
        {
            if (this.nativeKeyboard == null && this.WindowHandle != null && this.WindowHandle != IntPtr.Zero)
            {
                this.directInput = new DirectInput();
                this.nativeKeyboard = new DXKeyboard(this.directInput);
                this.nativeKeyboard.SetCooperativeLevel(this.WindowHandle, CooperativeLevel.NonExclusive | CooperativeLevel.Background);
                this.nativeKeyboard.Acquire();
            }

            if (this.nativeKeyboard != null)
            {
                nativeKeyboard.GetCurrentState(ref this.nativeState);
                if (this.nativeState.PressedKeys.Count > 0)
                {
                    return FormatConverter.Translate(this.nativeState);
                }
            }

            return new Framework.Input.KeyboardState(new Framework.Input.Keys[0]);
        }

        public void Dispose()
        {
            if (this.nativeKeyboard != null)
            {
                this.nativeKeyboard.Unacquire();
                this.nativeKeyboard.Dispose();
                this.nativeKeyboard = null;
            }

            if (this.directInput != null)
            {
                this.directInput.Dispose();
                this.directInput = null;
            }
        }
    }
}
