using ANX.Framework.NonXNA.Development;
using ANX.Framework.TestCenter.ContentPipeline.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    class NestedClassTests
    {
        public class GenericClass<T>
        {
            public class NestedClass<B>
            {
                public NestedClass()
                {

                }
            }
        }

        [Test]
        public void NestedGenericClass()
        {
            var nestedClass = new NestedClassTests.GenericClass<int>.NestedClass<float>();

            Comparer.CompareSerialization(nestedClass);
        }
    }
}
