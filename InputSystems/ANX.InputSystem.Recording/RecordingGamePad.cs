#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;
using ANX.Framework;
using System.IO;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Recording
{
    [Flags]
    public enum GamePadRecordInfo : int
    {
        LeftStick = 1,
        RightStick = 2,
        LeftTrigger = 4,
        RightTrigger = 8,
        AButton = 16,
        BButton = 32,
        XButton = 64,
        YButton = 128,
        StartButton = 256,
        BackButton = 512,
        LeftShoulderButton = 1024,
        RightShoulderButton = 2048,
        LeftStickButton = 4096,
        RightStickButton = 8192,
        DPadUp = 16384,
        DPadDown = 32768,
        DPadLeft = 65536,
        DPadRight = 131072,

        BothSticks = LeftStick | RightStick,
        BothTriggers = LeftTrigger | RightTrigger,
        AllAnalog = BothSticks | BothTriggers,
        ABXYButton = AButton | BButton | XButton | YButton,
        BothStickButtons = LeftStickButton | RightStickButton,
        BothSoulderButtons = LeftShoulderButton | RightShoulderButton,
        AllDPad = DPadUp | DPadDown | DPadLeft | DPadRight,
        AllButtons = ABXYButton | BothStickButtons | StartButton | BackButton | BothSoulderButtons,
        All = AllAnalog | AllButtons | AllDPad
    }
    
    /// <summary>
    /// Wrapper arround another IGamePad, will record all inputs and allows playback.
    /// </summary>
    public class RecordingGamePad : RecordableDevice, IGamePad
    {
        private IGamePad realGamePad;
        private GamePadRecordInfo recordInfo;
        
        public GamePadCapabilities GetCapabilities(PlayerIndex playerIndex) //no recording here...
        {
            return realGamePad.GetCapabilities(playerIndex);
        }

        public GamePadState GetState(PlayerIndex playerIndex, out bool isConnected, out int packetNumber)
        {
            throw new NotImplementedException();
        }

        public GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode, out bool isConnected, out int packetNumber)
        {
            throw new NotImplementedException();
        }

        public bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Intializes this instance using a new MemoryStream as the Buffer, the
        /// default's InputSystems GamePad and the passed GamePadRecordInfo.
        /// </summary>
        public void Initialize(GamePadRecordInfo info)
        {
            this.Initialize(info, new MemoryStream(), InputDeviceFactory.Instance.CreateDefaultGamePad());
        }

        /// <summary>
        /// Intializes this instance using a new MemoryStream as the Buffer, recording 
        /// the passed IGamePad, using the passed GamePadRecordInfo.
        /// </summary>
        public void Initialize(GamePadRecordInfo info, IGamePad gamePad)
        {
            this.Initialize(info, new MemoryStream(), gamePad);
        }

        /// <summary>
        /// Intializes this instance using the passed Stream as the Buffer, the
        /// default's InputSystems GamePad and the passed GamePadRecordInfo.
        /// </summary>
        public void Initialize(GamePadRecordInfo info, Stream bufferStream)
        {
            this.Initialize(info, bufferStream, InputDeviceFactory.Instance.CreateDefaultGamePad());
        }

        /// <summary>
        /// Intializes this instance using the passed Stream as the Buffer, recording 
        /// the passed IGamePad, using the passed GamePadRecordInfo.
        /// </summary>
        public void Initialize(GamePadRecordInfo info, Stream bufferStream, IGamePad gamePad)
        {
            realGamePad = gamePad;

            recordInfo = info;
            PacketLenght = GetPaketSize(info);

            base.Initialize(bufferStream);
        }

        private int GetPaketSize(GamePadRecordInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
