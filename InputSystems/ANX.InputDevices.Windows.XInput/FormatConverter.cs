#region Using Statements
using System;
using ANX.Framework.Input;
using SharpDX.XInput;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.XInput
{
    internal class FormatConverter
    {
        public static ANX.Framework.Input.Keys Translate(SharpDX.DirectInput.Key key)
        {
            //TODO: implement all keys
            //TODO: test if a array with the mapping which is constructed once is faster

            switch (key)
            {
                case SharpDX.DirectInput.Key.A:
                    return Keys.A;
                case SharpDX.DirectInput.Key.AT:
                    break;
                case SharpDX.DirectInput.Key.AX:
                    break;
                case SharpDX.DirectInput.Key.AbntC1:
                    break;
                case SharpDX.DirectInput.Key.AbntC2:
                    break;
                case SharpDX.DirectInput.Key.Add:
                        return Keys.Add;
                case SharpDX.DirectInput.Key.Apostrophe:
                    break;
                case SharpDX.DirectInput.Key.Applications:
                //check       
                return Keys.Apps;   
                case SharpDX.DirectInput.Key.B:
                     return Keys.B;
                case SharpDX.DirectInput.Key.Back:
                             return Keys.Back;
                case SharpDX.DirectInput.Key.Backslash:
                       return Keys.OemBackslash;
                case SharpDX.DirectInput.Key.C:
                     return Keys.C;
                case SharpDX.DirectInput.Key.Calculator:
                    break;
                case SharpDX.DirectInput.Key.Capital:
                  //check
                    return Keys.CapsLock;
                case SharpDX.DirectInput.Key.Colon:
                    break;
                case SharpDX.DirectInput.Key.Comma:
                  return Keys.OemComma;
                case SharpDX.DirectInput.Key.Convert:
                   return Keys.ImeConvert;
                case SharpDX.DirectInput.Key.D:
                     return Keys.D;
                case SharpDX.DirectInput.Key.D0:
                    return Keys.D0;
                case SharpDX.DirectInput.Key.D1:
                    return Keys.D1;
                case SharpDX.DirectInput.Key.D2:
 return Keys.D2;
                case SharpDX.DirectInput.Key.D3:
                    return Keys.D3;
                case SharpDX.DirectInput.Key.D4:
                    return Keys.D4;
                case SharpDX.DirectInput.Key.D5:
                   return Keys.D5;
                case SharpDX.DirectInput.Key.D6:
                   return Keys.D6;
                case SharpDX.DirectInput.Key.D7:
                   return Keys.D7;
                case SharpDX.DirectInput.Key.D8:
                    return Keys.D8;
                case SharpDX.DirectInput.Key.D9:
                     return Keys.D9;
                case SharpDX.DirectInput.Key.Decimal:
                    return Keys.Decimal;
                case SharpDX.DirectInput.Key.Delete:
                     return Keys.Delete;
                case SharpDX.DirectInput.Key.Divide:
                    return Keys.Divide;
                case SharpDX.DirectInput.Key.Down:
                    return Keys.Down;
                case SharpDX.DirectInput.Key.E:
                    return Keys.E;
                case SharpDX.DirectInput.Key.End:
                    return Keys.End;
                case SharpDX.DirectInput.Key.Equals:
                    break;
                case SharpDX.DirectInput.Key.Escape:
                   return Keys.Escape;
                case SharpDX.DirectInput.Key.F:
                   return Keys.F;
                case SharpDX.DirectInput.Key.F1:
                     return Keys.F1;
                case SharpDX.DirectInput.Key.F10:
                   return Keys.F10;
                case SharpDX.DirectInput.Key.F11:
                    return Keys.F11;
                case SharpDX.DirectInput.Key.F12:
                     return Keys.F12;
                case SharpDX.DirectInput.Key.F13:
                     return Keys.F13;
                case SharpDX.DirectInput.Key.F14:
                    return Keys.F14;
                case SharpDX.DirectInput.Key.F15:
                     return Keys.F15;
                case SharpDX.DirectInput.Key.F2:
                    return Keys.F2;
                case SharpDX.DirectInput.Key.F3:
                    return Keys.F3;
                case SharpDX.DirectInput.Key.F4:
                   return Keys.F4;
                case SharpDX.DirectInput.Key.F5:
                    return Keys.F5;
                case SharpDX.DirectInput.Key.F6:
                   return Keys.F6;
                case SharpDX.DirectInput.Key.F7:
                   return Keys.F7;
                case SharpDX.DirectInput.Key.F8:
                   return Keys.F8;
                case SharpDX.DirectInput.Key.F9:
                    return Keys.F9;
                case SharpDX.DirectInput.Key.G:
                  return Keys.G;
                case SharpDX.DirectInput.Key.Grave:
                    break;
                case SharpDX.DirectInput.Key.H:
                      return Keys.H;
                case SharpDX.DirectInput.Key.Home:
                       return Keys.Home;
                case SharpDX.DirectInput.Key.I:
                   return Keys.I;
                case SharpDX.DirectInput.Key.Insert:
                    return Keys.Insert;
                case SharpDX.DirectInput.Key.J:
                    return Keys.J;
                case SharpDX.DirectInput.Key.K:
                    return Keys.K;
                case SharpDX.DirectInput.Key.Kana:
                   return Keys.Kana;
                case SharpDX.DirectInput.Key.Kanji:
                   return Keys.Kanji;
                case SharpDX.DirectInput.Key.L:
                   return Keys.L;
                case SharpDX.DirectInput.Key.Left:
                     return Keys.Left;
                case SharpDX.DirectInput.Key.LeftAlt:
                   return Keys.LeftAlt;
                case SharpDX.DirectInput.Key.LeftBracket:
                     return Keys.OemOpenBrackets;
                case SharpDX.DirectInput.Key.LeftControl:
                   return Keys.LeftControl;
                case SharpDX.DirectInput.Key.LeftShift:
                 return Keys.LeftShift;
                case SharpDX.DirectInput.Key.LeftWindowsKey:
                    return Keys.LeftWindows;
                case SharpDX.DirectInput.Key.M:
                    return Keys.M;
                case SharpDX.DirectInput.Key.Mail:
                   return Keys.LaunchMail;
                case SharpDX.DirectInput.Key.MediaSelect:
                     return Keys.SelectMedia;
                case SharpDX.DirectInput.Key.MediaStop:
                     return Keys.MediaStop;
                case SharpDX.DirectInput.Key.Minus:
                    break;
                case SharpDX.DirectInput.Key.Multiply:
                     return Keys.Multiply;
                case SharpDX.DirectInput.Key.Mute:
                    return Keys.VolumeMute;
                case SharpDX.DirectInput.Key.MyComputer:
                    break;
                case SharpDX.DirectInput.Key.N:
                    return Keys.N;
                case SharpDX.DirectInput.Key.NextTrack:
                    return Keys.MediaNextTrack;
                case SharpDX.DirectInput.Key.NoConvert:
                    return Keys.ImeNoConvert;
                case SharpDX.DirectInput.Key.NumberLock:
                  return Keys.NumLock;
                case SharpDX.DirectInput.Key.NumberPad0:
                  return Keys.NumPad0;
                case SharpDX.DirectInput.Key.NumberPad1:
                   return Keys.NumPad1;
                case SharpDX.DirectInput.Key.NumberPad2:
                   return Keys.NumPad2;
                case SharpDX.DirectInput.Key.NumberPad3:
                   return Keys.NumPad3;
                case SharpDX.DirectInput.Key.NumberPad4:
                   return Keys.NumPad4;
                case SharpDX.DirectInput.Key.NumberPad5:
                   return Keys.NumPad5;
                case SharpDX.DirectInput.Key.NumberPad6:
                   return Keys.NumPad6;;
                case SharpDX.DirectInput.Key.NumberPad7:
                   return Keys.NumPad7;
                case SharpDX.DirectInput.Key.NumberPad8:
                   return Keys.NumPad8;
                case SharpDX.DirectInput.Key.NumberPad9:
                   return Keys.NumPad9;
                case SharpDX.DirectInput.Key.NumberPadComma:
                   return Keys.OemComma;
                case SharpDX.DirectInput.Key.NumberPadEnter:
                   return Keys.Enter;
                case SharpDX.DirectInput.Key.NumberPadEquals:
                    break;
                case SharpDX.DirectInput.Key.O:
                    return Keys.O;
                case SharpDX.DirectInput.Key.Oem102:
                    break;
                case SharpDX.DirectInput.Key.P:
                  return Keys.P;
                case SharpDX.DirectInput.Key.PageDown:
                    return Keys.PageDown;
                case SharpDX.DirectInput.Key.PageUp:
                      return Keys.PageUp;
                case SharpDX.DirectInput.Key.Pause:
                     return Keys.Pause;
                case SharpDX.DirectInput.Key.Period:
                     return Keys.OemPeriod;
                case SharpDX.DirectInput.Key.PlayPause:
                    return Keys.MediaPlayPause;
                case SharpDX.DirectInput.Key.Power:
                    break;
                case SharpDX.DirectInput.Key.PreviousTrack:
                   return Keys.MediaPreviousTrack;
                case SharpDX.DirectInput.Key.PrintScreen:
                    return Keys.PrintScreen;
                case SharpDX.DirectInput.Key.Q:
                    return Keys.Q;
                case SharpDX.DirectInput.Key.R:
                   return Keys.R;
                case SharpDX.DirectInput.Key.Return:
                    //check
                     return Keys.Enter;
                case SharpDX.DirectInput.Key.Right:
                     return Keys.Right;
                case SharpDX.DirectInput.Key.RightAlt:
                     return Keys.RightAlt;
                case SharpDX.DirectInput.Key.RightBracket:
                    return Keys.OemCloseBrackets;
                case SharpDX.DirectInput.Key.RightControl:
                  return Keys.RightControl;
                case SharpDX.DirectInput.Key.RightShift:
                    return Keys.RightShift;
                case SharpDX.DirectInput.Key.RightWindowsKey:
                    return Keys.RightWindows;
                case SharpDX.DirectInput.Key.S:
                   return Keys.S;
                case SharpDX.DirectInput.Key.ScrollLock:
                    //check
                 return Keys.Scroll;
                case SharpDX.DirectInput.Key.Semicolon:
                 return Keys.OemSemicolon;
                case SharpDX.DirectInput.Key.Slash:
                    break;
                case SharpDX.DirectInput.Key.Sleep:
                    return Keys.Sleep;
                case SharpDX.DirectInput.Key.Space:
                     return Keys.Space;
                case SharpDX.DirectInput.Key.Stop:
                    break;
                case SharpDX.DirectInput.Key.Subtract:
                  return Keys.Subtract;
                case SharpDX.DirectInput.Key.T:
                    return Keys.T;
                case SharpDX.DirectInput.Key.Tab:
                   return Keys.Tab;
                case SharpDX.DirectInput.Key.U:
                      return Keys.U;
                case SharpDX.DirectInput.Key.Underline:
                    break;
                case SharpDX.DirectInput.Key.Unknown:
                    break;
                case SharpDX.DirectInput.Key.Unlabeled:
                    break;
                case SharpDX.DirectInput.Key.UpArrow:
                   return Keys.Up;
                case SharpDX.DirectInput.Key.V:
                     return Keys.V;
                case SharpDX.DirectInput.Key.VolumeDown:
                  return Keys.VolumeDown;
                case SharpDX.DirectInput.Key.VolumeUp:
                   return Keys.VolumeUp;
                case SharpDX.DirectInput.Key.W:
                     return Keys.W;
                case SharpDX.DirectInput.Key.Wake:
                    break;
                case SharpDX.DirectInput.Key.WebBack:
                     return Keys.BrowserBack;
                case SharpDX.DirectInput.Key.WebFavorites:
                     return Keys.BrowserFavorites;
                case SharpDX.DirectInput.Key.WebForward:
                   return Keys.BrowserForward;
                case SharpDX.DirectInput.Key.WebHome:
                    return Keys.BrowserHome;
                case SharpDX.DirectInput.Key.WebRefresh:
                    return Keys.BrowserRefresh;
                case SharpDX.DirectInput.Key.WebSearch:
                   return Keys.BrowserSearch;
                case SharpDX.DirectInput.Key.WebStop:
                     return Keys.BrowserStop;
                case SharpDX.DirectInput.Key.X:
                    return Keys.X;
                case SharpDX.DirectInput.Key.Y:
                    return Keys.Y;
                case SharpDX.DirectInput.Key.Yen:
                    break;
                case SharpDX.DirectInput.Key.Z:
                     return Keys.Z;
                default:
                    break;
            }

            throw new NotImplementedException();
        }

        public static KeyboardState Translate(SharpDX.DirectInput.KeyboardState keyboardState)
        {
            int keyCount = keyboardState.PressedKeys.Count;
            Keys[] keys = new Keys[keyCount];

            for (int i = 0; i < keyCount; i++)
            {
                keys[i] = Translate(keyboardState.PressedKeys[i]);
            }

            KeyboardState ks = new KeyboardState(keys);

            return ks;
        }

        public static Buttons Translate(SharpDX.XInput.GamepadButtonFlags buttons)
        {
            Buttons tb = 0;

            tb |= (buttons & GamepadButtonFlags.A) == GamepadButtonFlags.A ? Buttons.A : 0;
            tb |= (buttons & GamepadButtonFlags.B) == GamepadButtonFlags.B ? Buttons.B : 0;
            tb |= (buttons & GamepadButtonFlags.Back) == GamepadButtonFlags.Back ? Buttons.Back : 0;
            tb |= (buttons & GamepadButtonFlags.DPadDown) == GamepadButtonFlags.DPadDown ? Buttons.DPadDown : 0;
            tb |= (buttons & GamepadButtonFlags.DPadLeft) == GamepadButtonFlags.DPadLeft ? Buttons.DPadLeft : 0;
            tb |= (buttons & GamepadButtonFlags.DPadRight) == GamepadButtonFlags.DPadRight ? Buttons.DPadRight : 0;
            tb |= (buttons & GamepadButtonFlags.DPadUp) == GamepadButtonFlags.DPadUp ? Buttons.DPadUp : 0;
            tb |= (buttons & GamepadButtonFlags.LeftShoulder) == GamepadButtonFlags.LeftShoulder ? Buttons.LeftShoulder : 0;
            tb |= (buttons & GamepadButtonFlags.LeftThumb) == GamepadButtonFlags.LeftThumb ? Buttons.LeftTrigger : 0;
            tb |= (buttons & GamepadButtonFlags.RightShoulder) == GamepadButtonFlags.RightShoulder ? Buttons.RightShoulder : 0;
            tb |= (buttons & GamepadButtonFlags.RightThumb) == GamepadButtonFlags.RightThumb ? Buttons.RightTrigger : 0;
            tb |= (buttons & GamepadButtonFlags.Start) == GamepadButtonFlags.Start ? Buttons.Start : 0;
            tb |= (buttons & GamepadButtonFlags.X) == GamepadButtonFlags.X ? Buttons.X : 0;
            tb |= (buttons & GamepadButtonFlags.Y) == GamepadButtonFlags.Y ? Buttons.Y : 0;

            return tb;
        }
    }
}
