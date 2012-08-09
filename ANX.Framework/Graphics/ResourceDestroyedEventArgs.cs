#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ResourceDestroyedEventArgs : EventArgs
    {
        public ResourceDestroyedEventArgs(string name, object tag)
        {
            this.Tag = tag;
            this.Name = name;
        }

        public string Name
        {
            get;
            set;
        }

        public object Tag
        {
            get;
            set;
        }
    }
}
