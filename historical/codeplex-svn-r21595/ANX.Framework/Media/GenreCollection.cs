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
	public sealed class GenreCollection : IEnumerable<Genre>, IEnumerable, IDisposable
	{
		private readonly List<Genre> genres;

	    public bool IsDisposed { get; private set; }

	    public int Count
	    {
	        get { return genres.Count; }
	    }

	    public Genre this[int index]
	    {
	        get { return genres[index]; }
	    }

	    internal GenreCollection(IEnumerable<Genre> setGenres)
		{
            genres = new List<Genre>(setGenres);
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
