using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class DateTimeSerializer : ContentTypeSerializer<DateTime>
    {
        protected override void Serialize(IntermediateWriter output, DateTime value, ContentSerializerAttribute format)
        {
            output.Xml.WriteString(XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
        }

        protected override DateTime Deserialize(IntermediateReader input, ContentSerializerAttribute format, DateTime existingInstance)
        {
            return XmlConvert.ToDateTime(input.Xml.ReadContentAsString(), XmlDateTimeSerializationMode.RoundtripKind);
        }
    }
}
