using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class Vector3Serializer : ContentTypeSerializer<Vector3>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Vector3 value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.X);
            output.Xml.WritePart(value.Y);
            output.Xml.WritePart(value.Z);
        }

        protected override Vector3 Deserialize(IntermediateReader input, ContentSerializerAttribute format, Vector3 existingInstance)
        {
            return new Vector3
            {
                X = input.Xml.ReadSinglePart(),
                Y = input.Xml.ReadSinglePart(),
                Z = input.Xml.ReadSinglePart(),
            };
        }
    }
}
