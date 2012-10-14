using System;
using System.Collections;
using System.Collections.Generic;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [Developer("floAr, AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
	public class CurveKeyCollection : ICollection<CurveKey>, IEnumerable<CurveKey>, IEnumerable
	{
		#region Private
		private List<CurveKey> keys;
		#endregion

		#region Public
        public int Count
        {
            get { return keys.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public CurveKey this[int index]
		{
			get
			{
				return keys[index];
			}
			set
			{
                if (value == null)
                    throw new ArgumentNullException();

                if (index >= keys.Count)
                    throw new IndexOutOfRangeException();

                //if fitting add here
                if (keys[index].Position == value.Position)
                    keys[index] = value;
                else
                {
                    //if not let it be sorted
                    keys.RemoveAt(index);
                    Add(value);
                }

			}
		}
		#endregion

		#region Constructor
		public CurveKeyCollection()
		{
			keys = new List<CurveKey>();
		}
		#endregion

		#region Add
        public void Add(CurveKey item)
        {
            if (item == null)
                throw new ArgumentNullException();

            // If the list is empty or the item is bigger than the max, just add it
            if (keys.Count == 0 || item.CompareTo(keys[Count - 1]) > 0)
            {
                keys.Add(item);
                return;
            }

            int index = keys.BinarySearch(item);
            if(index < 0)
            {
                index = ~index;
                keys.Insert(index, item);
                return;
            }

            while (index < keys.Count)
            {
                if (item.Position != keys[index].Position)
                    break;

                index++;
            }

            keys.Insert(index, item);

            // Old broken code with own binary search implementation.
            // Just in case the binary search isn't available on any system, this can be fixed:
            //int min = 0;
            //int max = Count - 1;
            //while ((max - min) > 1)
            //{
            //    //Find half point
            //    int half = min + ((max - min) / 2);
            //    //Compare if it's bigger or smaller than the current item.
            //    int comp = item.CompareTo(keys[half]);
            //    if (comp == 0)
            //    {
            //        // Item is equal to half point
            //        keys.Insert(half + 1, item);
            //        return;
            //    }

            //    // If the item is smaller, move the max to half, otherwise move the min to half.
            //    if (comp == -1)
            //        max = half;
            //    else
            //        min = half;
            //}

            //if (item.CompareTo(keys[min]) <= 0)
            //    keys.Insert(min, item);
            //else
            //    keys.Insert(min + 1, item);
        }

        #endregion

        #region Clear
        public void Clear()
		{
			keys.Clear();
		}
		#endregion
		
		#region Contains
		public bool Contains(CurveKey item)
		{
			return keys.Contains(item);
		}
		#endregion

		#region CopyTo
		public void CopyTo(CurveKey[] array, int arrayIndex)
		{
			keys.CopyTo(array, arrayIndex);
		}
		#endregion

		#region Remove
		public bool Remove(CurveKey item)
		{
			return keys.Remove(item);
		}
		#endregion

		#region RemoveAt
		public void RemoveAt(int index)
		{
                keys.RemoveAt(index);
           
		}
		#endregion

		#region IndexOf
		public int IndexOf(CurveKey item)
		{
			return keys.IndexOf(item);
		}
		#endregion

		#region Clone
		public CurveKeyCollection Clone()
        {
            return new CurveKeyCollection
            {
                keys = new List<CurveKey>(keys),
            };
		}
		#endregion

		#region GetEnumerator
		public IEnumerator<CurveKey> GetEnumerator()
		{
			return keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)keys).GetEnumerator();
		}
		#endregion
	}
}
