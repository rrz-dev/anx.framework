using System;
using System.Collections.Generic;
using System.Collections;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(0)]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class NetworkSessionProperties : IList<int?>, ICollection<int?>,
		IEnumerable<int?>, IEnumerable
	{
		#region IList<int?> Member

		public int IndexOf(int? item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, int? item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public int? this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region ICollection<int?> Member

		public void Add(int? item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(int? item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(int?[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public bool Remove(int? item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<int?> Member

		public IEnumerator<int?> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Member

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
