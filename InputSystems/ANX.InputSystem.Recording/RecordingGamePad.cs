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

namespace ANX.InputSystem.Recording
{
    [Flags]
    enum GamePadRecordInfo : int
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
            this.Initialize(info, new MemoryStream(), InputDeviceFactory.Instance.GetDefaultGamePad());
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
            this.Initialize(info, bufferStream, InputDeviceFactory.Instance.GetDefaultGamePad());
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
