#region Using Statements
using System;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    public static class Mouse
    {
        private static IMouse mouse;

        static Mouse()
        {
            mouse = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Mouse;
        }

        public static IntPtr WindowHandle 
        {
            get 
            {
                return mouse != null ? mouse.WindowHandle : IntPtr.Zero;
            }
            set 
            {
                if (mouse != null)
                {
                    mouse.WindowHandle = value;
                }
            }
        }

        public static MouseState GetState() 
        {
            if (mouse != null)
            {
                return mouse.GetState();
            }
            else
            {
                return new MouseState();
            }
        }

        public static void SetPosition(int x, int y)
        {
            if (mouse != null)
            {
                mouse.SetPosition(x, y);
            }
        }
    }
}
