using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class PlaylistCollection
		: IEnumerable<Playlist>, IEnumerable, IDisposable
	{
		private List<Playlist> playlists;

		public bool IsDisposed
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return playlists.Count;
			}
		}

		public Playlist this[int index]
		{
			get
			{
				return playlists[index];
			}
		}

		internal PlaylistCollection()
		{
			playlists = new List<Playlist>();
			IsDisposed = false;
		}

		~PlaylistCollection()
		{
			Dispose();
		}

		#region GetEnumerator
		public IEnumerator<Playlist> GetEnumerator()
		{
			return playlists.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return playlists.GetEnumerator();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			IsDisposed = true;
			playlists.Clear();
		}
		#endregion
	}
}
