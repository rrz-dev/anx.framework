using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
    [ContentTypeWriter]
    internal class VertexDeclarationWriter : BuiltinTypeWriter<VertexDeclarationContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(VertexDeclaration).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, VertexDeclarationContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            if (value.VertexElements.Count == 0)
                throw new InvalidContentException("The vertex declaration has no vertex elements.", value.Identity);

            output.Write(value.VertexStride.Value);
            output.Write(value.VertexElements.Count);
            foreach (var element in value.VertexElements)
            {
                output.Write(element.Offset);
                output.Write((int)element.VertexElementFormat);
                output.Write((int)element.VertexElementUsage);
                output.Write(element.UsageIndex);
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(VertexDeclaration), targetPlatform);
        }
    }
}
