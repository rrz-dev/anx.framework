using System;
using ANX.Framework.Input.Touch;
using NUnit.Framework;

using XNATouchCollection = Microsoft.Xna.Framework.Input.Touch.TouchCollection;
using ANXTouchCollection = ANX.Framework.Input.Touch.TouchCollection;

using XNATouchLocation = Microsoft.Xna.Framework.Input.Touch.TouchLocation;
using ANXTouchLocation = ANX.Framework.Input.Touch.TouchLocation;

using XNATouchLocationState = Microsoft.Xna.Framework.Input.Touch.TouchLocationState;
using XNAVector2 = Microsoft.Xna.Framework.Vector2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Input.Touch
{
    class TouchCollectionTest
    {
        [Test]
        public void Constructor()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();

            Assert.AreEqual(xna.Count, anx.Count);
            Assert.AreEqual(xna.IsConnected, anx.IsConnected);
            Assert.AreEqual(xna.IsReadOnly, anx.IsReadOnly);
        }

        [Test]
        public void Constructor2()
        {
            var xna = new XNATouchCollection(new[] {new XNATouchLocation(15, XNATouchLocationState.Pressed, XNAVector2.Zero)});
            var anx = new ANXTouchCollection(new[] {new ANXTouchLocation(15, TouchLocationState.Pressed, Vector2.Zero)});

            Assert.AreEqual(xna.Count, anx.Count);
            Assert.AreEqual(xna[0].Id, anx[0].Id);
        }

        [Test]
        public void Constructor3()
        {
            TestDelegate xnaDeleg = () => { new XNATouchCollection(null); };
            TestDelegate anxDeleg = () => { new ANXTouchCollection(null); };

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentNullException>(xnaDeleg),
                Assert.Throws<ArgumentNullException>(anxDeleg), "Constructor3");
        }

        [Test]
        public void Constructor4()
        {
            TestDelegate xnaDeleg = () => { new XNATouchCollection(new XNATouchLocation[15]); };
            TestDelegate anxDeleg = () => { new ANXTouchCollection(new ANXTouchLocation[15]); };

            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(xnaDeleg),
                Assert.Throws<ArgumentOutOfRangeException>(anxDeleg), "Constructor4");
        }

        [Test]
        public void AccessorSet()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();
            TestDelegate xnaDeleg = () => { xna[0] = new XNATouchLocation(); };
            TestDelegate anxDeleg = () => { anx[0] = new ANXTouchLocation(); };

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "AccessorSet");
        }

        [Test]
        public void AccessorGet()
        {
            var xna = new XNATouchCollection(new[] {new XNATouchLocation(15, XNATouchLocationState.Pressed, XNAVector2.Zero)});
            var anx = new ANXTouchCollection(new[] {new ANXTouchLocation(15, TouchLocationState.Pressed, Vector2.Zero)});

            Assert.AreEqual(15, xna[0].Id);
            Assert.AreEqual(15, anx[0].Id);
        }

        [Test]
        public void FindIndexById()
        {
            var xna = new XNATouchCollection(new[] {new XNATouchLocation(15, XNATouchLocationState.Pressed, XNAVector2.Zero)});
            var anx = new ANXTouchCollection(new[] { new ANXTouchLocation(15, TouchLocationState.Pressed, Vector2.Zero) });

            XNATouchLocation xnaResult;
            ANXTouchLocation anxResult;
            Assert.AreEqual(xna.FindById(15, out xnaResult), anx.FindById(15, out anxResult));
            Assert.AreEqual(xnaResult.Id, anxResult.Id);
        }

        [Test]
        public void IndexOf()
        {
            var xnaLocation = new XNATouchLocation(15, XNATouchLocationState.Pressed, XNAVector2.Zero);
            var xna = new XNATouchCollection(new[] { xnaLocation });
            var anxLocation = new ANXTouchLocation(15, TouchLocationState.Pressed, Vector2.Zero);
            var anx = new ANXTouchCollection(new[] { anxLocation });

            Assert.AreEqual(xna.IndexOf(xnaLocation), anx.IndexOf(anxLocation));
        }

        [Test]
        public void Contains()
        {
            var xnaLocation = new XNATouchLocation(15, XNATouchLocationState.Pressed, XNAVector2.Zero);
            var xnaLocation2 = new XNATouchLocation(14, XNATouchLocationState.Released, XNAVector2.Zero);
            var xna = new XNATouchCollection(new[] { xnaLocation });
            var anxLocation = new ANXTouchLocation(15, TouchLocationState.Pressed, Vector2.Zero);
            var anxLocation2 = new ANXTouchLocation(14, TouchLocationState.Released, Vector2.Zero);
            var anx = new ANXTouchCollection(new[] { anxLocation });

            Assert.AreEqual(xna.Contains(xnaLocation), anx.Contains(anxLocation));
            Assert.AreEqual(xna.Contains(default(XNATouchLocation)), anx.Contains(default(ANXTouchLocation)));
            Assert.AreEqual(xna.Contains(xnaLocation2), anx.Contains(anxLocation2));
        }

        [Test]
        public void CopyTo()
        {
            var xnaLocation = new XNATouchLocation(15, XNATouchLocationState.Pressed, XNAVector2.Zero);
            var xnaLocation2 = new XNATouchLocation(14, XNATouchLocationState.Released, XNAVector2.Zero);
            var xna = new XNATouchCollection(new[] { xnaLocation, xnaLocation2 });
            var anxLocation = new ANXTouchLocation(15, TouchLocationState.Pressed, Vector2.Zero);
            var anxLocation2 = new ANXTouchLocation(14, TouchLocationState.Released, Vector2.Zero);
            var anx = new ANXTouchCollection(new[] { anxLocation, anxLocation2 });

            var xnaResult = new XNATouchLocation[2];
            xna.CopyTo(xnaResult, 0);
            var anxResult = new ANXTouchLocation[2];
            anx.CopyTo(anxResult, 0);

            Assert.AreEqual(xnaResult[0], xnaLocation);
            Assert.AreEqual(xnaResult[1], xnaLocation2);

            Assert.AreEqual(anxResult[0], anxLocation);
            Assert.AreEqual(anxResult[1], anxLocation2);
        }

        [Test]
        public void Insert()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();
            TestDelegate xnaDeleg = () => xna.Insert(0, default(XNATouchLocation));
            TestDelegate anxDeleg = () => anx.Insert(0, default(ANXTouchLocation));

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "Insert");
        }

        [Test]
        public void RemoveAt()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();
            TestDelegate xnaDeleg = () => xna.RemoveAt(0);
            TestDelegate anxDeleg = () => anx.RemoveAt(0);

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "RemoveAt");
        }

        [Test]
        public void Add()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();
            TestDelegate xnaDeleg = () => xna.Add(default(XNATouchLocation));
            TestDelegate anxDeleg = () => anx.Add(default(ANXTouchLocation));

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "Add");
        }

        [Test]
        public void Clear()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();
            TestDelegate xnaDeleg = xna.Clear;
            TestDelegate anxDeleg = anx.Clear;

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "Clear");
        }

        [Test]
        public void Remove()
        {
            var xna = new XNATouchCollection();
            var anx = new ANXTouchCollection();
            TestDelegate xnaDeleg = () => xna.Remove(default(XNATouchLocation));
            TestDelegate anxDeleg = () => anx.Remove(default(ANXTouchLocation));

            AssertHelper.ConvertEquals(Assert.Throws<NotSupportedException>(xnaDeleg),
                Assert.Throws<NotSupportedException>(anxDeleg), "Remove");
        }
    }
}
