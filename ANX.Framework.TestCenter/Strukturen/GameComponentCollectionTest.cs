using System;
using NUnit.Framework;

using XNAGameComponentCollection = Microsoft.Xna.Framework.GameComponentCollection;
using ANXGameComponentCollection = ANX.Framework.GameComponentCollection;

using XNAIGameComponent = Microsoft.Xna.Framework.IGameComponent;
using ANXIGameComponent = ANX.Framework.IGameComponent;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen
{
    class GameComponentCollectionTest
    {
        [Test]
        public void Constructor()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            Assert.AreEqual(xna.Count, anx.Count);
        }

        [Test]
        public void SetItemException1()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            TestDelegate xnaDeleg = () => xna[0] = new XNATestComponent();
            TestDelegate anxDeleg = () => anx[0] = new ANXTestComponent();

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(xnaDeleg),
                Assert.Throws<ArgumentOutOfRangeException>(anxDeleg), "SetItemException1");
        }

        [Test]
        public void SetItemException2()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            xna.Add(new XNATestComponent());
            anx.Add(new ANXTestComponent());

            TestDelegate xnaDeleg = delegate { xna[0] = new XNATestComponent(); };
            TestDelegate anxDeleg = delegate { anx[0] = new ANXTestComponent(); };

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "SetItemException2");
        }

        [Test]
        public void Add()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            xna.Add(new XNATestComponent());
            anx.Add(new ANXTestComponent());

            AssertHelper.ConvertEquals(xna.Count, anx.Count, "Add");
        }

        [Test]
        public void EventNotRaisedWhenNullItem()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            bool xnaRaised = false;
            bool anxRaised = false;

            xna.ComponentAdded += delegate { xnaRaised = true; };
            xna.ComponentRemoved += delegate { xnaRaised = true; };
            anx.ComponentAdded += delegate { anxRaised = true; };
            anx.ComponentRemoved += delegate { anxRaised = true; };

            xna.Add(null);
            anx.Add(null);
            xna.RemoveAt(0);
            anx.RemoveAt(0);

            Assert.False(xnaRaised);
            Assert.False(anxRaised);
        }

        [Test]
        public void EventsRaised()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            bool xnaAddRaised = false;
            bool anxAddRaised = false;
            bool xnaRemoveRaised = false;
            bool anxRemoveRaised = false;

            xna.ComponentAdded += delegate { xnaAddRaised = true; };
            xna.ComponentRemoved += delegate { xnaRemoveRaised = true; };
            anx.ComponentAdded += delegate { anxAddRaised = true; };
            anx.ComponentRemoved += delegate { anxRemoveRaised = true; };

            xna.Add(new XNATestComponent());
            anx.Add(new ANXTestComponent());
            xna.RemoveAt(0);
            anx.RemoveAt(0);

            Assert.True(xnaAddRaised);
            Assert.True(xnaRemoveRaised);
            Assert.True(anxAddRaised);
            Assert.True(anxRemoveRaised);
        }

        [Test]
        public void EventsRaisedOnClear()
        {
            var xna = new XNAGameComponentCollection();
            var anx = new ANXGameComponentCollection();

            int xnaRaiseCount = 0;
            int anxRaiseCount = 0;
            xna.ComponentRemoved += delegate { xnaRaiseCount++; };
            anx.ComponentRemoved += delegate { anxRaiseCount++; };

            xna.Add(new XNATestComponent());
            xna.Add(new XNATestComponent());
            xna.Add(new XNATestComponent());

            anx.Add(new ANXTestComponent());
            anx.Add(new ANXTestComponent());
            anx.Add(new ANXTestComponent());

            xna.Clear();
            anx.Clear();

            Assert.AreEqual(xnaRaiseCount, anxRaiseCount);
            Assert.AreEqual(xna.Count, anx.Count);
        }

        class XNATestComponent : XNAIGameComponent
        {
            public void Initialize() { }
        }

        class ANXTestComponent : ANXIGameComponent
        {
            public void Initialize() { }
        }
    }
}
