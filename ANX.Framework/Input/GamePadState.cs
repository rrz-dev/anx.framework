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
            this.isConnected = false;
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
            this.isConnected = false;
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
