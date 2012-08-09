#region Using Statements
using System;
using ANX.Framework.Input;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public  interface IGamePad
    {
        GamePadCapabilities GetCapabilities(PlayerIndex playerIndex);
        GamePadState GetState(PlayerIndex playerIndex, out bool isConnected, out int packetNumber);
        GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode, out bool isConnected, out int packetNumber);
        bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor);
    }
}
