using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    partial class IntermediateXmlReader
    {
        internal float ReadSinglePart()
        {
            return XmlConvert.ToSingle(ReadStringPart());
        }

        internal int ReadInt32Part()
        {
            return XmlConvert.ToInt32(ReadStringPart());
        }

        internal uint ReadUInt32Part()
        {
            return XmlConvert.ToUInt32(ReadStringPart());
        }

        internal bool ReadBooleanPart()
        {
            return XmlConvert.ToBoolean(ReadStringPart());
        }

        internal byte ReadBytePart()
        {
            return XmlConvert.ToByte(ReadStringPart());
        }

        internal sbyte ReadSBytePart()
        {
            return XmlConvert.ToSByte(ReadStringPart());
        }

        internal double ReadDoublePart()
        {
            return XmlConvert.ToDouble(ReadStringPart());
        }

        internal char ReadCharPart()
        {
            return XmlConvert.ToChar(ReadStringPart());
        }

        internal short ReadInt16Part()
        {
            return XmlConvert.ToInt16(ReadStringPart());
        }

        internal ushort ReadUInt16Part()
        {
            return XmlConvert.ToUInt16(ReadStringPart());
        }

        internal long ReadInt64Part()
        {
            return XmlConvert.ToInt64(ReadStringPart());
        }

        internal ulong ReadUInt64Part()
        {
            return XmlConvert.ToUInt64(ReadStringPart());
        }

        internal TEnum ReadEnumPart<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            if (typeof(TEnum).IsEnum == false)
                throw new ArgumentException("The type for TEnum is not an enum.");

            return (TEnum)Enum.Parse(typeof(TEnum), ReadStringPart());
        }
    }
}
