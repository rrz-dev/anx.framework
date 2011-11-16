#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using ANXGamePadDPad = ANX.Framework.Input.GamePadDPad;
using ANXButtons = ANX.Framework.Input.Buttons;
using ANXButtonState = ANX.Framework.Input.ButtonState;
using XNAGamePadDPad = Microsoft.Xna.Framework.Input.GamePadDPad;
using XNAButtons = Microsoft.Xna.Framework.Input.Buttons;
using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;


#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.TestCenter.Input
{
    [TestFixture]
    class GamePadDPadTest
    {
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

        [TestCaseSource("Stats16")]
        public void Up(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Up.ToString(), anx.Up.ToString());
        }
        [TestCaseSource("Stats16")]
        public void Down(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Down.ToString(), anx.Down.ToString());
        }
        [TestCaseSource("Stats16")]
        public void Left(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Left.ToString(), anx.Left.ToString());
        }
        [TestCaseSource("Stats16")]
        public void Right(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            Assert.AreEqual(xna.Right.ToString(), anx.Right.ToString());
        }
        [TestCaseSource("Stats16")]
        public void ToString(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            XNAGamePadDPad xna = new XNAGamePadDPad(upValue2, downValue2, leftValue2, rightValue2);

            AssertHelper.CompareString(xna.ToString(), anx.ToString(), "ToString");


        }
        [TestCaseSource("Stats16")]
        public void Equal(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            ANXGamePadDPad anx2 = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);

            if (anx.Equals(anx2))
            {
                Assert.Pass("Pass Equal");
            }
            else
            {
                Assert.Fail("Fail Equal");
            }
        }
        [Test]
        public void Equal2()
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released);
            ANXGamePadDPad anx2 = new ANXGamePadDPad(ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Released, ANXButtonState.Pressed);
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
        public void OperatorNoEqual(ANXButtonState upValue, ANXButtonState downValue, ANXButtonState leftValue, ANXButtonState rightValue, XNAButtonState upValue2, XNAButtonState downValue2, XNAButtonState leftValue2, XNAButtonState rightValue2)
        {
            ANXGamePadDPad anx = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);
            ANXGamePadDPad anx2 = new ANXGamePadDPad(upValue, downValue, leftValue, rightValue);

            if (!(anx != anx2))
            {
                Assert.Pass("Pass !=");
            }
            else
            {
                Assert.Fail("Fail !=");
            }
        }
    }
}
