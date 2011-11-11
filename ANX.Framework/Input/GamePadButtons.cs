#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;

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
    public struct GamePadButtons
    {
        private int buttons;
        public GamePadButtons (int value)
        {
            this.buttons = value;
        }
        public GamePadButtons(Buttons buttons)
        {
            this.buttons = (int)buttons;
        }
        public ButtonState A
        {
            get
            {
                if ((this.buttons & (int)Buttons.A) == (int)Buttons.A) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState B
        {
            get
            {
                if ((this.buttons & (int)Buttons.B) == (int)Buttons.B) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState Back
        {
            get
            {
                if ((this.buttons & (int)Buttons.Back) == (int)Buttons.Back) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState BigButton
        {
            get
            {
                if ((this.buttons & (int)Buttons.BigButton) == (int)Buttons.BigButton) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState LeftShoulder
        {
            get
            {
                if ((this.buttons & (int)Buttons.LeftShoulder) == (int)Buttons.LeftShoulder) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState LeftStick
        {
            get
            {
                if ((this.buttons & (int)Buttons.LeftStick) == (int)Buttons.LeftStick) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState RightShoulder
        {
            get
            {
                if ((this.buttons & (int)Buttons.RightShoulder) == (int)Buttons.RightShoulder) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState RightStick
        {
            get
            {
                if ((this.buttons & (int)Buttons.RightStick) == (int)Buttons.RightStick) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState Start
        {
            get
            {
                if ((this.buttons & (int)Buttons.Start) == (int)Buttons.Start) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState X
        {
            get
            {
                if ((this.buttons & (int)Buttons.X) == (int)Buttons.X) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public ButtonState Y
        {
            get
            {
                if ((this.buttons & (int)Buttons.Y) == (int)Buttons.Y) return ButtonState.Pressed;
                else return ButtonState.Released;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is GamePadButtons)
            {
                return this == (GamePadButtons)obj;
            }

            return false;
        }
        public static bool operator ==(GamePadButtons left, GamePadButtons right)
        {
            return left.X == right.X &&
                left.Y == right.Y &&
                left.A == right.A &&
                left.B == right.B &&
                left.Back == right.Back &&
                left.BigButton == right.BigButton &&
                left.LeftShoulder == right.LeftShoulder &&
                left.LeftStick == right.LeftStick &&
                left.RightShoulder == right.RightShoulder &&
                left.RightStick == right.RightStick &&
                left.Start == right.Start;
        }

        public static bool operator !=(GamePadButtons left, GamePadButtons right)
        {
            return left.X != right.X ||
                left.Y != right.Y ||
                left.A != right.A ||
                left.B != right.B ||
                left.Back != right.Back ||
                left.BigButton != right.BigButton ||
                left.LeftShoulder != right.LeftShoulder ||
                left.LeftStick != right.LeftStick ||
                left.RightShoulder != right.RightShoulder ||
                left.RightStick != right.RightStick ||
                left.Start != right.Start;
        }
        public override int GetHashCode()
        {
            return (((((((((A.GetHashCode() ^ B.GetHashCode()) ^ Back.GetHashCode()) ^ BigButton.GetHashCode()) ^ LeftShoulder.GetHashCode()) ^ LeftStick.GetHashCode()) ^ RightShoulder.GetHashCode()) ^ RightStick.GetHashCode()) ^ Start.GetHashCode()) ^ X.GetHashCode()) ^ Y.GetHashCode();
        }
        public override string ToString()
        {
            string buttons = String.Empty;

            buttons += A == ButtonState.Pressed ? "A" : "";
            buttons += B == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "B" : "";
            buttons += Back == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Back" : "";
            buttons += BigButton == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "BigButton" : "";
            buttons += LeftShoulder == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "LeftShoulder" : "";
            buttons += LeftStick == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "LeftStick" : "";
            buttons += RightShoulder == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "RightShoulder" : "";
            buttons += RightStick == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "RightStick" : "";
            buttons += Start == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Start" : "";
            buttons += X == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "X" : "";
            buttons += Y == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Y" : "";

            return buttons;
        }

    }
}
