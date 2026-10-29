#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public class VertexDeclarationContent : ContentItem
    {
        public VertexDeclarationContent()
        {

        }

        public Collection<VertexElement> VertexElements
        {
            get;
            private set;
        }

        public Nullable<int> VertexStride
        {
            get;
            set;
        }
    }
}
