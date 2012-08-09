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

		public GamePadState GetState(PlayerIndex playerIndex, out bool isConnected,
			out int packetNumber)
		{
			GamePadState gamepad;
			switch (playerIndex)
			{
				case PlayerIndex.One:
					isConnected = true;
					packetNumber = 0;
					gamepad = new GamePadState(new Vector2(100, 100), new Vector2(100, 100),
						0.5f, 0.5f, Buttons.A, Buttons.B);
					break;

				case PlayerIndex.Two:
					isConnected = true;
					packetNumber = 0;
					gamepad = new GamePadState(new Vector2(200, 200), new Vector2(100, 100),
						0.5f, 0.5f, Buttons.A, Buttons.BigButton);
					break;

				case PlayerIndex.Three:
					isConnected = true;
					packetNumber = 0;
					gamepad = new GamePadState(new Vector2(100, 100), new Vector2(100, 100),
						0.5f, 0.5f, Buttons.A, Buttons.X);
					break;

				case PlayerIndex.Four:
				default:
					isConnected = false;
					packetNumber = 0;
					gamepad = new GamePadState();
					break;
			}
			return gamepad;
		}

		public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode,
			out bool isConnected, out int packetNumber)
		{
			return this.GetState(playerIndex, out isConnected, out packetNumber);
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
