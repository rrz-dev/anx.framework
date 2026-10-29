#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Recording
{
    /// <summary>
    /// Abstract Class. Classes derived from this class allow recording and Playback of Inputs.
    /// </summary>
    public abstract class RecordableDevice
    {
        protected Stream recordStream; //The stream where the input is written to
        protected int nullStateCounter; //Used to sum up frames with no input.
        protected bool isInitialized = false;

        public RecordingState RecordingState { get; protected set; }

        public event EventHandler EndOfPlaybackReached;

        /// <summary>
        /// How many bytes this Instance requires per Packet. Must never change!
        /// </summary>
        public int PacketLenght { get; protected set; }

        public RecordableDevice()
        {
            RecordingState = RecordingState.None;
        }

        /// <summary>
        /// Initializes the Device using the specified stream
        /// for input-buffering.
        /// </summary>
        protected virtual void Initialize(Stream bufferStream)
        {
            if (!bufferStream.CanRead || !bufferStream.CanWrite)
                throw new ArgumentException("The stream must support read and write opearions!", "bufferStream");
            
            recordStream = bufferStream;

            isInitialized = true;
        }

        public virtual void StartRecording()
        {
            if (!isInitialized)
                throw new InvalidOperationException("This instance is not initialized!");
            
            if (RecordingState == RecordingState.Recording)
                return;

            //Reset the stream if we can seek
            if(recordStream.CanSeek)
                recordStream.Position = 0;

            RecordingState = RecordingState.Recording;
        }

        public virtual void StopRecording()
        {
            if (RecordingState != RecordingState.Recording)
                throw new InvalidOperationException("Recording wasn't started for this device!");

            nullStateCounter = 0;
            RecordingState = RecordingState.None;
        }

        public virtual void StartPlayback()
        {
            if (!isInitialized)
                throw new InvalidOperationException("This instance is not initialized!");
            
            if (RecordingState == RecordingState.Recording)
                throw new InvalidOperationException("Recording is currently running for this device.");

            //Reset the stream if we can seek
            if (recordStream.CanSeek)
                recordStream.Position = 0;

            RecordingState = RecordingState.Playback;
        }

        public virtual void StopPlayback()
        {
            RecordingState = RecordingState.None;
        }

        /// <summary>
        /// Writes the current input state to the buffering stream. Pass null
        /// for state, if no input is done (no keys down etc.).
        /// </summary>
        protected virtual void WriteState(byte[] state)
        {
            if (state == null)
            {
                nullStateCounter++;
                return;
            }

            if (state.Length != PacketLenght)
                throw new InvalidOperationException("The passed state's lenght does not match the speficed FramePacketLenght.");

            TryWriteNullStates();

            recordStream.WriteByte((byte)PacketType.InputData);
            recordStream.Write(state, 0, state.Length);
        }

        /// <summary>
        /// Writes a custom packet to the stream. When this packet is found during ReadState()
        /// HandleUserPacket is called to handle the packet. Please note that the packetTypes 0 and
        /// 1 are reseved.
        /// </summary>
        protected virtual void WriteUserState(byte packetType, byte[] packetData)
        {
            TryWriteNullStates();

            recordStream.WriteByte(packetType);
					// TODO: cast to byte correct? created build error cause of WriteByte and int as value
            recordStream.WriteByte((byte)packetData.Length);
        }

        private void TryWriteNullStates()
        {
            if (nullStateCounter > 0) //Note how many packets we had nothing
            {
                recordStream.WriteByte((byte)PacketType.NullFrameCounter);
                recordStream.Write(BitConverter.GetBytes(nullStateCounter), 0, 4);
                nullStateCounter = 0;
            }
        }

        /// <summary>
        /// Reads the next input-state from the buffering stream. Might
        /// return null, if no input was made at this Position.
        /// </summary>
        protected virtual byte[] ReadState()
        {
            if (nullStateCounter > 0) //we have null-states pending
            {
                nullStateCounter--;
                return null;
            }

            if (recordStream.Position == recordStream.Length)
            {
                OnEndOfPlaybackReached();
                return null;
            }

            PacketType type = (PacketType)recordStream.ReadByte();
            switch (type)
            {
                case PacketType.NullFrameCounter:
                    byte[] buffer = new byte[4];
                    recordStream.Read(buffer, 0, 4);
                    nullStateCounter = BitConverter.ToInt32(buffer, 0) - 1;
                    return null;
                case PacketType.InputData:
                    byte[] buffer2 = new byte[PacketLenght];
                    recordStream.Read(buffer2, 0, PacketLenght);
                    return buffer2;
                default: //Custom Packet Type
                    byte packetLenght = (byte)recordStream.ReadByte();
                    byte[] buffer3 = new byte[packetLenght];
                    recordStream.Read(buffer3, 0, packetLenght);
                    HandleUserPacket((byte)type, buffer3);
                    return ReadState(); //We read another packet until we find a "valid" one.
            }

        }

        /// <summary>
        /// Fires the EndOfPlaybackReaced event and Calls StopPlayback().
        /// Overwrite this method to change this behavoir.
        /// </summary>
        protected virtual void OnEndOfPlaybackReached()
        {
            StopPlayback();

            if (EndOfPlaybackReached != null)
                EndOfPlaybackReached(this, EventArgs.Empty);
        }

        /// <summary>
        /// Overwrite this method to handle custom packet types written by WriteUserState(). When
        /// ReadState() encounters an unknown packet type this method is called.
        /// </summary>
        protected virtual void HandleUserPacket(byte packetType, byte[] packetData)
        {
        }
    }
}
