using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.PrimitiveTypeSerializers
{
    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [ContentTypeSerializer]
    internal class Int32Serializer : ContentTypeSerializer<int>
    {
        public Int32Serializer()
            : base("int")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, int value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override int Deserialize(IntermediateReader input, ContentSerializerAttribute format, int existingInstance)
        {
            return input.Xml.ReadInt32Part();
        }
    }

    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [ContentTypeSerializer]
    internal class UInt32Serializer : ContentTypeSerializer<uint>
    {
        public UInt32Serializer()
            : base("uint")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, uint value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override uint Deserialize(IntermediateReader input, ContentSerializerAttribute format, uint existingInstance)
        {
            return input.Xml.ReadUInt32Part();
        }
    }
}
