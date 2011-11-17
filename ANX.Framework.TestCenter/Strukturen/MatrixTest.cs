#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion // Using Statements

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

using XNAMatrix = Microsoft.Xna.Framework.Matrix;
using ANXMatrix = ANX.Framework.Matrix;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class MatrixTest
    {
        #region Testdata
        static object[] sixteenfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
        };

        #endregion
        #region Properties
        [Test]
        public void Identity()
        {
            AssertHelper.ConvertEquals(XNAMatrix.Identity, ANXMatrix.Identity, "Identity");
        }

        #endregion // Properties

        #region Constructors

        [Test]
        public void constructor0()
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            AssertHelper.ConvertEquals(xnaM, anxM, "Constructor0");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void constructor1(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM, anxM, "Constructor1");
        }

        #endregion // Constructors


        [Test, TestCaseSource("sixteenfloats")]
        public void MultiplyOperator(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1 * xnaM2, anxM1 * anxM2, "MultiplyOperator");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Multiply(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Multiply(xnaM1, xnaM2), ANXMatrix.Multiply(anxM1, anxM2), "Multiply");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Multiply2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaResult;

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxResult;

            XNAMatrix.Multiply(ref xnaM1, ref xnaM2, out xnaResult);
            ANXMatrix.Multiply(ref anxM1, ref anxM2, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "Multiply2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Multiply3(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Multiply(xnaM1, m11), ANXMatrix.Multiply(anxM1, m11), "Multiply3");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Multiply4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaResult;

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxResult;

            XNAMatrix.Multiply(ref xnaM1, m11, out xnaResult);
            ANXMatrix.Multiply(ref anxM1, m11, out anxResult);

            AssertHelper.ConvertEquals(xnaResult, anxResult, "Multiply4");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void AddOperator(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1 + xnaM2, anxM1 + anxM2, "AddOperator");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void SubtractOperator(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1 - xnaM2, anxM1 - anxM2, "SubtractOperator");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void DivideOperator(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1 / xnaM2, anxM1 / anxM2, "DivideOperator");
        }
        [Test, TestCaseSource("sixteenfloats")]
        public void Invert(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix();

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix();

            AssertHelper.ConvertEquals(xnaM2, anxM2, "DivideOperator");
        }
        [Test, TestCaseSource("sixteenfloats")]
        public void CreateRotationX(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaMatrix = XNAMatrix.CreateRotationX(m11);
            ANXMatrix anxMatrix = ANXMatrix.CreateRotationX(m11);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateRotationX");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateRotationY(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaMatrix = XNAMatrix.CreateRotationY(m11);
            ANXMatrix anxMatrix = ANXMatrix.CreateRotationY(m11);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateRotationY");
        }


        [Test, TestCaseSource("sixteenfloats")]
        public void CreateRotationZ(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaMatrix = XNAMatrix.CreateRotationZ(m11);
            ANXMatrix anxMatrix = ANXMatrix.CreateRotationZ(m11);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateRotationZ");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateTranslation(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaMatrix = XNAMatrix.CreateTranslation(m11, m12, m13);
            ANXMatrix anxMatrix = ANXMatrix.CreateTranslation(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateTranslation");
        }

    }
}
