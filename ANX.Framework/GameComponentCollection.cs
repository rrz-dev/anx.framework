#region Using Statements
using System;
using System.Collections.ObjectModel;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
    public sealed class GameComponentCollection : Collection<IGameComponent>
    {
        #region Events
        public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded;
        public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;

        #endregion

        public GameComponentCollection()
        {
            // nothing to do here
        }

        protected override void ClearItems()
        {
            for (int i = 0; i < base.Count; i++)
            {
                OnComponentRemoved(base[i]);
            }

            base.Clear();
        }

        protected override void InsertItem(int index, IGameComponent item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            base.Insert(index, item);
            OnComponentAdded(item);
        }

        protected override void RemoveItem(int index)
        {
            IGameComponent component = base[index];
            base.Remove(component);
            OnComponentRemoved(component);
        }

        protected override void SetItem(int index, IGameComponent item)
        {
            base[index] = item;
            OnComponentAdded(item);
        }

        private void OnComponentAdded(IGameComponent component)
        {
            if (ComponentAdded != null)
            {
                ComponentAdded(this, new GameComponentCollectionEventArgs(component));
            }
        }

        private void OnComponentRemoved(IGameComponent component)
        {
            if (ComponentRemoved != null)
            {
                ComponentRemoved(this, new GameComponentCollectionEventArgs(component));
            }
        }
    }
}
