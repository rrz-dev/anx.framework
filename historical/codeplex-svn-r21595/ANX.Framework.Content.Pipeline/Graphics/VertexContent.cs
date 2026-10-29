#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Processors;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public sealed class VertexContent
    {
        public VertexChannelCollection Channels
        {
            get;
            private set;
        }

        public VertexChannel<int> PositionIndices
        {
            get;
            private set;
        }

        public IndirectPositionCollection Positions
        {
            get;
            private set;
        }

        public int VertexCount
        {
            get { return PositionIndices.Count; }
        }

        public VertexContent(GeometryContent content)
        {
            Channels = new VertexChannelCollection();
            PositionIndices = new VertexChannel<int>("PositionIndices");
            Positions = new IndirectPositionCollection(content, PositionIndices);
        }

        public int Add(int positionIndex)
        {
            
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<int> positionIndexCollection)
        {
            throw new NotImplementedException();
        }

        public VertexBufferContent CreateVertexBuffer()
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, int positionIndex)
        {
            throw new NotImplementedException();
        }

        public void InsertRange(int index, IEnumerable<int> positionIndexCollection)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(int index, int count)
        {
            throw new NotImplementedException();
        }
    }
}
