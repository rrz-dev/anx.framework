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

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class MipmapChainCollection : Collection<MipmapChain>
    {
        private bool _fixedSize = false;

        public MipmapChainCollection(int levels, bool fixedSize)
        {
            for (int i = 0; i < levels; i++)
            {
                base.Add(new MipmapChain());
            }

            //fixedSize is interesting for cube and 2D textures.
            //Mipmaps are still created by adding additional bitmap to the faces of the MipmapChain, but the number of faces is fixed.
            //For volumetric textures, the MipmapChain represents the depth of the texture.
            this._fixedSize = fixedSize;
        }

        protected override void ClearItems()
        {
            if (this._fixedSize)
                throw new NotSupportedException(string.Format("The {0} has a fixed size.", this.GetType()));

            base.ClearItems();
        }

        protected override void InsertItem(int index, MipmapChain item)
        {
            if (this._fixedSize)
                throw new NotSupportedException(string.Format("The {0} has a fixed size.", this.GetType()));

            if (item == null)
                throw new ArgumentNullException("item");

            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            if (this._fixedSize)
                throw new NotSupportedException(string.Format("The {0} has a fixed size.", this.GetType()));

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, MipmapChain item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            base.SetItem(index, item);
        }
    }
}
