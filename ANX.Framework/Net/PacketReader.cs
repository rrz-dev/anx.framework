using System;
using System.IO;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class PacketReader : BinaryReader
	{
        public int Length
        {
            get { return (int)BaseStream.Length; }
        }

        public int Position
        {
            get { return (int)BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        public PacketReader()
			: this(0)
		{
		}

		public PacketReader(int capacity)
			: base(new MemoryStream(capacity))
		{
		}

		public Vector2 ReadVector2()
		{
			return new Vector2
			{
				X = ReadSingle(),
				Y = ReadSingle()
			};
		}

		public Vector3 ReadVector3()
		{
			return new Vector3
			{
				X = ReadSingle(),
				Y = ReadSingle(),
				Z = ReadSingle()
			};
		}

		public Vector4 ReadVector4()
		{
			return new Vector4
			{
				X = ReadSingle(),
				Y = ReadSingle(),
				Z = ReadSingle(),
				W = ReadSingle()
			};
		}

		public Matrix ReadMatrix()
		{
			return new Matrix
			{
				M11 = ReadSingle(),
				M12 = ReadSingle(),
				M13 = ReadSingle(),
				M14 = ReadSingle(),
				M21 = ReadSingle(),
				M22 = ReadSingle(),
				M23 = ReadSingle(),
				M24 = ReadSingle(),
				M31 = ReadSingle(),
				M32 = ReadSingle(),
				M33 = ReadSingle(),
				M34 = ReadSingle(),
				M41 = ReadSingle(),
				M42 = ReadSingle(),
				M43 = ReadSingle(),
				M44 = ReadSingle()
			};
		}

		public Quaternion ReadQuaternion()
		{
			return new Quaternion
			{
				X = ReadSingle(),
				Y = ReadSingle(),
				Z = ReadSingle(),
				W = ReadSingle()
			};
		}

		public Color ReadColor()
		{
			return new Color
			{
				PackedValue = ReadUInt32()
			};
		}
	}
}
