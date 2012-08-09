#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    public sealed class GameComponentCollection : Collection<IGameComponent>
    {
        public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded;
        public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;

        public GameComponentCollection()
        {
            throw new NotImplementedException();
        }

        protected override void ClearItems()
        {
            throw new NotImplementedException();
        }

        protected override void InsertItem(int index, IGameComponent item)
        {
            throw new NotImplementedException();
        }

        private void OnComponentAdded(GameComponentCollectionEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        private void OnComponentRemoved(GameComponentCollectionEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        protected override void RemoveItem(int index)
        {
            throw new NotImplementedException();
        }

        protected override void SetItem(int index, IGameComponent item)
        {
            throw new NotImplementedException();
        }
    }

}
