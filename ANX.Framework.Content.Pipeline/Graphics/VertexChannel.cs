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

        [ContentSerializerIgnore]
        public abstract Type ElementType
        {
            get;
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

        internal void AddRange(IEnumerable vertices)
        {
            InsertRange(0, vertices);
        }

        internal void AddRange(int vertexCount)
        {
            for (int i = 0; i < vertexCount; i++)
                Source.Add(Activator.CreateInstance(ElementType));
        }

        internal void InsertRange(int index, IEnumerable vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException("vertices");

            foreach (object value in vertices)
            {
                if (value == null || value.GetType() != this.ElementType)
                    throw new ArgumentException("The added vertices are not of the same type as the vertex channel \"{0}\"", this.ElementType.Name);

                Source.Insert(index, value);
                index++;
            }
        }

        internal void Insert(int index, object vertex)
        {
            if (vertex == null || vertex.GetType() != this.ElementType)
                throw new ArgumentException("The added vertices are not of the same type as the vertex channel \"{0}\"", this.ElementType.Name);

            Source.Insert(index, vertex);
        }

        internal void RemoveRange(int startIndex, int count)
        {
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex");

            for (int i = startIndex + count - 1; i >= startIndex; i--)
                Source.RemoveAt(i);
        }

        internal void Clear()
        {
            Source.Clear();
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

        public abstract IEnumerable<TargetType> ReadConvertedContent<TargetType>();
        #endregion
    }

    public class VertexChannel<T> : VertexChannel, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
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

        public override Type ElementType
        {
            get
            {
                return typeof(T);
            }
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

        public int IndexOf(T item, int startIndex)
        {
            return source.IndexOf(item, startIndex);
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

        public override IEnumerable<TargetType> ReadConvertedContent<TargetType>()
        {
            Converter<T, TargetType> converter = VectorConverter.GetConverter<T, TargetType>();
            foreach (T current in this.source)
            {
                yield return converter(current);
            }
            yield break;
        }
        #endregion
    }
}
