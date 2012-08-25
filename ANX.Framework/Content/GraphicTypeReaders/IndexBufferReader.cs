#region Using Statements
using ANX.Framework.Graphics;


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    public class IndexBufferReader : ContentTypeReader<IndexBuffer>
    {
        protected internal override IndexBuffer Read(ContentReader input, IndexBuffer existingInstance)
        {
            GraphicsDevice graphics = input.ResolveGraphicsDevice();
            bool is16Bit = input.ReadBoolean();
            int dataSize = input.ReadInt32();
            byte[] data = new byte[dataSize];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = input.ReadByte();
            }

            IndexElementSize indexElementSize = IndexElementSize.ThirtyTwoBits;
            int indexCount = dataSize / 4;
            if (is16Bit)
            {
                indexElementSize = IndexElementSize.SixteenBits;
                indexCount = dataSize / 2;
            }

            IndexBuffer indexBuffer = new IndexBuffer(graphics, indexElementSize, indexCount, BufferUsage.None);
            indexBuffer.SetData<byte>(data, 0, dataSize);
            return indexBuffer;
        }
    }
}
