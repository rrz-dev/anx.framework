#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public sealed class ModelMeshPartContent
    {
        public IndexCollection IndexBuffer
        {
            get;
            internal set;
        }

        public MaterialContent Material
        {
            get;
            set;
        }

        public int NumVertices
        {
            get;
            internal set;
        }

        public int PrimitiveCount
        {
            get;
            internal set;
        }

        public int StartIndex
        {
            get;
            internal set;
        }

        public Object Tag
        {
            get;
            set;
        }

        public VertexBufferContent VertexBuffer
        {
            get;
            internal set;
        }

        public int VertexOffset
        {
            get;
            internal set;
        }
    }
}
