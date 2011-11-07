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

using XNAColor = Microsoft.Xna.Framework.Color;
using ANXColor = ANX.Framework.Color;

using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;

using NUnit.Framework;

namespace ANX.Framework.TestCenter.Strukturen
{
    [TestFixture]
    class ColorTest
    {
        static object[] fourfloats =
        {
            new object[] { DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat },
            new object[] { DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat },
            new object[] { DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat },
            new object[] { DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat },
            new object[] { DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat,  DataFactory.RandomNormalizedFloat }
        };

        static object[] eightfloats =
        {
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat }
        };

        static object[] ninefloats =
        {
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat },
            new object[] {  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat,  DataFactory.RandomFloat }
        };

        static object[] colors =
        {
            new Object[] { XNAColor.AliceBlue, ANXColor.AliceBlue, "Color.AliceBlue" },
            new Object[] { XNAColor.AntiqueWhite, ANXColor.AntiqueWhite, "Color.AntiqueWhite" },
            new Object[] { XNAColor.Aqua, ANXColor.Aqua, "Color.Aqua" },
            new Object[] { XNAColor.Aquamarine, ANXColor.Aquamarine, "Color.Aquamarine" },
            new Object[] { XNAColor.Azure, ANXColor.Azure, "Color.Azure" },
            new Object[] { XNAColor.Beige, ANXColor.Beige, "Color.Beige" },
            new Object[] { XNAColor.Bisque, ANXColor.Bisque, "Color.Bisque" },
            new Object[] { XNAColor.Black, ANXColor.Black, "Color.Black" },
            new Object[] { XNAColor.BlanchedAlmond, ANXColor.BlanchedAlmond, "Color.BlanchedAlmond" },
            new Object[] { XNAColor.Blue, ANXColor.Blue, "Color.Blue" },
            new Object[] { XNAColor.BlueViolet, ANXColor.BlueViolet, "Color.BlueViolet" },
            new Object[] { XNAColor.Brown, ANXColor.Brown, "Color.Brown" },
            new Object[] { XNAColor.BurlyWood, ANXColor.BurlyWood, "Color.BurlyWood" },
            new Object[] { XNAColor.CadetBlue, ANXColor.CadetBlue, "Color.CadetBlue" },
            new Object[] { XNAColor.Chartreuse, ANXColor.Chartreuse, "Color.Chartreuse" },
            new Object[] { XNAColor.Chocolate, ANXColor.Chocolate, "Color.Choclate" },
            new Object[] { XNAColor.Coral, ANXColor.Coral, "Color.Coral" },
            new Object[] { XNAColor.CornflowerBlue, ANXColor.CornflowerBlue, "Color.CornflowerBlue" },
            new Object[] { XNAColor.Cornsilk, ANXColor.Cornsilk, "Color.Cornsilk" },
            new Object[] { XNAColor.Crimson, ANXColor.Crimson, "Color.Crimson" },
            new Object[] { XNAColor.Cyan, ANXColor.Cyan, "Color.Cyan" },
            new Object[] { XNAColor.DarkBlue, ANXColor.DarkBlue, "Color.DarkBlue" },
            new Object[] { XNAColor.DarkCyan, ANXColor.DarkCyan, "Color.DarkCyan" },
            new Object[] { XNAColor.DarkGoldenrod, ANXColor.DarkGoldenrod, "Color.DarkGoldenrod" },
            new Object[] { XNAColor.DarkGray, ANXColor.DarkGray, "Color.DarkGray" },
            new Object[] { XNAColor.DarkGreen, ANXColor.DarkGreen, "Color.DarkGreen" },
            new Object[] { XNAColor.DarkKhaki, ANXColor.DarkKhaki, "Color.DarkKhaki" },
            new Object[] { XNAColor.DarkMagenta, ANXColor.DarkMagenta, "Color.DarkMagenta" },
            new Object[] { XNAColor.DarkOliveGreen, ANXColor.DarkOliveGreen, "Color.DarkOliveGreen" },
            new Object[] { XNAColor.DarkOrange, ANXColor.DarkOrange, "Color.DarkOrange" },
            new Object[] { XNAColor.DarkOrchid, ANXColor.DarkOrchid, "Color.DarkOrchid" },
            new Object[] { XNAColor.DarkRed, ANXColor.DarkRed, "Color.DarkRed" },
            new Object[] { XNAColor.DarkSalmon, ANXColor.DarkSalmon, "Color.DarkSalmon" },
            new Object[] { XNAColor.DarkSeaGreen, ANXColor.DarkSeaGreen, "Color.DarkSeaGreen" },
            new Object[] { XNAColor.DarkSlateBlue, ANXColor.DarkSlateBlue, "Color.DarkSlateBlue" },
            new Object[] { XNAColor.DarkSlateGray, ANXColor.DarkSlateGray, "Color.DarkSlateGray" },
            new Object[] { XNAColor.DarkTurquoise, ANXColor.DarkTurquoise, "Color.DarkTurquoise" },
            new Object[] { XNAColor.DarkViolet, ANXColor.DarkViolet, "Color.DarkViolet" },
            new Object[] { XNAColor.DeepPink, ANXColor.DeepPink, "Color.DeepPink" },
            new Object[] { XNAColor.DeepSkyBlue, ANXColor.DeepSkyBlue, "Color.DeepSkyBlue" },
            new Object[] { XNAColor.DimGray, ANXColor.DimGray, "Color.DimGray" },
            new Object[] { XNAColor.DodgerBlue, ANXColor.DodgerBlue, "Color.DodgerBlue" },
            new Object[] { XNAColor.Firebrick, ANXColor.Firebrick, "Color.Firebrick" },
            new Object[] { XNAColor.FloralWhite, ANXColor.FloralWhite, "Color.FloralWhite" },
            new Object[] { XNAColor.ForestGreen, ANXColor.ForestGreen, "Color.ForestGreen" },
            new Object[] { XNAColor.Fuchsia, ANXColor.Fuchsia, "Color.Fuchsia" },
            new Object[] { XNAColor.Gainsboro, ANXColor.Gainsboro, "Color.Gainsboro" },
            new Object[] { XNAColor.GhostWhite, ANXColor.GhostWhite, "Color.GhostWhite" },
            new Object[] { XNAColor.Gold, ANXColor.Gold, "Color.Gold" },
            new Object[] { XNAColor.Goldenrod, ANXColor.Goldenrod, "Color.Goldenrod" },
            new Object[] { XNAColor.Gray, ANXColor.Gray, "Color.Gray" },
            new Object[] { XNAColor.Green, ANXColor.Green, "Color.Green" },
            new Object[] { XNAColor.GreenYellow, ANXColor.GreenYellow, "Color.GreenYellow" },
            new Object[] { XNAColor.Honeydew, ANXColor.Honeydew, "Color.Honeydew" },
            new Object[] { XNAColor.HotPink, ANXColor.HotPink, "Color.HotPink" },
            new Object[] { XNAColor.IndianRed, ANXColor.IndianRed, "Color.IndianRed" },
            new Object[] { XNAColor.Indigo, ANXColor.Indigo, "Color.Indigo" },
            new Object[] { XNAColor.Ivory, ANXColor.Ivory, "Color.Ivory" },
            new Object[] { XNAColor.Khaki, ANXColor.Khaki, "Color.Khaki" },
            new Object[] { XNAColor.Lavender, ANXColor.Lavender, "Color.Lavender" },
            new Object[] { XNAColor.LavenderBlush, ANXColor.LavenderBlush, "Color.LavenderBlush" },
            new Object[] { XNAColor.LawnGreen, ANXColor.LawnGreen, "Color.LawnGreen" },
            new Object[] { XNAColor.LemonChiffon, ANXColor.LemonChiffon, "Color.LemonChiffon" },
            new Object[] { XNAColor.LightBlue, ANXColor.LightBlue, "Color.LightBlue" },
            new Object[] { XNAColor.LightCoral, ANXColor.LightCoral, "Color.LightCoral" },
            new Object[] { XNAColor.LightCyan, ANXColor.LightCyan, "Color.LightCyan" },
            new Object[] { XNAColor.LightGoldenrodYellow, ANXColor.LightGoldenrodYellow, "Color.LightGoldenrodYellow" },
            new Object[] { XNAColor.LightGray, ANXColor.LightGray, "Color.LightGray" },
            new Object[] { XNAColor.LightGreen, ANXColor.LightGreen, "Color.LightGreen" },
            new Object[] { XNAColor.LightPink, ANXColor.LightPink, "Color.LightPink" },
            new Object[] { XNAColor.LightSalmon, ANXColor.LightSalmon, "Color.LightSalmon" },
            new Object[] { XNAColor.LightSeaGreen, ANXColor.LightSeaGreen, "Color.LightSeaGreen" },
            new Object[] { XNAColor.LightSkyBlue, ANXColor.LightSkyBlue, "Color.LightSkyBlue" },
            new Object[] { XNAColor.LightSlateGray, ANXColor.LightSlateGray, "Color.LightSlateGray" },
            new Object[] { XNAColor.LightSteelBlue, ANXColor.LightSteelBlue, "Color.LightSteelBlue" },
            new Object[] { XNAColor.LightYellow, ANXColor.LightYellow, "Color.LightYellow" },
            new Object[] { XNAColor.Lime, ANXColor.Lime, "Color.Lime" },
            new Object[] { XNAColor.LimeGreen, ANXColor.LimeGreen, "" },
            new Object[] { XNAColor.Linen, ANXColor.Linen, "" },
            new Object[] { XNAColor.Magenta, ANXColor.Magenta, "" },
            new Object[] { XNAColor.Maroon, ANXColor.Maroon, "" },
            new Object[] { XNAColor.MediumAquamarine, ANXColor.MediumAquamarine, "" },
            new Object[] { XNAColor.MediumBlue, ANXColor.MediumBlue, "" },
            new Object[] { XNAColor.MediumOrchid, ANXColor.MediumOrchid, "" },
            new Object[] { XNAColor.MediumPurple, ANXColor.MediumPurple, "" },
            new Object[] { XNAColor.MediumSeaGreen, ANXColor.MediumSeaGreen, "" },
            new Object[] { XNAColor.MediumSlateBlue, ANXColor.MediumSlateBlue, "" },
            new Object[] { XNAColor.MediumSpringGreen, ANXColor.MediumSpringGreen, "" },
            new Object[] { XNAColor.MediumTurquoise, ANXColor.MediumTurquoise, "" },
            new Object[] { XNAColor.MediumVioletRed, ANXColor.MediumVioletRed, "" },
            new Object[] { XNAColor.MidnightBlue, ANXColor.MidnightBlue, "" },
            new Object[] { XNAColor.MintCream, ANXColor.MintCream, "" },
            new Object[] { XNAColor.MistyRose, ANXColor.MistyRose, "" },
            new Object[] { XNAColor.Moccasin, ANXColor.Moccasin, "" },
            new Object[] { XNAColor.NavajoWhite, ANXColor.NavajoWhite, "" },
            new Object[] { XNAColor.Navy, ANXColor.Navy, "" },
            new Object[] { XNAColor.OldLace, ANXColor.OldLace, "" },
            new Object[] { XNAColor.Olive, ANXColor.Olive, "" },
            new Object[] { XNAColor.OliveDrab, ANXColor.OliveDrab, "" },
            new Object[] { XNAColor.Orange, ANXColor.Orange, "" },
            new Object[] { XNAColor.OrangeRed, ANXColor.OrangeRed, "" },
            new Object[] { XNAColor.Orchid, ANXColor.Orchid, "" },
            new Object[] { XNAColor.PaleGoldenrod, ANXColor.PaleGoldenrod, "" },
            new Object[] { XNAColor.PaleGreen, ANXColor.PaleGreen, "" },
            new Object[] { XNAColor.PaleTurquoise, ANXColor.PaleTurquoise, "" },
            new Object[] { XNAColor.PaleVioletRed, ANXColor.PaleVioletRed, "" },
            new Object[] { XNAColor.PapayaWhip, ANXColor.PapayaWhip, "" },
            new Object[] { XNAColor.PeachPuff, ANXColor.PeachPuff, "" },
            new Object[] { XNAColor.Peru, ANXColor.Peru, "" },
            new Object[] { XNAColor.Pink, ANXColor.Pink, "" },
            new Object[] { XNAColor.Plum, ANXColor.Plum, "" },
            new Object[] { XNAColor.PowderBlue, ANXColor.PowderBlue, "" },
            new Object[] { XNAColor.Purple, ANXColor.Purple, "" },
            new Object[] { XNAColor.Red, ANXColor.Red, "" },
            new Object[] { XNAColor.RosyBrown, ANXColor.RosyBrown, "" },
            new Object[] { XNAColor.RoyalBlue, ANXColor.RoyalBlue, "" },
            new Object[] { XNAColor.SaddleBrown, ANXColor.SaddleBrown, "" },
            new Object[] { XNAColor.Salmon, ANXColor.Salmon, "" },
            new Object[] { XNAColor.SandyBrown, ANXColor.SandyBrown, "" },
            new Object[] { XNAColor.SeaGreen, ANXColor.SeaGreen, "" },
            new Object[] { XNAColor.SeaShell, ANXColor.SeaShell, "" },
            new Object[] { XNAColor.Sienna, ANXColor.Sienna, "" },
            new Object[] { XNAColor.Silver, ANXColor.Silver, "" },
            new Object[] { XNAColor.SkyBlue, ANXColor.SkyBlue, "" },
            new Object[] { XNAColor.SlateBlue, ANXColor.SlateBlue, "" },
            new Object[] { XNAColor.SlateGray, ANXColor.SlateGray, "" },
            new Object[] { XNAColor.Snow, ANXColor.Snow, "" },
            new Object[] { XNAColor.SpringGreen, ANXColor.SpringGreen, "" },
            new Object[] { XNAColor.SteelBlue, ANXColor.SteelBlue, "" },
            new Object[] { XNAColor.Tan, ANXColor.Tan, "" },
            new Object[] { XNAColor.Teal, ANXColor.Teal, "" },
            new Object[] { XNAColor.Thistle, ANXColor.Thistle, "" },
            new Object[] { XNAColor.Tomato, ANXColor.Tomato, "" },
            new Object[] { XNAColor.Transparent, ANXColor.Transparent, "" },
            new Object[] { XNAColor.Turquoise, ANXColor.Turquoise, "" },
            new Object[] { XNAColor.Violet, ANXColor.Violet, "" },
            new Object[] { XNAColor.Wheat, ANXColor.Wheat, "" },
            new Object[] { XNAColor.White, ANXColor.White, "" },
            new Object[] { XNAColor.WhiteSmoke, ANXColor.WhiteSmoke, "" },
            new Object[] { XNAColor.Yellow, ANXColor.Yellow, "" },
            new Object[] { XNAColor.YellowGreen, ANXColor.YellowGreen, "" },
        };

        #region Constructors
        [Test]
        public void constructor0()
        {
            XNAColor xna = new XNAColor();
            ANXColor anx = new ANXColor();

            AssertHelper.ConvertEquals(xna, anx, "constructor0");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor1(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(new Microsoft.Xna.Framework.Vector3(r, g, b));
            ANXColor anx = new ANXColor(new ANX.Framework.Vector3(r, g, b));

            AssertHelper.ConvertEquals(xna, anx, "constructor1");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor2(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(new Microsoft.Xna.Framework.Vector4(r, g, b, a));
            ANXColor anx = new ANXColor(new ANX.Framework.Vector4(r, g, b, a));

            AssertHelper.ConvertEquals(xna, anx, "constructor2");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor3(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(r, g, b);
            ANXColor anx = new ANXColor(r, g, b);

            AssertHelper.ConvertEquals(xna, anx, "constructor3");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor4(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor((int)(r * 255), (int)(g * 255), (int)(b * 255));
            ANXColor anx = new ANXColor((int)(r * 255), (int)(g * 255), (int)(b * 255));

            AssertHelper.ConvertEquals(xna, anx, "constructor4");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor5(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(r, g, b) * a;
            ANXColor anx = new ANXColor(r, g, b) * a;

            AssertHelper.ConvertEquals(xna, anx, "constructor5");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor6(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));
            ANXColor anx = new ANXColor((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));

            AssertHelper.ConvertEquals(xna, anx, "constructor6");
        }

        [Test]
        public void constructor7()
        {
            XNAColor xna = new XNAColor(512, 512, 512);
            ANXColor anx = new ANXColor(512, 512, 512);

            AssertHelper.ConvertEquals(xna, anx, "constructor7");
        }

        [Test, TestCaseSource("fourfloats")]
        public void constructor8(float r, float g, float b, float a)
        {
            XNAColor xna = new XNAColor(r, g, b, a);
            ANXColor anx = new ANXColor(r, g, b, a);

            AssertHelper.ConvertEquals(xna, anx, "constructor8");
        }

        [Test]
        public void constructor9()
        {
            XNAColor xna = new XNAColor(512, 512, 512, 512);
            ANXColor anx = new ANXColor(512, 512, 512, 512);

            AssertHelper.ConvertEquals(xna, anx, "constructor9");
        }

        #endregion

        #region Methods
        [Test, TestCaseSource("eightfloats")]
        public void EqualOperator(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            bool xna = xna1 == xna2;
            bool anx = anx1 == anx2;

            if (xna == anx)
            {
                Assert.Pass("EqualsOperator passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("eightfloats")]
        public void EqualOperator2(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            Object xna1 = new XNAColor(r1, g1, b1) * a1;
            Object xna2 = new XNAColor(r2, g2, b2) * a2;

            Object anx1 = new ANXColor(r1, g1, b1) * a1;
            Object anx2 = new ANXColor(r2, g2, b2) * a2;

            bool xna = xna1.Equals(xna2);
            bool anx = anx1.Equals(anx2);

            if (xna == anx)
            {
                Assert.Pass("EqualsOperator2 passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator2 failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("eightfloats")]
        public void EqualOperator3(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            Object xna1 = new XNAColor(r1, g1, b1) * a1;
            Object xna2 = new XNAColor(r2, g2, b2) * a2;

            Object anx1 = new ANXColor(r1, g1, b1) * a1;
            Object anx2 = new ANXColor(r2, g2, b2) * a2;

            bool xna = xna1.Equals(anx2);   // this is itentional!!!
            bool anx = anx1.Equals(xna2);   // this is itentional!!!

            if (xna == anx)
            {
                Assert.Pass("EqualsOperator3 passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator3 failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("eightfloats")]
        public void UnequalOperator(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2)
        {
            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            bool xna = xna1 != xna2;
            bool anx = anx1 != anx2;

            if (xna == anx)
            {
                Assert.Pass("UnequalsOperator passed");
            }
            else
            {
                Assert.Fail(String.Format("EqualsOperator failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void FromNonPremultipliedIntStatic(float r, float g, float b, float a)
        {
            XNAColor xna = XNAColor.FromNonPremultiplied((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));

            ANXColor anx = ANXColor.FromNonPremultiplied((int)(r * 255), (int)(g * 255), (int)(b * 255), (int)(a * 255));

            AssertHelper.ConvertEquals(xna, anx, "FromNonPremultipliedIntStatic");
        }

        [Test]
        public void FromNonPremultipliedIntStatic2()
        {
            XNAColor xna = XNAColor.FromNonPremultiplied(512, 512, 512, 512);
            ANXColor anx = ANXColor.FromNonPremultiplied(512, 512, 512, 512);

            AssertHelper.ConvertEquals(xna, anx, "FromNonPremultipliedIntStatic2");
        }

        [Test, TestCaseSource("fourfloats")]
        public void FromNonPremultipliedVector4Static(float r, float g, float b, float a)
        {
            XNAColor xna = XNAColor.FromNonPremultiplied(new Microsoft.Xna.Framework.Vector4(r, g, b, a));

            ANXColor anx = ANXColor.FromNonPremultiplied(new ANX.Framework.Vector4(r, g, b, a));

            AssertHelper.ConvertEquals(xna, anx, "FromNonPremultipliedVector4Static");
        }

        [Test, TestCaseSource("ninefloats")]
        public void LerpStatic(
            float r1, float g1, float b1, float a1,
            float r2, float g2, float b2, float a2, float amount)
        {
            amount = MathHelper.Clamp(amount, 0, 1);

            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            XNAColor xna = XNAColor.Lerp(xna1, xna2, amount);
            ANXColor anx = ANXColor.Lerp(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xna, anx, "LerpStatic");
        }

        [Test, TestCaseSource("ninefloats")]
        public void LerpStaticNaN(float r1, float g1, float b1, float a1,float r2, float g2, float b2, float a2, float nop)
        {
            float amount = float.NaN;

            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            XNAColor xna = XNAColor.Lerp(xna1, xna2, amount);
            ANXColor anx = ANXColor.Lerp(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xna, anx, "LerpStaticNan");
        }

        [Test, TestCaseSource("ninefloats")]
        public void LerpStaticNegative(float r1, float g1, float b1, float a1, float r2, float g2, float b2, float a2, float nop)
        {
            float amount = float.MinValue / 65536f;

            XNAColor xna1 = new XNAColor(r1, g1, b1) * a1;
            XNAColor xna2 = new XNAColor(r2, g2, b2) * a2;

            ANXColor anx1 = new ANXColor(r1, g1, b1) * a1;
            ANXColor anx2 = new ANXColor(r2, g2, b2) * a2;

            XNAColor xna = XNAColor.Lerp(xna1, xna2, amount);
            ANXColor anx = ANXColor.Lerp(anx1, anx2, amount);

            AssertHelper.ConvertEquals(xna, anx, "LerpStaticNegative");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyStatic(float r, float g, float b, float a, float scale, float x, float y, float z)
        {
            XNAColor xna = new XNAColor(r, g, b) * a;

            ANXColor anx = new ANXColor(r, g, b) * a;

            XNAColor.Multiply(xna, scale);
            ANXColor.Multiply(anx, scale);

            AssertHelper.ConvertEquals(xna, anx, "MultiplyStatic");
        }

        [Test, TestCaseSource("eightfloats")]
        public void MultiplyOperator(float r, float g, float b, float a, float scale, float x, float y, float z)
        {
            XNAColor xna = new XNAColor(r, g, b) * a;

            ANXColor anx = new ANXColor(r, g, b) * a;

            xna *= scale;
            anx *= scale;

            AssertHelper.ConvertEquals(xna, anx, "MultiplyOperator");
        }

        [Test, TestCaseSource("fourfloats")]
        public void ToVector3(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            Microsoft.Xna.Framework.Vector3 xna = xnaColor.ToVector3();
            ANX.Framework.Vector3 anx = anxColor.ToVector3();

            AssertHelper.ConvertEquals(xna, anx, "ToVector3");
        }

        [Test, TestCaseSource("fourfloats")]
        public void ToVector4(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            Microsoft.Xna.Framework.Vector4 xna = xnaColor.ToVector4();
            ANX.Framework.Vector4 anx = anxColor.ToVector4();

            AssertHelper.ConvertEquals(xna, anx, "ToVector4");
        }

        #endregion // Methods

        #region Properties

        [Test, TestCaseSource("fourfloats")]
        public void PackedValue(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            uint xna = xnaColor.PackedValue;
            uint anx = anxColor.PackedValue;

            if (xna.Equals(anx))
            {
                Assert.Pass("PackedValue passed");
            }
            else
            {
                Assert.Fail(String.Format("PackedValue failed: xna({0}) anx({1})", xna.ToString(), anx.ToString()));
            }
        }

        [Test, TestCaseSource("fourfloats")]
        public void R(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            AssertHelper.ConvertEquals(xnaColor.R, anxColor.R, "R");
        }

        [Test, TestCaseSource("fourfloats")]
        public void G(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            AssertHelper.ConvertEquals(xnaColor.G, anxColor.G, "G");
        }

        [Test, TestCaseSource("fourfloats")]
        public void B(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            AssertHelper.ConvertEquals(xnaColor.B, anxColor.B, "B");
        }

        [Test, TestCaseSource("fourfloats")]
        public void A(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor(r, g, b) * a;

            ANXColor anxColor = new ANXColor(r, g, b) * a;

            AssertHelper.ConvertEquals(xnaColor.A, anxColor.A, "A");
        }

        [Test, TestCaseSource("colors")]
        public void NamedColorProperties(XNAColor xnaColor, ANXColor anxColor, string name)
        {
            AssertHelper.ConvertEquals(xnaColor, anxColor, name);
        }

        [Test, TestCaseSource("fourfloats")]
        public void RSet(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor();
            ANXColor anxColor = new ANXColor();

            xnaColor.R = (byte)(r * 255);
            anxColor.R = (byte)(r * 255);

            AssertHelper.ConvertEquals(xnaColor.R, anxColor.R, "RSet");
        }

        [Test, TestCaseSource("fourfloats")]
        public void GSet(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor();
            ANXColor anxColor = new ANXColor();

            xnaColor.G = (byte)(r * 255);
            anxColor.G = (byte)(r * 255);

            AssertHelper.ConvertEquals(xnaColor.G, anxColor.G, "GSet");
        }

        [Test, TestCaseSource("fourfloats")]
        public void BSet(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor();
            ANXColor anxColor = new ANXColor();

            xnaColor.B = (byte)(r * 255);
            anxColor.B = (byte)(r * 255);

            AssertHelper.ConvertEquals(xnaColor.B, anxColor.B, "BSet");
        }

        [Test, TestCaseSource("fourfloats")]
        public void ASet(float r, float g, float b, float a)
        {
            XNAColor xnaColor = new XNAColor();
            ANXColor anxColor = new ANXColor();

            xnaColor.A = (byte)(r * 255);
            anxColor.A = (byte)(r * 255);

            AssertHelper.ConvertEquals(xnaColor.A, anxColor.A, "ASet");
        }

        #endregion
    }
}
