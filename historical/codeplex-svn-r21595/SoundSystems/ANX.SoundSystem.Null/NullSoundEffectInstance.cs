#region Using Statements
using System;
using ANX.Framework.NonXNA.SoundSystem;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Null
{
    public class NullSoundEffectInstance : ISoundEffectInstance
    {
        private Framework.Audio.SoundState soundState = Framework.Audio.SoundState.Stopped;

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
            get 
            { 
                return soundState; 
            }
        }

        public float Volume
        {
            get;
            set;
        }

        public void Play()
        {
            soundState = Framework.Audio.SoundState.Playing;
        }

        public void Pause()
        {
            soundState = Framework.Audio.SoundState.Paused;
        }

        public void Stop(bool immediate)
        {
            soundState = Framework.Audio.SoundState.Stopped;
        }

        public void Resume()
        {
            if (soundState == Framework.Audio.SoundState.Paused)
            {
                soundState = Framework.Audio.SoundState.Playing;
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
