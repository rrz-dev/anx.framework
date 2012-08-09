#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
    [SerializableAttribute]
#endif
    public struct MouseState
    {
        private int x;
        private int y;
        private int scrollWheel;
        private ButtonState leftButton;
        private ButtonState middleButton;
        private ButtonState rightButton;
        private ButtonState xButton1;
        private ButtonState xButton2;
        public MouseState (int x, int y, int scrollWheel,ButtonState leftButton,ButtonState middleButton,ButtonState rightButton,ButtonState xButton1,ButtonState xButton2)
        {
            this.x = x;
            this.y = y;
            this.scrollWheel = scrollWheel;
            this.leftButton = leftButton;
            this.middleButton = middleButton;
            this.rightButton = rightButton;
            this.xButton1 = xButton1;
            this.xButton2 = xButton2;
        }

        public ButtonState LeftButton { get { return this.leftButton; } }
        public ButtonState MiddleButton { get { return this.middleButton; } }
        public ButtonState RightButton { get { return this.rightButton; } }
        public ButtonState XButton1 { get { return this.xButton1; } }
        public ButtonState XButton2 { get { return this.xButton2; } }
        public int ScrollWheelValue { get { return this.scrollWheel; } }
        public int X { get { return this.x; } }
        public int Y { get { return this.y; } }

		public static bool operator ==(MouseState left, MouseState right)
		{
			return left.X == right.X &&
				left.Y == right.Y &&
				left.LeftButton == right.LeftButton &&
				left.MiddleButton == right.MiddleButton &&
				left.RightButton == right.RightButton &&
				left.XButton1 == right.XButton1 &&
				left.XButton2 == right.XButton2 &&
				left.ScrollWheelValue == right.ScrollWheelValue;
		}

		public static bool operator !=(MouseState left, MouseState right)
		{
			return left.X != right.X ||
				left.Y != right.Y ||
				left.LeftButton != right.LeftButton ||
				left.MiddleButton != right.MiddleButton ||
				left.RightButton != right.RightButton ||
				left.XButton1 != right.XButton1 ||
				left.XButton2 != right.XButton2 ||
				left.ScrollWheelValue != right.ScrollWheelValue;
		}

		public override bool Equals(object obj)
		{
			if (obj is MouseState)
			{
				return this == (MouseState)obj;
			}

			return false;
		}

        public override int GetHashCode()
        {
            return ((((((this.x.GetHashCode() ^ this.y.GetHashCode()) ^ this.leftButton.GetHashCode()) ^ this.rightButton.GetHashCode()) ^ this.middleButton.GetHashCode()) ^ this.xButton1.GetHashCode()) ^ this.xButton2.GetHashCode()) ^ this.scrollWheel.GetHashCode();
        }

        public override string ToString()
        {
            string buttons = String.Empty;

            buttons += leftButton == ButtonState.Pressed ? "Left" : "";
            buttons += rightButton == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Right" : "";
            buttons += middleButton == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Middle" : "";
            buttons += xButton1 == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "XButton1" : "";
            buttons += xButton2 == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "XButton2" : "";

            return string.Format("{{X:{0} Y:{1} Buttons:{2} Wheel:{3}}}", this.x, this.y, buttons, this.scrollWheel);
        }
    }
}
