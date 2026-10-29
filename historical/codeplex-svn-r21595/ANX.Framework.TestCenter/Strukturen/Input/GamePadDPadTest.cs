#region Using Statements
using System;
using NUnit.Framework;
#endregion // Using Statements

using ANXGamePadDPad = ANX.Framework.Input.GamePadDPad;
using ANXButtons = ANX.Framework.Input.Buttons;
using ANXButtonState = ANX.Framework.Input.ButtonState;
using XNAGamePadDPad = Microsoft.Xna.Framework.Input.GamePadDPad;
using XNAButtons = Microsoft.Xna.Framework.Input.Buttons;
using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class GamePadDPadTest
    {
        #region Test Data
        static object[] Stats16 =
        {
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Released, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Released ,XNAButtonState.Pressed, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Released, ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Released},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Released, ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Released, XNAButtonState.Pressed},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Pressed,  ANXButtonState.Released, XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Released},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Pressed,  ANXButtonState.Pressed,  XNAButtonState.Pressed, XNAButtonState.Pressed ,XNAButtonState.Pressed, XNAButtonState.Pressed},
        };

        static object[] Stats16ANX =
        {
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, (ANXButtons)1 },
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Pressed,  (ANXButtons)8},
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  ANXButtonState.Released, (ANXButtons)4},
            new object[] { ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  ANXButtonState.Pressed,  (ANXButtons)12},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, ANXButtonState.Released, (ANXButtons)2},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, ANXButtonState.Pressed,  (ANXButtons)10},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  ANXButtonState.Released, (ANXButtons)6},
            new object[] { ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  (ANXButtons)14},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Released, (ANXButtons)1},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Released, ANXButtonState.Pressed,  (ANXButtons)9},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Released, (ANXButtons)5},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Released,  ANXButtonState.Pressed,  ANXButtonState.Pressed,  (ANXButtons)13},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Released, ANXButtonState.Released, (ANXButtons)3},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Released, ANXButtonState.Pressed,  (ANXButtons)11},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Pressed,  ANXButtonState.Released, (ANXButtons)7},
            new object[] { ANXButtonState.Pressed,  ANXButtonState.Pressed,   ANXButtonState.Pressed,  ANXButtonState.Pressed,  (ANXButtons)15},
        };
        #endregion

        [TestCaseSource("Stats16")]
        public void Up(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue,
            XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Up.ToString(), anx.Up.ToString());
        }

        [TestCaseSource("Stats16")]
        public void Down(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue,
            XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Down.ToString(), anx.Down.ToString());
        }

        [TestCaseSource("Stats16")]
        public void Left(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue,
            XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Left.ToString(), anx.Left.ToString());
        }

        [TestCaseSource("Stats16")]
        public void Right(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue,
            XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Right.ToString(), anx.Right.ToString());
        }

        [TestCaseSource("Stats16")]
        public void ToString(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue,
            ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2,
            XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }

        [TestCaseSource("Stats16")]
        public void Equal(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue,
            XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            ANXGamePadDPad anx2 = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);

            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);
            XNAGamePadDPad xna2 = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2),"Equal");
        }

        [Test]
        public void Equal2()
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released,
                ANXButtonState.Released);
            ANXGamePadDPad anx2 = new ANXGamePadDPad(ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released,
                ANXButtonState.Pressed);
            //this test is for Codecover
            if (!anx.Equals(anx2))
            {
                Assert.Pass("Pass Equal2");
            }
            else
            {
                Assert.Fail("Fail Equal2");
            }
        }

        [TestCaseSource("Stats16")]
        public void Equal3(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue,
            ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2,
            XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);
 
            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equal3");
        }

        [TestCaseSource("Stats16")]
        public void OperatorNoEqual(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue,
            ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2,
            XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            ANXGamePadDPad anx2 = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);

            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);
            XNAGamePadDPad xna2 = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "OperatorNoEqual");
        }

        [TestCaseSource("Stats16")]
        public void GetHashCode(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue,
            ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2,
            XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }
    }
}
