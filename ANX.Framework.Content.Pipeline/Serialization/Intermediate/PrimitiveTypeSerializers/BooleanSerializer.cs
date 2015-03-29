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
    internal class BooleanSerializer : ContentTypeSerializer<bool>
    {
        public BooleanSerializer()
            : base("bool")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, bool value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override bool Deserialize(IntermediateReader input, ContentSerializerAttribute format, bool existingInstance)
        {
            return input.Xml.ReadBooleanPart();
        }
    }
}
