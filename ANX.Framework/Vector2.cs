using System;
using System.Globalization;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [Developer("???")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    public struct Vector2 : IEquatable<Vector2>
    {
        #region fields
        public float X;
        public float Y;
        #endregion

        #region properties
        #region One
        /// <summary>
        /// Returns a <see cref="Vector2"/> with both of its components set to one.
        /// </summary>
        public static Vector2 One
        {
            get
            {
                return new Vector2(1f, 1f);
            }
        }
        #endregion

        #region Zero
        /// <summary>
        /// Returns a <see cref="Vector2"/> with both of its components set to zero.
        /// </summary>
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0f, 0f);
            }
        }
        #endregion

        #region UnitX
        /// <summary>
        /// Returns the unit vector for the x-axis.
        /// </summary>
        public static Vector2 UnitX
        {
            get
            {
                return new Vector2(1f, 0f);
            }
        }
        #endregion

        #region UnitY
        /// <summary>
        /// Returns the unit vector for the y-axis.
        /// </summary>
        public static Vector2 UnitY
        {
            get
            {
                return new Vector2(0f, 1f);
            }
        }
        #endregion
        #endregion

        #region constructors
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
        #region Add
        public static Vector2 Add(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Add(ref value1, ref value2, out result);
            return result;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2,
                    out Vector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }
        #endregion

        #region Barycentric
        public static Vector2 Barycentric(Vector2 value1, Vector2 value2,
            Vector2 value3, float amount1, float amount2)
        {
            Vector2 result;
            Vector2.Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out result);
            return result;
        }

        public static void Barycentric(ref Vector2 value1, ref Vector2 value2,
                    ref Vector2 value3, float amount1, float amount2, out Vector2 result)
        {
            result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
        }
        #endregion

        #region CatmullRom
        public static Vector2 CatmullRom(Vector2 value1, Vector2 value2,
            Vector2 value3, Vector2 value4, float amount)
        {
            Vector2 result;
            Vector2.CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out result);
            return result;
        }

        public static void CatmullRom(ref Vector2 value1, ref Vector2 value2,
                    ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
        {
            result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
        }
        #endregion

        #region Clamp
        public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
        {
            Vector2 result;
            Vector2.Clamp(ref value1, ref min, ref max, out result);
            return result;
        }

        public static void Clamp(ref Vector2 value1, ref Vector2 min,
                    ref Vector2 max, out Vector2 result)
        {
            result.X = MathHelper.Clamp(value1.X, min.X, max.X);
            result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
        }
        #endregion

        #region Distance
        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float result;
            Vector2.Distance(ref value1, ref value2, out result);
            return result;
        }

        public static void Distance(ref Vector2 value1, ref Vector2 value2,
                    out float result)
        {
            Vector2 tmp;
            Vector2.Subtract(ref value1, ref value2, out tmp);
            result = tmp.Length();
        }
        #endregion

        #region DistanceSquared
        public static float DistanceSquared(Vector2 value1, Vector2 value2)
        {
            float result;
            Vector2.DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

        public static void DistanceSquared(ref Vector2 value1,
                    ref Vector2 value2, out float result)
        {
            Vector2 tmp;
            Vector2.Subtract(ref value1, ref value2, out tmp);
            result = tmp.LengthSquared();
        }
        #endregion

        #region Divide
        public static Vector2 Divide(Vector2 value1, float divider)
        {
            Vector2 result;
            Vector2.Divide(ref value1, divider, out result);
            return result;
        }

        public static void Divide(ref Vector2 value1, float divider,
                    out Vector2 result)
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

        public static void Divide(ref Vector2 value1, ref Vector2 value2,
                    out Vector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }
        #endregion

        #region Dot
        public static float Dot(Vector2 value1, Vector2 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }

        public static void Dot(ref Vector2 value1, ref Vector2 value2,
                    out float result)
        {
            result = value1.X * value2.X + value1.Y * value2.Y;
        }
        #endregion

        #region GetHashCode
        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode();
        }
        #endregion

        #region Hermite
        public static Vector2 Hermite(Vector2 value1, Vector2 tangent1,
            Vector2 value2, Vector2 tangent2, float amount)
        {
            Vector2 result;
            Vector2.Hermite(ref value1, ref tangent1, ref value2, ref tangent2,
                            amount, out result);
            return result;
        }

        public static void Hermite(ref Vector2 value1, ref Vector2 tangent1,
                    ref Vector2 value2, ref Vector2 tangent2, float amount,
                    out Vector2 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X,
                            tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y,
                            tangent2.Y, amount);
        }
        #endregion

        #region Length
        public float Length()
        {
            return (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y));
        }
        #endregion

        #region LengthSquared
        public float LengthSquared()
        {
            return (this.X * this.X) + (this.Y * this.Y);
        }
        #endregion

        #region Lerp
        public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
        {
            Vector2 result;
            Vector2.Lerp(ref value1, ref value2, amount, out result);
            return result;
        }

        public static void Lerp(ref Vector2 value1, ref Vector2 value2,
                    float amount, out Vector2 result)
        {
            result.X = value1.X + (value2.X - value1.X) * amount;
            result.Y = value1.Y + (value2.Y - value1.Y) * amount;
        }
        #endregion

        #region Max
        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Max(ref value1, ref value2, out result);
            return result;
        }

        public static void Max(ref Vector2 value1, ref Vector2 value2,
                    out Vector2 result)
        {
            result.X = (value1.X > value2.X) ? value1.X : value2.X;
            result.Y = (value1.Y > value2.Y) ? value1.Y : value2.Y;
        }
        #endregion

        #region Min
        public static Vector2 Min(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Min(ref value1, ref value2, out result);
            return result;
        }

        public static void Min(ref Vector2 value1, ref Vector2 value2,
                    out Vector2 result)
        {
            result.X = (value1.X < value2.X) ? value1.X : value2.X;
            result.Y = (value1.Y < value2.Y) ? value1.Y : value2.Y;
        }
        #endregion

        #region Multiply
        public static Vector2 Multiply(Vector2 value1, float scaleFactor)
        {
            Vector2 result;
            Vector2.Multiply(ref value1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref Vector2 value1, float scaleFactor,
                    out Vector2 result)
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

        public static void Multiply(ref Vector2 value1, ref Vector2 value2,
                    out Vector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }
        #endregion

        #region Negate
        public static Vector2 Negate(Vector2 value)
        {
            // Is a bit faster than copying the vector floats to the operator method.
            return new Vector2(-value.X, -value.Y);
        }

        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            // Is a bit faster than copying the vector floats to the operator method.
            result.X = -value.X;
            result.Y = -value.Y;
        }
        #endregion

        #region Normalize
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
        #endregion

        /*
				 Vect2 = Vect1 - 2 * WallN * (WallN DOT Vect1)
				 Formula from : http://www.gamedev.net/topic/165537-2d-vector-reflection-/
				 */
        #region Reflect
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
        #endregion

        #region SmoothStep
        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2,
            float amount)
        {
            Vector2 result;
            Vector2.SmoothStep(ref value1, ref value2, amount, out result);
            return result;
        }

        public static void SmoothStep(ref Vector2 value1, ref Vector2 value2,
                    float amount, out Vector2 result)
        {
            result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
        }
        #endregion

        #region Subtract
        public static Vector2 Subtract(Vector2 value1, Vector2 value2)
        {
            Vector2 result;
            Vector2.Subtract(ref value1, ref value2, out result);
            return result;
        }

        public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            var culture = CultureInfo.CurrentCulture;
            // This may look a bit more ugly, but String.Format should
            // be avoided cause of it's bad performance!
            return "{X:" + X.ToString(culture) + " Y:" + Y.ToString(culture) + "}";
        }
        #endregion

        #region Transform
        public static Vector2 Transform(Vector2 position, Matrix matrix)
        {
            Vector2 result;
            Transform(ref position, ref matrix, out result);
            return result;
        }

        public static void Transform(ref Vector2 position, ref Matrix matrix,
                    out Vector2 result)
        {
            result.X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
            result.Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
        }

        public static Vector2 Transform(Vector2 value, Quaternion rotation)
        {
            Vector2 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        public static void Transform(ref Vector2 value, ref Quaternion rotation,
                    out Vector2 result)
        {
            float x = 2 * (-rotation.Z * value.Y);
            float y = 2 * (rotation.Z * value.X);
            float z = 2 * (rotation.X * value.Y - rotation.Y * value.X);

            result.X = value.X + x * rotation.W + (rotation.Y * z - rotation.Z * y);
            result.Y = value.Y + y * rotation.W + (rotation.Z * x - rotation.X * z);
        }

        public static void Transform(Vector2[] sourceArray, int sourceIndex,
                    ref Matrix matrix, Vector2[] destinationArray, int destinationIndex,
                    int length)
        {
            length += sourceIndex;
            for (int i = sourceIndex; i < length; i++, destinationIndex++)
            {
                Transform(ref sourceArray[i], ref matrix,
                    out destinationArray[destinationIndex]);
            }
        }

        public static void Transform(Vector2[] sourceArray, int sourceIndex,
                    ref Quaternion rotation, Vector2[] destinationArray,
                    int destinationIndex, int length)
        {
            length += sourceIndex;
            for (int i = sourceIndex; i < length; i++, destinationIndex++)
            {
                Transform(ref sourceArray[i], ref rotation,
                    out destinationArray[destinationIndex]);
            }
        }

        public static void Transform(Vector2[] sourceArray, ref Matrix matrix,
                    Vector2[] destinationArray)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                Transform(ref sourceArray[i], ref matrix, out destinationArray[i]);
            }
        }

        public static void Transform(Vector2[] sourceArray, ref Quaternion rotation,
                    Vector2[] destinationArray)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                Transform(ref sourceArray[i], ref rotation, out destinationArray[i]);
            }
        }
        #endregion

        #region TransformNormal
        public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
        {
            Vector2 result;
            TransformNormal(ref normal, ref matrix, out result);
            return result;
        }

        public static void TransformNormal(ref Vector2 normal, ref Matrix matrix,
                    out Vector2 result)
        {
            result.X = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
            result.Y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
        }

        public static void TransformNormal(Vector2[] sourceArray,
                    int sourceIndex, ref Matrix matrix, Vector2[] destinationArray,
                    int destinationIndex, int length)
        {
            length += sourceIndex;
            for (int i = sourceIndex; i < length; i++, destinationIndex++)
            {
                TransformNormal(ref sourceArray[i], ref matrix,
                    out destinationArray[destinationIndex]);
            }
        }

        public static void TransformNormal(Vector2[] sourceArray,
                    ref Matrix matrix, Vector2[] destinationArray)
        {
            for (int i = 0; i < sourceArray.Length; i++)
            {
                TransformNormal(ref sourceArray[i], ref matrix,
                    out destinationArray[i]);
            }
        }
        #endregion
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
            float factor = 1.0f / divider;
            return new Vector2(value1.X * factor, value1.Y * factor);
        }
        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            return new Vector2(value1.X / value2.X, value1.Y / value2.Y);
        }

        public static bool operator ==(Vector2 value1, Vector2 value2)
        {
            return (value1.X == value2.X) && (value1.Y == value2.Y);
        }
        public static bool operator !=(Vector2 value1, Vector2 value2)
        {
            return (value1.X != value2.X) || (value1.Y != value2.Y);
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
