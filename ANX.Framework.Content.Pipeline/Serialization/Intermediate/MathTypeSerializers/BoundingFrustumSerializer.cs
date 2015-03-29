using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [Developer("KorsarNek")]
    [ContentTypeSerializer]
    internal class BoundingFrustumSerializer : ContentTypeSerializer<BoundingFrustum>
    {
        private static readonly ContentSerializerAttribute matrixFormat = new ContentSerializerAttribute() { ElementName = "Matrix" };

        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, BoundingFrustum value, ContentSerializerAttribute format)
        {
            output.WriteObject<Matrix>(value.Matrix, BoundingFrustumSerializer.matrixFormat);
        }

        protected override BoundingFrustum Deserialize(IntermediateReader input, ContentSerializerAttribute format, BoundingFrustum existingInstance)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            
            return new BoundingFrustum(input.ReadObject<Matrix>(BoundingFrustumSerializer.matrixFormat));
        }
    }
}
