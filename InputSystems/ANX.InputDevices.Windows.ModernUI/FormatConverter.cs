using System.Collections.Generic;
using ANX.Framework.Input;
using ANX.Framework.NonXNA.Development;
using SharpDX.XInput;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
namespace ANX.InputDevices.Windows.ModernUI
{
    [PercentageComplete(80)]
    [TestState(TestStateAttribute.TestState.InProgress)]
    [Developer("rene87")]
    internal static class FormatConverter
    {
        private static Dictionary<GamepadButtonFlags, Buttons> gamePadButtonsMap;

        static FormatConverter()
        {
            CreateGamePadButtonMap();
        }

        #region CreateGamePadButtonMap
        private static void CreateGamePadButtonMap()
        {
            gamePadButtonsMap = new Dictionary<GamepadButtonFlags, Buttons>();
            gamePadButtonsMap.Add(GamepadButtonFlags.A, Buttons.A);
            gamePadButtonsMap.Add(GamepadButtonFlags.B, Buttons.B);
            gamePadButtonsMap.Add(GamepadButtonFlags.X, Buttons.X);
            gamePadButtonsMap.Add(GamepadButtonFlags.Y, Buttons.Y);
            gamePadButtonsMap.Add(GamepadButtonFlags.Back, Buttons.Back);
            gamePadButtonsMap.Add(GamepadButtonFlags.Start, Buttons.Start);
            gamePadButtonsMap.Add(GamepadButtonFlags.DPadDown, Buttons.DPadDown);
            gamePadButtonsMap.Add(GamepadButtonFlags.DPadLeft, Buttons.DPadLeft);
            gamePadButtonsMap.Add(GamepadButtonFlags.DPadRight, Buttons.DPadRight);
            gamePadButtonsMap.Add(GamepadButtonFlags.DPadUp, Buttons.DPadUp);
            gamePadButtonsMap.Add(GamepadButtonFlags.LeftShoulder, Buttons.LeftShoulder);
            gamePadButtonsMap.Add(GamepadButtonFlags.LeftThumb, Buttons.LeftStick);
            gamePadButtonsMap.Add(GamepadButtonFlags.RightShoulder, Buttons.RightShoulder);
            gamePadButtonsMap.Add(GamepadButtonFlags.RightThumb, Buttons.RightStick);

            // TODO: xna supports more than sharpdx it seems. Missing: l/r trigger, big, l/r/u/d thumbsticks
        }
        #endregion

        #region Translate (GamepadButtonFlags)
        public static Buttons Translate(SharpDX.XInput.GamepadButtonFlags buttons)
        {
            Buttons tb = 0;
            foreach (var key in gamePadButtonsMap.Keys)
                tb |= (buttons & key) == key ? gamePadButtonsMap[key] : 0;

            return tb;
        }
        #endregion

        #region Translate (DeviceSubType)
        public static GamePadType Translate(SharpDX.XInput.DeviceSubType type)
        {
            switch (type)
            {
                case DeviceSubType.ArcadeStick:
                    return GamePadType.ArcadeStick;
                case DeviceSubType.DancePad:
                    return GamePadType.DancePad;
                case DeviceSubType.DrumKit:
                    return GamePadType.DrumKit;
                case DeviceSubType.Gamepad:
                    return GamePadType.GamePad;
                case DeviceSubType.Guitar:
                    return GamePadType.Guitar;
                case DeviceSubType.Wheel:
                    return GamePadType.Wheel;
            }

            return GamePadType.Unknown;
        }
        #endregion

    }
}

