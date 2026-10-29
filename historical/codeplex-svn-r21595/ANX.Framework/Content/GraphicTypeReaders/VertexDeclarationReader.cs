#region Using Statements
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    internal class VertexDeclarationReader : ContentTypeReader<VertexDeclaration>
    {
        protected internal override VertexDeclaration Read(ContentReader input, VertexDeclaration existingInstance)
        {
            int vertexStride = input.ReadInt32();
            int elementCount = input.ReadInt32();
            VertexElement[] elements = new VertexElement[elementCount];
            for (int i = 0; i < elementCount; i++)
            {
                VertexElement element = new VertexElement();
                element.Offset = input.ReadInt32();
                element.VertexElementFormat = (VertexElementFormat)input.ReadInt32();
                element.VertexElementUsage = (VertexElementUsage)input.ReadInt32();
                element.UsageIndex = input.ReadInt32();
                elements[i] = element;
            }

            return new VertexDeclaration(vertexStride, elements);
        }
    }
}
