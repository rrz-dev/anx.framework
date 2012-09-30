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
		private static readonly IMouse mouse;

	    public static IntPtr WindowHandle
	    {
	        get { return mouse.WindowHandle; }
	        set { mouse.WindowHandle = value; }
	    }

	    static Mouse()
        {
            mouse = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Mouse;
        }

        public static MouseState GetState() 
        {
            return mouse.GetState();
        }

        public static void SetPosition(int x, int y)
        {
            mouse.SetPosition(x, y);
        }
    }
}
