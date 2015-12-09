using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Test
{
	public class KeyboardCreator : IKeyboardCreator
	{
		public IKeyboard CreateDevice()
		{
			return new Keyboard();
		}

		public string Name
		{
			get
			{
				return "Test.Keyboard";
			}
		}

		public int Priority
		{
			get
			{
				return 99;
			}
		}


        public string Provider
        {
            get { return "Test"; }
        }
    }
}
