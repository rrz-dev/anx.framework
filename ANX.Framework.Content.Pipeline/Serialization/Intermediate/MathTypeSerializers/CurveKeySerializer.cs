using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [Developer("KorsarNek")]
    [ContentTypeSerializer]
    internal class CurveKeySerializer : ContentTypeSerializer<CurveKey>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, CurveKey value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.Position);
            output.Xml.WritePart(value.Value);
            output.Xml.WritePart(value.TangentIn);
            output.Xml.WritePart(value.TangentOut);
            output.Xml.WriteStringPart(value.Continuity.ToString());
        }

        protected override CurveKey Deserialize(IntermediateReader input, ContentSerializerAttribute format, CurveKey existingInstance)
        {
            float position = input.Xml.ReadSinglePart();
            float value = input.Xml.ReadSinglePart();
            float tangentIn = input.Xml.ReadSinglePart();
            float tangentOut = input.Xml.ReadSinglePart();
            CurveContinuity continuity = input.Xml.ReadEnumPart<CurveContinuity>();
            return new CurveKey(position, value, tangentIn, tangentOut, continuity);
        }
    }
}
