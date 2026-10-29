using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
	[Serializable]
#endif
	[PercentageComplete(100)]
    [Developer("AstrorEnales")]
	[TestState(TestStateAttribute.TestState.Tested)]
	public struct MouseState
	{
		public ButtonState LeftButton { get; private set; }
		public ButtonState MiddleButton { get; private set; }
		public ButtonState RightButton { get; private set; }
		public ButtonState XButton1 { get; private set; }
		public ButtonState XButton2 { get; private set; }
		public int ScrollWheelValue { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }

		public MouseState(int x, int y, int scrollWheel, ButtonState leftButton, ButtonState middleButton,
			ButtonState rightButton, ButtonState xButton1, ButtonState xButton2)
			: this()
		{
			X = x;
			Y = y;
			ScrollWheelValue = scrollWheel;
			LeftButton = leftButton;
			MiddleButton = middleButton;
			RightButton = rightButton;
			XButton1 = xButton1;
			XButton2 = xButton2;
		}

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
		    return obj is MouseState && this == (MouseState)obj;
		}

	    public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ LeftButton.GetHashCode() ^ RightButton.GetHashCode() ^
				MiddleButton.GetHashCode() ^ XButton1.GetHashCode() ^ XButton2.GetHashCode() ^ ScrollWheelValue.GetHashCode();
		}

		public override string ToString()
		{
			string buttons = LeftButton == ButtonState.Pressed ? "Left" : "";
			buttons += RightButton == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Right" : "";
			buttons += MiddleButton == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Middle" : "";
			buttons += XButton1 == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "XButton1" : "";
			buttons += XButton2 == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "XButton2" : "";

			return String.Format("{{X:{0} Y:{1} Buttons:{2} Wheel:{3}}}", X, Y, buttons, ScrollWheelValue);
		}
	}
}
