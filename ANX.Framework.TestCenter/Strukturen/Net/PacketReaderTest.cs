using System;
using System.IO;
using NUnit.Framework;

using XNAPacketReader = Microsoft.Xna.Framework.Net.PacketReader;
using ANXPacketReader = ANX.Framework.Net.PacketReader;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Net
{
    class PacketReaderTest
    {
        [Test]
        public void Constructor1()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);
        }

        [Test]
        public void Constructor2()
        {
            var xna = new XNAPacketReader(256);
            var anx = new ANXPacketReader(256);

            Assert.AreEqual(((MemoryStream)xna.BaseStream).Capacity, ((MemoryStream)anx.BaseStream).Capacity);
        }

        [Test]
        public void ReadVector2()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            var xnaWriter = new BinaryWriter(xna.BaseStream);
            var anxWriter = new BinaryWriter(anx.BaseStream);

            xnaWriter.Write(4.5f);
            xnaWriter.Write(1f);

            anxWriter.Write(4.5f);
            anxWriter.Write(1f);

            xna.BaseStream.Position = 0;
            anx.BaseStream.Position = 0;

            AssertHelper.ConvertEquals(xna.ReadVector2(), anx.ReadVector2(), "ReadVector2");
        }

        [Test]
        public void ReadVector3()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            var xnaWriter = new BinaryWriter(xna.BaseStream);
            var anxWriter = new BinaryWriter(anx.BaseStream);

            xnaWriter.Write(4.5f);
            xnaWriter.Write(1f);
            xnaWriter.Write(0.4f);

            anxWriter.Write(4.5f);
            anxWriter.Write(1f);
            anxWriter.Write(0.4f);

            xna.BaseStream.Position = 0;
            anx.BaseStream.Position = 0;

            AssertHelper.ConvertEquals(xna.ReadVector3(), anx.ReadVector3(), "ReadVector3");
        }

        [Test]
        public void ReadVector4()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            var xnaWriter = new BinaryWriter(xna.BaseStream);
            var anxWriter = new BinaryWriter(anx.BaseStream);

            xnaWriter.Write(4.5f);
            xnaWriter.Write(1f);
            xnaWriter.Write(0.4f);
            xnaWriter.Write(16f);

            anxWriter.Write(4.5f);
            anxWriter.Write(1f);
            anxWriter.Write(0.4f);
            anxWriter.Write(16f);

            xna.BaseStream.Position = 0;
            anx.BaseStream.Position = 0;

            AssertHelper.ConvertEquals(xna.ReadVector4(), anx.ReadVector4(), "ReadVector4");
        }

        [Test]
        public void ReadMatrix()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            var xnaWriter = new BinaryWriter(xna.BaseStream);
            var anxWriter = new BinaryWriter(anx.BaseStream);

            Matrix mat = Matrix.CreateRotationY(0.1f);
            xnaWriter.Write(mat.M11);
            xnaWriter.Write(mat.M12);
            xnaWriter.Write(mat.M13);
            xnaWriter.Write(mat.M14);
            xnaWriter.Write(mat.M21);
            xnaWriter.Write(mat.M22);
            xnaWriter.Write(mat.M23);
            xnaWriter.Write(mat.M24);
            xnaWriter.Write(mat.M31);
            xnaWriter.Write(mat.M32);
            xnaWriter.Write(mat.M33);
            xnaWriter.Write(mat.M34);
            xnaWriter.Write(mat.M41);
            xnaWriter.Write(mat.M42);
            xnaWriter.Write(mat.M43);
            xnaWriter.Write(mat.M44);

            anxWriter.Write(mat.M11);
            anxWriter.Write(mat.M12);
            anxWriter.Write(mat.M13);
            anxWriter.Write(mat.M14);
            anxWriter.Write(mat.M21);
            anxWriter.Write(mat.M22);
            anxWriter.Write(mat.M23);
            anxWriter.Write(mat.M24);
            anxWriter.Write(mat.M31);
            anxWriter.Write(mat.M32);
            anxWriter.Write(mat.M33);
            anxWriter.Write(mat.M34);
            anxWriter.Write(mat.M41);
            anxWriter.Write(mat.M42);
            anxWriter.Write(mat.M43);
            anxWriter.Write(mat.M44);

            xna.BaseStream.Position = 0;
            anx.BaseStream.Position = 0;

            AssertHelper.ConvertEquals(xna.ReadMatrix(), anx.ReadMatrix(), "ReadMatrix");
        }

        [Test]
        public void ReadQuaternion()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            var xnaWriter = new BinaryWriter(xna.BaseStream);
            var anxWriter = new BinaryWriter(anx.BaseStream);

            xnaWriter.Write(4.5f);
            xnaWriter.Write(0.4f);
            xnaWriter.Write(1f);
            xnaWriter.Write(16f);

            anxWriter.Write(4.5f);
            anxWriter.Write(0.4f);
            anxWriter.Write(1f);
            anxWriter.Write(16f);

            xna.BaseStream.Position = 0;
            anx.BaseStream.Position = 0;

            AssertHelper.ConvertEquals(xna.ReadQuaternion(), anx.ReadQuaternion(), "ReadQuaternion");
        }

        [Test]
        public void ReadColor()
        {
            var xna = new XNAPacketReader();
            var anx = new ANXPacketReader();

            var xnaWriter = new BinaryWriter(xna.BaseStream);
            var anxWriter = new BinaryWriter(anx.BaseStream);

            xnaWriter.Write(Color.LightBlue.PackedValue);
            anxWriter.Write(Color.LightBlue.PackedValue);

            xna.BaseStream.Position = 0;
            anx.BaseStream.Position = 0;

            AssertHelper.ConvertEquals(xna.ReadColor(), anx.ReadColor(), "ReadColor");
        }
    }
}
