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
    internal class Int16Serializer : ContentTypeSerializer<short>
    {
        public Int16Serializer()
            : base("short")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, short value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override short Deserialize(IntermediateReader input, ContentSerializerAttribute format, short existingInstance)
        {
            return input.Xml.ReadInt16Part();
        }
    }

    [Developer("KorsarNek")]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [ContentTypeSerializer]
    internal class UInt16Serializer : ContentTypeSerializer<ushort>
    {
        public UInt16Serializer()
            : base("ushort")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, ushort value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override ushort Deserialize(IntermediateReader input, ContentSerializerAttribute format, ushort existingInstance)
        {
            return input.Xml.ReadUInt16Part();
        }
    }
}
