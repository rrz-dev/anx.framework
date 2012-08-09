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
    public static class GamePad
    {
        private static IGamePad gamePad;

        static GamePad()
        {
            gamePad = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().GamePad;
        }

        public static GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
        {
            return gamePad.GetCapabilities(playerIndex);
        }
        
        public static GamePadState GetState(PlayerIndex playerIndex) 
        {
            bool isConnected;
            int packetNumber;
            GamePadState ret = gamePad.GetState(playerIndex,out isConnected, out packetNumber);
            ret.IsConnected = isConnected;
            ret.PacketNumber = packetNumber;
            return ret;
        }
        
        public static GamePadState GetState (PlayerIndex playerIndex,GamePadDeadZone deadZoneMode)
        {
            bool isConnected;
            int packetNumber;
            GamePadState ret = gamePad.GetState(playerIndex,deadZoneMode, out isConnected, out packetNumber);
            ret.IsConnected = isConnected;
            ret.PacketNumber = packetNumber;
            return ret;
        }

        public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            return gamePad.SetVibration(playerIndex, leftMotor, rightMotor);
        }

    }
}
