using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Media
{
	public sealed class PictureCollection
		: IEnumerable<Picture>, IEnumerable, IDisposable
	{
		private List<Picture> pictures;

		public bool IsDisposed
		{
			get;
			private set;
		}

		public int Count
		{
			get
			{
				return pictures.Count;
			}
		}

		public Picture this[int index]
		{
			get
			{
				return pictures[index];
			}
		}

		public PictureCollection()
		{
			pictures = new List<Picture>();
			IsDisposed = false;
		}

		~PictureCollection()
		{
			Dispose();
		}

		#region GetEnumerator
		public IEnumerator<Picture> GetEnumerator()
		{
			return pictures.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return pictures.GetEnumerator();
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
			IsDisposed = true;
			pictures.Clear();
		}
		#endregion
	}
}
