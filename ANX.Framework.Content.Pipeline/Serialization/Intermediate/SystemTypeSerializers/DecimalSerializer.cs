using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class DecimalSerializer : ContentTypeSerializer<decimal>
    {
        protected override void Serialize(IntermediateWriter output, decimal value, ContentSerializerAttribute format)
        {
            output.Xml.WriteString(XmlConvert.ToString(value));
        }

        protected override decimal Deserialize(IntermediateReader input, ContentSerializerAttribute format, decimal existingInstance)
        {
            return XmlConvert.ToDecimal(input.Xml.ReadContentAsString());
        }
    }
}
