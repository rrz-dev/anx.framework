using System;
using System.Collections;
using System.Collections.Generic;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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
