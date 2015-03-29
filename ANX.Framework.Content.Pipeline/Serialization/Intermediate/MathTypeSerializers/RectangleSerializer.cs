using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class RectangleSerializer : ContentTypeSerializer<Rectangle>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Rectangle value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.X);
            output.Xml.WritePart(value.Y);
            output.Xml.WritePart(value.Width);
            output.Xml.WritePart(value.Height);
        }

        protected override Rectangle Deserialize(IntermediateReader input, ContentSerializerAttribute format, Rectangle existingInstance)
        {
            return new Rectangle
            {
                X = input.Xml.ReadInt32Part(),
                Y = input.Xml.ReadInt32Part(),
                Width = input.Xml.ReadInt32Part(),
                Height = input.Xml.ReadInt32Part(),
            };
        }
    }
}
