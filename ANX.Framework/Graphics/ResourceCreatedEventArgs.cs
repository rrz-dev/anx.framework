#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ResourceCreatedEventArgs : EventArgs
    {
        public ResourceCreatedEventArgs(object resource)
        {
            this.Resource = resource;
        }

        public object Resource
        {
            get;
            set;
        }
    }
}
