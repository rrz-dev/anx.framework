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



// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
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
