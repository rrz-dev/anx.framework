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
    public class NullDynamicSoundEffectInstance : IDynamicSoundEffectInstance
    {
        private SoundState state = SoundState.Stopped;

        public event EventHandler<EventArgs> BufferNeeded;

        public int PendingBufferCount
        {
            get { return 0; }
        }

        public void SubmitBuffer(byte[] buffer)
        {
        }

        public void SubmitBuffer(byte[] buffer, int offset, int count)
        {
        }

        public bool IsLooped
        {
            get;
            set;
        }

        public float Pan
        {
            get;
            set;
        }

        public float Pitch
        {
            get;
            set;
        }

        public Framework.Audio.SoundState State
        {
            get { return state; }
        }

        public float Volume
        {
            get;
            set;
        }

        public void Play()
        {
            state = SoundState.Playing;
        }

        public void Pause()
        {
            state = SoundState.Paused;
        }

        public void Stop(bool immediate)
        {
            state = SoundState.Stopped;
        }

        public void Resume()
        {
            if (state == SoundState.Paused)
            {
                state = SoundState.Playing;
            }
        }

        public void Apply3D(Framework.Audio.AudioListener[] listeners, Framework.Audio.AudioEmitter emitter)
        {
        }

        public void Dispose()
        {
        }
    }
}
