#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    public struct GamePadButtons
    {
        #region Private Members
        internal Buttons buttonValue;

        internal ButtonState button_a;
        internal ButtonState button_b;
        internal ButtonState button_x;
        internal ButtonState button_y;
        internal ButtonState stick_left;
        internal ButtonState stick_right;
        internal ButtonState shoulder_left;
        internal ButtonState shoulder_right;
        internal ButtonState button_back;
        internal ButtonState button_start;
        internal ButtonState button_big;

        #endregion // Private Members

        public GamePadButtons(Buttons buttons)
        {
            this.button_a = GetButtonState(buttons, Buttons.A);
            this.button_b = GetButtonState(buttons, Buttons.B);
            this.button_x = GetButtonState(buttons, Buttons.X);
            this.button_y = GetButtonState(buttons, Buttons.Y);
            this.stick_left = GetButtonState(buttons, Buttons.LeftStick);
            this.stick_right = GetButtonState(buttons, Buttons.RightStick);
            this.shoulder_left = GetButtonState(buttons, Buttons.LeftShoulder);
            this.shoulder_right = GetButtonState(buttons, Buttons.RightShoulder);
            this.button_back = GetButtonState(buttons, Buttons.Back);
            this.button_start = GetButtonState(buttons, Buttons.Start);
            this.button_big = GetButtonState(buttons, Buttons.BigButton);

            this.buttonValue = buttons;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadButtons))
            {
                return this == (GamePadButtons)obj;
            }

            return false;
        }

        public static bool operator ==(GamePadButtons lhs, GamePadButtons rhs)
        {
            return lhs.buttonValue == rhs.buttonValue;
        }

        public static bool operator !=(GamePadButtons lhs, GamePadButtons rhs)
        {
            return lhs.buttonValue != rhs.buttonValue;
        }

        private static ButtonState GetButtonState(Buttons buttons, Buttons button)
        {
            return (buttons & button) == button ? ButtonState.Pressed : ButtonState.Released;
        }

        public ButtonState A
        {
            get
            {
                return this.button_a;
            }
        }

        public ButtonState B
        {
            get
            {
                return this.button_b;
            }
        }

        public ButtonState Back
        {
            get
            {
                return this.button_back;
            }
        }

        public ButtonState BigButton
        {
            get
            {
                return this.button_big;
            }
        }

        public ButtonState LeftShoulder
        {
            get
            {
                return this.shoulder_left;
            }
        }

        public ButtonState LeftStick
        {
            get
            {
                return stick_left;
            }
        }

        public ButtonState RightShoulder
        {
            get
            {
                return this.shoulder_right;
            }
        }

        public ButtonState RightStick
        {
            get
            {
                return this.stick_right;
            }
        }

        public ButtonState Start
        {
            get
            {
                return this.button_start;
            }
        }

        public ButtonState X
        {
            get
            {
                return this.button_x;
            }
        }

        public ButtonState Y
        {
            get
            {
                return this.button_y;
            }
        }

        public override int GetHashCode()
        {
            return (int)this.buttonValue;
        }

        public override string ToString()
        {
            String buttons = String.Empty;

            buttons += this.button_a == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "A" : "";
            buttons += this.button_b == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "B" : "";
            buttons += this.button_x == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "X" : "";
            buttons += this.button_y == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Y" : "";
            buttons += this.shoulder_left == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "LeftShoulder" : "";
            buttons += this.shoulder_right == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "RightShoulder" : "";
            buttons += this.stick_left == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "LeftStick" : "";
            buttons += this.stick_right == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "RightStick" : "";
            buttons += this.button_start == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Start" : "";
            buttons += this.button_back == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Back" : "";
            buttons += this.button_big == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "BigButton" : "";

            if (string.IsNullOrEmpty(buttons))
            {
                buttons = "None";
            }

            return String.Format("{{Buttons:{0}}}", buttons);
        }

        internal Buttons Buttons
        {
            get
            {
                return this.buttonValue;
            }
        }
    }
}
