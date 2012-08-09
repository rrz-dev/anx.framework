#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class ModelMeshPart
    {
        internal ModelMesh parentMesh;

        private Effect effect;

        public Effect Effect
        {
            get { return effect; }
            set 
            {
                if (this.effect != value)
                {
                    var old = this.effect;
                    this.effect = value;
                    this.parentMesh.EffectChangedOnMeshPart(this, old, value);
                }
            }
        }

        private IndexBuffer indexBuffer;

        public IndexBuffer IndexBuffer
        {
            get { return indexBuffer; }
            internal set { this.indexBuffer = value; }
        }

        private int numVertices;

        public int NumVertices
        {
            get { return numVertices; }
        }

        private int primitiveCount;

        public int PrimitiveCount
        {
            get { return primitiveCount; }
        }

        private int startIndex;

        public int StartIndex
        {
            get { return startIndex; }
        }

        private Object tag;

        public Object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private VertexBuffer vertexBuffer;

        public VertexBuffer VertexBuffer
        {
            get { return vertexBuffer; }
            internal set { this.vertexBuffer = value; }
        }

        private int vertexOffset;

        public int VertexOffset
        {
            get { return vertexOffset; }
        }

        internal ModelMeshPart(int vertexOffset, int numVertices, int startIndex, int primitiveCount, object tag)
        {
            this.vertexOffset = vertexOffset;
            this.numVertices = numVertices;
            this.startIndex = startIndex;
            this.primitiveCount = primitiveCount;
            this.tag = tag;
        }
    }
}
