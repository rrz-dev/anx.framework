#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNAGamePadButtons = Microsoft.Xna.Framework.Input.GamePadButtons;
using ANXGamePadButtons = ANX.Framework.Input.GamePadButtons;

using XNAGamePadThumbSticks = Microsoft.Xna.Framework.Input.GamePadThumbSticks;
using ANXGamePadThumbSticks = ANX.Framework.Input.GamePadThumbSticks;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;


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
    class GamePadThumbSticksTest
    {
        #region TestCase
        static object[] twofloats =
        {
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[]{-2,-2,-2,-2}
        };
        #endregion
        [TestCaseSource("twofloats")]
        public void Left(float leftX, float leftY,float rightX,float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX,leftY),new ANXVector2(rightX,rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX,leftY),new XNAVector2(rightX,rightY));
            AssertHelper.ConvertEquals(xna.Left, anx.Left, "Left");
        }
        [TestCaseSource("twofloats")]
        public void Right(float leftX, float leftY, float rightX, float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
            AssertHelper.ConvertEquals(xna.Right, anx.Right, "Right");
        }
        [TestCaseSource("twofloats")]
        public void ToString(float leftX, float leftY,float rightX,float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX,leftY),new ANXVector2(rightX,rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX,leftY),new XNAVector2(rightX,rightY));
            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }
        [TestCaseSource("twofloats")]
        public void Equals(float leftX, float leftY,float rightX,float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX,leftY),new ANXVector2(rightX,rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX,leftY),new XNAVector2(rightX,rightY));
            ANXGamePadThumbSticks anx2 = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna2 = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "Equals");
        }
        [TestCaseSource("twofloats")]
        public void Equals2(float leftX, float leftY,float rightX,float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX,leftY),new ANXVector2(rightX,rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX,leftY),new XNAVector2(rightX,rightY));
            ANXGamePadThumbSticks anx2 = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna2 = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals2");
        }
        [TestCaseSource("twofloats")]
        public void Equals3(float leftX, float leftY, float rightX, float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals3");
        }
        [TestCaseSource("twofloats")]
        public void NotEquals(float leftX, float leftY,float rightX,float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX,leftY),new ANXVector2(rightX,rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX,leftY),new XNAVector2(rightX,rightY));
            while (leftX == leftY)
            {
                leftX = DataFactory.RandomFloat;
            }
            ANXGamePadThumbSticks anx2 = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna2 = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "NotEquals");
        }

    }
}
