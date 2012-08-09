#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNAGamePadButtons = Microsoft.Xna.Framework.Input.GamePadButtons;
using ANXGamePadButtons = ANX.Framework.Input.GamePadButtons;

using XNAButtons = Microsoft.Xna.Framework.Input.Buttons;
using ANXButtons = ANX.Framework.Input.Buttons;

using XNAButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using ANXButtonState = ANX.Framework.Input.ButtonState;


// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class GamePadButtonsTest
    {
        #region TestCase
        static object[] buttonSample =
        {
            0,
            DataFactory.RandomIntValueMinMax(0,32769),
            DataFactory.RandomIntValueMinMax(0,32769),
            DataFactory.RandomIntValueMinMax(0,32769),
            DataFactory.RandomIntValueMinMax(0,32769),                    
            DataFactory.RandomIntValueMinMax(0,32769),
            DataFactory.RandomIntValueMinMax(0,32769),
            DataFactory.RandomIntValueMinMax(0,32769),
            32768
        };

        #endregion

        [TestCaseSource("buttonSample")]
        public void A(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.A, anx.A, "A");
        }

        [TestCaseSource("buttonSample")]
        public void B(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.B, anx.B, "B");
        }

        [TestCaseSource("buttonSample")]
        public void X(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.X, anx.X, "X");
        }

        [TestCaseSource("buttonSample")]
        public void Y(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.Y, anx.Y, "Y");
        }

        [TestCaseSource("buttonSample")]
        public void Back(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.Back, anx.Back, "BAck");
        }

        [TestCaseSource("buttonSample")]
        public void BigButton(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.BigButton, anx.BigButton, "Bigbutton");
        }

        [TestCaseSource("buttonSample")]
        public void LeftShoulder(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.LeftShoulder, anx.LeftShoulder, "LeftShoulder");
        }

        [TestCaseSource("buttonSample")]
        public void LeftStick(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.LeftStick, anx.LeftStick, "LeftStick");
        }

        [TestCaseSource("buttonSample")]
        public void RightShoulder(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.RightShoulder, anx.RightShoulder, "RightShoulder");
        }

        [TestCaseSource("buttonSample")]
        public void RightStick(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.RightStick, anx.RightStick, "RightStick");
        }

        [TestCaseSource("buttonSample")]
        public void Start(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.Start, anx.Start, "Start");
        }

        [TestCaseSource("buttonSample")]
        public void ToString(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "Start");
        }

        //[TestCaseSource("buttonSample")]
        //public void GetHashCode(int buttons)
        //{
        //    XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
        //    ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);

        //    AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        //}

        [TestCaseSource("buttonSample")]
        public void Equals(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);
            XNAGamePadButtons xna2 = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx2 = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals");
        }

        [TestCaseSource("buttonSample")]
        public void Equals2(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);
 
            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals2");
        }
        
        [TestCaseSource("buttonSample")]
        public void op_Equality(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);
            XNAGamePadButtons xna2 = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx2 = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna==xna2, anx==anx2, "op_Equality");
        }
 
        [TestCaseSource("buttonSample")]
        public void op_Inequality(int buttons)
        {
            XNAGamePadButtons xna = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx = new ANXGamePadButtons((ANXButtons)buttons);
            XNAGamePadButtons xna2 = new XNAGamePadButtons((XNAButtons)buttons);
            ANXGamePadButtons anx2 = new ANXGamePadButtons((ANXButtons)buttons);

            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "op_Equality");
        }
    }
}