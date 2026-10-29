using System;
using System.Collections;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.InProgress)]
	public sealed class SongCollection : IEnumerable<Song>, IEnumerable, IDisposable
	{
		private readonly List<Song> songs;

	    public bool IsDisposed { get; private set; }

	    public int Count
	    {
	        get { return songs.Count; }
	    }

	    public Song this[int index]
	    {
	        get { return songs[index]; }
	    }

	    internal SongCollection(IEnumerable<Song> allSongs)
		{
            songs = new List<Song>(allSongs);
			IsDisposed = false;
		}

		~SongCollection()
		{
			Dispose();
		}

		#region GetEnumerator
		public IEnumerator<Song> GetEnumerator()
		{
			return songs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return songs.GetEnumerator();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			IsDisposed = true;
			songs.Clear();
		}
		#endregion
	}
}
