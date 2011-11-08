#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

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

#region Datatype Usings
using XNAColor = Microsoft.Xna.Framework.Color;
using ANXColor = ANX.Framework.Color;

using XNAVector2 = Microsoft.Xna.Framework.Vector2;
using ANXVector2 = ANX.Framework.Vector2;

using XNAVector3 = Microsoft.Xna.Framework.Vector3;
using ANXVector3 = ANX.Framework.Vector3;

using XNAVector4 = Microsoft.Xna.Framework.Vector4;
using ANXVector4 = ANX.Framework.Vector4;

using XNABoundingBox = Microsoft.Xna.Framework.BoundingBox;
using ANXBoundingBox = ANX.Framework.BoundingBox;

using XNABoundingSphere = Microsoft.Xna.Framework.BoundingSphere;
using ANXBoundingSphere = ANX.Framework.BoundingSphere;

using XNABoundingFrustum = Microsoft.Xna.Framework.BoundingFrustum;
using ANXBoundingFrustum = ANX.Framework.BoundingFrustum;

using XNAPlane = Microsoft.Xna.Framework.Plane;
using ANXPlane = ANX.Framework.Plane;

using XNARect = Microsoft.Xna.Framework.Rectangle;
using ANXRect = ANX.Framework.Rectangle;

using XNABgr565 = Microsoft.Xna.Framework.Graphics.PackedVector.Bgr565;
using ANXBgr565 = ANX.Framework.Graphics.PackedVector.Bgr565;

using XNABgra5551 = Microsoft.Xna.Framework.Graphics.PackedVector.Bgra5551;
using ANXBgra5551 = ANX.Framework.Graphics.PackedVector.Bgra5551;

using XNABgra4444 = Microsoft.Xna.Framework.Graphics.PackedVector.Bgra4444;
using ANXBgra4444 = ANX.Framework.Graphics.PackedVector.Bgra4444;

using XNAByte4 = Microsoft.Xna.Framework.Graphics.PackedVector.Byte4;
using ANXByte4 = ANX.Framework.Graphics.PackedVector.Byte4;

using XNAHalfSingle = Microsoft.Xna.Framework.Graphics.PackedVector.HalfSingle;
using ANXHalfSingle = ANX.Framework.Graphics.PackedVector.HalfSingle;

using XNAHalfVector2 = Microsoft.Xna.Framework.Graphics.PackedVector.HalfVector2;
using ANXHalfVector2 = ANX.Framework.Graphics.PackedVector.HalfVector2;

using XNAHalfVector4 = Microsoft.Xna.Framework.Graphics.PackedVector.HalfVector4;
using ANXHalfVector4 = ANX.Framework.Graphics.PackedVector.HalfVector4;

using XNARg32 = Microsoft.Xna.Framework.Graphics.PackedVector.Rg32;
using ANXRg32 = ANX.Framework.Graphics.PackedVector.Rg32;

using XNARgba1010102 = Microsoft.Xna.Framework.Graphics.PackedVector.Rgba1010102;
using ANXRgba1010102 = ANX.Framework.Graphics.PackedVector.Rgba1010102;

using XNARgba64 = Microsoft.Xna.Framework.Graphics.PackedVector.Rgba64;
using ANXRgba64 = ANX.Framework.Graphics.PackedVector.Rgba64;

using XNANormalizedByte2 = Microsoft.Xna.Framework.Graphics.PackedVector.NormalizedByte2;
using ANXNormalizedByte2 = ANX.Framework.Graphics.PackedVector.NormalizedByte2;

using XNANormalizedByte4 = Microsoft.Xna.Framework.Graphics.PackedVector.NormalizedByte4;
using ANXNormalizedByte4 = ANX.Framework.Graphics.PackedVector.NormalizedByte4;

#endregion // Datatype usings

namespace ANX.Framework.TestCenter
{
    class AssertHelper
    {
        public static void ConvertEquals(XNABgr565 lhs, ANXBgr565 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Bgr565 XNA: ({1}) Bgr565 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNABgra5551 lhs, ANXBgra5551 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Bgra5551 XNA: ({1}) Bgra5551 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNABgra4444 lhs, ANXBgra4444 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Bgra4444 XNA: ({1}) Bgra4444 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNAByte4 lhs, ANXByte4 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Byte4 XNA: ({1}) Byte4 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNAHalfSingle lhs, ANXHalfSingle rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: HalfSingle XNA: ({1}) HalfSingle ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNAHalfVector2 lhs, ANXHalfVector2 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: HalfVector2 XNA: ({1}) HalfVector2 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNAHalfVector4 lhs, ANXHalfVector4 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: HalfVector4 XNA: ({1}) HalfVector4 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNARg32 lhs, ANXRg32 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Rg32 XNA: ({1}) Rg32 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNARgba1010102 lhs, ANXRgba1010102 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Rgba1010102 XNA: ({1}) Rgba1010102 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNARgba64 lhs, ANXRgba64 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: Rgba64 XNA: ({1}) Rgba64 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNANormalizedByte2 lhs, ANXNormalizedByte2 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: NormalizedByte2 XNA: ({1}) NormalizedByte2 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(XNANormalizedByte4 lhs, ANXNormalizedByte4 rhs, String test)
        {
            if (lhs.PackedValue == rhs.PackedValue)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0] failed: NormalizedByte4 XNA: ({1}) NormalizedByte4 ANX: ({2})", test, lhs, rhs));
            }
        }

        public static void ConvertEquals(float a, float b, String test)
        {
            if (a.Equals(b))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: float a: ({1}) float b: ({2})", test, a, b));
            }
        }

        public static void ConvertEquals(byte a, byte b, String test)
        {
            if (a == b)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: byte a: ({1}) byte b: ({2})", test, a, b));
            }
        }

        public static void ConvertEquals(XNAColor xna, ANXColor anx, String test)
        {
            if (xna.R == anx.R &&
                xna.G == anx.G &&
                xna.B == anx.B &&
                xna.A == anx.A)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
            }
        }

        public static void ConvertEquals(XNAVector2 xna, ANXVector2 anx, String test)
        {
            //comparing string to catch "not defined" and "infinity" (which seems not to be equal)
            if (xna.X.ToString().Equals(anx.X.ToString()) && xna.Y.ToString().Equals(anx.Y.ToString()))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(test + " failed: xna({" + xna.X + "}{" + xna.Y + "}) anx({" + anx.X + "}{" + anx.Y + "})");
            }
        }

        public static void ConvertEquals(ANXVector2 xna, ANXVector2 anx, String test)
        {
            //comparing string to catch "not defined" and "infinity" (which seems not to be equal)
            if (anx.X.ToString().Equals(anx.X.ToString()) && anx.Y.ToString().Equals(anx.Y.ToString()))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(test + " failed: anx({" + xna.X + "}{" + xna.Y + "}) compared to anx({" + anx.X + "}{" + anx.Y + "})");
            }
        }

        public static void ConvertEquals(XNAVector3 xna, ANXVector3 anx, String test)
        {
            //comparing string to catch "not defined" and "infinity" (which seems not to be equal)
            if ((xna.X == anx.X) && (xna.Y == anx.Y) && (xna.Z == anx.Z))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(string.Format("{0} failed: xna({1},{2},{3}) anx({4},{5},{6})", test, xna.X, xna.Y, xna.Z, anx.X, anx.Y, anx.Z));
            }
        }

        public static void ConvertEquals(XNAVector4 xna, ANXVector4 anx, String test)
        {
            //comparing string to catch "not defined" and "infinity" (which seems not to be equal)
            if ((xna.X == anx.X) && (xna.Y == anx.Y) && (xna.Z == anx.Z) && (xna.W == anx.W))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(string.Format("{0} failed: xna({1},{2},{3},{4}) anx({5},{6},{7},{8})", test, xna.X, xna.Y, xna.Z, xna.W, anx.X, anx.Y, anx.Z, anx.W));
            }
        }

        public static void ConvertEquals(XNABoundingBox xna, ANXBoundingBox anx, String test)
        {
            if (xna.Min.X == anx.Min.X &&
                xna.Min.Y == anx.Min.Y &&
                xna.Min.Z == anx.Min.Z &&
                xna.Max.X == anx.Max.X &&
                xna.Max.Y == anx.Max.Y &&
                xna.Max.Z == anx.Max.Z)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
            }
        }

        public static void ConvertEquals(XNABoundingSphere xna, ANXBoundingSphere anx, String test)
        {
            if (xna.Center.X == anx.Center.X &&
                xna.Center.Y == anx.Center.Y &&
                xna.Center.Z == anx.Center.Z &&
                xna.Radius == anx.Radius)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
            }
        }

        public static void ConvertEquals(XNARect xna, ANXRect anx, String test)
        {
            //comparing string to catch "not defined" and "infinity" (which seems not to be equal)
            if (xna.ToString().Equals(anx.ToString()))
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(test + " failed: xna({" + xna.X + "}{" + xna.Y + "}{" + xna.Width + "}{" + xna.Height + "}) anx({" + anx.X + "}{" + anx.Y + "}{" + anx.Width + "}{" + anx.Height + "})");
            }
        }

        public static void ConvertEquals(XNABoundingFrustum xna, ANXBoundingFrustum anx, String test)
        {
            if (xna.Matrix.M11 == anx.Matrix.M11 &&
                xna.Matrix.M12 == anx.Matrix.M12 &&
                xna.Matrix.M13 == anx.Matrix.M13 &&
                xna.Matrix.M14 == anx.Matrix.M14 &&
                xna.Matrix.M21 == anx.Matrix.M21 &&
                xna.Matrix.M22 == anx.Matrix.M22 &&
                xna.Matrix.M23 == anx.Matrix.M23 &&
                xna.Matrix.M24 == anx.Matrix.M24 &&
                xna.Matrix.M31 == anx.Matrix.M31 &&
                xna.Matrix.M32 == anx.Matrix.M32 &&
                xna.Matrix.M33 == anx.Matrix.M33 &&
                xna.Matrix.M34 == anx.Matrix.M34 &&
                xna.Matrix.M41 == anx.Matrix.M41 &&
                xna.Matrix.M42 == anx.Matrix.M42 &&
                xna.Matrix.M43 == anx.Matrix.M43 &&
                xna.Matrix.M44 == anx.Matrix.M44)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
            }
        }

        public static void ConvertEquals(XNAPlane xna, ANXPlane anx, String test)
        {
            if (xna.D == anx.D &&
                xna.Normal.X == anx.Normal.X &&
                xna.Normal.Y == anx.Normal.Y &&
                xna.Normal.Z == anx.Normal.Z)
            {
                Assert.Pass(test + " passed");
            }
            else
            {
                Assert.Fail(String.Format("{0} failed: xna({1}) anx({2})", test, xna.ToString(), anx.ToString()));
            }
        }
    }
}
