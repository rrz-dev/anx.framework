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
    public static class Mouse
    {
        private static IMouse mouse;

        public static WindowHandle WindowHandle
        {
            get { return NativeInstance.WindowHandle; }
            set { NativeInstance.WindowHandle = value; }
        }

        private static IMouse NativeInstance
        {
            get
            {
                if (mouse == null)
                {
                    mouse = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Mouse;
                }
                return mouse;
            }
        }

        public static MouseState GetState() 
        {
            return NativeInstance.GetState();
        }

        public static void SetPosition(int x, int y)
        {
            NativeInstance.SetPosition(x, y);
        }
    }
}
