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
    public struct GamePadButtons
	{
	    private readonly ButtonState a;
        private readonly ButtonState b;
        private readonly ButtonState back;
        private readonly ButtonState big;
        private readonly ButtonState leftShoulder;
        private readonly ButtonState leftStick;
        private readonly ButtonState rightShoulder;
        private readonly ButtonState rightStick;
        private readonly ButtonState start;
        private readonly ButtonState x;
        private readonly ButtonState y;

        #region Public
	    public ButtonState A
	    {
            get { return a; }
	    }

        public ButtonState B
        {
            get { return b; }
        }

        public ButtonState Back
        {
            get { return back; }
        }

        public ButtonState BigButton
        {
            get { return big; }
        }

        public ButtonState LeftShoulder
        {
            get { return leftShoulder; }
        }

        public ButtonState LeftStick
        {
            get { return leftStick; }
        }

        public ButtonState RightShoulder
        {
            get { return rightShoulder; }
        }

        public ButtonState RightStick
        {
            get { return rightStick; }
        }

        public ButtonState Start
        {
            get { return start; }
        }

        public ButtonState X
        {
            get { return x; }
        }

        public ButtonState Y
        {
            get { return y; }
        }
		#endregion

        public GamePadButtons(Buttons buttons)
			: this()
        {
            a = GetButtonState(buttons, Buttons.A);
            b = GetButtonState(buttons, Buttons.B);
            x = GetButtonState(buttons, Buttons.X);
            y = GetButtonState(buttons, Buttons.Y);
			leftStick = GetButtonState(buttons, Buttons.LeftStick);
			rightStick = GetButtonState(buttons, Buttons.RightStick);
			leftShoulder = GetButtonState(buttons, Buttons.LeftShoulder);
			rightShoulder = GetButtonState(buttons, Buttons.RightShoulder);
			back = GetButtonState(buttons, Buttons.Back);
            start = GetButtonState(buttons, Buttons.Start);
			big = GetButtonState(buttons, Buttons.BigButton);
        }

        public override bool Equals(object obj)
        {
            return obj is GamePadButtons && this == (GamePadButtons)obj;
        }

	    public static bool operator ==(GamePadButtons lhs, GamePadButtons rhs)
        {
            return lhs.a == rhs.a && lhs.b == rhs.b && lhs.x == rhs.x && lhs.y == rhs.y &&
                lhs.leftShoulder == rhs.leftShoulder && lhs.leftStick == rhs.leftStick &&
                lhs.rightShoulder == rhs.rightShoulder && lhs.rightStick == rhs.rightStick &&
                lhs.back == rhs.back && lhs.start == rhs.start && lhs.big == rhs.big;
        }

        public static bool operator !=(GamePadButtons lhs, GamePadButtons rhs)
        {
            return lhs.a != rhs.a || lhs.b != rhs.b || lhs.x != rhs.x || lhs.y != rhs.y ||
                lhs.leftShoulder != rhs.leftShoulder || lhs.leftStick != rhs.leftStick ||
                lhs.rightShoulder != rhs.rightShoulder || lhs.rightStick != rhs.rightStick ||
                lhs.back != rhs.back || lhs.start != rhs.start || lhs.big != rhs.big;
        }

        private static ButtonState GetButtonState(Buttons buttons, Buttons button)
        {
            return (buttons & button) == button ? ButtonState.Pressed : ButtonState.Released;
        }

        public override int GetHashCode()
        {
            return HashHelper.GetGCHandleHashCode(this);
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
