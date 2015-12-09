using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
    [Developer("Konstantin Koch")]
    [ContentTypeWriter]
    internal class VertexBufferWriter : BuiltinTypeWriter<VertexBufferContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(VertexBuffer).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, VertexBufferContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            if (value.VertexData.Length == 0)
                throw new InvalidContentException("The vertex buffer is empty.", value.Identity);

            if (value.VertexData.Length % value.VertexDeclaration.VertexStride.Value != 0)
                throw new InvalidContentException("The size of the vertex buffer is not a multiple of the vertex stride.", value.Identity);

            output.WriteRawObject(value.VertexDeclaration);
            output.Write(value.VertexData.Length / value.VertexDeclaration.VertexStride.Value);
            output.Write(value.VertexData);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(VertexBuffer), targetPlatform);
        }
    }
}
