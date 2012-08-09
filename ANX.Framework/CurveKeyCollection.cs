using System;
using System.Collections;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
	public class CurveKeyCollection : ICollection<CurveKey>,
		IEnumerable<CurveKey>, IEnumerable
	{
		#region Private
		private List<CurveKey> keys;
		#endregion

		#region Public
		public int Count
		{
			get
			{
				return keys.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
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
                    keys.Add(value);
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
            {
                throw new ArgumentNullException();
            }
            // keys.Add(item);

            if (this.keys.Count == 0)
            {
                //No list items
                this.keys.Add(item);
                return;
            }
            if (item.CompareTo(this[Count - 1]) > 0)
            {
                //Bigger than Max
                this.keys.Add(item);
                return;
            }
            int min = 0;
            int max = Count - 1;
            while ((max - min) > 1)
            {
                //Find half point
                int half = min + ((max - min) / 2);
                //Compare if it's bigger or smaller than the current item.
                int comp = item.CompareTo(this[half]);
                if (comp == 0)
                {
                    //Item is equal to half point
                    this.keys.Insert(half+1, item);
                    return;
                }
                else if (comp < 0) max = half;   //Item is smaller
                else min = half;   //Item is bigger
            }
            if (item.CompareTo(this[min]) <= 0) this.keys.Insert(min, item);
            else this.keys.Insert(min + 1, item);
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
            CurveKeyCollection result = new CurveKeyCollection();
            foreach (CurveKey key in this.keys)
                result.Add(key);
            return result;

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
