using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class NullableSerializer<T> : ContentTypeSerializer<T?> where T : struct
    {
        private ContentTypeSerializer underlyingTypeSerializer;
        private readonly ContentSerializerAttribute underlyingFormat = new ContentSerializerAttribute() { FlattenContent = true };

        protected internal override void Initialize(IntermediateSerializer serializer)
        {
            this.underlyingTypeSerializer = serializer.GetTypeSerializer(typeof(T));
        }

        protected override void Serialize(IntermediateWriter output, T? value, ContentSerializerAttribute format)
        {
            output.WriteRawObject<T>(value.Value, this.underlyingFormat, this.underlyingTypeSerializer);
        }

        protected override T? Deserialize(IntermediateReader input, ContentSerializerAttribute format, T? existingInstance)
        {
            return new T?(input.ReadRawObject<T>(this.underlyingFormat, this.underlyingTypeSerializer));
        }
    }
}
