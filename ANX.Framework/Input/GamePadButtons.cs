using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public struct GamePadButtons
    {
        #region Public
		public ButtonState A { get; private set; }
		public ButtonState B { get; private set; }
		public ButtonState Back { get; private set; }
		public ButtonState BigButton { get; private set; }
		public ButtonState LeftShoulder { get; private set; }
		public ButtonState LeftStick { get; private set; }
		public ButtonState RightShoulder { get; private set; }
		public ButtonState RightStick { get; private set; }
		public ButtonState Start { get; private set; }
		public ButtonState X { get; private set; }
		public ButtonState Y { get; private set; }
		internal Buttons Buttons { get; private set; }
		#endregion

        public GamePadButtons(Buttons buttons)
			: this()
        {
            A = GetButtonState(buttons, Buttons.A);
            B = GetButtonState(buttons, Buttons.B);
            X = GetButtonState(buttons, Buttons.X);
            Y = GetButtonState(buttons, Buttons.Y);
			LeftStick = GetButtonState(buttons, Buttons.LeftStick);
			RightStick = GetButtonState(buttons, Buttons.RightStick);
			LeftShoulder = GetButtonState(buttons, Buttons.LeftShoulder);
			RightShoulder = GetButtonState(buttons, Buttons.RightShoulder);
			Back = GetButtonState(buttons, Buttons.Back);
            Start = GetButtonState(buttons, Buttons.Start);
			BigButton = GetButtonState(buttons, Buttons.BigButton);

			Buttons = buttons;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadButtons))
                return this == (GamePadButtons)obj;

            return false;
        }

        public static bool operator ==(GamePadButtons lhs, GamePadButtons rhs)
        {
			return lhs.Buttons == rhs.Buttons;
        }

        public static bool operator !=(GamePadButtons lhs, GamePadButtons rhs)
        {
			return lhs.Buttons != rhs.Buttons;
        }

        private static ButtonState GetButtonState(Buttons buttons, Buttons button)
        {
            return (buttons & button) == button ? ButtonState.Pressed : ButtonState.Released;
        }

        public override int GetHashCode()
        {
			return (int)Buttons;
        }

        public override string ToString()
        {
            string buttons = A == ButtonState.Pressed ? "A" : "";
            buttons += B == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "B" : "";
            buttons += X == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "X" : "";
            buttons += Y == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Y" : "";
            buttons += LeftShoulder == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "LeftShoulder" : "";
            buttons += RightShoulder == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "RightShoulder" : "";
            buttons += LeftStick == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "LeftStick" : "";
            buttons += RightStick == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "RightStick" : "";
            buttons += Start == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Start" : "";
            buttons += Back == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Back" : "";
            buttons += BigButton == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "BigButton" : "";

            if (String.IsNullOrEmpty(buttons))
                buttons = "None";

            return String.Format("{{Buttons:{0}}}", buttons);
        }
    }
}
