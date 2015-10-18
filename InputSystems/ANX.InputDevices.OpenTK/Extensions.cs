using ANX.Framework;
using ANX.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Input = OpenTK.Input;

namespace ANX.InputDevices.OpenTK
{
    static class Extensions
    {
        #region GamePad
        public static GamePadState ToAnx(this Input.GamePadState state)
        {
            return new GamePadState(state.ThumbSticks.ToAnx(), state.Triggers.ToAnx(), state.Buttons.ToAnx(), state.DPad.ToAnx())
            {
                IsConnected = state.IsConnected,
                PacketNumber = state.PacketNumber,
            };
        }

        public static GamePadThumbSticks ToAnx(this Input.GamePadThumbSticks sticks)
        {
            return new GamePadThumbSticks(sticks.Left.ToAnx(), sticks.Right.ToAnx());
        }

        public static GamePadTriggers ToAnx(this Input.GamePadTriggers triggers)
        {
            return new GamePadTriggers(triggers.Left, triggers.Right);
        }

        public static GamePadDPad ToAnx(this Input.GamePadDPad dpad)
        {
            return new GamePadDPad(dpad.Up.ToAnx(), dpad.Down.ToAnx(), dpad.Left.ToAnx(), dpad.Right.ToAnx());
        }

        public static GamePadButtons ToAnx(this Input.GamePadButtons buttons)
        {
            return new GamePadButtons(buttons.ToButtons());
        }

        public static Buttons ToButtons(this Input.GamePadButtons buttons)
        {
            Buttons anxButtons = default(Buttons);
            if (buttons.A == Input.ButtonState.Pressed)
                anxButtons |= Buttons.A;

            if (buttons.B == Input.ButtonState.Pressed)
                anxButtons |= Buttons.B;

            if (buttons.Back == Input.ButtonState.Pressed)
                anxButtons |= Buttons.Back;

            if (buttons.BigButton == Input.ButtonState.Pressed)
                anxButtons |= Buttons.BigButton;

            if (buttons.LeftShoulder == Input.ButtonState.Pressed)
                anxButtons |= Buttons.LeftShoulder;

            if (buttons.LeftStick == Input.ButtonState.Pressed)
                anxButtons |= Buttons.LeftStick;

            if (buttons.RightShoulder == Input.ButtonState.Pressed)
                anxButtons |= Buttons.RightShoulder;

            if (buttons.RightStick == Input.ButtonState.Pressed)
                anxButtons |= Buttons.RightStick;

            if (buttons.Start == Input.ButtonState.Pressed)
                anxButtons |= Buttons.Start;

            if (buttons.X == Input.ButtonState.Pressed)
                anxButtons |= Buttons.X;

            if (buttons.Y == Input.ButtonState.Pressed)
                anxButtons |= Buttons.Y;

            return anxButtons;
        }

        #endregion

        #region Keyboard

        private static Dictionary<Input.Key, Keys> keyMap = null;

        private static void initKeyMap()
        {
            if (keyMap != null)
                return;

            keyMap = new Dictionary<Input.Key, Keys>();
            keyMap[Input.Key.A] = Keys.A;
            keyMap[Input.Key.AltLeft] = Keys.LeftAlt;
            keyMap[Input.Key.AltRight] = Keys.RightAlt;
            keyMap[Input.Key.B] = Keys.B;
            keyMap[Input.Key.Back] = Keys.Back;
            keyMap[Input.Key.BackSlash] = Keys.OemBackslash;
            //keyMap[Input.Key.BackSpace] = Keys.Back;
            keyMap[Input.Key.BracketLeft] = Keys.OemOpenBrackets;
            keyMap[Input.Key.BracketRight] = Keys.OemCloseBrackets;
            keyMap[Input.Key.C] = Keys.C;
            keyMap[Input.Key.CapsLock] = Keys.CapsLock;
            keyMap[Input.Key.Clear] = Keys.OemClear;
            keyMap[Input.Key.Comma] = Keys.OemComma;
            keyMap[Input.Key.ControlLeft] = Keys.LeftControl;
            keyMap[Input.Key.ControlRight] = Keys.RightControl;
            keyMap[Input.Key.D] = Keys.D;
            keyMap[Input.Key.Delete] = Keys.Delete;
            keyMap[Input.Key.Down] = Keys.Down;
            keyMap[Input.Key.E] = Keys.E;
            keyMap[Input.Key.End] = Keys.End;
            keyMap[Input.Key.Enter] = Keys.Enter;
            keyMap[Input.Key.Escape] = Keys.Escape;
            keyMap[Input.Key.F] = Keys.F;
            keyMap[Input.Key.F1] = Keys.F1;
            keyMap[Input.Key.F10] = Keys.F10;
            keyMap[Input.Key.F11] = Keys.F11;
            keyMap[Input.Key.F12] = Keys.F12;
            keyMap[Input.Key.F13] = Keys.F13;
            keyMap[Input.Key.F14] = Keys.F14;
            keyMap[Input.Key.F15] = Keys.F15;
            keyMap[Input.Key.F16] = Keys.F16;
            keyMap[Input.Key.F17] = Keys.F17;
            keyMap[Input.Key.F18] = Keys.F18;
            keyMap[Input.Key.F19] = Keys.F19;
            keyMap[Input.Key.F2] = Keys.F2;
            keyMap[Input.Key.F20] = Keys.F20;
            keyMap[Input.Key.F21] = Keys.F21;
            keyMap[Input.Key.F22] = Keys.F22;
            keyMap[Input.Key.F23] = Keys.F23;
            keyMap[Input.Key.F24] = Keys.F24;
            //keyMap[Input.Key.F25] = Keys.;
            //... F35
            keyMap[Input.Key.F3] = Keys.F3;
            keyMap[Input.Key.F4] = Keys.F4;
            keyMap[Input.Key.F5] = Keys.F5;
            keyMap[Input.Key.F6] = Keys.F6;
            keyMap[Input.Key.F7] = Keys.F7;
            keyMap[Input.Key.F8] = Keys.F8;
            keyMap[Input.Key.F9] = Keys.F9;
            keyMap[Input.Key.G] = Keys.G;
            keyMap[Input.Key.Grave] = Keys.OemTilde;
            keyMap[Input.Key.H] = Keys.H;
            keyMap[Input.Key.Home] = Keys.Home;
            keyMap[Input.Key.I] = Keys.I;
            keyMap[Input.Key.Insert] = Keys.Insert;
            keyMap[Input.Key.J] = Keys.J;
            keyMap[Input.Key.K] = Keys.K;
            keyMap[Input.Key.Keypad0] = Keys.NumPad0;
            keyMap[Input.Key.Keypad1] = Keys.NumPad1;
            keyMap[Input.Key.Keypad2] = Keys.NumPad2;
            keyMap[Input.Key.Keypad3] = Keys.NumPad3;
            keyMap[Input.Key.Keypad4] = Keys.NumPad4;
            keyMap[Input.Key.Keypad5] = Keys.NumPad5;
            keyMap[Input.Key.Keypad6] = Keys.NumPad6;
            keyMap[Input.Key.Keypad7] = Keys.NumPad7;
            keyMap[Input.Key.Keypad8] = Keys.NumPad8;
            keyMap[Input.Key.Keypad9] = Keys.NumPad9;
            keyMap[Input.Key.KeypadAdd] = Keys.Add;
            keyMap[Input.Key.KeypadDecimal] = Keys.Decimal;
            keyMap[Input.Key.KeypadDivide] = Keys.Divide;
            //keyMap[Input.Key.KeypadEnter] = Keys.Enter;
            keyMap[Input.Key.KeypadMinus] = Keys.Subtract;
            keyMap[Input.Key.KeypadMultiply] = Keys.Multiply;
            keyMap[Input.Key.KeypadPeriod] = Keys.OemPeriod;
            keyMap[Input.Key.KeypadPlus] = Keys.Add;
            keyMap[Input.Key.KeypadSubtract] = Keys.Subtract;
            keyMap[Input.Key.L] = Keys.L;
            keyMap[Input.Key.LAlt] = Keys.LeftAlt;
            keyMap[Input.Key.LBracket] = Keys.OemOpenBrackets;
            keyMap[Input.Key.LControl] = Keys.LeftControl;
            keyMap[Input.Key.Left] = Keys.Left;
            keyMap[Input.Key.LShift] = Keys.LeftShift;
            keyMap[Input.Key.LWin] = Keys.LeftWindows;
            keyMap[Input.Key.M] = Keys.M;
            //keyMap[Input.Key.Menu] = Keys.;
            keyMap[Input.Key.Minus] = Keys.OemMinus;
            keyMap[Input.Key.N] = Keys.N;
            //keyMap.Add(Input.Key.NonUSBackSlash);
            keyMap[Input.Key.Number0] = Keys.D0;
            keyMap[Input.Key.Number1] = Keys.D1;
            keyMap[Input.Key.Number2] = Keys.D2;
            keyMap[Input.Key.Number3] = Keys.D3;
            keyMap[Input.Key.Number4] = Keys.D4;
            keyMap[Input.Key.Number5] = Keys.D5;
            keyMap[Input.Key.Number6] = Keys.D6;
            keyMap[Input.Key.Number7] = Keys.D7;
            keyMap[Input.Key.Number8] = Keys.D8;
            keyMap[Input.Key.Number9] = Keys.D9;
            keyMap[Input.Key.NumLock] = Keys.NumLock;
            keyMap[Input.Key.O] = Keys.O;
            keyMap[Input.Key.P] = Keys.P;
            keyMap[Input.Key.PageDown] = Keys.PageDown;
            keyMap[Input.Key.PageUp] = Keys.PageUp;
            keyMap[Input.Key.Pause] = Keys.Pause;
            keyMap[Input.Key.Period] = Keys.OemPeriod;
            keyMap[Input.Key.Plus] = Keys.OemPlus;
            keyMap[Input.Key.PrintScreen] = Keys.PrintScreen;
            keyMap[Input.Key.Q] = Keys.Q;
            keyMap[Input.Key.Quote] = Keys.OemQuotes;
            keyMap[Input.Key.R] = Keys.R;
            keyMap[Input.Key.RAlt] = Keys.RightAlt;
            keyMap[Input.Key.RBracket] = Keys.OemCloseBrackets;
            keyMap[Input.Key.RControl] = Keys.RightControl;
            keyMap[Input.Key.Right] = Keys.Right;
            keyMap[Input.Key.RShift] = Keys.RightShift;
            keyMap[Input.Key.RWin] = Keys.RightWindows;
            keyMap[Input.Key.S] = Keys.S;
            keyMap[Input.Key.ScrollLock] = Keys.Scroll;
            keyMap[Input.Key.Semicolon] = Keys.OemSemicolon;
            keyMap[Input.Key.ShiftLeft] = Keys.LeftShift;
            keyMap[Input.Key.ShiftRight] = Keys.RightShift;
            //keyMap[Input.Key.Slash] = Keys.;
            keyMap[Input.Key.Sleep] = Keys.Sleep;
            keyMap[Input.Key.Space] = Keys.Space;
            keyMap[Input.Key.T] = Keys.T;
            keyMap[Input.Key.Tab] = Keys.Tab;
            keyMap[Input.Key.Tilde] = Keys.OemTilde;
            keyMap[Input.Key.U] = Keys.U;
            keyMap[Input.Key.Up] = Keys.Up;
            keyMap[Input.Key.V] = Keys.V;
            keyMap[Input.Key.W] = Keys.W;
            keyMap[Input.Key.WinLeft] = Keys.LeftWindows;
            keyMap[Input.Key.WinRight] = Keys.RightWindows;
            keyMap[Input.Key.X] = Keys.X;
            keyMap[Input.Key.Y] = Keys.Y;
            keyMap[Input.Key.Z] = Keys.Z;
            //TODO: Many XNA buttons are missing
            //like media and kanji
        }

        public static Keys ToAnx(this Input.Key key)
        {
            initKeyMap();

            Keys keys;
            if (keyMap.TryGetValue(key, out keys))
                return keys;
            else
            {
                Console.Error.WriteLine("Unknown key: " + key.ToString());
                return Keys.None;
            }
        }

        public static KeyboardState ToAnx(this Input.KeyboardState state)
        {
            List<Keys> downKeys = new List<Keys>();

            foreach (Input.Key key in Enum.GetValues(typeof(Input.Key)))
            {
                if (state.IsKeyDown(key))
                    downKeys.Add(key.ToAnx());
            }
            
            return new KeyboardState(downKeys.ToArray());
        }

        #endregion

        public static ButtonState ToAnx(this Input.ButtonState state)
        {
            return (ButtonState)state;
        }

        public static Vector2 ToAnx(this global::OpenTK.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}
