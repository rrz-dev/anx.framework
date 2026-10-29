using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public static class MediaPlayer
    {
        #region Events
        public static event EventHandler<EventArgs> ActiveSongChanged;
        public static event EventHandler<EventArgs> MediaStateChanged;
        #endregion

        private static bool isShuffled;
        private static bool isRepeating;
        private static float volume;
        private static MediaState currentState;
        internal static float VolumeToUse
        {
            get { return IsMuted ? 0f : volume; }
        }

        #region Public
        public static bool IsShuffled
        {
            get { return isShuffled; }
            set
            {
                isShuffled = value;
                Queue.UpdateOrder();
            }
        }

        public static bool IsRepeating
        {
            get { return isRepeating; }
            set { isRepeating = value; }
        }

        public static float Volume
        {
            get { return volume; }
            set { volume = MathHelper.Clamp(value, 0f, 1f); }
        }

        public static bool IsVisualizationEnabled { get; set; }
        public static bool IsMuted { get; set; }
        public static MediaQueue Queue { get; private set; }
        public static MediaState State
        {
            get { return currentState; }
            private set
            {
                if (currentState == value)
                    return;

                currentState = value;
                if (MediaStateChanged != null)
                    MediaStateChanged(null, EventArgs.Empty);
            }
        }

        public static TimeSpan PlayPosition
        {
            get
            {
                return Queue.ActiveSong == null
                    ? TimeSpan.Zero : Queue.ActiveSong.NativeSong.PlayPosition;
            }
        }

        public static bool GameHasControl
        {
            get { return true; }
        }
        #endregion

        #region Constructor
        static MediaPlayer()
        {
            currentState = MediaState.Stopped;
            volume = 1f;
            isRepeating = false;
            IsMuted = false;
            IsVisualizationEnabled = false;
            isShuffled = false;
            Queue = new MediaQueue();
            FrameworkDispatcher.OnUpdate += Tick;
        }
        #endregion

        #region Play
        public static void Play(Song song)
        {
            Queue.Play(song);
        }

        public static void Play(SongCollection songCollection)
        {
            Queue.Play(songCollection);
        }

        public static void Play(SongCollection songCollection, int index)
        {
            Queue.Play(songCollection, index);
        }
        #endregion

        #region Pause
        public static void Pause()
        {
            if (Queue.ActiveSong != null)
                Queue.ActiveSong.Pause();
        }
        #endregion

        #region Resume
        public static void Resume()
        {
            if (Queue.ActiveSong != null)
                Queue.ActiveSong.Resume();
        }
        #endregion

        #region Stop
        public static void Stop()
        {
            Queue.Stop();
        }
        #endregion

        #region MoveNext
        public static void MoveNext()
        {
            Queue.MoveNext(false);
        }
        #endregion

        #region MovePrevious
        public static void MovePrevious()
        {
            Queue.MovePrevious();
        }
        #endregion

        #region Tick
        private static void Tick()
        {
            if (Queue.ActiveSong == null)
            {
                State = MediaState.Stopped;
                return;
            }

            Queue.ActiveSong.NativeSong.Update();

            State = Queue.ActiveSong.State;
            if (Queue.ActiveSong.State != MediaState.Stopped)
                return;

            if (Queue.MoveNext(isRepeating))
                State = MediaState.Playing;

            if (ActiveSongChanged != null)
                ActiveSongChanged(null, EventArgs.Empty);
        }
        #endregion

        #region GetVisualizationData
        public static void GetVisualizationData(VisualizationData visualizationData)
        {
            if (visualizationData == null)
                throw new ArgumentNullException("visualizationData");

            if (IsVisualizationEnabled == false)
                return;

            if(Queue.ActiveSong != null)
                Queue.ActiveSong.NativeSong.GetVisualizationData(visualizationData);
        }
        #endregion
    }
}
