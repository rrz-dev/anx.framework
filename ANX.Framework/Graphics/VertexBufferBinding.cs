#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public struct VertexBufferBinding
    {
        #region Private Members
        private VertexBuffer vertexBuffer;
        private int instanceFrequency;
        private int vertexOffset;
        #endregion // Private Members

        public VertexBufferBinding(VertexBuffer vertexBuffer)
        {
            this.vertexBuffer = vertexBuffer;
            this.vertexOffset = 0;
            this.instanceFrequency = 0;
        }

        public VertexBufferBinding(VertexBuffer vertexBuffer, int vertexOffset)
        {
            this.vertexBuffer = vertexBuffer;
            this.vertexOffset = vertexOffset;
            this.instanceFrequency = 0;
        }

        public VertexBufferBinding(VertexBuffer vertexBuffer, int vertexOffset, int instanceFrequency)
        {
            this.vertexBuffer = vertexBuffer;
            this.vertexOffset = vertexOffset;
            this.instanceFrequency = instanceFrequency;
        }

        public static implicit operator VertexBufferBinding(VertexBuffer vertexBuffer)
        {
            return new VertexBufferBinding(vertexBuffer);
        }

        public VertexBuffer VertexBuffer
        {
            get
            {
                return this.vertexBuffer;
            }
        }

        public int InstanceFrequency
        {
            get
            {
                return this.instanceFrequency;
            }
        }

        public int VertexOffset
        {
            get
            {
                return this.vertexOffset;
            }
        }
    }
}
