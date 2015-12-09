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
    [Serializable]
    public class NamedValueDictionary<T> : IDictionary<string, T>, ICollection<KeyValuePair<string, T>>, IEnumerable<KeyValuePair<string, T>>, IEnumerable
    {
        private Dictionary<string, T> keyValues;

        public NamedValueDictionary()
        {
            keyValues = new Dictionary<string, T>();
        }

        public NamedValueDictionary(IDictionary<string, T> dictionary)
        {
            keyValues = new Dictionary<string, T>(dictionary);
        }

        public int Count
        {
            get
            {
                return this.keyValues.Count;
            }
        }

        public T this[string key]
        {
            get
            {
                return this.keyValues[key];
            }
            set
            {
                this.keyValues[key] = value;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return this.keyValues.Keys;
            }
        }

        public ICollection<T> Values
        {
            get
            {
                return this.keyValues.Values;
            }
        }

        protected internal virtual Type DefaultSerializerType
        {
            get
            {
                return typeof(T);
            }
        }

        public void Add(string key, T value)
        {
            this.keyValues.Add(key, value);
        }

        public void AddRange(IEnumerable<KeyValuePair<string, T>> enumerable, bool overwrite = false)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            if (overwrite == false)
            {
                foreach (var item in enumerable)
                {
                    this.Add(item.Key, item.Value);
                }
            }
            else
            {
                foreach (var item in enumerable)
                {
                    this[item.Key] = item.Value;
                }
            }
        }

        public void Clear()
        {
            this.keyValues.Clear();
        }

        public bool ContainsKey(string key)
        {
            return this.keyValues.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return this.keyValues.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return this.keyValues.Remove(key);
        }

        public bool TryGetValue(string key, out T value)
        {
            return this.keyValues.TryGetValue(key, out value);
        }

        protected virtual void AddItem(string key, T value)
        {
            this.keyValues.Add(key, value);
        }

        protected virtual void ClearItems()
        {
            this.keyValues.Clear();
        }

        protected virtual bool RemoveItem(string key)
        {
            return this.keyValues.Remove(key);
        }

        protected virtual void SetItem(string key, T value)
        {
            this.keyValues[key] = value;
        }

        bool ICollection<KeyValuePair<string, T>>.IsReadOnly
        {
            get 
            { 
                return false; 
            }
        }

        void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> item)
        {
            this.keyValues.Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> item)
        {
            return this.keyValues.ContainsKey(item.Key);
        }

        void ICollection<KeyValuePair<string, T>>.CopyTo(KeyValuePair<string, T>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, T>>)this.keyValues).CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.keyValues.GetEnumerator();
        }

        bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> item)
        {
            return this.keyValues.Remove(item.Key);
        }
    }
}
