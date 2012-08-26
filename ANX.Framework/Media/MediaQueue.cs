using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class MediaQueue
	{
		#region Private
		private List<Song> queue;
		#endregion

		#region Public
		public int Count
		{
			get
			{
				return queue.Count;
			}
		}

		public int ActiveSongIndex
		{
			get;
			set;
		}

		public Song ActiveSong
		{
			get
			{
				return queue[ActiveSongIndex];
			}
		}

		public Song this[int index]
		{
			get
			{
				return queue[index];
			}
		}
		#endregion

		#region Constructor
		internal MediaQueue()
		{
			queue = new List<Song>();
		}
		#endregion

		internal void Play(Song song)
		{
			if (song == null)
				throw new ArgumentNullException("song");

			queue.Add(song);
		}

		internal void Play(SongCollection songCollection)
		{
			if (songCollection == null)
				throw new ArgumentNullException("songCollection");

			queue.AddRange(songCollection);
		}

		internal void MoveNext()
		{
			if (Count > 0)
			{
				if (ActiveSongIndex < Count - 1)
					ActiveSongIndex++;
				else
					ActiveSongIndex = 0;
			}
		}

		internal void MovePrevious()
		{
			if (Count > 0)
			{
				if (ActiveSongIndex > 0)
					ActiveSongIndex--;
				else
					ActiveSongIndex = Count - 1;
			}
		}
	}
}
