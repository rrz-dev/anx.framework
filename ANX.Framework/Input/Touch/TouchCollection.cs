#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Input.Touch
{
    public struct TouchCollection : IList<TouchLocation>, ICollection<TouchLocation>, IEnumerable<TouchLocation>, IEnumerable
    {
        public TouchCollection(TouchLocation[] touches)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(TouchLocation item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TouchLocation item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public TouchLocation this[int index]
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

        public void Add(TouchLocation item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TouchLocation item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TouchLocation[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool FindById(int id, out TouchLocation touchLocation)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsConnected 
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TouchLocation item)
        {
            throw new NotImplementedException();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<TouchLocation> IEnumerable<TouchLocation>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public struct Enumerator : IEnumerator<TouchLocation>, IDisposable, IEnumerator
        {
            private TouchCollection collection;
            private int position;
            internal Enumerator(TouchCollection collection)
            {
                this.collection = collection;
                this.position = -1;
            }

            public TouchLocation Current
            {
                get
                {
                    return this.collection[this.position];
                }
            }
            public bool MoveNext()
            {
                this.position++;
                if (this.position >= this.collection.Count)
                {
                    this.position = this.collection.Count;
                    return false;
                }
                return true;
            }

            void IEnumerator.Reset()
            {
                this.position = -1;
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }

    }
}
