using System;
using System.Linq;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input
{
	[PercentageComplete(100)]
    [Developer("AstrorEnales")]
	[TestState(TestStateAttribute.TestState.Tested)]
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

	    public bool IsConnected { get; internal set; }
	    public int PacketNumber { get; internal set; }

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

            AddToBitfieldIfPressed(Input.Buttons.A, buttons.A);
            AddToBitfieldIfPressed(Input.Buttons.B, buttons.B);
            AddToBitfieldIfPressed(Input.Buttons.X, buttons.X);
            AddToBitfieldIfPressed(Input.Buttons.Y, buttons.Y);
            AddToBitfieldIfPressed(Input.Buttons.BigButton, buttons.BigButton);
            AddToBitfieldIfPressed(Input.Buttons.Back, buttons.Back);
            AddToBitfieldIfPressed(Input.Buttons.Start, buttons.Start);
            AddToBitfieldIfPressed(Input.Buttons.RightStick, buttons.RightStick);
            AddToBitfieldIfPressed(Input.Buttons.LeftStick, buttons.LeftStick);
            AddToBitfieldIfPressed(Input.Buttons.RightShoulder, buttons.RightShoulder);
            AddToBitfieldIfPressed(Input.Buttons.LeftShoulder, buttons.LeftShoulder);

		    AddToBitfieldIfPressed(Input.Buttons.DPadUp, dPad.Up);
		    AddToBitfieldIfPressed(Input.Buttons.DPadDown, dPad.Down);
		    AddToBitfieldIfPressed(Input.Buttons.DPadLeft, dPad.Left);
		    AddToBitfieldIfPressed(Input.Buttons.DPadRight, dPad.Right);
		    
            this.IsConnected = true;
            this.PacketNumber = 0;
        }

        private void AddToBitfieldIfPressed(Buttons button, ButtonState state)
        {
            if (state == ButtonState.Pressed)
                buttonsValue |= button;
        }

        public GamePadState(Vector2 leftThumbStick, Vector2 rightThumbStick, float leftTrigger, float rightTrigger,
			params Buttons[] buttons)
			: this()
        {
            this.thumbSticks = new GamePadThumbSticks(leftThumbStick, rightThumbStick);
            this.triggers = new GamePadTriggers(leftTrigger, rightTrigger);

            Buttons buttonField = buttons.Aggregate<Buttons, Buttons>(0, (current, t) => current | t);

            this.buttonsValue = buttonField;
            this.IsConnected = true;
            this.PacketNumber = 0;

            this.buttons = new GamePadButtons(this.buttonsValue);
            this.dPad = new GamePadDPad(this.buttonsValue);
        }

        public override bool Equals(object obj)
        {
            return obj is GamePadState && this == (GamePadState)obj;
        }

	    public static bool operator ==(GamePadState lhs, GamePadState rhs)
        {
            return lhs.IsConnected == rhs.IsConnected && lhs.PacketNumber == rhs.PacketNumber &&
                lhs.thumbSticks == rhs.thumbSticks && lhs.triggers == rhs.triggers && lhs.buttons == rhs.buttons &&
                lhs.dPad == rhs.dPad;
        }

        public static bool operator !=(GamePadState lhs, GamePadState rhs)
        {
            return lhs.IsConnected != rhs.IsConnected || lhs.PacketNumber != rhs.PacketNumber ||
                lhs.thumbSticks != rhs.thumbSticks || lhs.triggers != rhs.triggers || lhs.buttons != rhs.buttons ||
                lhs.dPad != rhs.dPad;
        }

        public override int GetHashCode()
        {
            return thumbSticks.GetHashCode() ^ triggers.GetHashCode() ^ (buttons.GetHashCode() ^ IsConnected.GetHashCode()) ^
                (dPad.GetHashCode() ^ PacketNumber.GetHashCode());
        }

        public override string ToString()
        {
            return String.Format("{{IsConnected:{0}}}", IsConnected);
        }

		public bool IsButtonDown(Buttons button)
		{
			return (buttonsValue & button) == button;
		}

		public bool IsButtonUp(Buttons button)
		{
			return (buttonsValue & button) != button;
		}
    }
}
