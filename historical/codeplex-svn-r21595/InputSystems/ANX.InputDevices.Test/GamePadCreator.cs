using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Test
{
	public class GamePadCreator : IGamePadCreator
	{
		public IGamePad CreateDevice()
		{
			return new GamePad();
		}

		public string Name
		{
			get
			{
				return "Test.GamePad";
			}
		}

		public int Priority
		{
			get
			{
				return 99;
			}
		}
	}
}
