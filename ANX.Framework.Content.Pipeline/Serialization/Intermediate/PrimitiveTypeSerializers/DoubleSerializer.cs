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
    internal class DoubleSerializer : ContentTypeSerializer<double>
    {
        public DoubleSerializer()
            : base("double")
        {

        }

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, double value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value);
        }

        protected override double Deserialize(IntermediateReader input, ContentSerializerAttribute format, double existingInstance)
        {
            return input.Xml.ReadDoublePart();
        }
    }
}
