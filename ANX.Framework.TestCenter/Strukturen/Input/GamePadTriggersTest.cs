#region Using Statements
using System;
using NUnit.Framework;

#endregion // Using Statements

using ANXGamePadTriggers = ANX.Framework.Input.GamePadTriggers;
using XNAGamePadTriggers = Microsoft.Xna.Framework.Input.GamePadTriggers;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license
namespace ANX.Framework.TestCenter.Strukturen.Input
{
    [TestFixture]
    class GamePadTriggersTest
    {
        static object[] twofloats =
        {
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[]{-1, -1}
        };

        [TestCaseSource("twofloats")]
        public void Left(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna.Left, anx.Left, "Left");
        }

        [TestCaseSource("twofloats")]
        public void Right(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna.Right, anx.Right, "Right");
        }

        [TestCaseSource("twofloats")]
        public void ToString(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna.ToString(), anx.ToString(), "ToString");
        }

        [TestCaseSource("twofloats")]
        public void Equals(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            ANXGamePadTriggers anx2 = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna2 = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna == xna2, anx == anx2, "Equals");
        }

        [TestCaseSource("twofloats")]
        public void Equals2(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            ANXGamePadTriggers anx2 = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna2 = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna.Equals(xna2), anx.Equals(anx2), "Equals2");
        }

        [TestCaseSource("twofloats")]
        public void Equals3(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna.Equals(null), anx.Equals(null), "Equals3");
        }

        [TestCaseSource("twofloats")]
        public void NotEquals(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            while (leftTrigger == rightTrigger)
            {
                leftTrigger = DataFactory.RandomFloat;
            }
            ANXGamePadTriggers anx2 = new ANXGamePadTriggers(rightTrigger,leftTrigger);
            XNAGamePadTriggers xna2 = new XNAGamePadTriggers(rightTrigger,leftTrigger);
            AssertHelper.ConvertEquals(xna != xna2, anx != anx2, "NotEquals");
        }

        [TestCaseSource("twofloats")]
        public void GetHashCode(float leftTrigger, float rightTrigger)
        {
            ANXGamePadTriggers anx = new ANXGamePadTriggers(leftTrigger, rightTrigger);
            XNAGamePadTriggers xna = new XNAGamePadTriggers(leftTrigger, rightTrigger);
            AssertHelper.ConvertEquals(xna.GetHashCode(), anx.GetHashCode(), "GetHashCode");
        }
    }
}
