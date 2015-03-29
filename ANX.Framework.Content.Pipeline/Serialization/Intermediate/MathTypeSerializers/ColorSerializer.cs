using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    [Developer("KorsarNek")]
    [ContentTypeSerializer]
    internal class ColorSerializer : PackedVectorSerializer32<Color>
    {
        protected override void Serialize(IntermediateWriter output, Color value, ContentSerializerAttribute format)
        {
            base.Serialize(output, ColorSerializer.SwapBgra(value), format);
        }

        protected override Color Deserialize(IntermediateReader input, ContentSerializerAttribute format, Color existingInstance)
        {
            return ColorSerializer.SwapBgra(base.Deserialize(input, format, existingInstance));
        }

        private static Color SwapBgra(Color value)
        {
            uint packedValue = value.PackedValue;
            //switch first and third byte and keep fourth and second
            value.PackedValue = ((packedValue & 0xFF) << 16 | (packedValue & 0xFF0000) >> 16 | (packedValue & 0xFF00FF00));
            return value;
        }
    }
}
