using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Test
{
	public class GamePad : IGamePad
	{
		public GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
		{
			return new GamePadCapabilities();
		}

		public GamePadState GetState(PlayerIndex playerIndex)
		{
			GamePadState gamepad;
			switch (playerIndex)
			{
				case PlayerIndex.One:
					gamepad = new GamePadState(new Vector2(100, 100), new Vector2(100, 100), 0.5f, 0.5f, Buttons.A, Buttons.B)
					{
						IsConnected = true,
						PacketNumber = 0,
					};
					break;

				case PlayerIndex.Two:
					gamepad = new GamePadState(new Vector2(200, 200), new Vector2(100, 100), 0.5f, 0.5f, Buttons.A,
						Buttons.BigButton)
					{
						IsConnected = true,
						PacketNumber = 0,
					};
					break;

				case PlayerIndex.Three:
					gamepad = new GamePadState(new Vector2(100, 100), new Vector2(100, 100), 0.5f, 0.5f, Buttons.A, Buttons.X)
					{
						IsConnected = true,
						PacketNumber = 0,
					};
					break;

				case PlayerIndex.Four:
				default:
					gamepad = new GamePadState()
					{
						IsConnected = false,
						PacketNumber = 0,
					};
					break;
			}
			return gamepad;
		}

		public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
		{
			return GetState(playerIndex);
		}

		public bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			switch (playerIndex)
			{
				case PlayerIndex.One:
					return (leftMotor == 0.5f && rightMotor == 0.5f);

				case PlayerIndex.Two:
					return (leftMotor == 0.7f && rightMotor == 0.5f);

				case PlayerIndex.Three:
					return (leftMotor == -0.5f && rightMotor == 0.7f);

				case PlayerIndex.Four:
					return (leftMotor == 0.5f && rightMotor == 0.5f);
			}

			return false;
		}
	}
}
