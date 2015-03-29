using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using XnaFrame = Microsoft.Xna.Framework;
using AnxFrame = ANX.Framework;
using ANX.Framework.TestCenter.ContentPipeline.Helper;

namespace ANX.Framework.TestCenter.ContentPipeline.Serialization.Intermediate
{
    [TestFixture]
     public class CollectionSerializerTests
     {
         class testList<T> : List<T>
         {
             public int customProperty
             {
                 get;
                 set;
             }
         }

         struct testStruct
         {
             public int field1;
             public String field2;
         }

         [Test]
         public void ByteList()
         {
             List<byte> list = new List<byte>();
             list.Add(2);
             list.Add(10);

             Comparer.CompareSerialization(list);
         }

         [Test]
         public void CustomList()
         {
             testList<byte> list = new testList<byte>();
             list.Add(2);
             list.Add(10);

             list.customProperty = 50;

             Comparer.CompareSerialization(list);
         }

         [Test]
         public void StringList()
         {
             List<string> list = new List<string>();
             list.Add("TestString");
             list.Add("TestString 2");

             Comparer.CompareSerialization(list);
         }

         [Test]
         public void IntArray()
         {
             int[] items = new [] { 10, -44 };

             Comparer.CompareSerialization(items);
         }

        public void NullableIntArray()
         {
             int?[] items = new int?[] { 10, 22, null, 14 };

             Comparer.CompareSerialization(items);
         }

         public void NullableIntList()
         {
             List<int?> list = new List<int?>();
             list.Add(10);
             list.Add(null);

             Comparer.CompareSerialization(list);
         }

         [Test]
         public void StringArray()
         {
             string[] items = new[] { "Test", "Test 2" };

             Comparer.CompareSerialization(items);
         }

         [Test]
         public void PolymorphicList()
         {
             List<ValueType> items = new List<ValueType>();

             items.Add(32);
             items.Add(44.4f);
             items.Add(new testStruct() { field1 = 11, field2 = "test" });

             Comparer.CompareSerialization(items);
         }

         [Test]
         public void Dictionaries()
         {
             Dictionary<string, int> items = new Dictionary<string, int>();

             items.Add("Test", 10);
             items.Add("Test 2", -44);

             Comparer.CompareSerialization(items);
         }
     }
}
