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
    public abstract class VertexChannel : IList, ICollection, IEnumerable
    {
        #region Properties
        public string Name
        {
            get;
            private set;
        }

        protected IList Source 
        { 
            get; set; 
        }

        public int Count
        {
            get { return Source.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return Source.SyncRoot; }
        }

        bool IList.IsFixedSize
        {
            get { return true; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        public object this[int index]
        {
            get { return Source[index]; }
            set { Source[index] = value; }
        }
        #endregion

        #region Constructor
        public VertexChannel(string name)
        {
            this.Name = name;
        }
        #endregion

        #region Methods
        int IList.Add(object value)
        {
            throw new NotSupportedException("Size is fixed");
        }

        void IList.Insert(int index, object value)
        {
            throw new NotSupportedException("Size is fixed");
        }

        void IList.Clear()
        {
            throw new NotSupportedException("Size is fixed");
        }

        void IList.Remove(object value)
        {
            throw new NotSupportedException("Size is fixed");
        }

        void IList.RemoveAt(int index)
        {
            throw new NotSupportedException("Size is fixed");
        }

        public bool Contains(object value)
        {
            return Source.Contains(value);
        }

        public int IndexOf(object value)
        {
            return Source.IndexOf(value);
        }

        public void CopyTo(Array array, int index)
        {
            Source.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return Source.GetEnumerator();
        }
        #endregion
    }

    public sealed class VertexChannel<T> : VertexChannel, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private List<T> source;

        #region Properties
        public new T this[int index]
        {
            get { return source[index]; }
            set { source[index] = value; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }
        #endregion

        #region Constructor
        public VertexChannel(string name)
            : base(name)
        {
            source = new List<T>();
            Source = source;
        }
        #endregion

        #region Methods
        void IList<T>.Insert(int index, T item)
        {
            throw new NotSupportedException("Size is fixed");
        }

        void IList<T>.RemoveAt(int index)
        {
            throw new NotSupportedException("Size is fixed");
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException("Size is fixed");
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException("Size is fixed");
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException("Size is fixed");
        }

        public int IndexOf(T item)
        {
            return source.IndexOf(item);
        }

        public bool Contains(T item)
        {
            return source.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            source.CopyTo(array, arrayIndex);
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return source.GetEnumerator();
        }
        #endregion
    }
}
