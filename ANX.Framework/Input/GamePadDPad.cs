using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public struct GamePadDPad
    {
		public ButtonState Down { get; private set; }
		public ButtonState Left { get; private set; }
		public ButtonState Right { get; private set; }
		public ButtonState Up { get; private set; }
		internal Buttons Buttons { get; private set; }

        public GamePadDPad(ButtonState upValue, ButtonState downValue, ButtonState leftValue, ButtonState rightValue)
			: this()
        {
			Up = upValue;
            Down = downValue;
            Left = leftValue;
            Right = rightValue;
			AddToButtonsIfPressed(upValue, Buttons.DPadUp);
			AddToButtonsIfPressed(downValue, Buttons.DPadDown);
			AddToButtonsIfPressed(leftValue, Buttons.DPadLeft);
			AddToButtonsIfPressed(rightValue, Buttons.DPadRight);
        }

		internal GamePadDPad(Buttons buttons)
			: this()
        {
			Buttons = buttons;
			Up = GetButtonStateFrom(Buttons.DPadUp);
			Left = GetButtonStateFrom(Buttons.DPadLeft);
			Down = GetButtonStateFrom(Buttons.DPadDown);
			Right = GetButtonStateFrom(Buttons.DPadRight);
        }

		private ButtonState GetButtonStateFrom(Buttons button)
		{
			return (Buttons & button) == button ? ButtonState.Pressed : ButtonState.Released;
		}

		private void AddToButtonsIfPressed(ButtonState state, Buttons button)
		{
			Buttons |= (state == ButtonState.Pressed ? button : 0);
		}

        public override int GetHashCode()
        {
            return (int)Buttons;
        }

        public override string ToString()
        {
            string buttons = Up == ButtonState.Pressed ? "Up" : "";
            buttons += Down == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Down" : "";
            buttons += Left == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Left" : "";
            buttons += Right == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Right" : "";

            if (String.IsNullOrEmpty(buttons))
                buttons = "None";

            return String.Format("{{DPad:{0}}}", buttons);
        }

        public override bool Equals(object obj)
        {
            if (obj is GamePadDPad)
                return this == (GamePadDPad)obj;

            return false;
        }

        public static bool operator ==(GamePadDPad lhs, GamePadDPad rhs)
        {
			return lhs.Buttons == rhs.Buttons;
        }

        public static bool operator !=(GamePadDPad lhs, GamePadDPad rhs)
        {
			return lhs.Buttons != rhs.Buttons;
        }
    }
}
