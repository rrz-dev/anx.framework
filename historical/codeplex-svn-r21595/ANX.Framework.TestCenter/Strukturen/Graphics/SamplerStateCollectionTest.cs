using System;
using System.Globalization;
using System.Reflection;
using ANX.Framework.Graphics;
using NUnit.Framework;

using XNASamplerStateCollection = Microsoft.Xna.Framework.Graphics.SamplerStateCollection;
using XNASamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Graphics
{
    class SamplerStateCollectionTest
    {
        [Test]
        public void Constructor()
        {
            XNASamplerStateCollection xna;
            SamplerStateCollection anx;
            CreateCollections(out xna, out anx);

            Assert.Null(xna[0]);
            Assert.Null(anx[0]);
        }

        [Test]
        public void AccessorOutOfRangeLowerBound()
        {
            XNASamplerStateCollection xna;
            SamplerStateCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaOutOfRangeDeleg = () => xna[-1] = null;
            TestDelegate anxOutOfRangeDeleg = () => anx[-1] = null;

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(xnaOutOfRangeDeleg),
                Assert.Throws<ArgumentOutOfRangeException>(anxOutOfRangeDeleg), "AccessorOutOfRangeLowerBound");
        }

        [Test]
        public void AccessorOutOfRangeUpperBound()
        {
            XNASamplerStateCollection xna;
            SamplerStateCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaOutOfRangeDeleg = () => xna[20] = null;
            TestDelegate anxOutOfRangeDeleg = () => anx[20] = null;

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(xnaOutOfRangeDeleg),
                Assert.Throws<ArgumentOutOfRangeException>(anxOutOfRangeDeleg), "AccessorOutOfRangeUpperBound");
        }

        [Test]
        public void AccessorNullException()
        {
            XNASamplerStateCollection xna;
            SamplerStateCollection anx;
            CreateCollections(out xna, out anx);

            TestDelegate xnaOutOfRangeDeleg = () => xna[2] = null;
            TestDelegate anxOutOfRangeDeleg = () => anx[2] = null;

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(xnaOutOfRangeDeleg),
                Assert.Throws<ArgumentNullException>(anxOutOfRangeDeleg), "AccessorNullException");
        }

        private void CreateCollections(out XNASamplerStateCollection xna, out SamplerStateCollection anx)
        {
            var xnaParameters = new object[] { null, 0, 8 };
            xna = (XNASamplerStateCollection)Activator.CreateInstance(typeof(XNASamplerStateCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, xnaParameters, CultureInfo.InvariantCulture);

            var anxParameters = new object[] { null, 8 };
            anx = (SamplerStateCollection)Activator.CreateInstance(typeof(SamplerStateCollection),
                BindingFlags.NonPublic | BindingFlags.Instance, null, anxParameters, CultureInfo.InvariantCulture);
        }
    }
}
