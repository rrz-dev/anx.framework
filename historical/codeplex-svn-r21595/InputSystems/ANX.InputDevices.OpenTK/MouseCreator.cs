using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
	public class MouseCreator : IMouseCreator
	{
		public string Name
		{
			get
			{
				return "OpenTK.Mouse";
			}
		}

		public int Priority
		{
			get
			{
				return 100;
			}
		}

		public IMouse CreateDevice()
		{
			return new Mouse();
		}
	}
}
