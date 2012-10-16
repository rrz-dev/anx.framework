#region Using Statements
using System;
using NUnit.Framework;

#endregion // Using Statements

using XNAGamePadButtons = Microsoft.Xna.Framework.Input.GamePadButtons;
using ANXGamePadButtons = ANX.Framework.Input.GamePadButtons;

using XNAGamePadThumbSticks = Microsoft.Xna.Framework.Input.GamePadThumbSticks;
using ANXGamePadThumbSticks = ANX.Framework.Input.GamePadThumbSticks;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
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

        [TestCaseSource("twofloats")]
        public void GetHashCode(float leftX, float leftY, float rightX, float rightY)
        {
            ANXGamePadThumbSticks anx = new ANXGamePadThumbSticks(new ANXVector2(leftX, leftY), new ANXVector2(rightX, rightY));
            XNAGamePadThumbSticks xna = new XNAGamePadThumbSticks(new XNAVector2(leftX, leftY), new XNAVector2(rightX, rightY));
            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }
    }
}
