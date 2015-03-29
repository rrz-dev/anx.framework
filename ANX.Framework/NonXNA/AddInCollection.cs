using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.NonXNA
{
    class AddInCollection : ICollection<AddIn>
    {
        private List<AddIn> items = new List<AddIn>();

        public void Add(AddIn item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (ContainsName(item.Name))
                throw new Exception("Duplicate creator found. A creator with the name '" + item.Name + "' was already registered.");

            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(AddIn item)
        {
            return items.Contains(item);
        }

        public bool ContainsName(string name)
        {
            return items.Any((x) => string.Compare(x.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public void CopyTo(AddIn[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AddIn item)
        {
            return items.Remove(item);
        }

        public IEnumerator<AddIn> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public AddIn this[string name]
        {
            get
            {
                try
                {
                    return items.First((x) => string.Compare(x.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
                }
                catch (InvalidOperationException exc)
                {
                    throw new KeyNotFoundException(string.Format("Can't find an AddIn with the name \"{0}\".", name), exc);
                }
            }
        }
    }
}
