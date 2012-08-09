#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XNAMath = Microsoft.Xna.Framework.MathHelper;
using ANXMath = ANX.Framework.MathHelper;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class MathHelperTest
    {
        #region Data
        static object[] onefloat =
        {
            float.NegativeInfinity,
            float.MinValue
            -3.2f,
            -1.6f,
            0,
            1.6f,
            3.2f,
            float.MaxValue ,
            float.PositiveInfinity,
            DataFactory.RandomFloat,
            DataFactory.RandomFloat,
            DataFactory.RandomFloat,
        };

        static object[] twofloats =
        {
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat }
        };

        static object[] threefloats =
        {
            new object[] { 0 ,0, 0 },
            new object[] { 0, 0, 1 },
            new object[] { 0, 1, 0 },
            new object[] { 1, 0, 0 },
            new object[] { 0, 1, 1 },
            new object[] { 1, 0, 1 },
            new object[] { 1, 1, 0 },
            new object[] { 3, 2, 1 },
            new object[] { 1, 2, 3 },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat }
        };

        static object[] fivefloats =
        {
            new object[] { 0 ,0, 0,0,0 },
            new object[] { 0, 0, 1,0,0 },
            new object[] { 0, 1, 0,0,0 },
            new object[] { 1, 0, 0,0,0 },
            new object[] { 0, 1, 1,0,0 },
            new object[] { 1, 0, 1,0,1 },
            new object[] { 1, 1, 0,0,1 },
            new object[] { 3, 2, 1,0,1 },
            new object[] { 1, 2, 3,0,1 },
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat},
            new object[] { DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat}
        };

        static object[] hermiteFloats =
        {
            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, 0.0f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, 0.0f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, 0.0f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, 0.0f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, 0.0f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, 0.0f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, 0.0f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, 0.0f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, 0.0f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, 0.0f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, 0.0f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, 0.0f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, 0.0f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, 0.0f },

            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, 1.0f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, 1.0f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, 1.0f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, 1.0f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, 1.0f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, 1.0f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, 1.0f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, 1.0f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, 1.0f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, 1.0f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, 1.0f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, 1.0f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, 1.0f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, 1.0f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, 1.0f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, 1.0f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, 1.0f },

            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.25f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.25f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.25f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.25f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.25f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, 0.25f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, 0.25f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, 0.25f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, 0.25f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, 0.25f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, 0.25f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, 0.25f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, 0.25f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, 0.25f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, 0.25f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, 0.25f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, 0.25f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, 0.25f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, 0.25f },

            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.5f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.5f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.5f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.5f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.5f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, 0.5f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, 0.5f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, 0.5f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, 0.5f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, 0.5f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, 0.5f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, 0.5f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, 0.5f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, 0.5f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, 0.5f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, 0.5f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, 0.5f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, 0.5f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, 0.5f },

            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.75f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.75f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.75f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.75f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.75f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, 0.75f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, 0.75f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, 0.75f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, 0.75f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, 0.75f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, 0.75f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, 0.75f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, 0.75f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, 0.75f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, 0.75f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, 0.75f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, 0.75f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, 0.75f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, 0.75f },
        
            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, -1.0f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, -1.0f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, -1.0f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, -1.0f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, -1.0f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, -1.0f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, -1.0f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, -1.0f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, -1.0f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, -1.0f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, -1.0f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, -1.0f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, -1.0f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, -1.0f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, -1.0f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, -1.0f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, -1.0f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, -1.0f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, -1.0f },

            new object[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.1f },
            new object[] { 1.0f, 0.0f, 0.0f, 0.0f, 1.2f },
            new object[] { 0.0f, 1.0f, 0.0f, 0.0f, 1.3f },
            new object[] { 0.0f, 0.0f, 1.0f, 0.0f, 1.4f },
            new object[] { 0.0f, 0.0f, 0.0f, 1.0f, 1.5f },
            new object[] { 1.0f, 1.0f, 0.0f, 0.0f, 1.6f },
            new object[] { 1.0f, 0.0f, 1.0f, 0.0f, 1.7f },
            new object[] { 1.0f, 0.0f, 0.0f, 1.0f, 1.8f },
            new object[] { 1.0f, 1.0f, 1.0f, 0.0f, 1.9f },
            new object[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.1f },
            new object[] { 0.3f, 0.0f, 0.0f, 0.0f, 1.2f },
            new object[] { 0.0f, 0.2f, 0.0f, 0.0f, 1.3f },
            new object[] { 0.0f, 0.0f, 0.1f, 0.0f, 1.4f },
            new object[] { 0.0f, 0.0f, 0.0f, 0.4f, 1.5f },
            new object[] { 0.5f, 0.6f, 0.0f, 0.0f, 1.6f },
            new object[] { 1.6f, 0.0f, 0.7f, 0.0f, 1.7f },
            new object[] { 0.9f, 0.0f, 0.0f, 0.8f, 1.8f },
            new object[] { 1.5f, 0.1f, 0.3f, 0.0f, 1.9f },
            new object[] { 1.4f, 1.3f, 0.2f, 1.2f, 1.1f },
        };

        #endregion // Data

        [TestCaseSource("fivefloats")]
        public void Barycentric(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float xna = XNAMath.Barycentric(value1, tangent1, value2, tangent2, amount);
            float anx = ANXMath.Barycentric(value1, tangent1, value2, tangent2, amount);
            AssertHelper.ConvertEquals(xna, anx, "Barycentric");
        }

        [TestCaseSource("fivefloats")]
        public void CatmullRom(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float xna = XNAMath.CatmullRom(value1, tangent1, value2, tangent2, amount);
            float anx = ANXMath.CatmullRom(value1, tangent1, value2, tangent2, amount);
            AssertHelper.ConvertEquals(xna, anx, "CatmullRom");
        }

        [Test, TestCaseSource("threefloats")]
        public void Clamp(float value1, float value2, float amount)
        {
            float xna = XNAMath.Clamp(value1, value2, amount);
            float anx = ANXMath.Clamp(value1, value2, amount);
            AssertHelper.ConvertEquals(xna, anx, "Clamp");
        }

        [Test, TestCaseSource("threefloats")]
        public void SmoothStep(float value1, float value2, float amount)
        {
            float xna = XNAMath.SmoothStep(value1, value2, amount);
            float anx = ANXMath.SmoothStep(value1, value2, amount);
            AssertHelper.ConvertEquals(xna, anx, "SmoothStep");
        }

        [Test, TestCaseSource("threefloats")]
        public void Lerp(float value1, float value2, float amount)
        {
            float xna = XNAMath.Lerp(value1, value2, amount);
            float anx = ANXMath.Lerp(value1, value2, amount);
            AssertHelper.ConvertEquals(xna, anx, "Lerp");
        }

        [TestCaseSource("twofloats")]
        public void Distance(float min, float max)
        {
            float xna = XNAMath.Distance(min, max);
            float anx = ANXMath.Distance(min, max);
            AssertHelper.ConvertEquals(xna, anx, "Distance");
        }

        [TestCaseSource("twofloats")]
        public void Max(float min, float max)
        {
            float xna = XNAMath.Max(min, max);
            float anx = ANXMath.Max(min, max);
            AssertHelper.ConvertEquals(xna, anx, "Max");
        }

        [TestCaseSource("twofloats")]
        public void Min(float min, float max)
        {
            float xna = XNAMath.Min(min, max);
            float anx = ANXMath.Min(min, max);
            AssertHelper.ConvertEquals(xna, anx, "Min");
        }

        [TestCaseSource("onefloat")]
        public void ToDegrees(float one)
        {
            float xna = XNAMath.ToDegrees(one);
            float anx = ANXMath.ToDegrees(one);
            AssertHelper.ConvertEquals(xna, anx, "ToRadians");

        }

        [TestCaseSource("onefloat")]
        public void ToRadians(float one)
        {
            float xna = XNAMath.ToRadians(one);
            float anx = ANXMath.ToRadians(one);

            AssertHelper.ConvertEquals(xna, anx, "ToRadians");
        }

        [TestCaseSource("onefloat")]
        public void WrapAngle(float one)
        {
            float xna = XNAMath.WrapAngle(one);
            float anx = ANXMath.WrapAngle(one);
            AssertHelper.ConvertEquals(xna, anx, "WrapAngle");
        }

        [TestCaseSource("hermiteFloats")]
        public void Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
        {
            float xna = XNAMath.Hermite(value1, tangent1, value2, tangent2, amount);
            float anx = ANXMath.Hermite(value1, tangent1, value2, tangent2, amount);

            AssertHelper.ConvertEquals(xna, anx, "Hermite");
        }
    }
}
