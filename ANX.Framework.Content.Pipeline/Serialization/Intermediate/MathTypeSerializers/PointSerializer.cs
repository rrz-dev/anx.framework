using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class PointSerializer : ContentTypeSerializer<Point>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Point value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.X);
            output.Xml.WritePart(value.Y);
        }

        protected override Point Deserialize(IntermediateReader input, ContentSerializerAttribute format, Point existingInstance)
        {
            return new Point
            {
                X = input.Xml.ReadInt32Part(),
                Y = input.Xml.ReadInt32Part(),
            };
        }
    }
}
