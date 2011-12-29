#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework;
using ANX.Framework.Input;
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
    /// <summary>
    /// Wrapper arround another IKeyboard, will record all inputs and allows playback.
    /// </summary>
    public class RecordingKeyboard : RecordableDevice, IKeyboard
    {
        private IKeyboard realKeyboard;
        private Keys[] recordedKeys;
        private byte[] keyBitmasks;

        private IntPtr tmpWindowHandle = IntPtr.Zero;

        public IntPtr WindowHandle
        {
            get
            {
                if (!isInitialized)
                    return IntPtr.Zero;
                else
                    return realKeyboard.WindowHandle;
            }
            set
            {
                if (!isInitialized) //The GameWindow might assign a WindowHadle even before the real Mouse is loaded. We save this Handle and assign it in Initialize()
                    tmpWindowHandle = value;
                else
                    realKeyboard.WindowHandle = value;
            }
        }

        public KeyboardState GetState()
        {
            switch (RecordingState)
            {
                case RecordingState.None:
                    return realKeyboard.GetState();
                case RecordingState.Playback:
                    return ReadKeybaordState(PlayerIndex.One);
                case RecordingState.Recording:
                    KeyboardState realState = realKeyboard.GetState();
                    WriteKeyboardState(realState, PlayerIndex.One);
                    return realState;
                default:
                    throw new InvalidOperationException("The recordingState is invalid!");
            }
        }

        public KeyboardState GetState(PlayerIndex playerIndex)
        {
            switch (RecordingState)
            {
                case RecordingState.None:
                    return realKeyboard.GetState(playerIndex);
                case RecordingState.Playback:
                    return ReadKeybaordState(playerIndex);
                case RecordingState.Recording:
                    KeyboardState realState = realKeyboard.GetState(playerIndex);
                    WriteKeyboardState(realState, playerIndex);
                    return realState;
                default:
                    throw new InvalidOperationException("The recordingState is invalid!");
            }
        }

        private void WriteKeyboardState(KeyboardState state, PlayerIndex index)
        {
            Keys[] pressedKeys = state.GetPressedKeys();

            if (pressedKeys.Length == 0 || !pressedKeys.Any((key) => recordedKeys.Contains(key))) //No Key or none of the recorded keys is down
            {
                WriteState(null);
                return;
            }

            byte[] buffer = new byte[PacketLenght];

            buffer[0] = (byte)((int)index & 3); //Just the first two bits

            for (int i = 0; i < PacketLenght; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == PacketLenght - 1 && j == (i + 2) % 8)
                        break;

                    if (state.IsKeyDown(recordedKeys[i * 8 + j]))
                        buffer[i] |= keyBitmasks[i * 8 + j];
                }
            }

            WriteState(buffer);
        }

        private KeyboardState ReadKeybaordState(PlayerIndex expectedIndex)
        {
            byte[] buffer = ReadState();

            if (buffer == null)
                return new KeyboardState();

            if ((PlayerIndex)(buffer[0] & 3) != expectedIndex)
                throw new InvalidOperationException("The requested playerIndex does no match the next recorded state. Refer to documetation.");

            KeyboardState state = new KeyboardState();

            for (int i = 0; i < PacketLenght; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == PacketLenght - 1 && j == (i + 2) % 8)
                        break;

                    if ((buffer[i] & keyBitmasks[i * 8 + j]) != 0)
                        state.AddPressedKey(recordedKeys[i * 8 + j]);
                }
            }

            return state;
        }

        public void Dispose()
        {
            if (realKeyboard != null)
                realKeyboard.Dispose();
        }

        /// <summary>
        /// Intializes this instance using a new MemoryStream as the Buffer and the
        /// default's InputSystems Keyboard, recording the passed Keys.
        /// </summary>
        public void Initialize(params Keys[] keys)
        {
            Initialize(new MemoryStream(), InputDeviceFactory.Instance.GetDefaultKeyboard(), keys);
        }

        /// <summary>
        /// Intializes this instance using a new MemoryStream as the Buffer and the
        /// passed Keyboard, recording the passed Keys.
        /// </summary>
        public void Initialize(IKeyboard keyboard, params Keys[] keys)
        {
            Initialize(new MemoryStream(), keyboard, keys);
        }

        /// <summary>
        /// Intializes this instance using the passed Stream as the Buffer and the
        /// default's InputSystems Keyboard, recording the passed Keys.
        /// </summary>
        public void Initialize(Stream bufferStream, params Keys[] keys)
        {
            Initialize(bufferStream, InputDeviceFactory.Instance.GetDefaultKeyboard(), keys);
        }

        /// <summary>
        /// Intializes this instance using the passed Stream as the Buffer and the
        /// passed Keyboard, recording the passed Keys.
        /// </summary>
        public void Initialize(Stream bufferStream, IKeyboard keyboard, params Keys[] keys)
        {
            realKeyboard = keyboard;
            recordedKeys = keys;

            if (tmpWindowHandle != IntPtr.Zero)
                WindowHandle = tmpWindowHandle;

            PacketLenght = GetPaketSize(); //8bit per byte

            UpdateBitmasks();

            base.Initialize(bufferStream);
        }

        private int GetPaketSize()
        {
            if (recordedKeys.Length % 8 <= 6) //two bit free in the last byte
                return (int)Math.Ceiling((double)recordedKeys.Length / 8.0);
            else
                return (int)Math.Ceiling((double)recordedKeys.Length / 8.0) + 1; //we need a additional byte to store the player index
        }

        private void UpdateBitmasks()
        {
            keyBitmasks = new byte[recordedKeys.Length];

            for (int i = 0; i < recordedKeys.Length; i++)
            {
                keyBitmasks[i] = (byte)Math.Pow(2.0, (i + 2) % 8); //The first two bits are reserved for the player index
            }
        }
    }
}
