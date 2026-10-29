#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class NodeContentCollection : ChildCollection<NodeContent, NodeContent>
    {
        public NodeContentCollection(NodeContent parent)
            : base(parent)
        {
        }

        protected override NodeContent GetParent(NodeContent child)
        {
            return child.Parent;
        }

        protected override void SetParent(NodeContent child, NodeContent parent)
        {
            child.Parent = parent;
        }
    }
}
