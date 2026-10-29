using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using Sce.PlayStation.Core.Environment;
using SceInput = Sce.PlayStation.Core.Input;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.PsVita
{
    public class GamePad : IGamePad
    {
		public GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
		{
			return new GamePadCapabilities()
			{
				GamePadType = GamePadType.GamePad,
				HasAButton = true,
				HasBackButton = true,
				HasBButton = true,
				// TODO: same as todo in ConvertButtons!
				HasBigButton = true,

				HasDPadDownButton = true,
				HasDPadLeftButton = true,
				HasDPadRightButton = true,
				HasDPadUpButton = true,
				HasLeftShoulderButton = true,
				HasLeftStickButton = false,
				HasLeftTrigger = false,
				HasLeftVibrationMotor = false,
				HasLeftXThumbStick = true,
				HasLeftYThumbStick = true,
				HasRightShoulderButton = true,
				HasRightStickButton = false,
				HasRightTrigger = false,
				HasRightVibrationMotor = false,
				HasRightXThumbStick = true,
				HasRightYThumbStick = true,
				HasStartButton = true,
				HasVoiceSupport = false,
				HasXButton = true,
				HasYButton = true,
				IsConnected = playerIndex == PlayerIndex.One
			};
		}

		public GamePadState GetState(PlayerIndex playerIndex)
		{
			return GetState(playerIndex, GamePadDeadZone.None);
		}

		public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
		{
			// TODO: GamePadDeadZone

			SceInput.GamePadData data = SceInput.GamePad.GetData((int)playerIndex);

			return new GamePadState(new Vector2(data.AnalogLeftX, data.AnalogLeftY),
				new Vector2(data.AnalogRightX, data.AnalogRightY), 0f, 0f, ConvertButtons(data.ButtonsDown))
			{
				IsConnected = data.Skip == false,
				PacketNumber = 0 // TODO
			};
		}

		public bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			return false;
		}

		private Buttons ConvertButtons(SceInput.GamePadButtons sceButtons)
		{
			Buttons result = (Buttons)0;

			if ((sceButtons & SceInput.GamePadButtons.Up) > 0)
				result |= Buttons.DPadUp;
			if ((sceButtons & SceInput.GamePadButtons.Down) > 0)
				result |= Buttons.DPadDown;
			if ((sceButtons & SceInput.GamePadButtons.Left) > 0)
				result |= Buttons.DPadLeft;
			if ((sceButtons & SceInput.GamePadButtons.Right) > 0)
				result |= Buttons.DPadRight;

			if ((sceButtons & SceInput.GamePadButtons.Start) > 0)
				result |= Buttons.Start;
			if ((sceButtons & SceInput.GamePadButtons.Back) > 0)
				result |= Buttons.Back;
			if ((sceButtons & SceInput.GamePadButtons.L) > 0)
				result |= Buttons.LeftShoulder;
			if ((sceButtons & SceInput.GamePadButtons.R) > 0)
				result |= Buttons.RightShoulder;

			if ((sceButtons & SceInput.GamePadButtons.Triangle) > 0)
				result |= Buttons.Y;
			if ((sceButtons & SceInput.GamePadButtons.Square) > 0)
				result |= Buttons.X;
			if ((sceButtons & SceInput.GamePadButtons.Circle) > 0)
				result |= Buttons.B;
			if ((sceButtons & SceInput.GamePadButtons.Cross) > 0)
				result |= Buttons.A;

			// TODO: think about this mapping
			if ((sceButtons & SceInput.GamePadButtons.Select) > 0)
				result |= Buttons.BigButton;

			if ((sceButtons & SceInput.GamePadButtons.Enter) > 0)
			{
				var enterMeaning = SystemParameters.GamePadButtonMeaning;
				if (enterMeaning == GamePadButtonMeaning.CircleIsEnter)
					result |= Buttons.B;
				else
					result |= Buttons.A;
			}

			return result;
		}
	}
}
