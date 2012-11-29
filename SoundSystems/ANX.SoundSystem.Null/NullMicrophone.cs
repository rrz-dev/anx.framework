#region Using Statements
using System;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.Audio;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Null
{
    public class NullMicrophone : IMicrophone
    {
        private MicrophoneState state = MicrophoneState.Stopped;

        public MicrophoneState State
        {
            get { return state; }
        }

        public int SampleRate
        {
            get { return 0; }
        }

        public bool IsHeadset
        {
            get { return false; }
        }

        public TimeSpan BufferDuration
        {
            get;
            set;
        }

        public event EventHandler<EventArgs> BufferReady;

        public void Stop()
        {
            state = MicrophoneState.Stopped;
        }

        public void Start()
        {
            state = MicrophoneState.Started;
        }

        public int GetSampleSizeInBytes(ref TimeSpan duration)
        {
            return 0;
        }

        public TimeSpan GetSampleDuration(int sizeInBytes)
        {
            return TimeSpan.Zero;
        }

        public int GetData(byte[] buffer)
        {
            return 0;
        }

        public int GetData(byte[] buffer, int offset, int count)
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
