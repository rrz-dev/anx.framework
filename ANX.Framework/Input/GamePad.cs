using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Tested)]
	public static class GamePad
	{
		private static readonly IGamePad gamePad;

		static GamePad()
		{
			gamePad = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().GamePad;
		}

		public static GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
		{
			return gamePad.GetCapabilities(playerIndex);
		}

		public static GamePadState GetState(PlayerIndex playerIndex)
		{
			return gamePad.GetState(playerIndex);
		}

		public static GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
		{
			return gamePad.GetState(playerIndex, deadZoneMode);
		}

		public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			return gamePad.SetVibration(playerIndex, leftMotor, rightMotor);
		}
	}
}
