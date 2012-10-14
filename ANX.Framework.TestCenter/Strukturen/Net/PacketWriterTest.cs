using System;
using System.IO;
using NUnit.Framework;

using XNAPacketWriter = Microsoft.Xna.Framework.Net.PacketWriter;
using ANXPacketWriter = ANX.Framework.Net.PacketWriter;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Net
{
    internal class PacketWriterTest
    {
        [Test]
        public void Constructor1()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);
        }

        [Test]
        public void Constructor2()
        {
            var xna = new XNAPacketWriter(256);
            var anx = new ANXPacketWriter(256);

            Assert.AreEqual(((MemoryStream)xna.BaseStream).Capacity, ((MemoryStream)anx.BaseStream).Capacity);
        }

        [Test]
        public void WriteVector2()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            xna.Write(new Microsoft.Xna.Framework.Vector2(15f, 3.5f));
            anx.Write(new Vector2(15f, 3.5f));

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);

            xna.Position = 0;
            anx.Position = 0;
            var xnaReader = new BinaryReader(xna.BaseStream);
            var anxReader = new BinaryReader(anx.BaseStream);

            for (int index = 0; index < 2; index++)
                Assert.AreEqual(xnaReader.ReadSingle(), anxReader.ReadSingle());
        }

        [Test]
        public void WriteVector3()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            xna.Write(new Microsoft.Xna.Framework.Vector3(15f, 3.5f, 0.1f));
            anx.Write(new Vector3(15f, 3.5f, 0.1f));

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);

            xna.Position = 0;
            anx.Position = 0;
            var xnaReader = new BinaryReader(xna.BaseStream);
            var anxReader = new BinaryReader(anx.BaseStream);

            for (int index = 0; index < 3; index++)
                Assert.AreEqual(xnaReader.ReadSingle(), anxReader.ReadSingle());
        }

        [Test]
        public void WriteVector4()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            xna.Write(new Microsoft.Xna.Framework.Vector4(15f, 3.5f, 0.1f, 1f));
            anx.Write(new Vector4(15f, 3.5f, 0.1f, 1f));

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);

            xna.Position = 0;
            anx.Position = 0;
            var xnaReader = new BinaryReader(xna.BaseStream);
            var anxReader = new BinaryReader(anx.BaseStream);

            for (int index = 0; index < 4; index++)
                Assert.AreEqual(xnaReader.ReadSingle(), anxReader.ReadSingle());
        }

        [Test]
        public void WriteMatrix()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            xna.Write(Microsoft.Xna.Framework.Matrix.CreateRotationY(0.1f));
            anx.Write(Matrix.CreateRotationY(0.1f));

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);

            xna.Position = 0;
            anx.Position = 0;
            var xnaReader = new BinaryReader(xna.BaseStream);
            var anxReader = new BinaryReader(anx.BaseStream);

            for (int index = 0; index < 16; index++)
                Assert.AreEqual(xnaReader.ReadSingle(), anxReader.ReadSingle());
        }

        [Test]
        public void WriteQuaternion()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            xna.Write(new Microsoft.Xna.Framework.Quaternion(0.1f, 15f, 3.4f, 4f));
            anx.Write(new Quaternion(0.1f, 15f, 3.4f, 4f));

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);

            xna.Position = 0;
            anx.Position = 0;
            var xnaReader = new BinaryReader(xna.BaseStream);
            var anxReader = new BinaryReader(anx.BaseStream);

            for (int index = 0; index < 4; index++)
                Assert.AreEqual(xnaReader.ReadSingle(), anxReader.ReadSingle());
        }

        [Test]
        public void WriteColor()
        {
            var xna = new XNAPacketWriter();
            var anx = new ANXPacketWriter();

            xna.Write(Microsoft.Xna.Framework.Color.CornflowerBlue);
            anx.Write(Color.CornflowerBlue);

            Assert.AreEqual(xna.Length, anx.Length);
            Assert.AreEqual(xna.Position, anx.Position);

            xna.Position = 0;
            anx.Position = 0;
            var xnaReader = new BinaryReader(xna.BaseStream);
            var anxReader = new BinaryReader(anx.BaseStream);

            Assert.AreEqual(xnaReader.ReadUInt32(), anxReader.ReadUInt32());
        }
    }
}
