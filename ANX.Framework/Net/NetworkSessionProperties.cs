using System;
using System.Collections.Generic;
using System.Collections;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
	public class NetworkSessionProperties : IList<int?>, ICollection<int?>, IEnumerable<int?>, IEnumerable
    {
        private const int DataCount = 8;

        private readonly int?[] data = new int?[DataCount];

        public int Count
        {
            get { return DataCount; }
        }

        bool ICollection<int?>.IsReadOnly
        {
            get { return false; }
        }

        public int? this[int index]
        {
            get
            {
                if (index < 0 || index >= DataCount)
                    throw new ArgumentOutOfRangeException("index");

                return data[index];
            }
            set
            {
                if (index < 0 || index >= DataCount)
                    throw new ArgumentOutOfRangeException("index");

                data[index] = value;
            }
        }

        public int IndexOf(int? item)
        {
            return ((IList<int?>)data).IndexOf(item);
		}

        bool ICollection<int?>.Contains(int? item)
        {
            return ((IList<int?>)data).Contains(item);
        }

        void ICollection<int?>.CopyTo(int?[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public IEnumerator<int?> GetEnumerator()
        {
            return ((IList<int?>)data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        #region Unsupported Methods
        void IList<int?>.Insert(int index, int? item)
        {
            throw new NotSupportedException();
        }

        void ICollection<int?>.Add(int? item)
        {
            throw new NotSupportedException();
        }

        void ICollection<int?>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<int?>.Remove(int? item)
        {
            throw new NotSupportedException();
        }

        void IList<int?>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
        #endregion
	}
}
