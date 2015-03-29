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
    internal class Int64Serializer : ContentTypeSerializer<long>
    {
        public Int64Serializer()
            : base("long")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, long value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override long Deserialize(IntermediateReader input, ContentSerializerAttribute format, long existingInstance)
        {
            return input.Xml.ReadInt64Part();
        }
    }

    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [ContentTypeSerializer]
    internal class UInt64Serializer : ContentTypeSerializer<ulong>
    {
        public UInt64Serializer()
            : base("ulong")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, ulong value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override ulong Deserialize(IntermediateReader input, ContentSerializerAttribute format, ulong existingInstance)
        {
            return input.Xml.ReadUInt64Part();
        }
    }
}
