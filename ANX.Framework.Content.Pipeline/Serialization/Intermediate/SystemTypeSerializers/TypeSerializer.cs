using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class TypeSerializer : ContentTypeSerializer<Type>
    {
        protected override void Serialize(IntermediateWriter output, Type value, ContentSerializerAttribute format)
        {
            output.WriteTypeName(value);
        }

        protected override Type Deserialize(IntermediateReader input, ContentSerializerAttribute format, Type existingInstance)
        {
            return input.ReadTypeName();
        }
    }
}
