using System;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("AstrorEnales")]
	public sealed class MediaQueue
	{
		private readonly List<Song> queue;

		#region Public
	    public int Count
	    {
	        get { return queue.Count; }
	    }

	    public int ActiveSongIndex { get; set; }

	    public Song ActiveSong
	    {
            get { return queue.Count <= 0 ? null : queue[ActiveSongIndex]; }
	    }

	    public Song this[int index]
	    {
	        get { return queue[index]; }
	    }
	    #endregion

		#region Constructor
		internal MediaQueue()
		{
			queue = new List<Song>();
		}

        ~MediaQueue()
        {
            queue.Clear();
        }
		#endregion

        #region Play
        internal void Play(Song song)
		{
			if (song == null)
				throw new ArgumentNullException("song");

            Clear();
			queue.Add(song);
            ActiveSong.Play();
		}

		internal void Play(SongCollection songCollection)
		{
			if (songCollection == null)
				throw new ArgumentNullException("songCollection");

            Clear();
            queue.AddRange(songCollection);
            // TODO: check if the shuffle is calculated after each finished song or like this!
            if (MediaPlayer.IsShuffled)
		        Shuffle();
            ActiveSong.Play();
		}

        internal void Play(SongCollection songCollection, int index)
        {
            if (songCollection == null)
                throw new ArgumentNullException("songCollection");

            Clear();
            ActiveSongIndex = index;
            queue.AddRange(songCollection);
            // TODO: check if the shuffle is calculated after each finished song or like this!
            if (MediaPlayer.IsShuffled)
		        Shuffle();
            ActiveSong.Play();
        }
        #endregion

        private void Clear()
        {
            Stop();
            ActiveSongIndex = 0;
            queue.Clear();
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
                Song value = queue[k];
                queue[k] = queue[n];
                queue[n] = value;
            }
        }

        #region MoveNext
        internal bool MoveNext(bool stopIfEnded)
		{
		    if (Count <= 0)
		        return false;

            ActiveSong.Stop();

            if (ActiveSongIndex < Count - 1)
                ActiveSongIndex++;
            else
            {
                ActiveSongIndex = 0;
                if (stopIfEnded)
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
