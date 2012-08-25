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
    public class MeshContent : NodeContent
    {
        public GeometryContentCollection Geometry
        {
            get;
            private set;
        }

        public PositionCollection Positions
        {
            get;
            private set;
        }

        public MeshContent()
        {
            Geometry = new GeometryContentCollection(this);
            Positions = new PositionCollection();
        }

        
    }
}
