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

        protected override void Serialize(IntermediateWriter output, char value, ContentSerializerAttribute format)
        {
            output.Xml.WriteString(XmlConvert.ToString(value));
        }

        protected override char Deserialize(IntermediateReader input, ContentSerializerAttribute format, char existingInstance)
        {
            return XmlConvert.ToChar(input.Xml.ReadContentAsString());
        }
    }
}
