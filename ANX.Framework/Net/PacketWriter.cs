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
	public class PacketWriter : BinaryWriter
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

        public PacketWriter()
			: this(0)
		{
		}

		public PacketWriter(int capacity)
			: base(new MemoryStream(capacity))
		{
		}

		public void Write(Vector2 value)
		{
			Write(value.X);
			Write(value.Y);
		}

		public void Write(Vector3 value)
		{
			Write(value.X);
			Write(value.Y);
			Write(value.Z);
		}

		public void Write(Vector4 value)
		{
			Write(value.X);
			Write(value.Y);
			Write(value.Z);
			Write(value.W);
		}

		public void Write(Matrix value)
		{
			Write(value.M11);
			Write(value.M12);
			Write(value.M13);
			Write(value.M14);
			Write(value.M21);
			Write(value.M22);
			Write(value.M23);
			Write(value.M24);
			Write(value.M31);
			Write(value.M32);
			Write(value.M33);
			Write(value.M34);
			Write(value.M41);
			Write(value.M42);
			Write(value.M43);
			Write(value.M44);
		}

		public void Write(Quaternion value)
		{
			Write(value.X);
			Write(value.Y);
			Write(value.Z);
			Write(value.W);
		}

		public void Write(Color value)
		{
			Write(value.PackedValue);
		}
	}
}
