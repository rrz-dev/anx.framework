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
    public sealed class VertexChannelCollection : IList<VertexChannel>, ICollection<VertexChannel>, IEnumerable<VertexChannel>, IEnumerable
    {
        int IList<VertexChannel>.IndexOf(VertexChannel item)
        {
            throw new NotImplementedException();
        }

        void IList<VertexChannel>.Insert(int index, VertexChannel item)
        {
            throw new NotImplementedException();
        }

        void IList<VertexChannel>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        VertexChannel IList<VertexChannel>.this[int index]
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

        void ICollection<VertexChannel>.Add(VertexChannel item)
        {
            throw new NotImplementedException();
        }

        void ICollection<VertexChannel>.Clear()
        {
            throw new NotImplementedException();
        }

        bool ICollection<VertexChannel>.Contains(VertexChannel item)
        {
            throw new NotImplementedException();
        }

        void ICollection<VertexChannel>.CopyTo(VertexChannel[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int ICollection<VertexChannel>.Count
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<VertexChannel>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool ICollection<VertexChannel>.Remove(VertexChannel item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<VertexChannel> IEnumerable<VertexChannel>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
