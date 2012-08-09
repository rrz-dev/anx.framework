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
            buttons += this.down == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Down" : "";
            buttons += this.left == ButtonState.Pressed ? (buttons.Length > 0 ? " " : "") + "Left" : "";
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
