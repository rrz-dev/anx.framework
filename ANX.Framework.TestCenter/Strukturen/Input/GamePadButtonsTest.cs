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