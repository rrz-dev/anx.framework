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
		private static readonly IKeyboard keyboard;

	    internal static IntPtr WindowHandle
	    {
	        get { return keyboard.WindowHandle; }
	        set { keyboard.WindowHandle = value; }
	    }

	    static Keyboard()
		{
			keyboard = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().Keyboard;
		}

		public static KeyboardState GetState()
		{
			return keyboard.GetState();
		}

		public static KeyboardState GetState(PlayerIndex playerIndex)
		{
			return keyboard.GetState(playerIndex);
		}
	}
}
