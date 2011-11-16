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
    public struct Vector4 : IEquatable<Vector4>
    {
        #region Fields

        public float X;
        public float Y;
        public float Z;
        public float W;     // THIS ORDER OF THE FIELDS IS IMPORTANT WHEN SETTING VERTEX-BUFFERS e.g.

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="Vector4"/> with all of its components set to one.
        /// </summary>
        public static Vector4 One
        {
            get
            {
                return new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }

        /// <summary>
        /// Returns the unit vector for the x-axis.
        /// </summary>
        public static Vector4 UnitX
        {
            get
            {
                return new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Returns the unit vector for the y-axis.
        /// </summary>
        public static Vector4 UnitY
        {
            get
            {
                return new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Returns the unit vector for the z-axis.
        /// </summary>
        public static Vector4 UnitZ
        {
            get
            {
                return new Vector4(0.0f, 0.0f, 1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Returns the unit vector for the w-axis.
        /// </summary>
        public static Vector4 UnitW
        {
            get
            {
                return new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }

        /// <summary>
        /// Returns a <see cref="Vector2"/> with both of its components set to zero.
        /// </summary>
        public static Vector4 Zero
        {
            get
            {
                return new Vector4(0.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        #endregion

        #region Constructors

        public Vector4(float value)
        {
            this.W = value;
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.W = w;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector4(Vector2 value, float z, float w)
        {
            this.W = w;
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        public Vector4(Vector3 value, float w)
        {
            this.W = w;
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
        }

        #endregion

        #region Public Static methods

        public static Vector4 Add(Vector4 value1, Vector4 value2)
        {
            Vector4 result;
            Vector4.Add(ref value1, ref value2, out result);
            return result;
        }

        public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
        {
            Vector4 result;
            Vector4.Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out result);
            return result;
        }

        public static void Barycentric(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, float amount1, float amount2, out Vector4 result)
        {
            result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
            result.Z = MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
            result.W = MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2);
        }

        public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
        {
            Vector4 result;
            Vector4.CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out result);
            return result;
        }

        public static void CatmullRom(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, float amount, out Vector4 result)
        {
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
            result.Z = MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
            result.W = MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount);
        }

        public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
        {
            Vector4 result;
            Vector4.Clamp(ref value1, ref min, ref max, out result);
            return result;
        }

        public static void Clamp(ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            result.X = MathHelper.Clamp(value1.X, min.X, max.X);
            result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
            result.Z = MathHelper.Clamp(value1.Z, min.Z, max.Z);
            result.W = MathHelper.Clamp(value1.W, min.W, max.W);
        }

        public static float Distance(Vector4 value1, Vector4 value2)
        {
            float result;
            Vector4.Distance(ref value1, ref value2, out result);
            return result;
        }

        public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            Vector4 tmp;
            Vector4.Subtract(ref value1, ref value2, out tmp);
            result = tmp.Length();
        }

        public static float DistanceSquared(Vector4 value1, Vector4 value2)
        {
            float result;
            Vector4.DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

        public static void DistanceSquared(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            Vector4 tmp;
            Vector4.Subtract(ref value1, ref value2, out tmp);
            result = tmp.LengthSquared();
        }

        public static Vector4 Divide(Vector4 value1, float divider)
        {
            Vector4 result;
            Vector4.Divide(ref value1, divider, out result);
            return result;
        }
        public static void Divide(ref Vector4 value1, float divider, out Vector4 result)
        {
            result.X = value1.X / divider;
            result.Y = value1.Y / divider;
            result.Z = value1.Z / divider;
            result.W = value1.W / divider;
        }

        public static Vector4 Divide(Vector4 value1, Vector4 value2)
        {
            Vector4 result;
            Vector4.Divide(ref value1, ref value2, out result);
            return result;
        }

        public static void Divide(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
            result.W = value1.W / value2.W;
        }

        public static float Dot(Vector4 vector1, Vector4 vector2)
        {
            float result;
            Vector4.Dot(ref vector1, ref vector2, out result);
            return result;
        }

        public static void Dot(ref Vector4 vector1, ref Vector4 vector2, out float result)
        {
            result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
        }

        public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
        {
            Vector4 result;
            Vector4.Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void Hermite(ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, float amount, out Vector4 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
            result.W = MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
        }

        public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
        {
            Vector4 result;
            Vector4.Lerp(ref value1, ref value2, amount, out result);
            return result;
        }

        public static void Lerp(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
        {
            result.X = MathHelper.Lerp(value1.X, value2.X, amount);
            result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
            result.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
            result.W = MathHelper.Lerp(value1.W, value2.W, amount);
        }

        public static Vector4 Max(Vector4 value1, Vector4 value2)
        {
            Vector4 result;
            Vector4.Max(ref value1, ref value2, out result);
            return result;
        }

        public static void Max(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = (value1.X > value2.X) ? value1.X : value2.X;
            result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
            result.Z = (value1.Z > value2.Z) ? value1.Z : value2.Z;
            result.W = (value1.W > value2.W) ? value1.W : value2.W;
        }

        public static Vector4 Min(Vector4 value1, Vector4 value2)
        {
            Vector4 result;
            Vector4.Min(ref value1, ref value2, out result);
            return result;
        }

        public static void Min(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = (value1.X < value2.X) ? value1.X : value2.X;
            result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
            result.Z = (value1.Z < value2.Z) ? value1.Z : value2.Z;
            result.W = (value1.W < value2.W) ? value1.W : value2.W;
        }

        public static Vector4 Multiply(Vector4 value1, float scaleFactor)
        {
            Vector4 result;
            Vector4.Multiply(ref value1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref Vector4 value1, float scaleFactor, out Vector4 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
            result.W = value1.W * scaleFactor;
        }

        public static Vector4 Multiply(Vector4 value1, Vector4 value2)
        {
            Vector4 result;
            Vector4.Multiply(ref value1, ref value2, out result);
            return result;
        }

        public static void Multiply(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
            result.W = value1.W * value2.W;
        }

        public static Vector4 Negate(Vector4 value)
        {
            Vector4 result;
            Vector4.Negate(ref value, out result);
            return result;
        }

        public static void Negate(ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        public static Vector4 Normalize(Vector4 vector)
        {
            Vector4 result;
            Vector4.Normalize(ref vector, out result);
            return result;
        }

        public static void Normalize(ref Vector4 vector, out Vector4 result)
        {
            float divider = 1f / vector.Length();
            result.X = vector.X * divider;
            result.Y = vector.Y * divider;
            result.Z = vector.Z * divider;
            result.W = vector.W * divider;
        }

        public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
        {
            Vector4 result;
            Vector4.SmoothStep(ref value1, ref value2, amount, out result);
            return result;
        }

        public static void SmoothStep(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
        {
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
            result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
            result.W = MathHelper.SmoothStep(value1.W, value2.W, amount);
        }

        public static Vector4 Subtract(Vector4 value1, Vector4 value2)
        {
            Vector4 result;
            Vector4.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static void Subtract(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
            result.W = value1.W - value2.W;
        }

        public static Vector4 Transform(Vector2 position, Matrix matrix)
        {
            throw new NotImplementedException(); ;
        }

        public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static Vector4 Transform(Vector2 value, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static Vector4 Transform(Vector3 position, Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static Vector4 Transform(Vector3 value, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static Vector4 Transform(Vector4 vector, Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector4 vector, ref Matrix matrix, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static Vector4 Transform(Vector4 value, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector4 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector4[] sourceArray, int sourceIndex, ref Matrix matrix, Vector4[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector4[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector4[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector4[] sourceArray, ref Matrix matrix, Vector4[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector4[] sourceArray, ref Quaternion rotation, Vector4[] destinationArray)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Methods

        public void Normalize()
        {
            float divider = 1f / this.Length();
            this.X *= divider;
            this.Y *= divider;
            this.Z *= divider;
            this.W *= divider;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
        }

        public override string ToString()
        {
            return "{X:" + this.X + " Y:" + this.Y + " Z:" + this.Z + " W:" + this.W + "}";
        }

        public float Length()
        {
            return (float)Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
        }

        public float LengthSquared()
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
        }

        #endregion

        #region IEquatable implementation

        public override bool Equals(Object obj)
        {
            return (obj is Vector4) ? this.Equals((Vector4)obj) : false;

        }

        public bool Equals(Vector4 other)
        {
            return this.W == other.W && this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        #endregion

        #region Operator Overloading

        public static Vector4 operator +(Vector4 value1, Vector4 value2)
        {
            return new Vector4(value1.X + value2.X, value1.Y + value2.Y, value1.Z + value2.Z, value1.W + value2.W);
        }

        public static Vector4 operator /(Vector4 value1, float divider)
        {
            float fak = 1.0f / divider;
            return new Vector4(value1.X * fak, value1.Y * fak, value1.Z * fak, value1.W * fak);
        }

        public static Vector4 operator /(Vector4 value1, Vector4 value2)
        {
            return new Vector4(value1.X / value2.X, value1.Y / value2.Y, value1.Z / value2.Z, value1.W / value2.W);
        }

        public static bool operator ==(Vector4 value1, Vector4 value2)
        {
            return value1.X.Equals(value2.X) && value1.Y.Equals(value2.Y) && value1.Z.Equals(value2.Z) && value1.W.Equals(value2.W);
        }

        public static bool operator !=(Vector4 value1, Vector4 value2)
        {
            return !value1.X.Equals(value2.X) || !value1.Y.Equals(value2.Y) || !value1.Z.Equals(value2.Z) || !value1.W.Equals(value2.W);
        }

        public static Vector4 operator *(float scaleFactor, Vector4 value)
        {
            return new Vector4(value.X * scaleFactor, value.Y * scaleFactor, value.Z * scaleFactor, value.W * scaleFactor);
        }

        public static Vector4 operator *(Vector4 value, float scaleFactor)
        {
            return new Vector4(value.X * scaleFactor, value.Y * scaleFactor, value.Z * scaleFactor, value.W * scaleFactor);
        }

        public static Vector4 operator *(Vector4 value1, Vector4 value2)
        {
            return new Vector4(value1.X * value2.X, value1.Y * value2.Y, value1.Z * value2.Z, value1.W * value2.W);
        }

        public static Vector4 operator -(Vector4 value1, Vector4 value2)
        {
            return new Vector4(value1.X - value2.X, value1.Y - value2.Y, value1.Z - value2.Z, value1.W - value2.W);
        }

        public static Vector4 operator -(Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        #endregion
    }
}
