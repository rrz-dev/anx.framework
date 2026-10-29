using System.Collections.Generic;
using ANX.Framework.Input;
using ANX.Framework.NonXNA.Development;
using SharpDX.XInput;
using Windows.System;

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
        private static Dictionary<VirtualKey, Keys> keyboardKeyMap;

        static FormatConverter()
        {
            CreateGamePadButtonMap();
            CreateKeyboardKeyMap();
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

        #region CreateKeyboardKeyMap
        private static void CreateKeyboardKeyMap()
        {
            keyboardKeyMap = new Dictionary<VirtualKey, Keys>();
            keyboardKeyMap.Add(VirtualKey.A, Keys.A);
            keyboardKeyMap.Add(VirtualKey.Accept, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Add, Keys.Add);
            keyboardKeyMap.Add(VirtualKey.Application, Keys.Apps);
            keyboardKeyMap.Add(VirtualKey.B, Keys.B);
            keyboardKeyMap.Add(VirtualKey.Back, Keys.Back);
            keyboardKeyMap.Add(VirtualKey.C, Keys.C);
            keyboardKeyMap.Add(VirtualKey.Cancel, Keys.None);
            keyboardKeyMap.Add(VirtualKey.CapitalLock, Keys.CapsLock);
            keyboardKeyMap.Add(VirtualKey.Clear, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Control, Keys.Crsel);
            keyboardKeyMap.Add(VirtualKey.Convert, Keys.ImeConvert);
            keyboardKeyMap.Add(VirtualKey.D, Keys.D);
            keyboardKeyMap.Add(VirtualKey.Decimal, Keys.Decimal);
            keyboardKeyMap.Add(VirtualKey.Delete, Keys.Delete);
            keyboardKeyMap.Add(VirtualKey.Divide, Keys.Divide);
            keyboardKeyMap.Add(VirtualKey.Down, Keys.Down);
            keyboardKeyMap.Add(VirtualKey.E, Keys.E);
            keyboardKeyMap.Add(VirtualKey.End, Keys.End);
            keyboardKeyMap.Add(VirtualKey.Enter, Keys.Enter);
            keyboardKeyMap.Add(VirtualKey.Escape, Keys.Escape);
            keyboardKeyMap.Add(VirtualKey.Execute, Keys.Execute);
            keyboardKeyMap.Add(VirtualKey.F, Keys.F);
            keyboardKeyMap.Add(VirtualKey.F1, Keys.F1);
            keyboardKeyMap.Add(VirtualKey.F10, Keys.F10);
            keyboardKeyMap.Add(VirtualKey.F11, Keys.F11);
            keyboardKeyMap.Add(VirtualKey.F12, Keys.F12);
            keyboardKeyMap.Add(VirtualKey.F13, Keys.F13);
            keyboardKeyMap.Add(VirtualKey.F14, Keys.F14);
            keyboardKeyMap.Add(VirtualKey.F15, Keys.F15);
            keyboardKeyMap.Add(VirtualKey.F16, Keys.F16);
            keyboardKeyMap.Add(VirtualKey.F17, Keys.F17);
            keyboardKeyMap.Add(VirtualKey.F18, Keys.F18);
            keyboardKeyMap.Add(VirtualKey.F19, Keys.F19);
            keyboardKeyMap.Add(VirtualKey.F20, Keys.F20);
            keyboardKeyMap.Add(VirtualKey.F21, Keys.F21);
            keyboardKeyMap.Add(VirtualKey.F22, Keys.F22);
            keyboardKeyMap.Add(VirtualKey.F23, Keys.F23);
            keyboardKeyMap.Add(VirtualKey.F24, Keys.F24);
            keyboardKeyMap.Add(VirtualKey.F3, Keys.F3);
            keyboardKeyMap.Add(VirtualKey.F4, Keys.F4);
            keyboardKeyMap.Add(VirtualKey.F5, Keys.F5);
            keyboardKeyMap.Add(VirtualKey.F6, Keys.F6);
            keyboardKeyMap.Add(VirtualKey.F7, Keys.F7);
            keyboardKeyMap.Add(VirtualKey.F8, Keys.F8);
            keyboardKeyMap.Add(VirtualKey.F9, Keys.F9);
            keyboardKeyMap.Add(VirtualKey.F2, Keys.F2);
            keyboardKeyMap.Add(VirtualKey.Final, Keys.None);
            keyboardKeyMap.Add(VirtualKey.G, Keys.G);
            keyboardKeyMap.Add(VirtualKey.H, Keys.H);
            keyboardKeyMap.Add(VirtualKey.Hangul, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Hanja, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Help, Keys.Help);
            keyboardKeyMap.Add(VirtualKey.Home, Keys.Home);
            keyboardKeyMap.Add(VirtualKey.I, Keys.I);
            keyboardKeyMap.Add(VirtualKey.Insert, Keys.Insert);
            keyboardKeyMap.Add(VirtualKey.J, Keys.J);
            keyboardKeyMap.Add(VirtualKey.Junja, Keys.None);
            keyboardKeyMap.Add(VirtualKey.K, Keys.K);
            //keyboardKeyMap.Add(VirtualKey.Kana, Keys.Kana);
            //keyboardKeyMap.Add(VirtualKey.Kanji, Keys.Kanji);
            keyboardKeyMap.Add(VirtualKey.L, Keys.L);
            keyboardKeyMap.Add(VirtualKey.Left, Keys.Left);
            keyboardKeyMap.Add(VirtualKey.LeftButton, Keys.None);
            keyboardKeyMap.Add(VirtualKey.LeftControl, Keys.LeftControl);
            keyboardKeyMap.Add(VirtualKey.LeftMenu, Keys.None);
            keyboardKeyMap.Add(VirtualKey.LeftShift, Keys.LeftShift);
            keyboardKeyMap.Add(VirtualKey.LeftWindows, Keys.LeftWindows);
            keyboardKeyMap.Add(VirtualKey.M, Keys.M);
            keyboardKeyMap.Add(VirtualKey.Menu, Keys.None);
            keyboardKeyMap.Add(VirtualKey.MiddleButton, Keys.None);
            keyboardKeyMap.Add(VirtualKey.ModeChange, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Multiply, Keys.Multiply);
            keyboardKeyMap.Add(VirtualKey.N, Keys.N);
            keyboardKeyMap.Add(VirtualKey.NonConvert, Keys.ImeNoConvert);
            keyboardKeyMap.Add(VirtualKey.None, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Number0, Keys.NumPad0);
            keyboardKeyMap.Add(VirtualKey.Number1, Keys.NumPad1);
            keyboardKeyMap.Add(VirtualKey.Number2, Keys.NumPad2);
            keyboardKeyMap.Add(VirtualKey.Number3, Keys.NumPad3);
            keyboardKeyMap.Add(VirtualKey.Number4, Keys.NumPad4);
            keyboardKeyMap.Add(VirtualKey.Number5, Keys.NumPad5);
            keyboardKeyMap.Add(VirtualKey.Number6, Keys.NumPad6);
            keyboardKeyMap.Add(VirtualKey.Number7, Keys.NumPad7);
            keyboardKeyMap.Add(VirtualKey.Number8, Keys.NumPad8);
            keyboardKeyMap.Add(VirtualKey.Number9, Keys.NumPad9);
            keyboardKeyMap.Add(VirtualKey.NumberKeyLock, Keys.NumLock);
            keyboardKeyMap.Add(VirtualKey.NumberPad0, Keys.NumPad0);
            keyboardKeyMap.Add(VirtualKey.NumberPad1, Keys.NumPad1);
            keyboardKeyMap.Add(VirtualKey.NumberPad2, Keys.NumPad2);
            keyboardKeyMap.Add(VirtualKey.NumberPad3, Keys.NumPad3);
            keyboardKeyMap.Add(VirtualKey.NumberPad4, Keys.NumPad4);
            keyboardKeyMap.Add(VirtualKey.NumberPad5, Keys.NumPad5);
            keyboardKeyMap.Add(VirtualKey.NumberPad6, Keys.NumPad6);
            keyboardKeyMap.Add(VirtualKey.NumberPad7, Keys.NumPad7);
            keyboardKeyMap.Add(VirtualKey.NumberPad8, Keys.NumPad8);
            keyboardKeyMap.Add(VirtualKey.NumberPad9, Keys.NumPad9);
            keyboardKeyMap.Add(VirtualKey.O, Keys.O);
            keyboardKeyMap.Add(VirtualKey.P, Keys.P);
            keyboardKeyMap.Add(VirtualKey.PageDown, Keys.PageDown);
            keyboardKeyMap.Add(VirtualKey.PageUp, Keys.PageUp);
            keyboardKeyMap.Add(VirtualKey.Pause, Keys.Pause);
            keyboardKeyMap.Add(VirtualKey.Print, Keys.Print);
            keyboardKeyMap.Add(VirtualKey.Q, Keys.Q);
            keyboardKeyMap.Add(VirtualKey.R, Keys.R);
            keyboardKeyMap.Add(VirtualKey.Right, Keys.Right);
            keyboardKeyMap.Add(VirtualKey.RightButton, Keys.None);
            keyboardKeyMap.Add(VirtualKey.RightControl, Keys.RightControl);
            keyboardKeyMap.Add(VirtualKey.RightMenu, Keys.None);
            keyboardKeyMap.Add(VirtualKey.RightShift, Keys.RightShift);
            keyboardKeyMap.Add(VirtualKey.RightWindows, Keys.RightWindows);
            keyboardKeyMap.Add(VirtualKey.S, Keys.S);
            keyboardKeyMap.Add(VirtualKey.Scroll, Keys.Scroll);
            keyboardKeyMap.Add(VirtualKey.Select, Keys.Select);
            keyboardKeyMap.Add(VirtualKey.Separator, Keys.Separator);
            keyboardKeyMap.Add(VirtualKey.Shift, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Sleep, Keys.Sleep);
            keyboardKeyMap.Add(VirtualKey.Snapshot, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Space, Keys.Space);
            keyboardKeyMap.Add(VirtualKey.Subtract, Keys.Subtract);
            keyboardKeyMap.Add(VirtualKey.T, Keys.T);
            keyboardKeyMap.Add(VirtualKey.Tab, Keys.Tab);
            keyboardKeyMap.Add(VirtualKey.U, Keys.U);
            keyboardKeyMap.Add(VirtualKey.Up, Keys.Up);
            keyboardKeyMap.Add(VirtualKey.V, Keys.V);
            keyboardKeyMap.Add(VirtualKey.W, Keys.W);
            keyboardKeyMap.Add(VirtualKey.X, Keys.X);
            keyboardKeyMap.Add(VirtualKey.XButton1, Keys.None);
            keyboardKeyMap.Add(VirtualKey.XButton2, Keys.None);
            keyboardKeyMap.Add(VirtualKey.Y, Keys.Y);
            keyboardKeyMap.Add(VirtualKey.Z, Keys.Z);
        }
        #endregion

        #region Translate (VirtualKey)
        public static Keys Translate(VirtualKey key)
        {
            return keyboardKeyMap.ContainsKey(key) ? keyboardKeyMap[key] : Keys.None;
        }
        #endregion

    }
}

