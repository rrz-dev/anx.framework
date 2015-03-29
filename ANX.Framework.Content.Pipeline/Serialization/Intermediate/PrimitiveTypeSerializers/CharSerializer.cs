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
    internal class CharSerializer : ContentTypeSerializer<char>
    {
        public CharSerializer()
            : base("char")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, char value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override char Deserialize(IntermediateReader input, ContentSerializerAttribute format, char existingInstance)
        {
            return input.Xml.ReadCharPart();
        }
    }
}
