#region Using Statements
using System;
using System.IO;
using ANX.Framework.NonXNA;
using NUnit.Framework;
#endregion // Using Statements

using XNACurve = Microsoft.Xna.Framework.Curve;
using ANXCurve = ANX.Framework.Curve;

using XNACurveKey = Microsoft.Xna.Framework.CurveKey;
using ANXCurveKey = ANX.Framework.CurveKey;

using XNACurveLoopType = Microsoft.Xna.Framework.CurveLoopType;
using ANXCurveLoopType = ANX.Framework.CurveLoopType;

using XNACurveKeyCollection = Microsoft.Xna.Framework.CurveKeyCollection;
using ANXCurveKeyCollection = ANX.Framework.CurveKeyCollection;

using XNACurveTangent = Microsoft.Xna.Framework.CurveTangent;
using ANXCurveTangent = ANX.Framework.CurveTangent;



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
namespace ANX.Framework.TestCenter.classTest
{
	[TestFixture]
	class CurveTest
	{
		static object[] inoutsame =
		{
			new object[] {XNACurveTangent.Flat,ANXCurveTangent.Flat    , DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
			new object[] {XNACurveTangent.Linear,ANXCurveTangent.Linear, DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000)},
			new object[] {XNACurveTangent.Smooth,ANXCurveTangent.Smooth, DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000)}

		};

		[TestCaseSource("inoutsame")]
		public void Curve(XNACurveTangent xnainout, ANXCurveTangent anxinout, float f1, float f2, float f3, float f4, float f5, float f6, float f7, float f8, float f9, float f10, float f11, float f12, float f13, float f14, float f15, float f16)
		{
			XNACurve xna = new XNACurve();
			ANXCurve anx = new ANXCurve();

            xna.Keys.Add(new XNACurveKey(f1, f2));
            //xna.Keys.Add(new XNACurveKey(f3, f4));
            //xna.Keys.Add(new XNACurveKey(f5, f6));
            //xna.Keys.Add(new XNACurveKey(f7, f8));
            //xna.Keys.Add(new XNACurveKey(f9, f10));
            //xna.Keys.Add(new XNACurveKey(f11, f12));
            //xna.Keys.Add(new XNACurveKey(f13, f14));
            //xna.Keys.Add(new XNACurveKey(f15, f16));

            anx.Keys.Add(new ANXCurveKey(f1, f2));
            //anx.Keys.Add(new ANXCurveKey(f3, f4));
            //anx.Keys.Add(new ANXCurveKey(f5, f6));
            //anx.Keys.Add(new ANXCurveKey(f7, f8));
            //anx.Keys.Add(new ANXCurveKey(f9, f10));
            //anx.Keys.Add(new ANXCurveKey(f11, f12));
            //anx.Keys.Add(new ANXCurveKey(f13, f14));
            //anx.Keys.Add(new ANXCurveKey(f15, f16));

			AssertHelper.ConvertEquals(xna, anx, "Curve");
		}
   
		[TestCaseSource("inoutsame")]
		public void Clone(XNACurveTangent xnainout, ANXCurveTangent anxinout, float f1, float f2, float f3, float f4, float f5, float f6, float f7, float f8, float f9, float f10, float f11, float f12, float f13, float f14, float f15, float f16)
		{
			XNACurve xna = new XNACurve();
			ANXCurve anx = new ANXCurve();

			xna.Keys.Add(new XNACurveKey(f1, f2));
			xna.Keys.Add(new XNACurveKey(f3, f4));
			xna.Keys.Add(new XNACurveKey(f5, f6));
			xna.Keys.Add(new XNACurveKey(f7, f8));
			xna.Keys.Add(new XNACurveKey(f9, f10));
			xna.Keys.Add(new XNACurveKey(f11, f12));
			xna.Keys.Add(new XNACurveKey(f13, f14));
			xna.Keys.Add(new XNACurveKey(f15, f16));

			anx.Keys.Add(new ANXCurveKey(f1, f2));
			anx.Keys.Add(new ANXCurveKey(f3, f4));
			anx.Keys.Add(new ANXCurveKey(f5, f6));
			anx.Keys.Add(new ANXCurveKey(f7, f8));
			anx.Keys.Add(new ANXCurveKey(f9, f10));
			anx.Keys.Add(new ANXCurveKey(f11, f12));
			anx.Keys.Add(new ANXCurveKey(f13, f14));
			anx.Keys.Add(new ANXCurveKey(f15, f16));

			XNACurve xna2 = xna.Clone();
			ANXCurve anx2 = anx.Clone();

			AssertHelper.ConvertEquals(xna,xna2,anx, anx2,"Clone");

		}

		[TestCaseSource("inoutsame")]
		public void ComputeTangents(XNACurveTangent xnainout, ANXCurveTangent anxinout, float f1, float f2, float f3, float f4, float f5, float f6, float f7, float f8, float f9, float f10, float f11, float f12, float f13, float f14, float f15, float f16)
		{
			XNACurve xna = new XNACurve();
			ANXCurve anx = new ANXCurve();

			xna.Keys.Add(new XNACurveKey(f1, f2));
			xna.Keys.Add(new XNACurveKey(f3, f4));
			xna.Keys.Add(new XNACurveKey(f5, f6));
			xna.Keys.Add(new XNACurveKey(f7, f8));
			xna.Keys.Add(new XNACurveKey(f9, f10));
			xna.Keys.Add(new XNACurveKey(f11, f12));
			xna.Keys.Add(new XNACurveKey(f13, f14));
			xna.Keys.Add(new XNACurveKey(f15, f16));

			anx.Keys.Add(new ANXCurveKey(f1, f2));
			anx.Keys.Add(new ANXCurveKey(f3, f4));
			anx.Keys.Add(new ANXCurveKey(f5, f6));
			anx.Keys.Add(new ANXCurveKey(f7, f8));
			anx.Keys.Add(new ANXCurveKey(f9, f10));
			anx.Keys.Add(new ANXCurveKey(f11, f12));
			anx.Keys.Add(new ANXCurveKey(f13, f14));
			anx.Keys.Add(new ANXCurveKey(f15, f16));

			xna.ComputeTangents(xnainout);
			anx.ComputeTangents(anxinout);

			AssertHelper.ConvertEquals(xna, anx, "ComputeTangents");
		}

	}
}
