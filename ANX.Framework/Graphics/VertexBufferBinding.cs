#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct VertexBufferBinding
    {
        #region Private
        private readonly VertexBuffer vertexBuffer;
        private readonly int instanceFrequency;
        private readonly int vertexOffset;
		#endregion

		#region Public
	    public VertexBuffer VertexBuffer
	    {
	        get { return this.vertexBuffer; }
	    }

	    public int InstanceFrequency
	    {
	        get { return this.instanceFrequency; }
	    }

	    public int VertexOffset
	    {
	        get { return this.vertexOffset; }
	    }
	    #endregion

        public VertexBufferBinding(VertexBuffer vertexBuffer)
        {
            if (vertexBuffer == null)
                throw new ArgumentNullException("vertexBuffer");

            this.vertexBuffer = vertexBuffer;
            this.vertexOffset = 0;
            this.instanceFrequency = 0;
        }

        public VertexBufferBinding(VertexBuffer vertexBuffer, int vertexOffset)
        {
            if (vertexBuffer == null)
                throw new ArgumentNullException("vertexBuffer");

            if (vertexOffset < 0 || vertexOffset >= vertexBuffer.VertexCount)
                throw new ArgumentOutOfRangeException("vertexOffset");

            this.vertexBuffer = vertexBuffer;
            this.vertexOffset = vertexOffset;
            this.instanceFrequency = 0;
        }

        public VertexBufferBinding(VertexBuffer vertexBuffer, int vertexOffset, int instanceFrequency)
        {
            if (vertexBuffer == null)
                throw new ArgumentNullException("vertexBuffer");
            
            if (vertexOffset < 0 || vertexOffset >= vertexBuffer.VertexCount)
                throw new ArgumentOutOfRangeException("vertexOffset");
            
            if (instanceFrequency < 0)
                throw new ArgumentOutOfRangeException("instanceFrequency");

            this.vertexBuffer = vertexBuffer;
            this.vertexOffset = vertexOffset;
            this.instanceFrequency = instanceFrequency;
        }

        public static implicit operator VertexBufferBinding(VertexBuffer vertexBuffer)
        {
            return new VertexBufferBinding(vertexBuffer);
        }
    }
}
