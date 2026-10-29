using ANX.Framework.Input;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
	public interface IGamePad
	{
		GamePadCapabilities GetCapabilities(PlayerIndex playerIndex);
		GamePadState GetState(PlayerIndex playerIndex);
		GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode);
		bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor);
	}
}
