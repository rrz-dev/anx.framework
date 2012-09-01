using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Tested)]
    public static class Mouse
    {
		private static IMouse mouse;

		public static IntPtr WindowHandle
		{
			get
			{
				return mouse != null ? mouse.WindowHandle : IntPtr.Zero;
			}
			set
			{
				if (mouse != null)
					mouse.WindowHandle = value;
			}
		}

        static Mouse()
        {
            mouse = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Mouse;
        }

        public static MouseState GetState() 
        {
            return (mouse != null) ? mouse.GetState() : new MouseState();
        }

        public static void SetPosition(int x, int y)
        {
            if (mouse != null)
                mouse.SetPosition(x, y);
        }
    }
}
