using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class SongCollection
		: IEnumerable<Song>, IEnumerable, IDisposable
	{
		private List<Song> songs;

		public bool IsDisposed
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return songs.Count;
			}
		}

		public Song this[int index]
		{
			get
			{
				return songs[index];
			}
		}

		internal SongCollection()
		{
			songs = new List<Song>();
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
