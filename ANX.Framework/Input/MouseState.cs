#region Using Statements
using System;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Input
{
#if !WIN8      //TODO: search replacement for Win8
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
