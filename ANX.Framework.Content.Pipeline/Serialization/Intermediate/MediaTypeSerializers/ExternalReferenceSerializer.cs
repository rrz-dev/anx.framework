using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MediaTypeSerializers
{
    [Developer("KorsarNek")]
    [ContentTypeSerializer]
    internal class ExternalReferenceSerializer<T> : ContentTypeSerializer<ExternalReference<T>>
    {
        private ContentTypeSerializer baseSerializer;
        private readonly ContentSerializerAttribute baseFormat = new ContentSerializerAttribute() { FlattenContent = true };

        protected internal override void Initialize(IntermediateSerializer serializer)
        {
            //Serializer for ContentItem.
            this.baseSerializer = serializer.GetTypeSerializer(typeof(ExternalReference<T>).BaseType);
        }

        protected override void Serialize(IntermediateWriter output, ExternalReference<T> value, ContentSerializerAttribute format)
        {
            //Write the optional ContentItem data first.
            output.WriteRawObject<ExternalReference<T>>(value, this.baseFormat, this.baseSerializer);
            output.WriteExternalReference<T>(value);
        }

        protected override ExternalReference<T> Deserialize(IntermediateReader input, ContentSerializerAttribute format, ExternalReference<T> existingInstance)
        {
            ExternalReference<T> externalReference = existingInstance;
            if (externalReference == null)
            {
                externalReference = new ExternalReference<T>();
            }
            //Read all the ContentItem specific stuff.
            input.ReadRawObject<ExternalReference<T>>(this.baseFormat, this.baseSerializer, externalReference);
            //Then read the typical external reference data.
            input.ReadExternalReference<T>(externalReference);
            return externalReference;
        }
    }
}
