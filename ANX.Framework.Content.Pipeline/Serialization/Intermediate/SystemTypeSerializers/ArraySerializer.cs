#region Using Statements
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [Developer("KorsarNek")]
    class ArraySerializer<T> : ContentTypeSerializer<T[]>
    {
        private ContentTypeSerializer _listSerializer;

        protected internal override void Initialize(IntermediateSerializer serializer)
        {
            base.Initialize(serializer);

            _listSerializer = serializer.GetTypeSerializer(typeof(List<T>));
        }

        protected override T[] Deserialize(IntermediateReader input, ContentSerializerAttribute format, T[] existingInstance)
        {
            List<T> list = new List<T>();
            _listSerializer.Deserialize(input, format, list);
            return list.ToArray();
        }

        protected override void Serialize(IntermediateWriter output, T[] value, ContentSerializerAttribute format)
        {
            _listSerializer.Serialize(output, value.ToList(), format);
        }

        public override bool ObjectIsEmpty(T[] value)
        {
            return value.Length == 0;
        }
    }
}
