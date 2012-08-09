#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.ContentPipeline;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class VertexBufferReader : ContentTypeReader<VertexBuffer>
    {
        protected internal override VertexBuffer Read(ContentReader input, VertexBuffer existingInstance)
        {
            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            VertexDeclaration declaration = input.ReadRawObject<VertexDeclaration>();
            int vertexCount = input.ReadInt32();
            byte[] data = new byte[vertexCount * declaration.VertexStride];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = input.ReadByte();
            }

            VertexBuffer vertexBuffer = new VertexBuffer(graphics, declaration, vertexCount, BufferUsage.None);
            vertexBuffer.SetData<byte>(data, 0, data.Length);
            return vertexBuffer;
        }
    }
}
