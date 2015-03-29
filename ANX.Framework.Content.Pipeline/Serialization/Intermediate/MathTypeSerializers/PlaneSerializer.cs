using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [ContentTypeSerializer]
    internal class PlaneSerializer : ContentTypeSerializer<Plane>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Plane value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.Normal.X);
            output.Xml.WritePart(value.Normal.Y);
            output.Xml.WritePart(value.Normal.Z);
            output.Xml.WritePart(value.D);
        }

        protected override Plane Deserialize(IntermediateReader input, ContentSerializerAttribute format, Plane existingInstance)
        {
            return new Plane()
            {
                Normal = new Vector3()
                {
                    X = input.Xml.ReadSinglePart(),
                    Y = input.Xml.ReadSinglePart(),
                    Z = input.Xml.ReadSinglePart(),
                },
                D = input.Xml.ReadSinglePart(),
            };
        }
    }
}
