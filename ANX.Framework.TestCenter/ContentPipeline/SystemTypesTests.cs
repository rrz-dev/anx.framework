#region Using Statements
using System;
using System.Collections.Generic;
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
