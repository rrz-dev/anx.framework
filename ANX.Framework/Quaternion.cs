#region Using Statements
using System;
using System.Globalization;
#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    public struct Quaternion : IEquatable<Quaternion>
    {
        #region fields
        public float X;
        public float Y;
        public float Z;
        public float W;
        #endregion


        #region properties
        public static Quaternion Identity
        {
            get
            {
                return new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
        #endregion


        #region constructors
        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(Vector3 vectorPart, float scalarPart)
        {
            this.X = vectorPart.X;
            this.Y = vectorPart.Y;
            this.Z = vectorPart.Z;
            this.W = scalarPart;
        }
        #endregion


        #region public methods
        public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Add(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result.X = quaternion1.X + quaternion2.X;
            result.Y = quaternion1.Y + quaternion2.Y;
            result.Z = quaternion1.Z + quaternion2.Z;
            result.W = quaternion1.W + quaternion2.W;
        }

        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            Quaternion result;
            Quaternion.Concatenate(ref value1, ref value2, out result);
            return result;
        }

        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            Multiply(ref value2, ref value1, out result);
        }

        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion result;
            Quaternion.Conjugate(ref value, out result);
            return result;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            Quaternion result;
            Quaternion.CreateFromAxisAngle(ref axis, angle, out result);
            return result;
        }

        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            angle *= 0.5f;
            float sinAngle = (float)Math.Sin(angle);
            float cosAngle = (float)Math.Cos(angle);

            result.X = axis.X * sinAngle;
            result.Y = axis.Y * sinAngle;
            result.Z = axis.Z * sinAngle;
            result.W = cosAngle;
        }

        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            Quaternion result;
            Quaternion.CreateFromRotationMatrix(ref matrix, out result);
            return result;
        }

        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            // http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm

            float tr = matrix.M11 + matrix.M22 + matrix.M33;
            if (tr > 0f)
            {
                float s = (float)Math.Sqrt(tr + 1f);
                result.W = s * 0.5f;
                s = 0.5f / s;
                result.X = (matrix.M23 - matrix.M32) * s;
                result.Y = (matrix.M31 - matrix.M13) * s;
                result.Z = (matrix.M12 - matrix.M21) * s;
            }
            else if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                float s = (float)Math.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
                float s2 = 0.5f / s;
                result.W = (matrix.M23 - matrix.M32) * s2;
                result.X = 0.5f * s;
                result.Y = (matrix.M12 + matrix.M21) * s2;
                result.Z = (matrix.M13 + matrix.M31) * s2;
            }
            else if (matrix.M22 > matrix.M33)
            {
                float s = (float)Math.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
                float s2 = 0.5f / s;
                result.W = (matrix.M31 - matrix.M13) * s2;
                result.X = (matrix.M21 + matrix.M12) * s2;
                result.Y = 0.5f * s;
                result.Z = (matrix.M32 + matrix.M23) * s2;
            }
            else
            {
                float s = (float)Math.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
                float ss = 0.5f / s;
                result.W = (matrix.M12 - matrix.M21) * ss;
                result.X = (matrix.M31 + matrix.M13) * ss;
                result.Y = (matrix.M32 + matrix.M23) * ss;
                result.Z = 0.5f * s;
            }
        }

        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion result;
            Quaternion.CreateFromYawPitchRoll(yaw,pitch,roll, out result);
            return result;
        }

        public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            Vector3 yawAxis = Vector3.Up;
            Vector3 pitchAxis = Vector3.Right;
            Vector3 rollAxis = Vector3.Backward;

            Quaternion yawQuat;
            Quaternion pitchQuat;
            Quaternion rollQuat;

            Quaternion.CreateFromAxisAngle(ref yawAxis, yaw, out yawQuat);
            Quaternion.CreateFromAxisAngle(ref pitchAxis, pitch, out pitchQuat);
            Quaternion.CreateFromAxisAngle(ref rollAxis, roll, out rollQuat);

            Quaternion.Multiply(ref yawQuat, ref pitchQuat, out result);
            Quaternion.Multiply(ref result, ref rollQuat, out result);
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Divide(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            Quaternion q3 = Quaternion.Inverse(quaternion2);
            Quaternion.Multiply(ref quaternion1, ref q3, out result);
        }

        public static Quaternion Divide(Quaternion quaternion1, float divider)
        {
            Quaternion result;
            Quaternion.Divide(ref quaternion1, ref divider, out result);
            return result;
        }

        public static void Divide(ref Quaternion quaternion1, ref float divider, out Quaternion result)
        {
            divider = 1f / divider;
            result.X = quaternion1.X * divider;
            result.Y = quaternion1.Y * divider;
            result.Z = quaternion1.Z * divider;
            result.W = quaternion1.W * divider;
        }

        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            float result;
            Quaternion.Dot(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out float result)
        {
            result = (quaternion1.X * quaternion2.X +
                      quaternion1.Y * quaternion2.Y +
                      quaternion1.Z * quaternion2.Z +
                      quaternion1.W * quaternion2.W);
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
        }

        public static Quaternion Inverse(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Inverse(ref quaternion, out result);
            return result;
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            float lengthSqrt = quaternion.LengthSquared();
            float invLength = 1.0f / lengthSqrt;

            result.X = -quaternion.X * invLength;
            result.Y = -quaternion.Y * invLength;
            result.Z = -quaternion.Z * invLength;
            result.W = quaternion.W * invLength;
        }


        public float Length()
        {
            return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
        }

        public float LengthSquared()
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
        }

        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            Quaternion result;
            Quaternion.Lerp(ref quaternion1, ref quaternion2, amount, out result);
            return result;
        }

        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            float dotProduct;
            Quaternion.Dot(ref quaternion1, ref quaternion2, out dotProduct);
            float amount1 = 1.0f - amount;
            if (dotProduct < 0)
            {
                amount = -amount;
            }

            result.X = amount1 * quaternion1.X + amount * quaternion2.X;
            result.Y = amount1 * quaternion1.Y + amount * quaternion2.Y;
            result.Z = amount1 * quaternion1.Z + amount * quaternion2.Z;
            result.W = amount1 * quaternion1.W + amount * quaternion2.W;
            result.Normalize();
        }

        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Multiply(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            Quaternion temp;
            temp.X = quaternion1.W * quaternion2.X + quaternion1.X * quaternion2.W + quaternion1.Y * quaternion2.Z - quaternion1.Z * quaternion2.Y;
            temp.Y = quaternion1.W * quaternion2.Y - quaternion1.X * quaternion2.Z + quaternion1.Y * quaternion2.W + quaternion1.Z * quaternion2.X;
            temp.Z = quaternion1.W * quaternion2.Z + quaternion1.X * quaternion2.Y - quaternion1.Y * quaternion2.X + quaternion1.Z * quaternion2.W;
            temp.W = quaternion1.W * quaternion2.W - quaternion1.X * quaternion2.X - quaternion1.Y * quaternion2.Y - quaternion1.Z * quaternion2.Z;
            result = temp;
        }

        public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
        {
            Quaternion result;
            Quaternion.Multiply(ref quaternion1, scaleFactor, out result);
            return result;
        }

        public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
        {
            result.X = quaternion1.X * scaleFactor;
            result.Y = quaternion1.Y * scaleFactor;
            result.Z = quaternion1.Z * scaleFactor;
            result.W = quaternion1.W * scaleFactor;
        }

        public static Quaternion Negate(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Negate(ref quaternion, out result);
            return result;
        }

        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            result.X = -quaternion.X;
            result.Y = -quaternion.Y;
            result.Z = -quaternion.Z;
            result.W = -quaternion.W;
        }

        public void Normalize()
        {
            float invLength = 1.0f / this.Length();
            this.X *= invLength;
            this.Y *= invLength;
            this.Z *= invLength;
            this.W *= invLength;
        }

        public static Quaternion Normalize(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Normalize(ref quaternion, out result);
            return result;
        }

        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            float invLength = 1.0f / quaternion.Length();
            result.X = quaternion.X * invLength;
            result.Y = quaternion.Y * invLength;
            result.Z = quaternion.Z * invLength;
            result.W = quaternion.W * invLength;
        }

        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            Quaternion result;
            Quaternion.Slerp(ref quaternion1, ref quaternion2, amount, out result);
            return result; throw new NotImplementedException();
        }

        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            float cosAlpha;
            Dot(ref quaternion1, ref quaternion2, out cosAlpha);

            float alpha = (float)Math.Acos(cosAlpha);
            float invSinAlphaInv = 1.0f / (float)Math.Sin(alpha);

            float amount1 = (float)Math.Sin((1.0f - amount) * alpha) * invSinAlphaInv;
            float amount2 = (float)Math.Sin(amount * alpha) * invSinAlphaInv;

            result.X = quaternion1.X * amount1 + quaternion2.X * amount2;
            result.Y = quaternion1.Y * amount1 + quaternion2.Y * amount2;
            result.Z = quaternion1.Z * amount1 + quaternion2.Z * amount2;
            result.W = quaternion1.W * amount1 + quaternion2.W * amount2;
        }

        public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Subtract(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result.X = quaternion1.X - quaternion2.X;
            result.Y = quaternion1.Y - quaternion2.Y;
            result.Z = quaternion1.Z - quaternion2.Z;
            result.W = quaternion1.W - quaternion2.W;
        }

        public override string ToString()
        {
            var culture = CultureInfo.CurrentCulture;
            return "{X:" + X.ToString(culture) +
                " Y:" + Y.ToString(culture) +
                " Z:" + Z.ToString(culture) +
                " W:" + W.ToString(culture) + "}";
        }
        #endregion


        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is Quaternion) ? this.Equals((Quaternion)obj) : false;
        }
        public bool Equals(Quaternion other)
        {
            return this.W == other.W && this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }
        #endregion

        #region operator overloading
        public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Add(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Divide(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static Quaternion operator /(Quaternion quaternion1, float divider)
        {
            return Quaternion.Divide(quaternion1, divider);
        }

        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            return (quaternion1.X == quaternion2.X &&
                quaternion1.Y == quaternion2.Y &&
                quaternion1.Z == quaternion2.Z &&
                quaternion1.W == quaternion2.W);
        }

        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            return (quaternion1.X != quaternion2.X ||
                quaternion1.Y != quaternion2.Y ||
                quaternion1.Z != quaternion2.Z ||
                quaternion1.W != quaternion2.W);
        }

        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Multiply(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            Quaternion result;
            Multiply(ref quaternion1, scaleFactor, out result);
            return result;
        }

        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Subtract(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static Quaternion operator -(Quaternion quaternion)
        {
            Quaternion result;
            Negate(ref quaternion, out result);
            return result;
        }
        #endregion
    }
}
