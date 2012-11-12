#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using XNAMatrix = Microsoft.Xna.Framework.Matrix;
using ANXMatrix = ANX.Framework.Matrix;
using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;
using XNAQuaternion = Microsoft.Xna.Framework.Quaternion;
using ANXQuaternion = ANX.Framework.Quaternion;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class MatrixTest
    {
        #region Testdata
        static object[] threefloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f) },
            new object[] {  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f),  DataFactory.RandomValueMinMax(-1000f, 1000f) },
        };

        static object[] sixteenfloats =
        {
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
        };
        static object[] sixteenfloats2 =
        {
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  2                                                 ,  4                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
        };
        static object[] sixteenfloats3 =
        {
            new object[] {  1                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  2                                                 ,  4                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
        };
        static object[] sixteenfloatsE =
        {
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  -2                                                 ,  4                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  2                                                  ,  -4                                                ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  4                                                  ,  2                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
        };
        static object[] sixteenfloatsE2 =
        {
            new object[] {  1                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  -2                                                 ,  4                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  1                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  2                                                  ,  -4                                                ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  1                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  4                                                  ,  2                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  -1                                                ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  2                                                  ,  -4                                                ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
            new object[] {  4                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  4                                                  ,  2                                                 ,  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000),  DataFactory.RandomValueMinMax(float.Epsilon, 1000) },
       }; 
        #endregion
 
        #region Properties
        [Test]
        public void Identity()
        {
            AssertHelper.ConvertEquals(XNAMatrix.Identity, ANXMatrix.Identity, "Identity");
        }
        
        [Test, TestCaseSource("sixteenfloats")]
        public void Right(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();
            
            xnaM.Right= new XNAVector3(m11, m12, m13);

            anxM.Right= new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Right, anxM.Right, "Right");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Left(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            xnaM.Left = new XNAVector3(m11, m12, m13);

            anxM.Left = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Left, anxM.Left, "Left");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Up(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            xnaM.Up = new XNAVector3(m11, m12, m13);

            anxM.Up = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Up, anxM.Up, "Up");
        } 
        
        [Test, TestCaseSource("sixteenfloats")]
        public void Down(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            xnaM.Down = new XNAVector3(m11, m12, m13);

            anxM.Down = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Down, anxM.Down, "Down");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Backward(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            xnaM.Backward = new XNAVector3(m11, m12, m13);

            anxM.Backward = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Backward, anxM.Backward, "Backward");
        } 
 
        [Test, TestCaseSource("sixteenfloats")]
        public void Forward(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            xnaM.Forward = new XNAVector3(m11, m12, m13);

            anxM.Forward = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Forward, anxM.Forward, "Forward");
        }
 
        [Test, TestCaseSource("sixteenfloats")]
        public void Translation(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM = new XNAMatrix();
            ANXMatrix anxM = new ANXMatrix();

            xnaM.Translation = new XNAVector3(m11, m12, m13);

            anxM.Translation = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaM.Translation, anxM.Translation, "Translation");
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
        public void MultiplyOperator2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            float mult = DataFactory.RandomValueMinMax(float.Epsilon, 1000);

            AssertHelper.ConvertEquals(xnaM1 * mult, anxM1 * mult, "MultiplyOperator2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void MultiplyOperator3(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            float mult = DataFactory.RandomValueMinMax(float.Epsilon, 1000);

            AssertHelper.ConvertEquals(mult * xnaM1, mult * anxM1, "MultiplyOperator3");
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
        public void Add(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Add(xnaM1, xnaM2), ANXMatrix.Add(anxM1, anxM2), "Add");
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
        public void SubtractOperator2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(-xnaM1, -anxM1, "SubtractOperator2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Negate(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Negate( xnaM1),ANXMatrix.Negate(anxM1), "SubtractOperator2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Subtract(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Subtract(xnaM1, xnaM2), ANXMatrix.Subtract(anxM1, anxM2), "Subtract");
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
        public void DivideOperator2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
 
            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            float divide = DataFactory.RandomValueMinMax(float.Epsilon, 1000);

            AssertHelper.ConvertEquals(xnaM1 / divide, anxM1 / divide, "DivideOperator2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Divide(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Divide(xnaM1, xnaM2), ANXMatrix.Divide(anxM1, anxM2), "Divide");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Divide2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
 
            float divide= DataFactory.RandomValueMinMax(float.Epsilon, 1000);

            AssertHelper.ConvertEquals(XNAMatrix.Divide(xnaM1, divide), ANXMatrix.Divide(anxM1, divide), "Divide2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Lerp(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            float amount = DataFactory.RandomValueMinMax(float.Epsilon, 1000);

            AssertHelper.ConvertEquals(XNAMatrix.Lerp(xnaM1, xnaM2, amount), ANXMatrix.Lerp(anxM1, anxM2, amount), "Lerp");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void EqualsOperator(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1 == xnaM2, anxM1 == anxM2, "EqualsOperator");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void InEqualsOperator(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1 != xnaM2, anxM1 != anxM2, "InEqualsOperator");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Equals(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAMatrix xnaM2 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXMatrix anxM2 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);


            AssertHelper.ConvertEquals(xnaM1.Equals(xnaM2), anxM1.Equals(anxM2), "Equals");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Equals2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            float test = DataFactory.RandomFloat;

            AssertHelper.ConvertEquals(xnaM1.Equals(test), anxM1.Equals(test), "Equals2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Determinant(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1.Determinant(), anxM1.Determinant(), "Determinant");
        }
        
        [Test, TestCaseSource("sixteenfloats")]
        public void Invert(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
 
            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Invert(xnaM1), ANXMatrix.Invert(anxM1), "Invert");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void ToString(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(xnaM1.ToString(), anxM1.ToString(), "ToString");
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
            AssertHelper.ConvertEquals(XNAMatrix.CreateTranslation(m11, m12, m13), ANXMatrix.CreateTranslation(m11, m12, m13), "CreateTranslation");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateTranslation2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 xnav1 = new XNAVector3(m11, m12, m13);

            ANXVector3 anxv1 = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(XNAMatrix.CreateTranslation(xnav1), ANXMatrix.CreateTranslation(anxv1), "CreateTranslation2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateScale(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaMatrix = XNAMatrix.CreateScale(m11);
            ANXMatrix anxMatrix = ANXMatrix.CreateScale(m11);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateScale");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateScale2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaMatrix = XNAMatrix.CreateScale(m11, m12, m13);
            ANXMatrix anxMatrix = ANXMatrix.CreateScale(m11, m12, m13);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateScale2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateScale3(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 XNAV = new XNAVector3(m11, m12, m13);
            ANXVector3 ANXV = new ANXVector3(m11, m12, m13);
            XNAMatrix xnaMatrix = XNAMatrix.CreateScale(XNAV);
            ANXMatrix anxMatrix = ANXMatrix.CreateScale(ANXV);

            AssertHelper.ConvertEquals(xnaMatrix, anxMatrix, "CreateScale3");
        }
 
        [Test, TestCaseSource("sixteenfloats")]
        public void CreateWorld(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 XNAV = new XNAVector3(m11, m12, m13);
            ANXVector3 ANXV = new ANXVector3(m11, m12, m13);

            AssertHelper.ConvertEquals(XNAMatrix.CreateWorld(XNAV,XNAVector3.Forward,XNAVector3.Up), ANXMatrix.CreateWorld(ANXV,ANXVector3.Forward,ANXVector3.Up), "CreateWorld");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Transpose(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            AssertHelper.ConvertEquals(XNAMatrix.Transpose(xnaM1), ANXMatrix.Transpose(anxM1), "Transpose");
        }
 
        [Test, TestCaseSource("sixteenfloats")]
        public void GetHashCode(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
 
            AssertHelper.ConvertEquals(xnaM1.GetHashCode(), anxM1.GetHashCode(), "GetHashCode()");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Transform(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAQuaternion xnaQ = new XNAQuaternion(m11, m12, m13, m14);

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXQuaternion anxQ = new ANXQuaternion(m11, m12, m13, m14);

            AssertHelper.ConvertEquals(XNAMatrix.Transform(xnaM1, xnaQ), ANXMatrix.Transform(anxM1, anxQ), "Transform");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateBillboard(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 xnav1 = new XNAVector3(m11, m12, m13);
            XNAVector3 xnav2 = new XNAVector3(m21, m22, m23);

            ANXVector3 anxv1 = new ANXVector3(m11, m12, m13);
            ANXVector3 anxv2 = new ANXVector3(m21, m22, m23);

            AssertHelper.ConvertEquals(XNAMatrix.CreateBillboard(xnav1, xnav2, XNAVector3.Up, XNAVector3.Forward), ANXMatrix.CreateBillboard(anxv1, anxv2, ANXVector3.Up, ANXVector3.Forward), "CreateBillboard");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateConstrainedBillboard(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 xnav1 = new XNAVector3(m11, m12, m13);
            XNAVector3 xnav2 = new XNAVector3(m21, m22, m23);

            ANXVector3 anxv1 = new ANXVector3(m11, m12, m13);
            ANXVector3 anxv2 = new ANXVector3(m21, m22, m23);

            AssertHelper.ConvertEquals(XNAMatrix.CreateConstrainedBillboard(xnav1, xnav2, XNAVector3.Up, XNAVector3.Forward,XNAVector3.Down), ANXMatrix.CreateConstrainedBillboard(anxv1, anxv2, ANXVector3.Up, ANXVector3.Forward,ANXVector3.Down), "CreateConstrainedBillboard");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateFromAxisAngle(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 xnav1 = new XNAVector3(m11, m12, m13);

            ANXVector3 anxv1 = new ANXVector3(m11, m12, m13);
 
            AssertHelper.ConvertEquals(XNAMatrix.CreateFromAxisAngle(xnav1, m44), ANXMatrix.CreateFromAxisAngle(anxv1,m44), "CreateFromAxisAngle");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateFromQuaternion(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAQuaternion xnaQ = new XNAQuaternion(m11, m12, m13, m14);

            ANXQuaternion anxQ = new ANXQuaternion(m11, m12, m13, m14);

            AssertHelper.ConvertEquals(XNAMatrix.CreateFromQuaternion(xnaQ), ANXMatrix.CreateFromQuaternion(anxQ), "CreateFromQuaternion");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateFromYawPitchRoll(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(XNAMatrix.CreateFromYawPitchRoll(m11, m12, m13), ANXMatrix.CreateFromYawPitchRoll(m11, m12, m13), "CreateFromYawPitchRoll");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateLookAt(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 xnav1 = new XNAVector3(m11, m12, m13);
            XNAVector3 xnav2 = new XNAVector3(m21, m22, m23);
            XNAVector3 xnav3 = new XNAVector3(m31, m32, m33);

            ANXVector3 anxv1 = new ANXVector3(m11, m12, m13);
            ANXVector3 anxv2 = new ANXVector3(m21, m22, m23);
            ANXVector3 anxv3 = new ANXVector3(m31, m32, m33);

            AssertHelper.ConvertEquals(XNAMatrix.CreateLookAt(xnav1, xnav2, xnav3), ANXMatrix.CreateLookAt(anxv1, anxv2, anxv3), "CreateLookAt");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateOrthographic(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(XNAMatrix.CreateOrthographic(m11, m12, m13, m14), ANXMatrix.CreateOrthographic(m11, m12, m13, m14), "CreateOrthographic");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateOrthographicOffCenter(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(XNAMatrix.CreateOrthographicOffCenter(m11, m12, m13, m14, m21, m22), ANXMatrix.CreateOrthographicOffCenter(m11, m12, m13, m14,m21,m22), "CreateOrthographicOffCenter");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Decompose1(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAQuaternion xnaQ = new XNAQuaternion(m11, m12, m13, m14);
            XNAVector3 xnav2 = new XNAVector3();
            XNAVector3 xnav3 = new XNAVector3();

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXQuaternion anxQ = new ANXQuaternion(m11, m12, m13, m14);
            ANXVector3 anxv2 = new ANXVector3();
            ANXVector3 anxv3 = new ANXVector3();

            xnaM1.Decompose(out xnav2, out xnaQ, out xnav3);
            anxM1.Decompose(out anxv2 ,out anxQ, out anxv3);

            AssertHelper.ConvertEquals(xnav2,anxv2 , "Decompose1");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Decompose2(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAQuaternion xnaQ = new XNAQuaternion(m11, m12, m13, m14);
            XNAVector3 xnav2 = new XNAVector3();
            XNAVector3 xnav3 = new XNAVector3();

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXQuaternion anxQ = new ANXQuaternion(m11, m12, m13, m14);
            ANXVector3 anxv2 = new ANXVector3();
            ANXVector3 anxv3 = new ANXVector3();

            xnaM1.Decompose(out xnav2, out xnaQ, out xnav3);
            anxM1.Decompose(out anxv2, out anxQ, out anxv3);

            AssertHelper.ConvertEquals(xnav3, anxv3, "Decompose2");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void Decompose3(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAQuaternion xnaQ = new XNAQuaternion(m11, m12, m13, m14);
            XNAVector3 xnav2 = new XNAVector3();
            XNAVector3 xnav3 = new XNAVector3();

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXQuaternion anxQ = new ANXQuaternion(m11, m12, m13, m14);
            ANXVector3 anxv2 = new ANXVector3();
            ANXVector3 anxv3 = new ANXVector3();

            xnaM1.Decompose(out xnav2, out xnaQ, out xnav3);
            anxM1.Decompose(out anxv2, out anxQ, out anxv3);

            AssertHelper.ConvertEquals(xnaQ, anxQ, "Decompose3");
        }

        [Test, TestCase(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
        public void Decompose4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAQuaternion xnaQ = new XNAQuaternion(m11, m12, m13, m14);
            XNAVector3 xnav2 = new XNAVector3();
            XNAVector3 xnav3 = new XNAVector3();

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXQuaternion anxQ = new ANXQuaternion(m11, m12, m13, m14);
            ANXVector3 anxv2 = new ANXVector3();
            ANXVector3 anxv3 = new ANXVector3();

            bool m1 = xnaM1.Decompose(out xnav2, out xnaQ, out xnav3);
            bool m2 = anxM1.Decompose(out anxv2, out anxQ, out anxv3);

            AssertHelper.ConvertEquals( m1, m2, "Decompose4");
        }

        [Test, TestCaseSource("sixteenfloats")]
        public void DecomposeTranslation(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAMatrix xnaM1 = new XNAMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            XNAQuaternion xnaRotation;
            XNAVector3 xnaScale;
            XNAVector3 xnaTranslation;

            ANXMatrix anxM1 = new ANXMatrix(m11, m12, m13, m14, m21, m22, m23, m24, m31, m32, m33, m34, m41, m42, m43, m44);
            ANXQuaternion anxRotation;
            ANXVector3 anxScale;
            ANXVector3 anxTranslation;

            bool m1 = xnaM1.Decompose(out xnaScale, out xnaRotation, out xnaTranslation);
            bool m2 = anxM1.Decompose(out anxScale, out anxRotation, out anxTranslation);

            AssertHelper.ConvertEquals(xnaTranslation, anxTranslation, "DecomposeTranslation");

        }

        [Test, TestCaseSource("threefloats")]
        public void DecomposeScale(float s1, float s2, float s3)
        {
            XNAMatrix xnaM1 = XNAMatrix.CreateScale(s1, s2, s3);
            XNAQuaternion xnaRotation;
            XNAVector3 xnaScale;
            XNAVector3 xnaTranslation;

            ANXMatrix anxM1 = ANXMatrix.CreateScale(s1, s2, s3);
            ANXQuaternion anxRotation;
            ANXVector3 anxScale;
            ANXVector3 anxTranslation;

            bool m1 = xnaM1.Decompose(out xnaScale, out xnaRotation, out xnaTranslation);
            bool m2 = anxM1.Decompose(out anxScale, out anxRotation, out anxTranslation);

            AssertHelper.ConvertEquals(xnaScale, anxScale, "DecomposeScale");

        }

        [Test, TestCaseSource("sixteenfloats")]
        public void CreateShadow(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            XNAVector3 xnav1 = new XNAVector3(m11, m12, m13);
            Microsoft.Xna.Framework.Plane xnap = new Microsoft.Xna.Framework.Plane(m21, m22, m23, m24);

            ANXVector3 anxv1 = new ANXVector3(m11, m12, m13);
            Plane anxp = new Plane(m21, m22, m23, m24);

            AssertHelper.ConvertEquals(XNAMatrix.CreateShadow(xnav1,xnap ), ANXMatrix.CreateShadow(anxv1, anxp), "CreateShadow");
        }
 
        [Test, TestCaseSource("sixteenfloats")]
        public void CreateReflection(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            Microsoft.Xna.Framework.Plane xnap = new Microsoft.Xna.Framework.Plane(m21, m22, m23, m24);

            Plane anxp = new Plane(m21, m22, m23, m24);

            AssertHelper.ConvertEquals(XNAMatrix.CreateReflection(xnap), ANXMatrix.CreateReflection(anxp), "CreateReflection");
        }

        [Test, TestCaseSource("sixteenfloats2")]
        public void CreatePerspective(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(XNAMatrix.CreatePerspective(m11, m12, m13, m14), ANXMatrix.CreatePerspective(m11, m12, m13, m14), "CreatePerspective");
        }

        [Test, TestCaseSource("sixteenfloatsE")]
        public void CreatePerspectiveE(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
             AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(delegate { XNAMatrix.CreatePerspective(m11, m12, m13, m14); }), Assert.Throws<ArgumentOutOfRangeException>(delegate { ANXMatrix.CreatePerspective(m11, m12, m13, m14); }), "CreatePerspectiveE");
        }
        
        [Test, TestCaseSource("sixteenfloats3")]
        public void CreatePerspectiveFieldOfView(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(XNAMatrix.CreatePerspectiveFieldOfView(m11, m12, m13, m14), ANXMatrix.CreatePerspectiveFieldOfView(m11, m12, m13, m14), "CreatePerspectiveFieldOfView");
        }

        [Test, TestCaseSource("sixteenfloatsE2")]
        public void CreatePerspectiveFieldOfViewE(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(delegate { XNAMatrix.CreatePerspectiveFieldOfView(m11, m12, m13, m14); }), Assert.Throws<ArgumentOutOfRangeException>(delegate { ANXMatrix.CreatePerspectiveFieldOfView(m11, m12, m13, m14); }), "CreatePerspectiveE");
        }
        
        [Test, TestCaseSource("sixteenfloats2")]
        public void CreatePerspectiveOffCenter(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(XNAMatrix.CreatePerspectiveOffCenter(m21, m22, m11, m12, m13, m14), ANXMatrix.CreatePerspectiveOffCenter(m21, m22, m11, m12, m13, m14), "CreatePerspectiveOffCenter");
        }

        [Test, TestCaseSource("sixteenfloatsE")]
        public void CreatePerspectiveOffCenterE(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
        {
            AssertHelper.ConvertEquals(Assert.Throws<ArgumentOutOfRangeException>(delegate { XNAMatrix.CreatePerspectiveOffCenter(m21, m22, m11, m12, m13, m14); }), Assert.Throws<ArgumentOutOfRangeException>(delegate { ANXMatrix.CreatePerspectiveOffCenter(m21, m22, m11, m12, m13, m14); }), "CreatePerspectiveE");
        }
    }
}
