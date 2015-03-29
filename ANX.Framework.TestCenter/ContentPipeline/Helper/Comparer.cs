using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using XnaFrame = Microsoft.Xna.Framework;
using AnxFrame = ANX.Framework;

namespace ANX.Framework.TestCenter.ContentPipeline.Helper
{
    class Comparer
    {
        public static void CompareSerialization<T>(T item)
        {
            StringBuilder anxXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(anxXml))
                new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).SerializeObject(writer, item, null);

            StringBuilder xnaXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xnaXml))
                XnaFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, item, null);

            string xna = xnaXml.ToString();
            string anx = anxXml.ToString();
            Assert.AreEqual(xna, anx);
        }
    }
}
