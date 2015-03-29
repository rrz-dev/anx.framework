using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

using XnaFrame = Microsoft.Xna.Framework;
using AnxFrame = ANX.Framework;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    [TestFixture]
    public class CurveSerializerTests
    {
        [Test]
        public void SerializeCurve()
        {
            XnaFrame.Curve xnaCurve = new XnaFrame.Curve();
            xnaCurve.PreLoop = XnaFrame.CurveLoopType.Cycle;
            xnaCurve.Keys.Add(new XnaFrame.CurveKey(1f, 2f, 4f, -5f, XnaFrame.CurveContinuity.Step));
            xnaCurve.Keys.Add(new XnaFrame.CurveKey(5f, 3f, 10f, -2f, XnaFrame.CurveContinuity.Smooth));

            AnxFrame.Curve anxCurve = new AnxFrame.Curve();
            anxCurve.PreLoop = AnxFrame.CurveLoopType.Cycle;
            anxCurve.Keys.Add(new AnxFrame.CurveKey(1f, 2f, 4f, -5f, AnxFrame.CurveContinuity.Step));
            anxCurve.Keys.Add(new AnxFrame.CurveKey(5f, 3f, 10f, -2f, AnxFrame.CurveContinuity.Smooth));

            StringBuilder anxXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(anxXml))
                new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).SerializeObject(writer, anxCurve, null);

            StringBuilder xnaXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xnaXml))
                XnaFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, xnaCurve, null);

            string xna = xnaXml.ToString();
            string anx = anxXml.ToString();
            Assert.AreEqual(xna, anx);
        }

        [Test]
        public void Deserialize()
        {
            AnxFrame.Curve anxCurve = new AnxFrame.Curve();
            anxCurve.PreLoop = AnxFrame.CurveLoopType.Cycle;
            anxCurve.Keys.Add(new AnxFrame.CurveKey(1f, 2f, 4f, -5f, AnxFrame.CurveContinuity.Step));
            anxCurve.Keys.Add(new AnxFrame.CurveKey(5f, 3f, 10f, -2f, AnxFrame.CurveContinuity.Smooth));

            StringBuilder anxXml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(anxXml))
                new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).SerializeObject(writer, anxCurve, null);

            AnxFrame.Curve resultCurve = null;
            string anxText = anxXml.ToString();
            using (Stream stream = new MemoryStream(Encoding.Unicode.GetBytes(anxText)))
            using (XmlReader reader = XmlReader.Create(stream))
            {
                resultCurve = new AnxFrame.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer(changeToXnaNamespaces: true).DeserializeObject<AnxFrame.Curve>(reader, null);
            }

            Assert.NotNull(resultCurve);
            Assert.AreEqual(anxCurve.PostLoop, resultCurve.PostLoop);
            Assert.AreEqual(anxCurve.PreLoop, resultCurve.PreLoop);
            Assert.AreEqual(anxCurve.Keys.Count, resultCurve.Keys.Count);

            for (int i = 0; i < anxCurve.Keys.Count; i++)
            {
                Assert.AreEqual(anxCurve.Keys[i], resultCurve.Keys[i]);
            }
        }
    }
}
