#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class IndirectPositionCollection : IList<Vector3>, ICollection<Vector3>, IEnumerable<Vector3>, IEnumerable
    {

        int IList<Vector3>.IndexOf(Vector3 item)
        {
            throw new NotImplementedException();
        }

        void IList<Vector3>.Insert(int index, Vector3 item)
        {
            throw new NotImplementedException();
        }

        void IList<Vector3>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        Vector3 IList<Vector3>.this[int index]
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

        void ICollection<Vector3>.Add(Vector3 item)
        {
            throw new NotImplementedException();
        }

        void ICollection<Vector3>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<Vector3>.Contains(Vector3 item)
        {
            throw new NotImplementedException();
        }

        void ICollection<Vector3>.CopyTo(Vector3[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<Vector3>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<Vector3>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<Vector3>.Remove(Vector3 item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<Vector3> IEnumerable<Vector3>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
