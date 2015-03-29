using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class Vector2Serializer : ContentTypeSerializer<Vector2>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Vector2 value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.X);
            output.Xml.WritePart(value.Y);
        }

        protected override Vector2 Deserialize(IntermediateReader input, ContentSerializerAttribute format, Vector2 existingInstance)
        {
            return new Vector2
            {
                X = input.Xml.ReadSinglePart(),
                Y = input.Xml.ReadSinglePart()
            };
        }
    }
}
