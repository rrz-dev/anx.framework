using ANX.Framework.Content.Pipeline.Serialization.Intermediate;
using ANX.Framework.NonXNA.Development;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    //Not a real test, just wanted to see how it looks.
    [Developer("KorsarNek")]
    public class InterfaceSerializerTests
    {
        interface A
        {
            int Count { get; set; }
        }

        interface B : A
        {
            bool Ready { get; set; }
        }

        class BSerializer : ContentInterfaceSerializer<B>
        {
            protected override void Serialize(IntermediateWriter output, B value, Content.ContentSerializerAttribute format)
            {
                output.Xml.WritePart(value.Ready);
                output.Xml.WritePart(value.Count);
            }

            protected override B Deserialize(IntermediateReader input, Content.ContentSerializerAttribute format, B existingInstance)
            {
                existingInstance.Ready = input.Xml.ReadBooleanPart();
                existingInstance.Count = input.Xml.ReadInt32Part();
                return existingInstance;
            }
        }

        class ASerializer : ContentInterfaceSerializer<A>
        {
            protected override void Serialize(IntermediateWriter output, A value, Content.ContentSerializerAttribute format)
            {
                output.Xml.WritePart(value.Count);
            }

            protected override A Deserialize(IntermediateReader input, Content.ContentSerializerAttribute format, A existingInstance)
            {
                existingInstance.Count = input.Xml.ReadInt32Part();
                return existingInstance;
            }
        }

        abstract class TestBaseClass : A
        {
            public abstract int Count
            {
                get;
                set;
            }
        }

        class TestClass : TestBaseClass, B, A
        {

            public bool Ready
            {
                get;
                set;
            }

            public override int Count
            {
                get;
                set;
            }

            public string Name
            {
                get;
                set;
            }

            int A.Count
            {
                get { return 3; }
                set { }
            }
        }

        [Test]
        public void MultipleInterfaceSerializers()
        {
            var serializer = new IntermediateSerializer(searchSerializers: false);
            serializer.AddTypeSerializer(typeof(ASerializer));
            serializer.AddTypeSerializer(typeof(BSerializer));
            
            var obj = new TestClass();
            obj.Count = 4;
            obj.Name = "Test";

            StringBuilder text = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(text))
                serializer.SerializeObject(writer, obj, null);

            string result = text.ToString();
        }
    }
}
