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
    class MathTypesTests
    {
        public Helper.Compiler Compiler { get; set; }

        [SetUp]
        public void BeforeEach()
        {
            Compiler = new Compiler();
        }

        [Test]
        public void TestCompileWithoutCompression()
        {
            using (var stream = new MemoryStream())
            {
                Compiler.WriteAsset(stream, ANX.Framework.Color.CornflowerBlue);

                stream.Position = 0;
                byte[] buffer = new byte[6];
                stream.Read(buffer, 0, 6);

                Assert.AreEqual(buffer[0], 'X');
                Assert.AreEqual(buffer[1], 'N');
                Assert.AreEqual(buffer[2], 'B');
                Assert.AreEqual(buffer[3], 'w');

                bool compressed = (buffer[5] >> 4) > 0;
                Assert.AreEqual(compressed, false);
            }
        }

        [Test]
        public void TestCompileWithCompression() 
        {
            using (var stream = new MemoryStream())
            {
                //System.Diagnostics.Debugger.Launch();
                Compiler.WriteAsset(stream, ANX.Framework.Color.CornflowerBlue, true);

                stream.Position = 0;
                byte[] buffer = new byte[6];
                stream.Read(buffer, 0, 6);

                Assert.AreEqual(buffer[0], 'X');
                Assert.AreEqual(buffer[1], 'N');
                Assert.AreEqual(buffer[2], 'B');
                Assert.AreEqual(buffer[3], 'w');

                bool compressed = (buffer[5] >> 4) > 0;
                Assert.AreEqual(compressed, true);
            }
        }

        [Test]
        public void TestCurveSerialization()
        {
            //TODO: Assert.Fail("not implemented");
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestBoundingBoxSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.BoundingBox()
                {
                    Min = new XnaFrame.Vector3(1, 2, 3),
                    Max = new XnaFrame.Vector3(4, 5, 6)
                };
                var expect = new AnxFrame.BoundingBox() 
                {
                    Min = new AnxFrame.Vector3(1, 2, 3),
                    Max = new AnxFrame.Vector3(4, 5, 6)
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.BoundingBox>(null, stream, "Box");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestBoundingFrustumSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.BoundingFrustum(new XnaFrame.Matrix() { 
                    M11 = 11,
                    M12 = 12,
                    M13 = 13,
                    M14 = 14,

                    M21 = 21,
                    M22 = 22,
                    M23 = 23,
                    M24 = 24,

                    M31 = 31,
                    M32 = 32,
                    M33 = 33,
                    M34 = 34,

                    M41 = 41,
                    M42 = 42,
                    M43 = 43,
                    M44 = 44,
                });
                var expect = new AnxFrame.BoundingFrustum(new AnxFrame.Matrix()
                {
                    M11 = 11,
                    M12 = 12,
                    M13 = 13,
                    M14 = 14,

                    M21 = 21,
                    M22 = 22,
                    M23 = 23,
                    M24 = 24,

                    M31 = 31,
                    M32 = 32,
                    M33 = 33,
                    M34 = 34,

                    M41 = 41,
                    M42 = 42,
                    M43 = 43,
                    M44 = 44,
                });

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.BoundingFrustum>(null, stream, "Frustum");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestBoundingSphereSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.BoundingSphere()
                {
                    Center = new XnaFrame.Vector3(1, 2, 3),
                    Radius = 4
                };
                var expect = new AnxFrame.BoundingSphere()
                {
                    Center = new AnxFrame.Vector3(1, 2, 3),
                    Radius = 4
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.BoundingSphere>(null, stream, "Sphere");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestColorSerialization(bool compress)
        {
            using(var stream = new MemoryStream())
            {
                var input = Microsoft.Xna.Framework.Color.CornflowerBlue;
                Compiler.WriteAsset(stream, input, compress);

                //System.Diagnostics.Debugger.Launch();
                stream.Position = 0;
                var output = ANX.Framework.Content.ContentReader.ReadAsset<Color>(null, stream, "Color");

                Assert.AreEqual(input.R, output.R);
                Assert.AreEqual(input.G, output.G);
                Assert.AreEqual(input.B, output.B);
                Assert.AreEqual(input.A, output.A);
            }
        }


        [TestCase(false)]
        [TestCase(true)]
        public void TestMatrixSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Matrix()
                {
                    M11 = 11,
                    M12 = 12,
                    M13 = 13,
                    M14 = 14,

                    M21 = 21,
                    M22 = 22,
                    M23 = 23,
                    M24 = 24,

                    M31 = 31,
                    M32 = 32,
                    M33 = 33,
                    M34 = 34,

                    M41 = 41,
                    M42 = 42,
                    M43 = 43,
                    M44 = 44,
                };
                var expect = new AnxFrame.Matrix()
                {
                    M11 = 11,
                    M12 = 12,
                    M13 = 13,
                    M14 = 14,

                    M21 = 21,
                    M22 = 22,
                    M23 = 23,
                    M24 = 24,

                    M31 = 31,
                    M32 = 32,
                    M33 = 33,
                    M34 = 34,

                    M41 = 41,
                    M42 = 42,
                    M43 = 43,
                    M44 = 44,
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Matrix>(null, stream, "Matrix");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestPlaneSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Plane()
                {
                    Normal = new XnaFrame.Vector3(1, 2, 3),
                    D = 4
                };
                var expect = new AnxFrame.Plane()
                {
                    Normal = new AnxFrame.Vector3(1, 2, 3),
                    D = 4
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Plane>(null, stream, "Plane");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestPointSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Point()
                {
                    X = 1,
                    Y = 2
                };
                var expect = new AnxFrame.Point()
                {
                    X = 1,
                    Y = 2
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Point>(null, stream, "Point");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestQuaternionSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Quaternion()
                {
                    X = 1,
                    Y = 2,
                    Z = 3,
                    W = 4
                };
                var expect = new AnxFrame.Quaternion()
                {
                    X = 1,
                    Y = 2,
                    Z = 3,
                    W = 4
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Quaternion>(null, stream, "Quaternion");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestRaySerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Ray()
                {
                    Position = new XnaFrame.Vector3(1, 2, 3),
                    Direction = new XnaFrame.Vector3(4, 5, 6),
                };
                var expect = new AnxFrame.Ray()
                {
                    Position = new AnxFrame.Vector3(1, 2, 3),
                    Direction = new AnxFrame.Vector3(4, 5, 6),
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Ray>(null, stream, "Ray");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestRectangleSerialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Rectangle()
                {
                    X = 1,
                    Y = 2,
                    Width = 3,
                    Height = 4
                };
                var expect = new AnxFrame.Rectangle()
                {
                    X = 1,
                    Y = 2,
                    Width = 3,
                    Height = 4
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Rectangle>(null, stream, "Rectangle");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestVector2Serialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Vector2()
                {
                    X = 1,
                    Y = 2
                };
                var expect = new AnxFrame.Vector2()
                {
                    X = 1,
                    Y = 2
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Vector2>(null, stream, "Vector2");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestVector3Serialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Vector3()
                {
                    X = 1,
                    Y = 2,
                    Z = 3
                };
                var expect = new AnxFrame.Vector3()
                {
                    X = 1,
                    Y = 2,
                    Z = 3
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Vector3>(null, stream, "Vector3");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(false)]
        [TestCase(true)]
        public void TestVector4Serialization(bool compress)
        {
            using (var stream = new MemoryStream())
            {
                var input = new XnaFrame.Vector4()
                {
                    X = 1,
                    Y = 2,
                    Z = 3,
                    W = 4
                };
                var expect = new AnxFrame.Vector4()
                {
                    X = 1,
                    Y = 2,
                    Z = 3,
                    W = 4
                };

                Compiler.WriteAsset(stream, input, compress);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<AnxFrame.Vector4>(null, stream, "Vector4");

                Assert.AreEqual(expect, actual);
            }
        }
    }
}
