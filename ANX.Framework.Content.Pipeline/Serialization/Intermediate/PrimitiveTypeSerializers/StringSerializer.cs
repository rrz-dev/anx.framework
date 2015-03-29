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
    internal class StringSerializer : ContentTypeSerializer<string>
    {
        public StringSerializer()
            : base("string")
        {

        }

        protected override void Serialize(IntermediateWriter output, string value, ContentSerializerAttribute format)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            output.Xml.WriteString(value);
        }

        protected override string Deserialize(IntermediateReader input, ContentSerializerAttribute format, string existingInstance)
        {
            return input.Xml.ReadContentAsString();
        }
    }
}
