using System.Collections.Generic;
using ANX.Framework.Input;
using ANX.Framework.NonXNA.Development;
using SharpDX.XInput;
using Key = SharpDX.DirectInput.Key;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
	[PercentageComplete(80)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
	internal static class FormatConverter
	{
		private static Dictionary<GamepadButtonFlags, Buttons> gamePadButtonsMap;
		private static Dictionary<Key, Keys> keyboardKeyMap;

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

		#region CreateKeyboardKeyMap
		private static void CreateKeyboardKeyMap()
		{
			keyboardKeyMap = new Dictionary<Key, Keys>();
			keyboardKeyMap.Add(Key.Unknown, Keys.None);
			keyboardKeyMap.Add(Key.Escape, Keys.Escape);
			keyboardKeyMap.Add(Key.D1, Keys.D1);
			keyboardKeyMap.Add(Key.D2, Keys.D2);
			keyboardKeyMap.Add(Key.D3, Keys.D3);
			keyboardKeyMap.Add(Key.D4, Keys.D4);
			keyboardKeyMap.Add(Key.D5, Keys.D5);
			keyboardKeyMap.Add(Key.D6, Keys.D6);
			keyboardKeyMap.Add(Key.D7, Keys.D7);
			keyboardKeyMap.Add(Key.D8, Keys.D8);
			keyboardKeyMap.Add(Key.D9, Keys.D9);
			keyboardKeyMap.Add(Key.D0, Keys.D0);
			keyboardKeyMap.Add(Key.Minus, Keys.OemMinus);
			keyboardKeyMap.Add(Key.Back, Keys.Back);
			keyboardKeyMap.Add(Key.Tab, Keys.Tab);
			keyboardKeyMap.Add(Key.Q, Keys.Q);
			keyboardKeyMap.Add(Key.W, Keys.W);
			keyboardKeyMap.Add(Key.E, Keys.E);
			keyboardKeyMap.Add(Key.R, Keys.R);
			keyboardKeyMap.Add(Key.T, Keys.T);
			keyboardKeyMap.Add(Key.Y, Keys.Y);
			keyboardKeyMap.Add(Key.U, Keys.U);
			keyboardKeyMap.Add(Key.I, Keys.I);
			keyboardKeyMap.Add(Key.O, Keys.O);
			keyboardKeyMap.Add(Key.P, Keys.P);
			keyboardKeyMap.Add(Key.LeftBracket, Keys.OemOpenBrackets);
			keyboardKeyMap.Add(Key.RightBracket, Keys.OemCloseBrackets);
			keyboardKeyMap.Add(Key.Return, Keys.Enter);
			keyboardKeyMap.Add(Key.LeftControl, Keys.LeftControl);
			keyboardKeyMap.Add(Key.A, Keys.A);
			keyboardKeyMap.Add(Key.S, Keys.S);
			keyboardKeyMap.Add(Key.D, Keys.D);
			keyboardKeyMap.Add(Key.F, Keys.F);
			keyboardKeyMap.Add(Key.G, Keys.G);
			keyboardKeyMap.Add(Key.H, Keys.H);
			keyboardKeyMap.Add(Key.J, Keys.J);
			keyboardKeyMap.Add(Key.K, Keys.K);
			keyboardKeyMap.Add(Key.L, Keys.L);
			keyboardKeyMap.Add(Key.Semicolon, Keys.OemSemicolon);
			keyboardKeyMap.Add(Key.LeftShift, Keys.LeftShift);
			keyboardKeyMap.Add(Key.Backslash, Keys.OemBackslash);
			keyboardKeyMap.Add(Key.Z, Keys.Z);
			keyboardKeyMap.Add(Key.X, Keys.X);
			keyboardKeyMap.Add(Key.C, Keys.C);
			keyboardKeyMap.Add(Key.V, Keys.V);
			keyboardKeyMap.Add(Key.B, Keys.B);
			keyboardKeyMap.Add(Key.N, Keys.N);
			keyboardKeyMap.Add(Key.M, Keys.M);
			keyboardKeyMap.Add(Key.Comma, Keys.OemComma);
			keyboardKeyMap.Add(Key.Period, Keys.OemPeriod);
			keyboardKeyMap.Add(Key.RightShift, Keys.RightShift);
			keyboardKeyMap.Add(Key.Multiply, Keys.Multiply);
			keyboardKeyMap.Add(Key.LeftAlt, Keys.LeftAlt);
			keyboardKeyMap.Add(Key.Space, Keys.Space);
			keyboardKeyMap.Add(Key.Capital, Keys.CapsLock);
			keyboardKeyMap.Add(Key.F1, Keys.F1);
			keyboardKeyMap.Add(Key.F2, Keys.F2);
			keyboardKeyMap.Add(Key.F3, Keys.F3);
			keyboardKeyMap.Add(Key.F4, Keys.F4);
			keyboardKeyMap.Add(Key.F5, Keys.F5);
			keyboardKeyMap.Add(Key.F6, Keys.F6);
			keyboardKeyMap.Add(Key.F7, Keys.F7);
			keyboardKeyMap.Add(Key.F8, Keys.F8);
			keyboardKeyMap.Add(Key.F9, Keys.F9);
			keyboardKeyMap.Add(Key.F10, Keys.F10);
			keyboardKeyMap.Add(Key.NumberLock, Keys.NumLock);
			keyboardKeyMap.Add(Key.ScrollLock, Keys.Scroll);
			keyboardKeyMap.Add(Key.NumberPad7, Keys.NumPad7);
			keyboardKeyMap.Add(Key.NumberPad8, Keys.NumPad8);
			keyboardKeyMap.Add(Key.NumberPad9, Keys.NumPad9);
			keyboardKeyMap.Add(Key.Subtract, Keys.Subtract);
			keyboardKeyMap.Add(Key.NumberPad4, Keys.NumPad4);
			keyboardKeyMap.Add(Key.NumberPad5, Keys.NumPad5);
			keyboardKeyMap.Add(Key.NumberPad6, Keys.NumPad6);
			keyboardKeyMap.Add(Key.Add, Keys.Add);
			keyboardKeyMap.Add(Key.NumberPad1, Keys.NumPad1);
			keyboardKeyMap.Add(Key.NumberPad2, Keys.NumPad2);
			keyboardKeyMap.Add(Key.NumberPad3, Keys.NumPad3);
			keyboardKeyMap.Add(Key.NumberPad0, Keys.NumPad0);
			keyboardKeyMap.Add(Key.Decimal, Keys.Decimal);
			keyboardKeyMap.Add(Key.F11, Keys.F11);
			keyboardKeyMap.Add(Key.F12, Keys.F12);
			keyboardKeyMap.Add(Key.F13, Keys.F13);
			keyboardKeyMap.Add(Key.F14, Keys.F14);
			keyboardKeyMap.Add(Key.F15, Keys.F15);
			keyboardKeyMap.Add(Key.Kana, Keys.Kana);
			keyboardKeyMap.Add(Key.Convert, Keys.ImeConvert);
			keyboardKeyMap.Add(Key.NoConvert, Keys.ImeNoConvert);
			keyboardKeyMap.Add(Key.PreviousTrack, Keys.MediaPreviousTrack);
			keyboardKeyMap.Add(Key.Kanji, Keys.Kanji);
			keyboardKeyMap.Add(Key.Stop, Keys.MediaStop);
			keyboardKeyMap.Add(Key.NextTrack, Keys.MediaNextTrack);
			keyboardKeyMap.Add(Key.NumberPadEnter, Keys.Enter);
			keyboardKeyMap.Add(Key.RightControl, Keys.RightControl);
			keyboardKeyMap.Add(Key.Mute, Keys.VolumeMute);
			keyboardKeyMap.Add(Key.PlayPause, Keys.MediaPlayPause);
			keyboardKeyMap.Add(Key.MediaStop, Keys.MediaStop);
			keyboardKeyMap.Add(Key.VolumeDown, Keys.VolumeDown);
			keyboardKeyMap.Add(Key.VolumeUp, Keys.VolumeUp);
			keyboardKeyMap.Add(Key.WebHome, Keys.BrowserHome);
			keyboardKeyMap.Add(Key.NumberPadComma, Keys.OemComma);
			keyboardKeyMap.Add(Key.Divide, Keys.Divide);
			keyboardKeyMap.Add(Key.PrintScreen, Keys.PrintScreen);
			keyboardKeyMap.Add(Key.RightAlt, Keys.RightAlt);
			keyboardKeyMap.Add(Key.Pause, Keys.Pause);
			keyboardKeyMap.Add(Key.Home, Keys.Home);
			keyboardKeyMap.Add(Key.UpArrow, Keys.Up);
			keyboardKeyMap.Add(Key.PageUp, Keys.PageUp);
			keyboardKeyMap.Add(Key.Left, Keys.Left);
			keyboardKeyMap.Add(Key.Right, Keys.Right);
			keyboardKeyMap.Add(Key.End, Keys.End);
			keyboardKeyMap.Add(Key.Down, Keys.Down);
			keyboardKeyMap.Add(Key.PageDown, Keys.PageDown);
			keyboardKeyMap.Add(Key.Insert, Keys.Insert);
			keyboardKeyMap.Add(Key.Delete, Keys.Delete);
			keyboardKeyMap.Add(Key.LeftWindowsKey, Keys.LeftWindows);
			keyboardKeyMap.Add(Key.RightWindowsKey, Keys.RightWindows);
			keyboardKeyMap.Add(Key.Applications, Keys.Apps);
			keyboardKeyMap.Add(Key.Sleep, Keys.Sleep);
			keyboardKeyMap.Add(Key.WebSearch, Keys.BrowserSearch);
			keyboardKeyMap.Add(Key.WebFavorites, Keys.BrowserFavorites);
			keyboardKeyMap.Add(Key.WebRefresh, Keys.BrowserRefresh);
			keyboardKeyMap.Add(Key.WebStop, Keys.BrowserStop);
			keyboardKeyMap.Add(Key.WebForward, Keys.BrowserForward);
			keyboardKeyMap.Add(Key.WebBack, Keys.BrowserBack);
			keyboardKeyMap.Add(Key.Mail, Keys.LaunchMail);
			keyboardKeyMap.Add(Key.MediaSelect, Keys.SelectMedia);
			keyboardKeyMap.Add(Key.Slash, Keys.OemMinus);

			// TODO
			keyboardKeyMap.Add(Key.AbntC1, Keys.None);
			keyboardKeyMap.Add(Key.Yen, Keys.None);
			keyboardKeyMap.Add(Key.AbntC2, Keys.None);
			keyboardKeyMap.Add(Key.NumberPadEquals, Keys.None);
			keyboardKeyMap.Add(Key.AT, Keys.None);
			keyboardKeyMap.Add(Key.Colon, Keys.None);
			keyboardKeyMap.Add(Key.Underline, Keys.None);
			keyboardKeyMap.Add(Key.AX, Keys.None);
			keyboardKeyMap.Add(Key.Unlabeled, Keys.None);
			keyboardKeyMap.Add(Key.Calculator, Keys.None);
			keyboardKeyMap.Add(Key.Power, Keys.None);
			keyboardKeyMap.Add(Key.Wake, Keys.None);
			keyboardKeyMap.Add(Key.MyComputer, Keys.None);
			keyboardKeyMap.Add(Key.Equals, Keys.None);
			keyboardKeyMap.Add(Key.Apostrophe, Keys.None);
			keyboardKeyMap.Add(Key.Grave, Keys.None);
			keyboardKeyMap.Add(Key.Oem102, Keys.None);

			// Xna keys not mapped:
			// Select
			// Print
			// Execute
			// Help
			// Separator
			// F16
			// F17
			// F18
			// F19
			// F20
			// F21
			// F22
			// F23
			// F24
			// LaunchApplication1
			// LaunchApplication2
			// OemPlus
			// OemQuestion
			// OemTilde
			// ChatPadGreen
			// ChatPadOrange
			// OemPipe
			// OemQuotes
			// Oem8
			// ProcessKey
			// OemCopy
			// OemAuto
			// OemEnlW
			// Attn
			// Crsel
			// Exsel
			// EraseEof
			// Play
			// Zoom
			// Pa1
			// OemClear
		}
		#endregion

		#region Translate (Key)
		public static Keys Translate(Key key)
		{
			return keyboardKeyMap.ContainsKey(key) ? keyboardKeyMap[key] : Keys.None;
		}
		#endregion

		#region Translate (GamepadButtonFlags)
		public static Buttons Translate(SharpDX.XInput.GamepadButtonFlags buttons)
		{
			Buttons tb = 0;
			foreach(var key in gamePadButtonsMap.Keys)
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
                case DeviceSubType.FlightSick:
					return GamePadType.FlightStick;
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
