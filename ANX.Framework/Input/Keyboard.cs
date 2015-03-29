using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public static class Keyboard
    {
        private static IKeyboard keyboard;

        internal static WindowHandle WindowHandle
        {
            get { return NativeInstance.WindowHandle; }
            set { NativeInstance.WindowHandle = value; }
        }

        private static IKeyboard NativeInstance
        {
            get
            {
                if (keyboard == null)
                {
                    keyboard = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Keyboard;
                    AddInSystemFactory.Instance.PreventSystemChange(AddInType.InputSystem);
                }

                return keyboard;
            }
        }

        public static KeyboardState GetState()
        {
            return NativeInstance.GetState();
        }

        public static KeyboardState GetState(PlayerIndex playerIndex)
        {
            return NativeInstance.GetState(playerIndex);
        }
    }
}
