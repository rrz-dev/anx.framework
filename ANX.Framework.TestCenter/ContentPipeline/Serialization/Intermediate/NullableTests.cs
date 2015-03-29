using ANX.Framework.TestCenter.ContentPipeline.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    class NullableTests
    {
        [Test]
        public void NullableList()
        {
            List<int?> list = new List<int?>();
            list.Add(1);
            list.Add(null);
            list.Add(3);

            Comparer.CompareSerialization(list);
        }
    }
}
