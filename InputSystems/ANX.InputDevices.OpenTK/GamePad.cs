#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.OpenTK
{
    public class GamePad : IGamePad
    {
        public Framework.Input.GamePadCapabilities GetCapabilities(Framework.PlayerIndex playerIndex)
        {
            throw new NotImplementedException();
        }

        public Framework.Input.GamePadState GetState(Framework.PlayerIndex playerIndex, out bool isConnected, out int packetNumber)
        {
            throw new NotImplementedException();
        }

        public Framework.Input.GamePadState GetState(Framework.PlayerIndex playerIndex, Framework.Input.GamePadDeadZone deadZoneMode, out bool isConnected, out int packetNumber)
        {
            throw new NotImplementedException();
        }

        public bool SetVibration(Framework.PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            throw new NotImplementedException();
        }
    }
}
