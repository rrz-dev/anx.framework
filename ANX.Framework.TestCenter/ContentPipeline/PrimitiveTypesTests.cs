#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XnaFrame = Microsoft.Xna.Framework;
using AnxFrame = ANX.Framework;

using ANX.Framework.TestCenter.ContentPipeline.Helper;

namespace ANX.Framework.TestCenter.ContentPipeline
{
    [TestFixture]
    public class PrimitiveTypesTests
    {
        public Helper.Compiler Compiler { get; set; }

        [SetUp]
        public void BeforeEach()
        {
            Compiler = new Compiler();
        }

        [Test]
        public void TestBooleanSerialization()
        {
            foreach (var value in new bool[]{ true, false })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Boolean>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestByteSerialization()
        {
            foreach (var value in new byte[] { byte.MinValue, 0, byte.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Byte>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestSByteSerialization()
        {
            foreach (var value in new sbyte[] { sbyte.MinValue, 0, sbyte.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<SByte>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestCharSerialization()
        {
            foreach (var value in new char[] { char.MinValue, char.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<char>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestDoubleSerialization()
        {
            foreach (var value in new double[] { 
                double.MinValue, double.MaxValue, double.Epsilon, 
                double.NaN, double.NegativeInfinity, double.PositiveInfinity })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Double>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestInt16Serialization()
        {
            foreach (var value in new short[] { short.MinValue, short.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Int16>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestUInt16Serialization()
        {
            foreach (var value in new ushort[] { ushort.MinValue, ushort.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<UInt16>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestInt32Serialization()
        {
            foreach (var value in new int[] { int.MinValue, int.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Int32>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestUInt32Serialization()
        {
            foreach (var value in new uint[] { uint.MinValue, uint.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<UInt32>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestInt64Serialization()
        {
            foreach (var value in new long[] { long.MinValue, long.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Int64>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestUInt64Serialization()
        {
            foreach (var value in new ulong[] { ulong.MinValue, ulong.MaxValue })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<UInt64>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        //[Test]
        //public void TestObjectSerialization()
        //{
        //    Assert.Ignore("The object reader is never invoked");
        //}

        [Test]
        public void TestSingleSerialization()
        {
            foreach (var value in new float[] { 
                float.MinValue, float.MaxValue, float.Epsilon,
                float.NaN, float.NegativeInfinity, float.PositiveInfinity })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<Single>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestStringSerialization()
        {
            foreach (var value in new string[] { "Xna rocks!", "So does Anx!" })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<String>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }
    }
}
