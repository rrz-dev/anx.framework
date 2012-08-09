using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class GenreCollection
		: IEnumerable<Genre>, IEnumerable, IDisposable
	{
		private List<Genre> genres;

		public bool IsDisposed
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return genres.Count;
			}
		}

		public Genre this[int index]
		{
			get
			{
				return genres[index];
			}
		}

		public GenreCollection()
		{
			genres = new List<Genre>();
			IsDisposed = false;
		}

		~GenreCollection()
		{
			Dispose();
		}

		#region GetEnumerator
		public IEnumerator<Genre> GetEnumerator()
		{
			return genres.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return genres.GetEnumerator();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			IsDisposed = true;
			genres.Clear();
		}
		#endregion
	}
}
