#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;
using System.Runtime.InteropServices;

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
            Channels = new VertexChannelCollection(this);
            PositionIndices = new VertexChannel<int>("PositionIndices");
            Positions = new IndirectPositionCollection(content, PositionIndices);
        }

        public int Add(int positionIndex)
        {
            int vertexCount = this.VertexCount;
            this.Insert(vertexCount, positionIndex);
            return vertexCount;
        }

        public void AddRange(IEnumerable<int> positionIndexCollection)
        {
            this.InsertRange(this.VertexCount, positionIndexCollection);
        }

        public VertexBufferContent CreateVertexBuffer()
        {
            int vector3Size = Marshal.SizeOf(typeof(Vector3));

            VertexDeclarationContent vertexDeclaration = new VertexDeclarationContent();
            //first add a declaration for the usual positions.
            vertexDeclaration.VertexElements.Add(new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0));
            int vertexStride = vector3Size;

            //Now add all custom channels.
            foreach (var channel in Channels)
            {
                VertexElementFormat format;
                if (!VectorConverter.TryGetVertexElementFormat(channel.ElementType, out format))
                {
                    throw new InvalidContentException(string.Format("The element type of the channel \"{0}\" does not correspond to a {1}.", channel.Name, typeof(VertexElementFormat).Name));
                }

                VertexElementUsage usage;
                if (!VertexChannelNames.TryDecodeUsage(channel.Name, out usage))
                {
                    throw new InvalidContentException(string.Format("Can't decode the {0} of the channel \"{1}\".", typeof(VertexElementUsage), channel.Name));
                }


                vertexDeclaration.VertexElements.Add(new VertexElement(vertexStride, format, usage, VertexChannelNames.DecodeUsageIndex(channel.Name)));
                vertexStride += Marshal.SizeOf(channel.ElementType);
            }

            vertexDeclaration.VertexStride = vertexStride;

            VertexBufferContent vertexBuffer = new VertexBufferContent(this.VertexCount * vertexStride);
            vertexBuffer.VertexDeclaration = vertexDeclaration;

            vertexBuffer.Write<Vector3>(0, vertexStride, this.Positions);
            for (int i = 0; i < Channels.Count; i++)
            {
                var channel = Channels[i];
                //First VertexElements are the vertex positions.
                vertexBuffer.Write(vertexDeclaration.VertexElements[i + 1].Offset, vertexStride, channel.ElementType, channel);
            }

            return vertexBuffer;
        }

        public void Insert(int index, int positionIndex)
        {
            this.PositionIndices.Insert(index, positionIndex);
        }

        public void InsertRange(int index, IEnumerable<int> positionIndexCollection)
        {
            this.PositionIndices.InsertRange(index, positionIndexCollection);
        }

        public void RemoveAt(int index)
        {
            this.RemoveRange(index, 1);
        }

        public void RemoveRange(int index, int count)
        {
            throw new NotImplementedException();
        }
    }
}
