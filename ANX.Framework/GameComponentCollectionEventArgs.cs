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
    public class GameComponentCollectionEventArgs : EventArgs
    {
        public GameComponentCollectionEventArgs(IGameComponent gameComponent)
        {
            this.GameComponent = gameComponent;
        }

        public IGameComponent GameComponent
        {
            get;
            private set;
        }
    }

}
