#region Using Statements
using System;
using System.Collections.Generic;

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
    public struct BoundingBox : IEquatable<BoundingBox>
    {
        #region public fields
        public Vector3 Min;
        public Vector3 Max;
        public const int CornerCount = 8;
        #endregion


        #region constructors
        public BoundingBox(Vector3 min, Vector3 max)
        {
            this.Min = min;
            this.Max = max;
        }
        #endregion


        #region public methods
        public ContainmentType Contains(BoundingBox box)
        {
            ContainmentType result;
            this.Contains(ref box, out result);
            return result;
        }
        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public ContainmentType Contains(BoundingFrustum frustum)
        {
            ContainmentType result;
            this.Contains(ref frustum, out result);
            return result;
        }
        public ContainmentType Contains (ref BoundingFrustum frustum,out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public ContainmentType Contains(BoundingSphere sphere)
        {
            ContainmentType result;
            this.Contains(ref sphere, out result);
            return result;
        }
        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public ContainmentType Contains(Vector3 point)
        {
            ContainmentType result;
            this.Contains(ref point, out result);
            return result;
        }
        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public static BoundingBox CreateFromPoints(IEnumerable<Vector3> points)
        {
            throw new Exception("method has not yet been implemented");
        }

        public static BoundingBox CreateFromSphere(BoundingSphere sphere)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void CreateFromSphere(ref BoundingSphere sphere, out BoundingBox result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public static BoundingBox CreateMerged(BoundingBox original, BoundingBox additional)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void CreateMerged(ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public Vector3[] GetCorners()
        {
            throw new Exception("method has not yet been implemented");
        }
        public void GetCorners(Vector3[] corners)
        {
            throw new Exception("method has not yet been implemented");
        }

        public override int GetHashCode()
        {
            throw new Exception("method has not yet been implemented");
        }

        public bool Intersects(BoundingBox box)
        {
            throw new Exception("method has not yet been implemented");
        }
        public void Intersects(ref BoundingBox box, out bool result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public bool Intersects(BoundingFrustum frustum)
        {
            throw new Exception("method has not yet been implemented");
        }
        public bool Intersects(BoundingSphere sphere)
        {
            throw new Exception("method has not yet been implemented");
        }
        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public PlaneIntersectionType Intersects(Plane plane)
        {
            throw new Exception("method has not yet been implemented");
        }
        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public Nullable<float> Intersects(Ray ray)
        {
            throw new Exception("method has not yet been implemented");
        }
        public void Intersects(ref Ray ray, out Nullable<float> result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public override string ToString()
        {
            throw new Exception("method has not yet been implemented");
        }
        #endregion


        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is BoundingBox) ? this.Equals((BoundingBox)obj) : false;
        }
        public bool Equals(BoundingBox other)
        {
            return this.Max.Equals(other.Max) && this.Min.Equals(other.Min);
        }
        #endregion


        #region operator overloading
        public static bool operator ==(BoundingBox a, BoundingBox b)
        {
            return a.Max == b.Max && a.Min == b.Min;
        }
        public static bool operator !=(BoundingBox a, BoundingBox b)
        {
            throw new Exception("operator overloading has not yet been implemented");
        }
        #endregion
    }
}
