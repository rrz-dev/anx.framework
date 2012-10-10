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
	public sealed class AlbumCollection : IEnumerable<Album>, IEnumerable, IDisposable
	{
		private readonly List<Album> albums;

	    public bool IsDisposed { get; private set; }

	    public int Count
	    {
	        get { return albums.Count; }
	    }

	    public Album this[int index]
	    {
	        get { return albums[index]; }
	    }

	    internal AlbumCollection(IEnumerable<Album> setAlbums)
		{
            albums = new List<Album>(setAlbums);
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
