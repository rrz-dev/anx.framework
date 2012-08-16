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

namespace ANX.Framework.Content.Pipeline
{
    public class NamedValueDictionary<T> : IDictionary<string, T>, ICollection<KeyValuePair<string, T>>, IEnumerable<KeyValuePair<string, T>>, IEnumerable
    {
        public NamedValueDictionary()
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get;
            private set;
        }

        public T this[string key]
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

        public ICollection<string> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<T> Values
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected internal virtual Type DefaultSerializerType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Add(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out T value)
        {
            throw new NotImplementedException();
        }

        protected virtual void AddItem(string key, T value)
        {
            throw new NotImplementedException();
        }

        protected virtual void ClearItems()
        {
            throw new NotImplementedException();
        }

        protected virtual bool RemoveItem(string key)
        {
            throw new NotImplementedException();
        }

        protected virtual void SetItem(string key, T value)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, T>>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<string, T>>.CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> item)
        {
            throw new NotImplementedException();
        }
    }
}
