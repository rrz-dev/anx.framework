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
        public float W;
        public float X;
        public float Y;
        public float Z;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            throw new NotImplementedException();
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static Quaternion Inverse(Quaternion quaternion)
        {
            throw new NotImplementedException();
        }

        public static void Inverse(ref Quaternion quaternion, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public float Length()
        {
            throw new NotImplementedException();
        }

        public float LengthSquared()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
        {
            throw new NotImplementedException();
        }

        public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public static Quaternion Negate(Quaternion quaternion)
        {
            throw new NotImplementedException();
        }

        public static void Negate(ref Quaternion quaternion, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public static Quaternion Normalize(Quaternion quaternion)
        {
            throw new NotImplementedException();
        }

        public static void Normalize(ref Quaternion quaternion, out Quaternion result)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
        {
            throw new NotImplementedException();
        }

        public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
        {
            throw new NotImplementedException();
        }

        public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
        {
            throw new NotImplementedException();
        }

        public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
        {
            throw new NotImplementedException();
        }

        public static Quaternion operator -(Quaternion quaternion)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
