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
    public sealed class GeometryContentCollection : ChildCollection<MeshContent, GeometryContent>
    {
        protected GeometryContentCollection(MeshContent parent)
            : base(parent)
        {
            throw new NotImplementedException();
        }

        protected override MeshContent GetParent(GeometryContent child)
        {
            throw new NotImplementedException();
        }

        protected override void SetParent(GeometryContent child, MeshContent parent)
        {
            throw new NotImplementedException();
        }
    }
}
