using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
	[TestState(TestStateAttribute.TestState.Tested)]
    public struct GamePadDPad
	{
        private readonly ButtonState up;
        private readonly ButtonState right;
        private readonly ButtonState down;
        private readonly ButtonState left;

	    public ButtonState Down
	    {
	        get { return down; }
	    }

        public ButtonState Left
        {
            get { return left; }
        }

        public ButtonState Right
        {
            get { return right; }
        }

        public ButtonState Up
        {
            get { return up; }
        }

        public GamePadDPad(ButtonState upValue, ButtonState downValue, ButtonState leftValue, ButtonState rightValue)
        {
			up = upValue;
            down = downValue;
            left = leftValue;
            right = rightValue;
        }

		internal GamePadDPad(Buttons buttons)
            : this()
        {
			up = GetButtonStateFrom(buttons, Buttons.DPadUp);
			left = GetButtonStateFrom(buttons, Buttons.DPadLeft);
			down = GetButtonStateFrom(buttons, Buttons.DPadDown);
			right = GetButtonStateFrom(buttons, Buttons.DPadRight);
        }

        private ButtonState GetButtonStateFrom(Buttons buttons, Buttons button)
		{
            return (buttons & button) == button ? ButtonState.Pressed : ButtonState.Released;
		}

        public override int GetHashCode()
        {
            return HashHelper.GetGCHandleHashCode(this);
        }

        public override string ToString()
        {
            string buttons = up == ButtonState.Pressed ? "Up" : "";
            buttons += down == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Down" : "";
            buttons += left == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Left" : "";
            buttons += right == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Right" : "";

            if (String.IsNullOrEmpty(buttons))
                buttons = "None";

            return String.Format("{{DPad:{0}}}", buttons);
        }

        public override bool Equals(object obj)
        {
            return obj is GamePadDPad && this == (GamePadDPad)obj;
        }

	    public static bool operator ==(GamePadDPad lhs, GamePadDPad rhs)
        {
            return lhs.up == rhs.up && lhs.down == rhs.down && lhs.left == rhs.left && lhs.right == rhs.right;
        }

        public static bool operator !=(GamePadDPad lhs, GamePadDPad rhs)
        {
            return lhs.up != rhs.up || lhs.down != rhs.down || lhs.left != rhs.left || lhs.right != rhs.right;
        }
    }
}
