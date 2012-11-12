using System;
using ANX.Framework;
using ANX.Framework.Input;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;
using SharpDX.XInput;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
	public class GamePad : IGamePad
	{
		#region Constants
		private const int LeftThumbDeadZoneSquare = 7849 * 7849;
		private const int RightThumbDeadZoneSquare = 8689 * 8689;
		#endregion

		#region Private
		private Controller[] controller;
		private const float triggerRangeFactor = 1f / byte.MaxValue;
		private GamePadCapabilities emptyCaps = new GamePadCapabilities();
		private GamePadState emptyState = new GamePadState();
		#endregion

		#region Constructor
		public GamePad()
		{
			controller = new Controller[4];
            for (int index = 0; index < controller.Length; index++)
            {
                controller[index] = new Controller((UserIndex)index);

                try
                {
                    bool isConnected = controller[index].IsConnected;
                }
                catch (System.DllNotFoundException ex)
                {
                    controller[index] = null;
                    Logger.Warning("couldn't initialize GamePad " + index + " because " + ex.Message);
                }
            }
		}
		#endregion

		#region GetCapabilities
		public GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
		{
			var gamepad = controller[(int)playerIndex];
			if (gamepad == null || gamepad.IsConnected == false)
				return emptyCaps;

			try
			{
				Capabilities nativeCaps = gamepad.GetCapabilities(DeviceQueryType.Gamepad);
				return new GamePadCapabilities()
				{
					GamePadType = FormatConverter.Translate(nativeCaps.SubType),
					IsConnected = gamepad.IsConnected,
					HasAButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.A) != 0,
					HasBackButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.Back) != 0,
					HasBButton = (nativeCaps.Gamepad.Buttons & GamepadButtonFlags.B) != 0,
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
					HasLeftVibrationMotor = nativeCaps.Vibration.LeftMotorSpeed != 0,
					HasRightVibrationMotor = nativeCaps.Vibration.RightMotorSpeed != 0,
					HasVoiceSupport = (nativeCaps.Flags & CapabilityFlags.VoiceSupported) != 0,
					HasRightXThumbStick = nativeCaps.Gamepad.RightThumbX != 0,
					HasRightYThumbStick = nativeCaps.Gamepad.RightThumbY != 0,
					HasLeftXThumbStick = nativeCaps.Gamepad.LeftThumbX != 0,
					HasLeftYThumbStick = nativeCaps.Gamepad.LeftThumbY != 0,
					HasLeftTrigger = nativeCaps.Gamepad.LeftTrigger > 0,
					HasRightTrigger = nativeCaps.Gamepad.RightTrigger > 0,

					// Impossible to check
					HasBigButton = false,
				};
			}
			catch (Exception ex)
			{
				Logger.Info("Failed to get caps for gamepad " + playerIndex + ": " + ex);
				return emptyCaps;
			}
		}
		#endregion

		#region GetState
		public GamePadState GetState(PlayerIndex playerIndex)
		{
			return GetState(playerIndex, GamePadDeadZone.None);
		}

		public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
		{
            var controller = this.controller[(int)playerIndex];
            if (controller == null) return new GamePadState();

			bool isConnected = controller.IsConnected;
			if (isConnected == false)
				return emptyState;

			State nativeState = controller.GetState();
			Vector2 leftThumb = ApplyDeadZone(nativeState.Gamepad.LeftThumbX, nativeState.Gamepad.LeftThumbY,
				LeftThumbDeadZoneSquare, deadZoneMode);
			Vector2 rightThumb = ApplyDeadZone(nativeState.Gamepad.RightThumbX, nativeState.Gamepad.RightThumbY,
				RightThumbDeadZoneSquare, deadZoneMode);

			return new GamePadState(leftThumb, rightThumb, nativeState.Gamepad.LeftTrigger * triggerRangeFactor,
				nativeState.Gamepad.RightTrigger * triggerRangeFactor, FormatConverter.Translate(nativeState.Gamepad.Buttons))
				{
					PacketNumber = nativeState.PacketNumber,
					IsConnected = isConnected
				};
		}
		#endregion

		#region SetVibration
		public bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			if (controller[(int)playerIndex] == null || controller[(int)playerIndex].IsConnected == false)
				return false;

			var vib = new Vibration()
			{
				LeftMotorSpeed = (short)(Math.Min(Math.Abs(leftMotor), 1f) * short.MaxValue),
				RightMotorSpeed = (short)(Math.Min(Math.Abs(rightMotor), 1f) * short.MaxValue),
			};
			controller[(int)playerIndex].SetVibration(vib);

			return true;
		}
		#endregion

		#region ApplyDeadZone
		private Vector2 ApplyDeadZone(int x, int y, int deadZone, GamePadDeadZone deadZoneMode)
		{
			if (deadZoneMode != GamePadDeadZone.None)
			{
				int xSquare = x * x;
				int ySquare = y * y;
				if (deadZoneMode == GamePadDeadZone.IndependentAxes)
				{
					if (xSquare < deadZone)
						x = 0;
					if (ySquare < deadZone)
						y = 0;
				}
				else if (deadZoneMode == GamePadDeadZone.Circular && xSquare + ySquare < deadZone)
					x = y = 0;
			}

			float fx = x < 0 ? -(x / (float)short.MinValue) : x / (float)short.MaxValue;
			float fy = y < 0 ? -(y / (float)short.MinValue) : y / (float)short.MaxValue;
			return new Vector2(fx, fy);
		}
		#endregion
	}
}
