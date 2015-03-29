using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [Developer("KorsarNek")]
    [ContentTypeSerializer]
    internal class MatrixSerializer : ContentTypeSerializer<Matrix>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, Matrix value, ContentSerializerAttribute format)
        {
            output.Xml.WritePart(value.M11);
            output.Xml.WritePart(value.M12);
            output.Xml.WritePart(value.M13);
            output.Xml.WritePart(value.M14);
            output.Xml.WritePart(value.M21);
            output.Xml.WritePart(value.M22);
            output.Xml.WritePart(value.M23);
            output.Xml.WritePart(value.M24);
            output.Xml.WritePart(value.M31);
            output.Xml.WritePart(value.M32);
            output.Xml.WritePart(value.M33);
            output.Xml.WritePart(value.M34);
            output.Xml.WritePart(value.M41);
            output.Xml.WritePart(value.M42);
            output.Xml.WritePart(value.M43);
            output.Xml.WritePart(value.M44);
        }

        protected override Matrix Deserialize(IntermediateReader input, ContentSerializerAttribute format, Matrix existingInstance)
        {
            return new Matrix
            {
                M11 = input.Xml.ReadSinglePart(),
                M12 = input.Xml.ReadSinglePart(),
                M13 = input.Xml.ReadSinglePart(),
                M14 = input.Xml.ReadSinglePart(),
                M21 = input.Xml.ReadSinglePart(),
                M22 = input.Xml.ReadSinglePart(),
                M23 = input.Xml.ReadSinglePart(),
                M24 = input.Xml.ReadSinglePart(),
                M31 = input.Xml.ReadSinglePart(),
                M32 = input.Xml.ReadSinglePart(),
                M33 = input.Xml.ReadSinglePart(),
                M34 = input.Xml.ReadSinglePart(),
                M41 = input.Xml.ReadSinglePart(),
                M42 = input.Xml.ReadSinglePart(),
                M43 = input.Xml.ReadSinglePart(),
                M44 = input.Xml.ReadSinglePart(),
            };
        }
    }
}
