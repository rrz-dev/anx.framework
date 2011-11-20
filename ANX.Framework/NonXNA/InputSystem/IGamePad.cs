using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Input;

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
