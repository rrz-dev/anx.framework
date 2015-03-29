using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class Vector4Serializer : ContentTypeSerializer<Vector4>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Vector4 value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.X);
            output.Xml.WritePart(value.Y);
            output.Xml.WritePart(value.Z);
            output.Xml.WritePart(value.W);
        }

        protected override Vector4 Deserialize(IntermediateReader input, ContentSerializerAttribute format, Vector4 existingInstance)
        {
            return new Vector4
            {
                X = input.Xml.ReadSinglePart(),
                Y = input.Xml.ReadSinglePart(),
                Z = input.Xml.ReadSinglePart(),
                W = input.Xml.ReadSinglePart(),
            };
        }
    }
}
