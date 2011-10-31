#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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

        [Test]
        public void TestObjectSerialization()
        {
            Assert.Ignore("The object reader is never invoked");
        }

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
