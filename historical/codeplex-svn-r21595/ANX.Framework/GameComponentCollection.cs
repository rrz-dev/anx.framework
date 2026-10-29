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
    [TestState(TestStateAttribute.TestState.Tested)]
    [Developer("Glatzemann, AstrorEnales")]
    public sealed class GameComponentCollection : Collection<IGameComponent>
    {
        #region Events
        public event EventHandler<GameComponentCollectionEventArgs> ComponentAdded;
        public event EventHandler<GameComponentCollectionEventArgs> ComponentRemoved;
        #endregion
        
        protected override void ClearItems()
        {
            for (int i = 0; i < Count; i++)
                OnComponentRemoved(base[i]);

            base.ClearItems();
        }

        protected override void InsertItem(int index, IGameComponent item)
        {
            if (IndexOf(item) != -1)
                throw new ArgumentException(
                    "Cannot add the same game component to a game component collection multiple times.");

            base.InsertItem(index, item);
            if(item != null)
                OnComponentAdded(item);
        }

        protected override void RemoveItem(int index)
        {
            IGameComponent component = base[index];
            base.RemoveItem(index);
            if (component != null)
                OnComponentRemoved(component);
        }

        protected override void SetItem(int index, IGameComponent item)
        {
            throw new NotSupportedException(
                "Cannot set a value using operator[] on GameComponentCollection. Use Add/Remove instead.");
        }

        private void OnComponentAdded(IGameComponent component)
        {
            if (ComponentAdded != null)
                ComponentAdded(this, new GameComponentCollectionEventArgs(component));
        }

        private void OnComponentRemoved(IGameComponent component)
        {
            if (ComponentRemoved != null)
                ComponentRemoved(this, new GameComponentCollectionEventArgs(component));
        }
    }
}
