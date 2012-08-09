#region Using Statements
using System;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    public static class Keyboard
    {
        private static IKeyboard keyboard;

        static Keyboard()
        {
            keyboard = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Keyboard;
        }

        internal static IntPtr WindowHandle
        {
            get
            {
                return keyboard != null ? keyboard.WindowHandle : IntPtr.Zero;
            }
            set
            {
                if (keyboard != null)
                {
                    keyboard.WindowHandle = value;
                }
            }
        }

        public static KeyboardState GetState() 
        {
            return keyboard.GetState(); 
        }

        public static KeyboardState GetState (PlayerIndex playerIndex)
        {
            return keyboard.GetState(playerIndex);
        }

    }
}
