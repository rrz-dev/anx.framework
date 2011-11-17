#region Using Statements
using System;
using System.Collections.Generic;
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
    public class SystemTypesTests
    {
        public Helper.Compiler Compiler { get; set; }

        [SetUp]
        public void BeforeEach()
        {
            Compiler = new Compiler();
        }

        [Test]
        public void TestArraySerialization()
        {
            using (var stream = new MemoryStream())
            {
                var input = new int[] { 1, 2, 3, 4 };
                var expect = new int[] { 1, 2, 3, 4 };

                Compiler.WriteAsset(stream, input);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<int[]>(null, stream, "Asset");

                Assert.AreEqual(expect, actual);
            }
        }

        [TestCase(DateTimeKind.Unspecified)]
        //[TestCase(DateTimeKind.Local)]
        [TestCase(DateTimeKind.Utc)]
        public void TestDateTimeSerialization(DateTimeKind kind)
        {
            using (var stream = new MemoryStream())
            {
                var input = new DateTime(1234, kind);
                var expect = new DateTime(1234, kind);

                Compiler.WriteAsset(stream, input);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<DateTime>(null, stream, "Asset");

                Assert.AreEqual(expect.Ticks, actual.Ticks);
            }
        }

        [Test]
        public void TestDecimalSerialization()
        {
            foreach (var value in new decimal[] { 
                decimal.MaxValue, decimal.MinValue, decimal.One, decimal.MinusOne, decimal.Zero })
            {
                using (var stream = new MemoryStream())
                {
                    var input = value;
                    var expect = value;

                    Compiler.WriteAsset(stream, input);
                    stream.Position = 0;
                    var actual = AnxFrame.Content.ContentReader.ReadAsset<decimal>(null, stream, "Asset");

                    Assert.AreEqual(expect, actual);
                }
            }
        }

        [Test]
        public void TestDictionarySerialization()
        {
            using (var stream = new MemoryStream())
            {
                var input = new Dictionary<int, int>();
                input.Add(1, 1);
                input.Add(2, 2);
                input.Add(3, 3);

                var expect = new Dictionary<int, int>();
                expect.Add(1, 1);
                expect.Add(2, 2);
                expect.Add(3, 3);

                Compiler.WriteAsset(stream, input);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<Dictionary<int, int>>(null, stream, "Asset");

                Assert.AreEqual(expect, actual);
            }
        }

        [Test]
        public void TestEnumSerialization()
        {
            using (var stream = new MemoryStream())
            {
                var input = XnaFrame.Graphics.SurfaceFormat.Rg32;
                var expect = AnxFrame.Graphics.SurfaceFormat.Rg32;

                Compiler.WriteAsset(stream, (Enum)input);
                stream.Position = 0;
                var actual = (AnxFrame.Graphics.SurfaceFormat)AnxFrame.Content.ContentReader.ReadAsset<Enum>(null, stream, "Asset");

                Assert.AreEqual(expect, actual);
            }
        }

        [Test]
        public void TestExternalReferenceSerialization()
        {
            //TODO: Assert.Ignore("Feature is implemented but is currently untestable");
        }

        [Test]
        public void TestSharedResourceSerialization()
        {
            Compiler.AddTypeWriter(new Helper.ContentWithSharedResourceWriter());
            Compiler.AddTypeWriter(new Helper.SharedResourceWriter());

            using (var stream = new MemoryStream())
            {
                ContentWithSharedResource input = new ContentWithSharedResource();
                input.ResourceA = new ContentWithSharedResource.SharedResource();
                input.ResourceB = input.ResourceA;

                Compiler.WriteAsset(stream, input);

                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<ContentWithSharedResource>(null, stream, "Asset");

                Assert.AreSame(actual.ResourceA, actual.ResourceB);
            }

            using (var stream = new MemoryStream())
            {
                ContentWithSharedResource input = new ContentWithSharedResource();
                input.ResourceA = new ContentWithSharedResource.SharedResource();
                input.ResourceB = new ContentWithSharedResource.SharedResource();

                Compiler.WriteAsset(stream, input);

                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<ContentWithSharedResource>(null, stream, "Asset");

                Assert.AreNotSame(actual.ResourceA, actual.ResourceB);
            }
        }

        [Test]
        public void TestListSerialization()
        {
            using (var stream = new MemoryStream())
            {
                var input = new List<int>(new int[] { 1, 2, 3, 4 });
                var expect = new List<int>(new int[] { 1, 2, 3, 4 });

                Compiler.WriteAsset(stream, input);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<List<int>>(null, stream, "Asset");

                Assert.AreEqual(expect, actual);
            }
        }

        [Test]
        public void TestNullableSerialization()
        {
            using (var stream = new MemoryStream())
            {
                ContentWithNullable input = new ContentWithNullable();

                Compiler.AddTypeWriter(new Helper.ContentWithNullableWriter());
                Compiler.WriteAsset(stream, input);

                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<ContentWithNullable>(null, stream, "Asset");

                Assert.AreEqual(false, actual.NullableValue.HasValue);
            }
        }

        [Test]
        public void TestTimeSpanSerialization()
        {
            using (var stream = new MemoryStream())
            {
                var input = TimeSpan.FromTicks(12345678);
                var expect = TimeSpan.FromTicks(12345678);

                Compiler.WriteAsset(stream, input);
                stream.Position = 0;
                var actual = AnxFrame.Content.ContentReader.ReadAsset<TimeSpan>(null, stream, "Asset");

                Assert.AreEqual(expect, actual);
            }
        }
    }
}
