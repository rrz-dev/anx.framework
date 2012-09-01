using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public struct GamePadState
    {
        #region Private
        private GamePadThumbSticks thumbSticks;
        private GamePadTriggers triggers;
        private GamePadButtons buttons;
        private GamePadDPad dPad;

        private Buttons buttonsValue;
        #endregion

		#region Public
		public GamePadButtons Buttons
		{
			get { return this.buttons; }
		}

		public GamePadDPad DPad
		{
			get { return this.dPad; }
		}

		public bool IsConnected
		{
			get;
			internal set;
		}

		public int PacketNumber
		{
			get;
			internal set;
		}

		public GamePadThumbSticks ThumbSticks
		{
			get { return this.thumbSticks; }
		}
		public GamePadTriggers Triggers
		{
			get { return this.triggers; }
		}
		#endregion

		public GamePadState(GamePadThumbSticks thumbSticks, GamePadTriggers triggers, GamePadButtons buttons, GamePadDPad dPad)
			: this()
        {
            this.thumbSticks = thumbSticks;
            this.triggers = triggers;
            this.buttons = buttons;
			this.dPad = dPad;
			this.buttonsValue = this.buttons.Buttons | this.dPad.Buttons;
            this.IsConnected = true;
            this.PacketNumber = 0;
        }

        public GamePadState(Vector2 leftThumbStick, Vector2 rightThumbStick, float leftTrigger, float rightTrigger,
			params Buttons[] buttons)
			: this()
        {
            this.thumbSticks = new GamePadThumbSticks(leftThumbStick, rightThumbStick);
            this.triggers = new GamePadTriggers(leftTrigger, rightTrigger);

            Buttons buttonField = 0;
            for (int i = 0; i < buttons.Length; i++)
                buttonField |= buttons[i];
            
			this.buttonsValue = buttonField;
            this.IsConnected = true;
            this.PacketNumber = 0;

            this.buttons = new GamePadButtons(this.buttonsValue);
            this.dPad = new GamePadDPad(this.buttonsValue);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(GamePadState))
                return this == (GamePadState)obj;

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

		public bool IsButtonDown(Buttons button)
		{
			return (this.buttonsValue & button) == button;
		}

		public bool IsButtonUp(Buttons button)
		{
			return (this.buttonsValue & button) != button;
		}
    }
}
