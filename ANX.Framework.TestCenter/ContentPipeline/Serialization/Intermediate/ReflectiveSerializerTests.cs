using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using XnaFrame = Microsoft.Xna.Framework;
using AnxFrame = ANX.Framework;
using System.IO;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    [TestFixture]
    public class ReflectiveSerializerTests
    {
        #region Helper classes
        private class TestClass
        {
            [XnaFrame.Content.ContentSerializer]
            [AnxFrame.Content.ContentSerializer]
            int field = 2;

            [XnaFrame.Content.ContentSerializer]
            [AnxFrame.Content.ContentSerializer]
            byte field2 = 3;

            [XnaFrame.Content.ContentSerializer]
            [AnxFrame.Content.ContentSerializer]
            float? field3 = null;

            [XnaFrame.Content.ContentSerializer]
            [AnxFrame.Content.ContentSerializer]
            decimal? field4 = 10;

            [XnaFrame.Content.ContentSerializer(Optional = true)]
            [AnxFrame.Content.ContentSerializer(Optional = true)]
            object field5 = null;
        }

        class WrappedList
        {
            List<String> stringItems = new List<string>();
            List<int> intItems = new List<int>();

            public int Property1
            {
                get;
                set;
            }

            [AnxFrame.Content.ContentSerializer(FlattenContent = true, CollectionItemName = "IntItem")]
            [XnaFrame.Content.ContentSerializer(FlattenContent = true, CollectionItemName = "IntItem")]
            public List<int> Property2
            {
                get { return this.intItems; }
            }

            public float Property3
            {
                get;
                set;
            }

            [AnxFrame.Content.ContentSerializer(FlattenContent = true, CollectionItemName = "StringItem")]
            [XnaFrame.Content.ContentSerializer(FlattenContent = true, CollectionItemName = "StringItem")]
            public List<string> Property4
            {
                get { return this.stringItems; }
            }
        }

        class SharedContentClass
        {
            //Used to generate absolutely exact XML. For that we have to have the same class name, but the content must have different types to make sure the handling for
            //XNA classes is the same as for the ANX classes with the own serializer.
            #region ANX

            [XnaFrame.Content.ContentSerializerIgnore()]
            [AnxFrame.Content.ContentSerializer(SharedResource = true, ElementName = "shared")]
            public AnxFrame.Content.Pipeline.ContentIdentity sharedAnx;

            [XnaFrame.Content.ContentSerializerIgnore()]
            [AnxFrame.Content.ContentSerializer(ElementName = "Texture")]
            public AnxFrame.Content.Pipeline.ExternalReference<AnxFrame.Content.Pipeline.Graphics.TextureContent> TextureAnx
            {
                get;
                set;
            }

            #endregion

            #region XNA

            [AnxFrame.Content.ContentSerializerIgnore()]
            [XnaFrame.Content.ContentSerializer(SharedResource = true, ElementName = "shared")]
            public XnaFrame.Content.Pipeline.ContentIdentity sharedXna;

            [AnxFrame.Content.ContentSerializerIgnore()]
            [XnaFrame.Content.ContentSerializer(ElementName = "Texture")]
            public XnaFrame.Content.Pipeline.ExternalReference<XnaFrame.Content.Pipeline.Graphics.TextureContent> TextureXna
            {
                get;
                set;
            }

            #endregion
        }

        #endregion

        #region Tests

        [Test]
        public void ReflectiveSerializer()
        {
            StringBuilder anxXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(anxXml))
                new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).SerializeObject(writer, new TestClass(), null);

            StringBuilder xnaXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xnaXml))
                XnaFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, new TestClass(), null);
            
            string xna = xnaXml.ToString();
            string anx = anxXml.ToString();
            Assert.AreEqual(xna, anx);
        }

        [Test]
        public void FlattenContentSerialize()
        {
            var list = new WrappedList();
            list.Property1 = 4;
            list.Property4.Add("Test");
            list.Property4.Add("Test 2");
            list.Property3 = 2.5f;
            list.Property2.Add(2);
            list.Property2.Add(44);

            StringBuilder anxXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(anxXml))
                new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).SerializeObject(writer, list, null);

            StringBuilder xnaXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xnaXml))
                XnaFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, list, null);

            string xna = xnaXml.ToString();
            string anx = anxXml.ToString();
            Assert.AreEqual(xna, anx);
        }

        [Test]
        public void FlattenContentDeserialize()
        {
            var list = new WrappedList();
            list.Property1 = 4;
            list.Property4.Add("Test");
            list.Property4.Add("Test 2");
            list.Property3 = 2.5f;
            list.Property2.Add(2);
            list.Property2.Add(44);

            StringBuilder anxXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(anxXml))
                new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).SerializeObject(writer, list, null);

            //And now deserialize
            WrappedList result = null;
            using (Stream stream = new MemoryStream(Encoding.Unicode.GetBytes(anxXml.ToString())))
            using (XmlReader reader = XmlReader.Create(stream))
            {
                result = new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).DeserializeObject<WrappedList>(reader, null);
            }

            Assert.NotNull(result);
            Assert.AreEqual(list.Property1, result.Property1);
            Assert.AreEqual(list.Property3, result.Property3);

            Assert.AreEqual(list.Property2.Count, result.Property2.Count);
            for (int i = 0; i < list.Property2.Count; i++)
            {
                Assert.AreEqual(list.Property2[i], result.Property2[i]);
            }

            Assert.AreEqual(list.Property4.Count, result.Property4.Count);
            for (int i = 0; i < list.Property4.Count; i++)
            {
                Assert.AreEqual(list.Property4[i], result.Property4[i]);
            }
        }

        #endregion
    }
}
