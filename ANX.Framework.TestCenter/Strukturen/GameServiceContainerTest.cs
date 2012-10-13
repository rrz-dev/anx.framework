using System;
using NUnit.Framework;

using XNAGameServiceContainer = Microsoft.Xna.Framework.GameServiceContainer;
using ANXGameServiceContainer = ANX.Framework.GameServiceContainer;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    public class GameServiceContainerTest
    {
        [Test]
        public void GetServiceWithoutRegisteredService()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            Assert.Null(xna.GetService(typeof(int)));
            Assert.Null(anx.GetService(typeof(int)));
            Assert.AreEqual(xna.GetService(typeof(int)), anx.GetService(typeof(int)));
        }

        [Test]
        public void GetServiceWithNullType()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(() => xna.GetService(null)),
                Assert.Throws<ArgumentNullException>(() => anx.GetService(null)), "GetServiceWithNullType");
        }

        [Test]
        public void AddServiceWithNullType()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(() => xna.AddService(null, null)),
                Assert.Throws<ArgumentNullException>(() => anx.AddService(null, null)), "AddServiceWithNullType");
        }

        [Test]
        public void AddServiceWithNullProvider()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(() => xna.AddService(typeof(int), null)),
                Assert.Throws<ArgumentNullException>(() => anx.AddService(typeof(int), null)), "AddServiceWithNullProvider");
        }

        [Test]
        public void RemoveServiceWithNullType()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(() => xna.RemoveService(null)),
                Assert.Throws<ArgumentNullException>(() => anx.RemoveService(null)), "RemoveServiceWithNullType");
        }

        [Test]
        public void RemoveServiceWithoutRegisteredService()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            Assert.DoesNotThrow(() => xna.RemoveService(typeof(int)));
            Assert.DoesNotThrow(() => anx.RemoveService(typeof(int)));
        }

        [Test]
        public void AddAndGetService()
        {
            var xna = new XNAGameServiceContainer();
            var anx = new ANXGameServiceContainer();

            xna.AddService(typeof(int), 15);
            anx.AddService(typeof(int), 15);

            Assert.AreEqual(xna.GetService(typeof(int)), anx.GetService(typeof(int)));
        }
    }
}
