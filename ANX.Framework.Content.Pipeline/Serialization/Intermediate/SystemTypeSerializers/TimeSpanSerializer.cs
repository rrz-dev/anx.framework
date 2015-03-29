using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers
{
    [ContentTypeSerializer]
    internal class TimeSpanSerializer : ContentTypeSerializer<TimeSpan>
    {
        protected override void Serialize(IntermediateWriter output, TimeSpan value, ContentSerializerAttribute format)
        {
            output.Xml.WriteString(XmlConvert.ToString(value));
        }

        protected override TimeSpan Deserialize(IntermediateReader input, ContentSerializerAttribute format, TimeSpan existingInstance)
        {
            return XmlConvert.ToTimeSpan(input.Xml.ReadContentAsString());
        }
    }
}
