using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class ProcessorParameterDictionary : IDictionary<string, object>
    {
        private Dictionary<string, object> items = new Dictionary<string, object>();

        public event EventHandler<EventArgs> Invalidate;

        protected virtual void OnInvalidate(EventArgs e)
        {
            if (Invalidate != null)
                Invalidate(this, e);
        }

        public void Add(string key, object value)
        {
            items.Add(key, value);
            OnInvalidate(EventArgs.Empty);
        }

        public bool ContainsKey(string key)
        {
            return items.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return items.Keys; }
        }

        public bool Remove(string key)
        {
            if (items.Remove(key))
            {
                OnInvalidate(EventArgs.Empty);
                return true;
            }
            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            return items.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return items.Values; }
        }

        public object this[string key]
        {
            get
            {
                return items[key];
            }
            set
            {
                if (items.ContainsKey(key))
                {
                    var item = items[key];
                    if (item == null && value == null)
                    {
                        return;
                    }
                    else if (item != null && item.Equals(value))
                    {
                        return;
                    }
                }

                items[key] = value;
                OnInvalidate(EventArgs.Empty);
            }
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.items.Clear();
            OnInvalidate(EventArgs.Empty);
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)items).Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ((IDictionary<string, object>)items).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return ((IDictionary<string, object>)items).Remove(item);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
