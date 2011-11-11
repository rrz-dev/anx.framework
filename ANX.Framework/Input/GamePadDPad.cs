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
    public struct GamePadDPad
    {
        #region Private Members
        private Buttons buttons;

        private ButtonState up;
        private ButtonState down;
        private ButtonState left;
        private ButtonState right;

        #endregion // Private Members

        public GamePadDPad(ButtonState upValue, ButtonState downValue, ButtonState leftValue, ButtonState rightValue)
        {
            this.up = upValue;
            this.down = downValue;
            this.left = leftValue;
            this.right = rightValue;

            buttons = 0;
            buttons |= (upValue == ButtonState.Pressed ? Buttons.DPadUp : 0);
            buttons |= (downValue == ButtonState.Pressed ? Buttons.DPadDown : 0);
            buttons |= (leftValue == ButtonState.Pressed ? Buttons.DPadLeft : 0);
            buttons |= (rightValue == ButtonState.Pressed ? Buttons.DPadRight : 0);
        }

        internal GamePadDPad(Buttons buttons)
        {
            this.buttons = buttons;

            this.up = (buttons & Buttons.DPadUp) == Buttons.DPadUp ? ButtonState.Pressed : ButtonState.Released;
            this.left = (buttons & Buttons.DPadLeft) == Buttons.DPadLeft ? ButtonState.Pressed : ButtonState.Released;
            this.down = (buttons & Buttons.DPadDown) == Buttons.DPadDown ? ButtonState.Pressed : ButtonState.Released;
            this.right = (buttons & Buttons.DPadRight) == Buttons.DPadRight ? ButtonState.Pressed : ButtonState.Released;
        }

        public ButtonState Down 
        { 
            get 
            {
                return down;
            } 
        }

        public ButtonState Left
        {
            get
            {
                return left;
            }
        }

        public ButtonState Right
        {
            get
            {
                return right;
            }
        }
        
        public ButtonState Up
        {
            get
            {
                return up;
            }
        }

        public override int GetHashCode()
        {
            return (int)buttons;
        }

        public override string ToString()
        {
            String buttons = String.Empty;

            buttons += this.up == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Up" : "";
            buttons += this.left == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Left" : "";
            buttons += this.down == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Down" : "";
            buttons += this.right == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Right" : "";

            if (String.IsNullOrEmpty(buttons))
            {
                buttons = "None";
            }

            return String.Format("{{DPad:{0}}}", buttons);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadDPad))
            {
                return this == (GamePadDPad)obj;
            }

            return false;
        }

        public static bool operator ==(GamePadDPad lhs, GamePadDPad rhs)
        {
            return lhs.buttons == rhs.buttons;
        }

        public static bool operator !=(GamePadDPad lhs, GamePadDPad rhs)
        {
            return lhs.buttons != rhs.buttons;
        }

        internal Buttons Buttons
        {
            get
            {
                return this.buttons;
            }
        }
    }
}
