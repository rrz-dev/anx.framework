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

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
                    if (i == PacketLenght - 1 && j == recordedKeys.Length % 8)
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
                return new KeyboardState(new Keys[0]);

            if ((PlayerIndex)(buffer[0] & 3) != expectedIndex)
                throw new InvalidOperationException("The requested playerIndex does no match the next recorded state. Refer to documetation.");

            KeyboardState state = new KeyboardState(new Keys[0]);

            for (int i = 0; i < PacketLenght; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == PacketLenght - 1 && j == recordedKeys.Length % 8)
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
