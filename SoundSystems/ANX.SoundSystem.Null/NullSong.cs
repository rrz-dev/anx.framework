#region Using Statements
using System;
using ANX.Framework.NonXNA.SoundSystem;
using ANX.Framework.Media;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.SoundSystem.Null
{
    public class NullSong : ISong
    {
        private MediaState state = Framework.Media.MediaState.Stopped;

        public TimeSpan Duration
        {
            get { return TimeSpan.Zero; }
        }

        public TimeSpan PlayPosition
        {
            get { return TimeSpan.Zero; }
        }

        public MediaState State
        {
            get { return state; }
        }

        public void Play()
        {
            state = MediaState.Playing;
        }

        public void Stop()
        {
            state = MediaState.Stopped;
        }

        public void Pause()
        {
            state = MediaState.Paused;
        }

        public void Resume()
        {
            if (state == MediaState.Paused)
            {
                state = MediaState.Playing;
            }
        }

        public void Update()
        {
        }

        public void GetVisualizationData(VisualizationData data)
        {
            data = new VisualizationData();
        }

        public void Dispose()
        {
        }
    }
}
