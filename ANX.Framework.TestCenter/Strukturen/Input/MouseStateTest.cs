#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNAMouseState = Microsoft.Xna.Framework.Input.MouseState;
using ANXMouseState = ANX.Framework.Input.MouseState;

using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using ANXButtonState = ANX.Framework.Input.ButtonState;



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
namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class MouseStateTest
    {
        #region TestCase
        static object[] case1 =
        {
           new int[]{DataFactory.RandomIntValueMinMax(0,int.MaxValue),DataFactory.RandomIntValueMinMax(0,int.MaxValue),DataFactory.RandomIntValueMinMax(int.MinValue,int.MaxValue),DataFactory.RandomIntValueMinMax(0,2),DataFactory.RandomIntValueMinMax(0,2),DataFactory.RandomIntValueMinMax(0,2),DataFactory.RandomIntValueMinMax(0,2),DataFactory.RandomIntValueMinMax(0,2)},
        };
        #endregion


        [TestCaseSource("case1")]
        public void LeftButton(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna= new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.LeftButton == XNAButtonState.Pressed, anx.LeftButton == ANXButtonState.Pressed, "LeftButton");
        }

        [TestCaseSource("case1")]
        public void MiddleButton(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.MiddleButton == XNAButtonState.Pressed, anx.MiddleButton == ANXButtonState.Pressed, "MiddleButton");
        }
 
        [TestCaseSource("case1")]
        public void RightButton(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.RightButton == XNAButtonState.Pressed, anx.RightButton == ANXButtonState.Pressed, "RightButton");
        }

        [TestCaseSource("case1")]
        public void XButton1(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.XButton1 == XNAButtonState.Pressed, anx.XButton1 == ANXButtonState.Pressed, "XButton1");
        }

        [TestCaseSource("case1")]
        public void XButton2(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.XButton2 == XNAButtonState.Pressed, anx.XButton2 == ANXButtonState.Pressed, "XButton2");
        }

        [TestCaseSource("case1")]
        public void ScrollWheelValue(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.ScrollWheelValue, anx.ScrollWheelValue, "ScrollWheelValue");
        }

        [TestCaseSource("case1")]
        public void X(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.X, anx.X, "X");
        }

        [TestCaseSource("case1")]
        public void Y(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.Y, anx.Y, "Y");
        }

        [TestCaseSource("case1")]
        public void OpEqual(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);
            ANXMouseState anx2 = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna2 = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "OpEqual");
        }

        [TestCaseSource("case1")]
        public void Equals(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);
            ANXMouseState anx2 = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna2 = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals");
        }

        [TestCaseSource("case1")]
        public void Equals2(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);
            ANXMouseState anx2 = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna2 = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals2");
        }

        [TestCaseSource("case1")]
        public void OpUnEqual(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);
            ANXMouseState anx2 = new ANXMouseState(input[1], input[0], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna2 = new XNAMouseState(input[1], input[0], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "OpUnEqual");
        }

        [TestCaseSource("case1")]
        public void ToString(int[] input)
        {
            ANXMouseState anx = new ANXMouseState(input[0], input[1], input[2], (ANXButtonState)input[3], (ANXButtonState)input[4], (ANXButtonState)input[5], (ANXButtonState)input[6], (ANXButtonState)input[7]);
            XNAMouseState xna = new XNAMouseState(input[0], input[1], input[2], (XNAButtonState)input[3], (XNAButtonState)input[4], (XNAButtonState)input[5], (XNAButtonState)input[6], (XNAButtonState)input[7]);
 
            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }
    }
}
