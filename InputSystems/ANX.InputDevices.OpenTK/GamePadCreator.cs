using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
	public class GamePadCreator : IGamePadCreator
	{
		public string Name
		{
			get
			{
				return "OpenTK.GamePad";
			}
		}

		public void RegisterCreator(InputDeviceFactory factory)
		{
			factory.AddCreator(typeof(GamePadCreator), this);
		}

		public int Priority
		{
			get
			{
				return 100;
			}
		}

		public IGamePad CreateGamePadInstance()
		{
			return new GamePad();
		}
	}
}
