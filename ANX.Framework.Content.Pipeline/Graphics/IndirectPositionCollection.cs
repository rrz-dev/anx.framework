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
        private GeometryContent geometry;
        private VertexChannel<int> indices;

        #region Properties
        public int Count
        {
            get { return indices.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public Vector3 this[int index]
        {
            get
            {
                if (geometry.Parent == null)
                {
                    throw new InvalidOperationException("Geometry must have a mesh parent.");
                }
                int vIndex = this.indices[index];
                return geometry.Parent.Positions[vIndex];
            }
            set
            {
                throw new NotSupportedException();
            }
        }
        #endregion

        #region Constructor
        public IndirectPositionCollection(GeometryContent geometry, VertexChannel<int> positionIndices)
        {
            this.geometry = geometry;
            this.indices = positionIndices;
        }
        #endregion

        #region Methods
        public int IndexOf(Vector3 item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Contains(Vector3 item)
        {
            return this.IndexOf(item) >= 0;
        }

        public void CopyTo(Vector3[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array[arrayIndex++] = this[i];
            }
        }

        void IList<Vector3>.Insert(int index, Vector3 item)
        {
            throw new NotSupportedException("Collection is fixed size");
        }

        void IList<Vector3>.RemoveAt(int index)
        {
            throw new NotSupportedException("Collection is fixed size");
        }

        void ICollection<Vector3>.Add(Vector3 item)
        {
            throw new NotSupportedException("Collection is fixed size");
        }

        void ICollection<Vector3>.Clear()
        {
            throw new NotSupportedException("Collection is fixed size");
        }

        bool ICollection<Vector3>.Remove(Vector3 item)
        {
            throw new NotSupportedException("Collection is fixed size");
        }

        IEnumerator<Vector3> IEnumerable<Vector3>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return this[i];
            }
            yield break;
        }
        #endregion
    }
}
