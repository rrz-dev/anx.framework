using System;
using ANX.Framework.Media;

namespace ANX.Framework.NonXNA.SoundSystem
{
    public interface ISong : IDisposable
    {
        TimeSpan Duration { get; }
        TimeSpan PlayPosition { get; }
        MediaState State { get; }

        void Play();
        void Stop();
        void Pause();
        void Resume();
        void Update();
        void GetVisualizationData(VisualizationData data);
    }
}
