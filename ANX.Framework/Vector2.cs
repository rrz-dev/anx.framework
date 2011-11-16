#region Using Statements
using System;

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

namespace ANX.Framework
{
    public struct Vector2 : IEquatable<Vector2>
    {
        #region fields
        public float X;
        public float Y;
        #endregion


        #region properties
        #region One
        private static readonly Vector2 privateOne;
        /// <summary>
        /// Returns a <see cref="Vector2"/> with both of its components set to one.
        /// </summary>
        public static Vector2 One
        {
            get
            {
                return privateOne;
            }
        }
        #endregion

        #region Zero
        private static readonly Vector2 privateZero;
        /// <summary>
        /// Returns a <see cref="Vector2"/> with both of its components set to zero.
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                return privateZero;
            }
        }
        #endregion

        #region UnitX
        private static readonly Vector2 privateUnitX;
        /// <summary>
        /// Returns the unit vector for the x-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get
            {
                return privateUnitX;
            }
        }
        #endregion

        #region UnitY
        private static readonly Vector2 privateUnitY;
        /// <summary>
        /// Returns the unit vector for the y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get
            {
                return privateUnitY;
            }
        }
        #endregion
        #endregion


        #region constructors
        static Vector2()
        {
            privateOne = new Vector2(1.0f, 1.0f);
            privateUnitX = new Vector2(1.0f, 0.0f);
            privateUnitY = new Vector2(0.0f, 1.0f);
            privateZero = new Vector2(0.0f, 0.0f);
        }

        public Vector2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        #endregion


        #region public methods

        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Add(ref value1, ref value2, out result);
            return result;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
        {
            Vector2 result;
            Vector2.Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out result);
            return result;
        }
        public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
        {
            result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
        }




        public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
        {
            Vector2 result;
            Vector2.CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out result);
            return result;
        }
        public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
        {
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
        }

        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            Vector2 result;
            Vector2.Clamp(ref value1, ref min, ref max, out result);
            return result;
        }
        public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            result.X = MathHelper.Clamp(value1.X, min.X, max.X);
            result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
        }

        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float result;
            Vector2.Distance(ref value1, ref value2, out result);
            return result;
        }
        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            Vector2 tmp;
            Vector2.Subtract(ref value1, ref value2, out tmp);
            result = tmp.Length();
        }

        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float result;
            Vector2.DistanceSquared(ref value1, ref value2, out result);
            return result;
        }
        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            Vector2 tmp;
            Vector2.Subtract(ref value1, ref value2, out tmp);
            result = tmp.LengthSquared();
        }

        public static Vector2 Divide(Vector2 value1, float divider)
        {
            Vector2 result;
            Vector2.Divide(ref value1, divider, out result);
            return result;
        }
        public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
        {
            divider = 1f / divider;
            result.X = value1.X * divider;
            result.Y = value1.Y * divider;
        }
        public static Vector2 Divide(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Divide(ref value1, ref value2, out result);
            return result;
        }
        public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        public static float Dot(Vector2 value1, Vector2 value2)
        {
            float result;
            Vector2.Dot(ref value1, ref value2, out result);
            return result;
        }
        public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            result = value1.X * value2.X + value1.Y * value2.Y;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode();
        }

        public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
        {
            Vector2 result;
            Vector2.Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }
        public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        public float Length()
        {
            return (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y));
        }

        public float LengthSquared()
        {
            return (this.X * this.X) + (this.Y * this.Y);
        }

        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            Vector2 result;
            Vector2.Lerp(ref value1, ref value2, amount, out result);
            return result;
        }
        public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result.X = MathHelper.Lerp(value1.X, value2.X, amount);
            result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
        }

        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Max(ref value1, ref value2, out result);
            return result;

        }
        public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = (value1.X > value2.X) ? value1.X : value2.X;
            result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
        }

        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Min(ref value1, ref value2, out result);
            return result;
        }
        public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = (value1.X < value2.X) ? value1.X : value2.X;
            result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
        }

        public static Vector2 Multiply(Vector2 value1, float scaleFactor)
        {
            Vector2 result;
            Vector2.Multiply(ref value1, scaleFactor, out result);
            return result;
        }
        public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
        }
        public static Vector2 Multiply(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Multiply(ref value1, ref value2, out result);
            return result;
        }
        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        public static Vector2 Negate(Vector2 value)
        {
            Vector2 result;
            Vector2.Negate(ref value, out result);
            return result;
        }
        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result = -value;
        }

        public void Normalize()
        {
            float divider = 1f / this.Length();
            this.X *= divider;
            this.Y *= divider;
        }
        public static Vector2 Normalize(Vector2 value)
        {
            float divider = 1f / value.Length();
            return new Vector2(value.X * divider, value.Y * divider);
        }
        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            float divider = 1f / value.Length();
            result.X = value.X * divider;
            result.Y = value.Y * divider;
        }
        /*
         Vect2 = Vect1 - 2 * WallN * (WallN DOT Vect1)
         Formula from : http://www.gamedev.net/topic/165537-2d-vector-reflection-/
         */


        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Vector2 result;
            Vector2.Reflect(ref vector, ref normal, out result);
            return result;
        }
        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            float sub = 2 * Vector2.Dot(vector, normal);
            result.X = vector.X - (sub * normal.X);
            result.Y = vector.Y - (sub * normal.Y);
        }

        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
        {
            Vector2 result;
            Vector2.SmoothStep(ref value1, ref value2, amount, out result);
            return result;
        }
        public static void SmoothStep(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
        {
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        }

        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Subtract(ref value1, ref value2, out result);
            return result;
        }
        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result = value1 - value2;
        }



        public override string ToString()
        {
            return "{X:" + this.X + " Y:" + this.Y + "}";
        }

        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            return new Vector2(((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41,
                               ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42);
        }

        public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
        {
            result.X = ((position.X * matrix.M11) + (position.Y * matrix.M21)) + matrix.M41;
            result.Y = ((position.X * matrix.M12) + (position.Y * matrix.M22)) + matrix.M42;
        }

        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            throw new NotImplementedException();
        }
        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector2 result)
        {
            throw new NotImplementedException();
        }
        public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }
        public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector2[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }
        public static void Transform(Vector2[] sourceArray, ref Matrix matrix, Vector2[] destinationArray)
        {
            throw new NotImplementedException();
        }
        public static void Transform(Vector2[] sourceArray, ref Quaternion rotation, Vector2[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(
            Vector2[] sourceArray,
            int sourceIndex,
            ref Matrix matrix,
            Vector2[] destinationArray,
            int destinationIndex,
            int length)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(
            Vector2[] sourceArray,
            ref Matrix matrix,
            Vector2[] destinationArray)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is Vector2) ? this.Equals((Vector2)obj) : false;
        }
        public bool Equals(Vector2 other)
        {
            return this.X == other.X && this.Y == other.Y;
        }
        #endregion


        #region operator overloading
        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X + value2.X, value1.Y + value2.Y);
        }

        public static Vector2 operator /(Vector2 value1, float divider)
        {
            float fak = 1.0f / divider;
            return new Vector2(value1.X * fak, value1.Y * fak);
        }
        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X / value2.X, value1.Y / value2.Y);
        }

        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return value1.X.Equals(value2.X) && value1.Y.Equals(value2.Y);
        }
        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return !value1.X.Equals(value2.X) || !value1.Y.Equals(value2.Y);
        }

        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            return new Vector2(value.X * scaleFactor, value.Y * scaleFactor);
        }
        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            return new Vector2(value.X * scaleFactor, value.Y * scaleFactor);
        }
        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X * value2.X, value1.Y * value2.Y);
        }

        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X - value2.X, value1.Y - value2.Y);
        }
        public static Vector2 operator -(Vector2 value)
        {
            return new Vector2(-value.X, -value.Y);
        }
        #endregion

    }
}
