using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
	public class KeyboardCreator : IKeyboardCreator
	{
		public string Name
		{
			get
			{
				return "OpenTK.Keyboard";
			}
		}

		public int Priority
		{
			get
			{
				return 100;
			}
		}

		public IKeyboard CreateDevice()
		{
			return new Keyboard();
		}
	}
}
