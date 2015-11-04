using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
    [ContentTypeWriter]
    internal class IndexBufferWriter : BuiltinTypeWriter<IndexCollection>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(IndexBuffer).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, IndexCollection value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            bool are16BitIndices = true;
            for (int i = value.Count - 1; i >= 0; i--)
            {
                if (value[i] > ushort.MaxValue)
                {
                    are16BitIndices = false;
                    break;
                }
            }

            output.Write(are16BitIndices);
            output.Write(value.Count * (are16BitIndices ? 2 : 4));
            foreach (int index in value)
            {
                if (are16BitIndices)
                    output.Write((ushort)index);
                else
                    output.Write(index);
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(IndexBuffer), targetPlatform);
        }
    }
}
