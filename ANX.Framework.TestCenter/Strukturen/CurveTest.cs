using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


using XNACurve = Microsoft.Xna.Framework.Curve;
using ANXCurve = ANX.Framework.Curve;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class CurveTest
    {

        [Test]
        public void Evaluate()
    {
        XNACurve xna = new XNACurve();
        ANXCurve anx = new ANXCurve();

        Random r = new Random();
        for (int i = 0; i < 5; ++i)
        {
            float value = r.Next(-10, 10);
            xna.Keys.Add(new Microsoft.Xna.Framework.CurveKey(i, value));
            anx.Keys.Add(new CurveKey(i, value));
        }
        xna.PreLoop = Microsoft.Xna.Framework.CurveLoopType.Constant;
        anx.PreLoop = ANX.Framework.CurveLoopType.Constant;

        xna.PostLoop = Microsoft.Xna.Framework.CurveLoopType.Constant;
        anx.PostLoop = ANX.Framework.CurveLoopType.Constant;

        xna.ComputeTangents(Microsoft.Xna.Framework.CurveTangent.Flat);
        anx.ComputeTangents(CurveTangent.Flat);

        AssertHelper.CompareFloats(xna.Evaluate(3.5f), anx.Evaluate(3.5f), float.Epsilon);
    }
    }
}
