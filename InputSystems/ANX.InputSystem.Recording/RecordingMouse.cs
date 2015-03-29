#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Input;
using System.IO;
using System.Runtime.InteropServices;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Recording
{
    [Flags]
    public enum MouseRecordInfo : byte
    {
        LeftButton = 1,
        RightButton = 2,
        MiddleButton = 4,
        X1Button = 8,
        X2Button = 16,
        ScrollWheel = 32,
        XPosition = 64,
        YPosition = 128,

        LRMButtons = LeftButton | RightButton | MiddleButton,
        XButtons = X1Button | X2Button,
        AllButtons = LRMButtons | XButtons,
        Position = XPosition | YPosition,
        All = AllButtons | Position | ScrollWheel
    }

    /// <summary>
    /// Used to store the ButtonStates packed to one byte.
    /// </summary>
    [Flags]
    enum MouseButtons : byte
    {
        Left = 1,
        Right = 2,
        Middle = 4,
        X1 = 8,
        X2 = 16
    }

    /// <summary>
    /// Wrapper arround another IMouse, will record all inputs and allows playback.
    /// </summary>
    public class RecordingMouse : RecordableDevice, IMouse
    {
        private IMouse realMouse;
        private MouseRecordInfo recordInfo;

        private WindowHandle tmpWindowHandle;

        public WindowHandle WindowHandle
        {
            get
            {
                if (!isInitialized)
                    return new WindowHandle();
                else
                    return realMouse.WindowHandle;
            }
            set
            {
                if (!isInitialized) //The GameWindow might assign a WindowHadle even before the real Mouse is loaded. We save this Handle and assign it in Initialize()
                    tmpWindowHandle = value;
                else
                    realMouse.WindowHandle = value;
            }
        }

        public MouseState GetState() //The main recording/playback logic is placed here
        {
            if (!isInitialized)
                throw new InvalidOperationException("This instance is not initialized! Refer to documenation for details.");
            
            switch (RecordingState)
            {
                case RecordingState.None:
                    return realMouse.GetState();
                case RecordingState.Playback:
                    //Read a state from the Stream and recreate the MouseState from it.
                    byte[] stateData = ReadState();

                    if (stateData == null) //No input at this position
                        return new MouseState();

                    byte readOffset = 0;
                    ButtonState left, right, middle, x1, x2;
                    int scrollWheel, xPos, yPos;

                    if ((recordInfo & MouseRecordInfo.AllButtons) != 0) //Any of the Buttons is recorded
                    {
                        MouseButtons buttons = (MouseButtons)stateData[readOffset++];

                        left = buttons.HasFlag(MouseButtons.Left) ? ButtonState.Pressed : ButtonState.Released;
                        right = buttons.HasFlag(MouseButtons.Right) ? ButtonState.Pressed : ButtonState.Released;
                        middle = buttons.HasFlag(MouseButtons.Middle) ? ButtonState.Pressed : ButtonState.Released;
                        x1 = buttons.HasFlag(MouseButtons.X1) ? ButtonState.Pressed : ButtonState.Released;
                        x2 = buttons.HasFlag(MouseButtons.X2) ? ButtonState.Pressed : ButtonState.Released;
                    }
                    else
                        left = right = middle = x1 = x2 = ButtonState.Released;

                    if (recordInfo.HasFlag(MouseRecordInfo.ScrollWheel))
                    {
                        scrollWheel = BitConverter.ToInt32(stateData, readOffset);
                        readOffset += 4;
                    }
                    else
                        scrollWheel = 0;

                    if (recordInfo.HasFlag(MouseRecordInfo.XPosition))
                    {
                        xPos = BitConverter.ToInt32(stateData, readOffset);
                        readOffset += 4;
                    }
                    else
                        xPos = 0;

                    if (recordInfo.HasFlag(MouseRecordInfo.YPosition))
                    {
                        yPos = BitConverter.ToInt32(stateData, readOffset);
                        readOffset += 4;
                    }
                    else
                        yPos = 0;

                    return new MouseState(xPos, yPos, scrollWheel, left, middle, right, x1, x2);
                case RecordingState.Recording:
                    MouseState state = realMouse.GetState();
                    //Pack the state to a buffer and save it. In can be never null, because the mouse has allways a position.
                    //TODO: Check if the position is not recorded
                    byte[] buffer = new byte[PacketLenght];
                    byte writeOffset = 0;
                    if ((recordInfo & MouseRecordInfo.AllButtons) != 0) //Any of the Buttons is recorded
                    {
                        buffer[writeOffset] |= state.LeftButton == ButtonState.Pressed ? (byte)MouseButtons.Left : (byte)0;
                        buffer[writeOffset] |= state.RightButton == ButtonState.Pressed ? (byte)MouseButtons.Right : (byte)0;
                        buffer[writeOffset] |= state.MiddleButton == ButtonState.Pressed ? (byte)MouseButtons.Middle : (byte)0;
                        buffer[writeOffset] |= state.XButton1 == ButtonState.Pressed ? (byte)MouseButtons.X1 : (byte)0;
                        buffer[writeOffset] |= state.XButton2 == ButtonState.Pressed ? (byte)MouseButtons.X2 : (byte)0;
                        writeOffset++;
                    }

                    if (recordInfo.HasFlag(MouseRecordInfo.ScrollWheel))
                    {
                        Array.ConstrainedCopy(BitConverter.GetBytes(state.ScrollWheelValue), 0, buffer, writeOffset, 4); //int is always 4 byte long.
                        writeOffset += 4;
                    }

                    if (recordInfo.HasFlag(MouseRecordInfo.XPosition))
                    {
                        Array.ConstrainedCopy(BitConverter.GetBytes(state.X), 0, buffer, writeOffset, 4); //int is always 4 byte long.
                        writeOffset += 4;
                    }

                    if (recordInfo.HasFlag(MouseRecordInfo.YPosition))
                    {
                        Array.ConstrainedCopy(BitConverter.GetBytes(state.Y), 0, buffer, writeOffset, 4); //int is always 4 byte long.
                        writeOffset += 4;
                    }

                    WriteState(buffer);

                    return state;
                default:
                    throw new InvalidOperationException("The recordingState is invalid!");
            }
        }

        public void SetPosition(int x, int y)
        {
            //We just pass this call the underlying IMouse, unless we are in Playback mode (we don't want the Mouse to jump arround during playback)
            //There is no need in recording this calls, as they are reflected in the next frame's Mouse position.
            if (RecordingState != RecordingState.Playback)
                realMouse.SetPosition(x, y);
        }

        /// <summary>
        /// Intializes this instance using a new MemoryStream as the Buffer, the
        /// default's InputSystems Mouse and the passed MouseRecordInfo.
        /// </summary>
        public void Initialize(MouseRecordInfo info)
        {
            this.Initialize(info, new MemoryStream(), InputDeviceFactory.Instance.CreateDefaultMouse());
        }

        /// <summary>
        /// Intializes this instance using a new MemoryStream as the Buffer,recording 
        /// the passed IMouse, using the passed MouseRecordInfo.
        /// </summary>
        public void Initialize(MouseRecordInfo info, IMouse mouse)
        {
            this.Initialize(info, new MemoryStream(), mouse);
        }

        /// <summary>
        /// Intializes this instance using the passed Stream as the Buffer, the
        /// default's InputSystems Mouse and the passed MouseRecordInfo.
        /// </summary>
        public void Initialize(MouseRecordInfo info, Stream bufferStream)
        {
            this.Initialize(info, bufferStream, InputDeviceFactory.Instance.CreateDefaultMouse());
        }

        /// <summary>
        /// Intializes this instance using the passed Stream as the Buffer, recording 
        /// the passed IMouse, using the passed MouseRecordInfo.
        /// </summary>
        public void Initialize(MouseRecordInfo info, Stream bufferStream, IMouse mouse)
        {
            realMouse = mouse;

            if (tmpWindowHandle.IsValid)
                WindowHandle = tmpWindowHandle;

            recordInfo = info;
            PacketLenght = GetPaketSize(info);

            base.Initialize(bufferStream);
        }

        private int GetPaketSize(MouseRecordInfo info)
        {
            int ret = 0;

            if ((info & MouseRecordInfo.AllButtons) != 0) //We pack all Buttons in one byte, so it does not matter witch buttons are set.
                ret += sizeof(byte);

            if (info.HasFlag(MouseRecordInfo.XPosition))
                ret += sizeof(int);
            if (info.HasFlag(MouseRecordInfo.YPosition))
                ret += sizeof(int);
            if (info.HasFlag(MouseRecordInfo.ScrollWheel))
                ret += sizeof(int);

            return ret;
        }
    }
}
