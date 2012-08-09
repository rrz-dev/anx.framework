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
    public struct GamePadState
    {
        #region Private Members
        private GamePadThumbSticks thumbSticks;
        private GamePadTriggers triggers;
        private GamePadButtons buttons;
        private GamePadDPad dPad;

        private Buttons buttonsValue;

        private bool isConnected;
        private int packetNumber;

        #endregion // Private Members

        public GamePadState(GamePadThumbSticks thumbSticks, GamePadTriggers triggers, GamePadButtons buttons, GamePadDPad dPad)
        {
            this.thumbSticks = thumbSticks;
            this.triggers = triggers;
            this.buttons = buttons;
            this.dPad = dPad;
            this.isConnected = true;
            this.packetNumber = 0;
            this.buttonsValue = this.buttons.Buttons | this.dPad.Buttons;
        }

        public GamePadState(Vector2 leftThumbStick, Vector2 rightThumbStick, float leftTrigger, float rightTrigger, params Buttons[] buttons)
        {
            this.thumbSticks = new GamePadThumbSticks(leftThumbStick, rightThumbStick);
            this.triggers = new GamePadTriggers(leftTrigger, rightTrigger);

            Buttons buttonField = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonField |= buttons[i];
            }
            this.buttonsValue = buttonField;
            this.isConnected = true;
            this.packetNumber = 0;

            this.buttons = new GamePadButtons(this.buttonsValue);
            this.dPad = new GamePadDPad(this.buttonsValue);
        }

        //public GamePadState(int value, bool isConnected, int packetNumber, Vector2 thumbStickLeft, Vector2 thumbStickRight, float triggerLeft, float triggerRight)
        //{
        //    this.buttonsValue = value;
        //    //TODO: this.buttons = new GamePadButtons(value);
        //    this.dPad = new GamePadDPad(value);
        //    this.isConnected = isConnected;
        //    this.packetNumber = packetNumber;
        //    this.thumbSticks = new GamePadThumbSticks(thumbStickLeft, thumbStickRight);
        //    this.triggers = new GamePadTriggers(triggerLeft, triggerRight);
        //}

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadState))
            {
                return this == (GamePadState)obj;
            }

            return false;
        }

        public static bool operator ==(GamePadState lhs, GamePadState rhs)
        {
            return lhs.buttonsValue == rhs.buttonsValue;
        }

        public static bool operator !=(GamePadState lhs, GamePadState rhs)
        {
            return lhs.buttonsValue != rhs.buttonsValue;
        }

        public override int GetHashCode()
        {
            return (int)buttonsValue;
        }

        public override string ToString()
        {
            return String.Format("{{IsConnected:{0}}}", IsConnected);
        }

        public bool IsButtonDown(Buttons button) { return ((this.buttonsValue & button) == button); }
        public bool IsButtonUp(Buttons button) { return ((this.buttonsValue & button) != button); }
        public GamePadButtons Buttons { get { return this.buttons; } }
        public GamePadDPad DPad { get { return this.dPad; } }

        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
            internal set
            {
                this.isConnected = value;
            }
        }

        public int PacketNumber
        {
            get
            {
                return this.packetNumber;
            }
            internal set
            {
                this.packetNumber = value;
            }
        }

        public GamePadThumbSticks ThumbSticks { get { return this.thumbSticks; } }
        public GamePadTriggers Triggers { get { return this.triggers; } }



    }
}
