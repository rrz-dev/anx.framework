using System;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public sealed class MediaQueue
	{
        private readonly List<Song> queue;
        private readonly List<Song> shuffledQueue;
        private int activeSongIndex;

		#region Public
	    public int Count
	    {
	        get { return queue.Count; }
	    }

        public int ActiveSongIndex
        {
            get { return activeSongIndex; }
            set
            {
                if (Count <= 0)
                    return;

                ActiveSong.Stop();
                activeSongIndex = Math.Min(value, queue.Count);
                ActiveSong.Play();
            }
        }

        public Song ActiveSong
	    {
            get { return shuffledQueue.Count <= 0 ? null : shuffledQueue[ActiveSongIndex]; }
	    }

	    public Song this[int index]
	    {
            get { return shuffledQueue[index]; }
	    }
	    #endregion

		#region Constructor
		internal MediaQueue()
		{
			queue = new List<Song>();
		    shuffledQueue = new List<Song>();
		}

        ~MediaQueue()
        {
            queue.Clear();
            shuffledQueue.Clear();
        }
		#endregion

        #region Play
        internal void Play(Song song)
		{
			if (song == null)
				throw new ArgumentNullException("song");

            Clear();
			queue.Add(song);
            shuffledQueue.Add(song);
            ActiveSong.Play();
		}

		internal void Play(SongCollection songCollection)
		{
			if (songCollection == null)
				throw new ArgumentNullException("songCollection");

            Clear();
            queue.AddRange(songCollection);
            UpdateOrder();
            ActiveSong.Play();
		}

        internal void Play(SongCollection songCollection, int index)
        {
            if (songCollection == null)
                throw new ArgumentNullException("songCollection");

            Clear();
            ActiveSongIndex = index;
            queue.AddRange(songCollection);
            UpdateOrder();
            ActiveSong.Play();
        }
        #endregion

        internal void UpdateOrder()
        {
            if (Count <= 0)
                return;

            Song currentPlayingSong = ActiveSong;
            if (MediaPlayer.IsShuffled)
                Shuffle();
            else
            {
                shuffledQueue.Clear();
                shuffledQueue.AddRange(queue.ToArray());
            }

            activeSongIndex = shuffledQueue.IndexOf(currentPlayingSong);
        }

        private void Clear()
        {
            Stop();
            ActiveSongIndex = 0;
            queue.Clear();
            shuffledQueue.Clear();
        }
        
        internal void Stop()
        {
            if(ActiveSong != null)
                ActiveSong.Stop();
        }

        private void Shuffle()
        {
            var rand = new Random();
            int n = queue.Count;
            while (n > 1)
            {
                int k = rand.Next(n);
                n--;
                Song value = shuffledQueue[k];
                shuffledQueue[k] = shuffledQueue[n];
                shuffledQueue[n] = value;
            }
        }

        #region MoveNext
        internal bool MoveNext(bool isRepeating)
		{
		    if (Count <= 0)
		        return false;

            ActiveSong.Stop();

            if (ActiveSongIndex < Count - 1)
                ActiveSongIndex++;
            else
            {
                ActiveSongIndex = 0;
                if (isRepeating == false)
                    return false;
            }

            ActiveSong.Play();
            return true;
		}
        #endregion

        #region MovePrevious
        internal void MovePrevious()
		{
		    if (Count <= 0)
		        return;

            ActiveSong.Stop();

		    if (ActiveSongIndex > 0)
		        ActiveSongIndex--;
		    else
                ActiveSongIndex = Count - 1;

            ActiveSong.Play();
		}
	    #endregion
	}
}
