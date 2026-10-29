#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public abstract class ChildCollection<TParent, TChild> : Collection<TChild> where TParent : class where TChild : class
    {
        TParent parent;

        protected ChildCollection(TParent parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            this.parent = parent;
        }

        protected override void ClearItems()
        {
            foreach (var child in this)
            {
                this.SetParent(child, default(TParent));
            }
            base.ClearItems();
        }

        protected abstract TParent GetParent(TChild child);

        protected override void InsertItem(int index, TChild item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (this.GetParent(item) != null)
            {
                throw new ArgumentException("item already has a parent item");
            }
            base.InsertItem(index, item);
            this.SetParent(item, this.parent);
        }

        protected override void RemoveItem(int index)
        {
            var child = base[index];
            this.SetParent(child, default(TParent));
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, TChild item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            var child = base[index];
            if (child == item)
            {
                return;
            }

            if (this.GetParent(item) != null)
            {
                throw new ArgumentException("item already has a parent item");
            }

            base.SetItem(index, item);
            this.SetParent(item, parent);
            this.SetParent(child, default(TParent));
        }

        protected abstract void SetParent(TChild child, TParent parent);

    }
}
