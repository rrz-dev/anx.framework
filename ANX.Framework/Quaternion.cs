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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public void Conjugate()
        {
            this.Y = -this.Y;
            this.Z = -this.Z;
            this.W = -this.W;
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
            result.W = -value.W;
        }

        public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion CreateFromRotationMatrix(Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
        {
            throw new NotImplementedException();
        }

        public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Divide(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            result = Quaternion.Multiply(quaternion1, Quaternion.Inverse(quaternion2));
        }

        public static Quaternion Divide(Quaternion quaternion1, float divider)
        {
            Quaternion result;
            Quaternion.Divide(ref quaternion1, ref divider, out result);
            return result;
        }

        public static void Divide(ref Quaternion quaternion1, ref float divider, out Quaternion result)
        {
            result = Quaternion.Multiply(quaternion1, 1.0f / divider);
        }
        public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
        {
            throw new NotImplementedException();
        }

        public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out float result)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
        }

        public static Quaternion Inverse(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Inverse(ref quaternion, out result);
            return result;
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            //(a + i b + j c + k d)-1= (a - i b - j c - k d) / (a2 + b2 + c2 + d2)
            float magnitude = quaternion.Length();
            result = Quaternion.Conjugate(quaternion) / (magnitude * magnitude);

        }


        public float Length()
        {
            return (float)Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
        }

        public float LengthSquared()
        {
            return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
        }

        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            throw new NotImplementedException();
        }

        public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
        {
            Quaternion result;
            Quaternion.Multiply(ref quaternion1, ref quaternion2, out result);
            return result;
        }

        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            //z1 * z2= a*e - b*f - c*g- d*h + i (b*e + a*f + c*h - d*g) + j (a*g - b*h + c*e + d*f) + k (a*h + b*g - c*f + d*e)
            result.X = quaternion1.X * quaternion2.X - quaternion1.Y * quaternion2.Y - quaternion1.Z * quaternion2.Z - quaternion1.W * quaternion2.W;
            result.Y = quaternion1.Y * quaternion2.X + quaternion1.X * quaternion2.Y + quaternion1.Z * quaternion2.W - quaternion1.W * quaternion2.Z;
            result.Z = quaternion1.X * quaternion2.Z - quaternion1.Y * quaternion2.W + quaternion1.Z * quaternion2.X + quaternion1.W * quaternion2.Y;
            result.W = quaternion1.X * quaternion2.W + quaternion1.Y * quaternion2.Z - quaternion1.Z * quaternion2.Y + quaternion1.W * quaternion2.X;
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
            result = Quaternion.Conjugate(quaternion);
            result.X = -result.X;
        }

        public void Normalize()
        {
            float norm = this.Length();
            float scaler = 1.0f / norm;
            this.X *= scaler;
            this.Y *= scaler;
            this.Z *= scaler;
            this.W *= scaler;
        }

        public static Quaternion Normalize(Quaternion quaternion)
        {
            Quaternion result;
            Quaternion.Normalize(ref quaternion, out result);
            return result;
        }

        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            float norm = quaternion.Length();
            float scaler = 1.0f / norm;
            result.X = quaternion.X * scaler;
            result.Y = quaternion.Y * scaler;
            result.Z = quaternion.Z * scaler;
            result.W = quaternion.W * scaler;
        }

        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
        {
            throw new NotImplementedException();
        }

        public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
        {
            throw new NotImplementedException();
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
            return "{X:" + this.X + " Y:" + this.Y + " Z:" + this.Z + " W:" + this.W + "}";
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
            return Quaternion.Add(quaternion1, quaternion2);
        }

        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            return Quaternion.Divide(quaternion1, quaternion2);
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
            return Quaternion.Multiply(quaternion1, quaternion2);
        }

        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            return Quaternion.Multiply(quaternion1, scaleFactor);
        }

        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            return Quaternion.Subtract(quaternion1, quaternion2);
        }

        public static Quaternion operator -(Quaternion quaternion)
        {
            return Quaternion.Negate(quaternion);
        }
        #endregion
    }
}
