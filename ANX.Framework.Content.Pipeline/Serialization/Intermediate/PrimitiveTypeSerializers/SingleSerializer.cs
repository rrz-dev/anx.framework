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
    internal class SingleSerializer : ContentTypeSerializer<float>
    {
        public SingleSerializer()
            : base("float")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, float value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override float Deserialize(IntermediateReader input, ContentSerializerAttribute format, float existingInstance)
        {
            return input.Xml.ReadSinglePart();
        }
    }
}
