using ANX.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate.MathTypeSerializers
{
    internal class PackedVectorSerializer8<T> : ContentTypeSerializer<T> where T : struct, IPackedVector<byte>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, T value, ContentSerializerAttribute format)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            output.Xml.WriteString(value.PackedValue.ToString("X2", CultureInfo.InvariantCulture));
        }

        protected override T Deserialize(IntermediateReader input, ContentSerializerAttribute format, T existingInstance)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            T result = default(T);
            result.PackedValue = byte.Parse(input.Xml.ReadString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return result;
        }
    }

    internal class PackedVectorSerializer16<T> : ContentTypeSerializer<T> where T : struct, IPackedVector<ushort>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, T value, ContentSerializerAttribute format)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            output.Xml.WriteString(value.PackedValue.ToString("X4", CultureInfo.InvariantCulture));
        }

        protected override T Deserialize(IntermediateReader input, ContentSerializerAttribute format, T existingInstance)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            T result = default(T);
            result.PackedValue = ushort.Parse(input.Xml.ReadString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return result;
        }
    }

    internal class PackedVectorSerializer32<T> : ContentTypeSerializer<T> where T : struct, IPackedVector<uint>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, T value, ContentSerializerAttribute format)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            output.Xml.WriteString(value.PackedValue.ToString("X8", CultureInfo.InvariantCulture));
        }

        protected override T Deserialize(IntermediateReader input, ContentSerializerAttribute format, T existingInstance)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            T result = default(T);
            result.PackedValue = uint.Parse(input.Xml.ReadString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return result;
        }
    }

    internal class PackedVectorSerializer64<T> : ContentTypeSerializer<T> where T : struct, IPackedVector<ulong>
    {
        public override bool HasOnlyFlatContent
        {
            get
            {
                return true;
            }
        }

        protected override void Serialize(IntermediateWriter output, T value, ContentSerializerAttribute format)
        {
            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            output.Xml.WriteString(value.PackedValue.ToString("X16", CultureInfo.InvariantCulture));
        }

        protected override T Deserialize(IntermediateReader input, ContentSerializerAttribute format, T existingInstance)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            T result = default(T);
            result.PackedValue = ulong.Parse(input.Xml.ReadString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return result;
        }
    }

    [ContentTypeSerializer]
    internal class Alpha8Serializer : PackedVectorSerializer8<Alpha8>
    {
    }

    [ContentTypeSerializer]
    internal class Bgr565Serializer : PackedVectorSerializer16<Bgr565>
    {
    }

    [ContentTypeSerializer]
    internal class Bgra4444Serializer : PackedVectorSerializer16<Bgra4444>
    {
    }

    [ContentTypeSerializer]
    internal class Bgra5551Serializer : PackedVectorSerializer16<Bgra5551>
    {
    }

    [ContentTypeSerializer]
    internal class Byte4Serializer : PackedVectorSerializer32<Byte4>
    {
    }

    [ContentTypeSerializer]
    internal class HalfSingleSerializer : PackedVectorSerializer16<HalfSingle>
    {
    }

    [ContentTypeSerializer]
    internal class HalfVector2Serializer : PackedVectorSerializer32<HalfVector2>
    {
    }

    [ContentTypeSerializer]
    internal class HalfVector4Serializer : PackedVectorSerializer64<HalfVector4>
    {
    }

    [ContentTypeSerializer]
    internal class Rg32Serializer : PackedVectorSerializer32<Rg32>
    {
    }

    [ContentTypeSerializer]
    internal class Rgba1010102Serializer : PackedVectorSerializer32<Rgba1010102>
    {
    }

    [ContentTypeSerializer]
    internal class Rgba64Serializer : PackedVectorSerializer64<Rgba64>
    {
    }

    [ContentTypeSerializer]
    internal class Short2Serializer : PackedVectorSerializer32<Short2>
    {
    }

    [ContentTypeSerializer]
    internal class Short4Serializer : PackedVectorSerializer64<Short4>
    {
    }
}
