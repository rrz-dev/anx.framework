using System;
using System.Collections.Generic;
using NUnit.Framework;

using XNANetworkSessionProperties = Microsoft.Xna.Framework.Net.NetworkSessionProperties;
using ANXNetworkSessionProperties = ANX.Framework.Net.NetworkSessionProperties;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Net
{
    class NetworkSessionPropertiesTest
    {
        [Test]
        public void Constructor()
        {
            var xna = new XNANetworkSessionProperties();
            var anx = new ANXNetworkSessionProperties();

            Assert.AreEqual(xna.Count, anx.Count);
        }

        [Test]
        public void IndexAccessOutOfRange1()
        {
            var xna = new XNANetworkSessionProperties();
            var anx = new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = delegate { xna[15] = null; };
            TestDelegate anxDeleg = delegate { anx[15] = null; };

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(xnaDeleg),
                Assert.Throws<ArgumentOutOfRangeException>(anxDeleg), "IndexAccessOutOfRange");
        }

        [Test]
        public void IndexAccessOutOfRange2()
        {
            var xna = new XNANetworkSessionProperties();
            var anx = new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = delegate { xna[-4] = null; };
            TestDelegate anxDeleg = delegate { anx[-4] = null; };

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(xnaDeleg),
                Assert.Throws<ArgumentOutOfRangeException>(anxDeleg), "IndexAccessOutOfRange");
        }

        [Test]
        public void InitialValues()
        {
            var xna = new XNANetworkSessionProperties();
            var anx = new ANXNetworkSessionProperties();

            for (int index = 0; index < xna.Count; index++)
                Assert.AreEqual(xna[index], anx[index]);
        }

        [Test]
        public void IsReadOnly()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            AssertHelper.ConvertEquals(xna.IsReadOnly, anx.IsReadOnly, "IsReadOnly");
        }

        [Test]
        public void AddNotSupported()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = () => xna.Add(null);
            TestDelegate anxDeleg = () => anx.Add(null);

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "AddNotSupported");
        }

        [Test]
        public void RemoveNotSupported()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = () => xna.Remove(null);
            TestDelegate anxDeleg = () => anx.Remove(null);

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "RemoveNotSupported");
        }

        [Test]
        public void RemoveAtNotSupported()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = () => xna.RemoveAt(1);
            TestDelegate anxDeleg = () => anx.RemoveAt(1);

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "RemoveAtNotSupported");
        }

        [Test]
        public void ClearNotSupported()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = xna.Clear;
            TestDelegate anxDeleg = anx.Clear;

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "ClearNotSupported");
        }

        [Test]
        public void InsertNotSupported()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            TestDelegate xnaDeleg = () => xna.Insert(1, null);
            TestDelegate anxDeleg = () => anx.Insert(1, null);

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "InsertNotSupported");
        }

        [Test]
        public void IndexOf()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            xna[6] = 15;
            anx[6] = 15;

            AssertHelper.ConvertEquals(xna.IndexOf(15), anx.IndexOf(15), "IndexOf");
        }

        [Test]
        public void Contains()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            xna[6] = 15;
            anx[6] = 15;

            AssertHelper.ConvertEquals(xna.Contains(15), anx.Contains(15), "Contains");
        }

        [Test]
        public void CopyTo()
        {
            var xna = (IList<int?>)new XNANetworkSessionProperties();
            var anx = (IList<int?>)new ANXNetworkSessionProperties();

            xna[6] = 15;
            anx[6] = 15;

            var xnaData = new int?[8];
            var anxData = new int?[8];

            xna.CopyTo(xnaData, 0);
            anx.CopyTo(anxData, 0);

            for (int index = 0; index < xnaData.Length; index++)
                Assert.AreEqual(xnaData[index], anxData[index]);

            Assert.AreEqual(xnaData[6], 15);
            Assert.AreEqual(anxData[6], 15);
        }
    }
}
