using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class QuaternionSerializer : ContentTypeSerializer<Quaternion>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Quaternion value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.X);
            output.Xml.WritePart(value.Y);
            output.Xml.WritePart(value.Z);
            output.Xml.WritePart(value.W);
        }

        protected override Quaternion Deserialize(IntermediateReader input, ContentSerializerAttribute format, Quaternion existingInstance)
        {
            return new Quaternion
            {
                X = input.Xml.ReadSinglePart(),
                Y = input.Xml.ReadSinglePart(),
                Z = input.Xml.ReadSinglePart(),
                W = input.Xml.ReadSinglePart(),
            };
        }
    }
}
