using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class AlbumCollection
		: IEnumerable<Album>, IEnumerable, IDisposable
	{
		private List<Album> albums;

		public bool IsDisposed
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return albums.Count;
			}
		}

		public Album this[int index]
		{
			get
			{
				return albums[index];
			}
		}

		public AlbumCollection()
		{
			albums = new List<Album>();
			IsDisposed = false;
		}

		~AlbumCollection()
		{
			Dispose();
		}

		#region GetEnumerator
		public IEnumerator<Album> GetEnumerator()
		{
			return albums.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return albums.GetEnumerator();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			IsDisposed = true;
			albums.Clear();
		}
		#endregion
	}
}
