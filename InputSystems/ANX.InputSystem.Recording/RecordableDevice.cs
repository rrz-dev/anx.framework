#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// How many bytes this Instance requires per Paket. Must never change!
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
        protected void Initialize(Stream bufferStream)
        {
            recordStream = bufferStream;

            isInitialized = true;
        }

        public virtual void StartRecording()
        {
            if (!isInitialized)
                throw new InvalidOperationException("This instance is not initialized!");
            
            if (RecordingState == RecordingState.Recording)
                return;

            RecordingState = RecordingState.Recording;
        }

        public virtual void StopRecording()
        {
            if (RecordingState != RecordingState.Recording)
                throw new InvalidOperationException("Recording wasn't started for this device!");

            RecordingState = RecordingState.None;
        }

        public virtual void StartPlayback()
        {
            if (!isInitialized)
                throw new InvalidOperationException("This instance is not initialized!");
            
            if (RecordingState == RecordingState.Recording)
                throw new InvalidOperationException("Recording is currently running for this device.");

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
                throw new InvalidOperationException("The passed state's lenght does not match the speficed FramePaketLenght.");

            if (nullStateCounter > 0) //Note how many packets we had nothing
            {
                recordStream.WriteByte((byte)PacketType.NullFrameCounter);
                recordStream.Write(BitConverter.GetBytes(nullStateCounter), 0, 4);
            }

            recordStream.WriteByte((byte)PacketType.InputData);
            recordStream.Write(state, 0, state.Length);
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
                return null; //TODO: Better switch to RecordingState.None here?
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
                default:
                    throw new NotImplementedException("The PaketType " + Enum.GetName(typeof(PacketType), type) + "is not supported.");
            }

        }

        /// <summary>
        /// Fires the EndOfPlaybackReaced event. Overwrite this method to change
        /// this behavoir.
        /// </summary>
        protected virtual void OnEndOfPlaybackReached()
        {
            if (EndOfPlaybackReached != null)
                EndOfPlaybackReached(this, EventArgs.Empty);
        }
    }
}
