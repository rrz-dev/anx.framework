using System;
using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using SharpDX.XInput;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
	[PercentageComplete(90)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
	public class GamePad : IGamePad
	{
		#region Private
		private Controller[] controller;
		private const float thumbstickRangeFactor = 1f / short.MaxValue;
		private const float triggerRangeFactor = 1f / byte.MaxValue;
		private GamePadCapabilities emptyCaps;
		private GamePadState emptyState;
		#endregion

		public GamePad()
		{
			controller = new Controller[4];
			for (int index = 0; index < controller.Length; index++)
				controller[index] = new Controller((UserIndex)index);
		}

		public GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
		{
			try
			{
				Capabilities nativeCaps = controller[(int)playerIndex].GetCapabilities(DeviceQueryType.Gamepad);
				return new GamePadCapabilities()
				{
					GamePadType = FormatConverter.Translate(nativeCaps.SubType),
					IsConnected = controller[(int)playerIndex].IsConnected,
					HasAButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.A) != 0,
					HasBackButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.Back) != 0,
					HasBButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.B) != 0,
					HasBigButton = false, // TODO
					HasDPadDownButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.DPadDown) != 0,
					HasDPadLeftButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.DPadLeft) != 0,
					HasDPadRightButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.DPadRight) != 0,
					HasDPadUpButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.DPadUp) != 0,
					HasLeftShoulderButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.LeftShoulder) != 0,
					HasRightShoulderButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.RightShoulder) != 0,
					HasLeftStickButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.LeftThumb) != 0,
					HasRightStickButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.RightThumb) != 0,
					HasStartButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.Start) != 0,
					HasXButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.X) != 0,
					HasYButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.Y) != 0,
					HasLeftVibrationMotor = false,
					HasRightVibrationMotor = false, // TODO
					HasVoiceSupport = false, // TODO
					HasRightXThumbStick = false, // TODO
					HasRightYThumbStick = false, // TODO
					HasLeftXThumbStick = false, // TODO
					HasLeftYThumbStick = false, // TODO
					HasLeftTrigger = false, // TODO
					HasRightTrigger = false, // TODO
				};
			}
			catch
			{
				return emptyCaps;
			}
		}

		public GamePadState GetState(PlayerIndex playerIndex, out bool isConnected, out int packetNumber)
		{
			isConnected = controller[(int)playerIndex].IsConnected;
			if (isConnected == false)
			{
				packetNumber = 0;
				return emptyState;
			}

			State nativeState = controller[(int)playerIndex].GetState();
			var result = new GamePadState(
				new Vector2(nativeState.Gamepad.LeftThumbX, nativeState.Gamepad.LeftThumbY) * thumbstickRangeFactor,
				new Vector2(nativeState.Gamepad.RightThumbX, nativeState.Gamepad.RightThumbY) * thumbstickRangeFactor,
				nativeState.Gamepad.LeftTrigger * triggerRangeFactor, nativeState.Gamepad.RightTrigger * triggerRangeFactor,
				FormatConverter.Translate(nativeState.Gamepad.Buttons));

			packetNumber = nativeState.PacketNumber;
			return result;
		}

		public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode, out bool isConnected,
			out int packetNumber)
		{
			// TODO: deadZoneMode

			isConnected = controller[(int)playerIndex].IsConnected;
			if (isConnected == false)
			{
				packetNumber = 0;
				return emptyState;
			}

			State nativeState = controller[(int)playerIndex].GetState();
			Vector2 leftThumb = ConvertThumbStick(nativeState.Gamepad.LeftThumbX, nativeState.Gamepad.LeftThumbY,
				SharpDX.XInput.Gamepad.LeftThumbDeadZone, deadZoneMode);
			Vector2 rightThumb = ConvertThumbStick(nativeState.Gamepad.RightThumbX, nativeState.Gamepad.RightThumbY,
				SharpDX.XInput.Gamepad.LeftThumbDeadZone, deadZoneMode);

			var result = new GamePadState(leftThumb, rightThumb, nativeState.Gamepad.LeftTrigger * triggerRangeFactor,
				nativeState.Gamepad.RightTrigger * triggerRangeFactor, FormatConverter.Translate(nativeState.Gamepad.Buttons));

			packetNumber = nativeState.PacketNumber;
			return result;
		}

		public bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			if (controller[(int)playerIndex].IsConnected == false)
				return false;

			var vib = new Vibration()
			{
				LeftMotorSpeed = (short)((Math.Abs(leftMotor) > 1) ? 1 : Math.Abs(leftMotor) * short.MaxValue),
				RightMotorSpeed = (short)((Math.Abs(rightMotor) > 1) ? 1 : Math.Abs(rightMotor) * short.MaxValue),
			};
			controller[(int)playerIndex].SetVibration(vib);

			return true;
		}

		private Vector2 ConvertThumbStick(int x, int y, int deadZone, GamePadDeadZone deadZoneMode)
		{
			int deadZoneSquare = deadZone * deadZone;
			if (deadZoneMode == GamePadDeadZone.IndependentAxes)
			{
				if (x * x < deadZoneSquare)
					x = 0;
				if (y * y < deadZoneSquare)
					y = 0;
			}
			else if (deadZoneMode == GamePadDeadZone.Circular)
			{
				if ((x * x) + (y * y) < deadZoneSquare)
				{
					x = 0;
					y = 0;
				}
			}

			return new Vector2(x < 0 ? -((float)x / (float)short.MinValue) : (float)x / (float)short.MaxValue,
				y < 0 ? -((float)y / (float)short.MinValue) : (float)y / (float)short.MaxValue);
		}
	}
}
