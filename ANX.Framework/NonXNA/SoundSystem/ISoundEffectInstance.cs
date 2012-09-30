using System;
using ANX.Framework.Audio;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.SoundSystem
{
    public interface ISoundEffectInstance : IDisposable
    {
        bool IsLooped { get; set; }
        float Pan { get; set; }
        float Pitch { get; set; }
        SoundState State { get; }
        float Volume { get; set; }

        void Play();
        void Pause();
        void Stop(bool immediate);
        void Resume();
        void Apply3D(AudioListener[] listeners, AudioEmitter emitter);
    }
}
