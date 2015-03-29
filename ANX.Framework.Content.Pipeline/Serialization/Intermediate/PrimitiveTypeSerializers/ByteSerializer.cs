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
    internal class ByteSerializer : ContentTypeSerializer<byte>
    {
        public ByteSerializer()
            : base("byte")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, byte value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override byte Deserialize(IntermediateReader input, ContentSerializerAttribute format, byte existingInstance)
        {
            return input.Xml.ReadBytePart();
        }
    }

    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [ContentTypeSerializer]
    internal class SByteSerializer : ContentTypeSerializer<sbyte>
    {
        public SByteSerializer()
            : base("sbyte")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, sbyte value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override sbyte Deserialize(IntermediateReader input, ContentSerializerAttribute format, sbyte existingInstance)
        {
            return input.Xml.ReadSBytePart();
        }
    }
}
