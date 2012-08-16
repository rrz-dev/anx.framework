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
        protected ChildCollection(TParent parent)
        {
            throw new NotImplementedException();
        }

        protected override void ClearItems()
        {
            base.ClearItems();

            throw new NotImplementedException();
        }

        protected abstract TParent GetParent(TChild child);

        protected override void InsertItem(int index, TChild item)
        {
            base.InsertItem(index, item);

            throw new NotImplementedException();
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);

            throw new NotImplementedException();
        }

        protected override void SetItem(int index, TChild item)
        {
            base.SetItem(index, item);

            throw new NotImplementedException();
        }

        protected abstract void SetParent(TChild child, TParent parent);

    }
}
