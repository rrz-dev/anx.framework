using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    class CollectionSerializerBase<TCollection, TElement> : ContentTypeSerializer<TCollection> where TCollection : ICollection<TElement>
    {
        public override bool CanDeserializeIntoExistingObject
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, TCollection value, ContentSerializerAttribute format)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (format == null)
                throw new ArgumentNullException("format");

            if (value == null)
                throw new ArgumentNullException("value");

            ContentTypeSerializer serializer = output.Serializer.GetTypeSerializer(typeof(TElement));

            ContentSerializerAttribute contentSerializerAttribute = new ContentSerializerAttribute();
            contentSerializerAttribute.ElementName = format.CollectionItemName;
            contentSerializerAttribute.FlattenContent = serializer.HasOnlyFlatContent;
            foreach (TElement current in value)
            {
                output.WriteObject<TElement>(current, contentSerializerAttribute);
            }
        }

        protected override TCollection Deserialize(IntermediateReader input, ContentSerializerAttribute format, TCollection existingInstance)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (format == null)
                throw new ArgumentNullException("format");

            //We have CanDeserializeIntoExistingObject true, which means we expect to get an instance, especially because we are a serializer for interfaces and 
            //they must return true in CanDeserializeIntoExistingObject or else they are invalid.
            if (existingInstance == null)
                throw new ArgumentNullException("existingInstance");

            ContentTypeSerializer serializer = input.Serializer.GetTypeSerializer(typeof(TElement));

            ContentSerializerAttribute contentSerializerAttribute = new ContentSerializerAttribute();
            contentSerializerAttribute.ElementName = format.CollectionItemName;
            contentSerializerAttribute.FlattenContent = serializer.HasOnlyFlatContent;

            if (contentSerializerAttribute.FlattenContent)
            {
                while (input.Xml.HasMoreParts)
                {
                    existingInstance.Add(input.ReadObject<TElement>(contentSerializerAttribute, serializer));
                }
            }
            else
            {
                while (input.MoveToElement(format.CollectionItemName))
                {
                    existingInstance.Add(input.ReadObject<TElement>(contentSerializerAttribute, serializer));
                }
            }

            return existingInstance;
        }

        public override bool ObjectIsEmpty(TCollection value)
        {
            return value.Count == 0;
        }
    }
}
