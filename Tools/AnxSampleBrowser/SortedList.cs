using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AnxSampleBrowser
{
    class SortedList<T> : Collection<T>
    {
        IComparer<T> _comparer;

        public SortedList(IComparer<T> comparer)
            : base()
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            this.Comparer = comparer;
        }

        public void AddRange(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            foreach (var item in enumerable)
                this.Add(item);
        }

        protected override void InsertItem(int index, T item)
        {
            index = this.Items.BinarySearch(item, Comparer);
            if (index < 0)
                index = ~index;

            base.Items.Insert(index, item);
        }

        protected override void SetItem(int index, T item)
        {
            throw new NotSupportedException("Setting an element is not allowed, use Add/Remove instead.");
        }

        public IList<T> GetRange(int index, int count)
        {
            return new List<T>(this.Skip(index).Take(count));
        }

        public IComparer<T> Comparer
        {
            get { return _comparer; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _comparer = value;

                //Resort
                var oldValues = this.ToArray();
                this.Clear();
                this.AddRange(oldValues);
            }
        }

        protected new List<T> Items
        {
            //Dirty little dependency on an implementation detail.
            //Used to have access to BinarySearch.
            get { return (List<T>)base.Items; }
        }
    }
}
