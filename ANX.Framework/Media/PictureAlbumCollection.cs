using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class PictureAlbumCollection
		: IEnumerable<PictureAlbum>, IEnumerable, IDisposable
	{
		private List<PictureAlbum> pictureAlbums;

		public bool IsDisposed
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return pictureAlbums.Count;
			}
		}

		public PictureAlbum this[int index]
		{
			get
			{
				return pictureAlbums[index];
			}
		}

		public PictureAlbumCollection()
		{
			pictureAlbums = new List<PictureAlbum>();
			IsDisposed = false;
		}

		~PictureAlbumCollection()
		{
			Dispose();
		}

		#region GetEnumerator
		public IEnumerator<PictureAlbum> GetEnumerator()
		{
			return pictureAlbums.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return pictureAlbums.GetEnumerator();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			IsDisposed = true;
			pictureAlbums.Clear();
		}
		#endregion
	}
}
