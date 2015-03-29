using ANX.Framework.Content.Pipeline.Serialization.Intermediate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    [TestFixture]
    class RecursionTest
    {
        class RecursiveClass
        {
            public RecursiveClass obj;
        }

        [Test]
        public void RecursiveSerialize()
        {
            var recursive = new RecursiveClass();
            recursive.obj = recursive;

            StringBuilder text = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(text))
                Assert.Throws<InvalidOperationException>(() => IntermediateSerializer.Serialize(writer, recursive, null));
        }
    }
}
