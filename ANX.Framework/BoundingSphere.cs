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
    public struct BoundingSphere : IEquatable<BoundingSphere>
    {
        #region fields
   
        public Vector3 Center;
        public float Radius;
 
        #endregion


        #region constructors
        public BoundingSphere(Vector3 center, float radius)
        {
           
            this.Center = center;
            this.Radius = radius;
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
            byte value = 0;
            if (Vector3.DistanceSquared(box.Max, this.Center) < this.Radius * this.Radius)
            {
                value++;
            }
            if (Vector3.DistanceSquared(box.Min, this.Center) < this.Radius * this.Radius)
            {
                value++;
            }

            result = value == 0 ? ContainmentType.Disjoint : value == 1 ? ContainmentType.Intersects : ContainmentType.Contains;
        }
        public ContainmentType Contains(BoundingFrustum frustum)
        {
            ContainmentType result;
            this.Contains(ref frustum, out result);
            return result;
        }
        public ContainmentType Contains(ref BoundingFrustum frustum, out ContainmentType result)
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
            float distance = Vector3.Distance(this.Center, sphere.Center);
            float bothRadius = this.Radius + sphere.Radius;
            if (distance > bothRadius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            if (distance + sphere.Radius < this.Radius)
            {
                result = ContainmentType.Contains;
                return;
            }
            result = ContainmentType.Intersects;
        }
        public ContainmentType Contains(Vector3 point)
        {
            ContainmentType result;
            this.Contains(ref point, out result);
            return result;
        }
        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            float distance = Vector3.DistanceSquared(point, this.Center);

            if (distance > this.Radius*this.Radius)
                result= ContainmentType.Disjoint;

            else if (distance < this.Radius * this.Radius)
                result= ContainmentType.Contains;

            result= ContainmentType.Intersects;

        }

        public static BoundingSphere CreateFromBoundingBox(BoundingBox box)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void CreateFromBoundingBox(ref BoundingBox box, out BoundingSphere result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public static BoundingSphere CreateFromFrustum(BoundingFrustum frustum)
        {
            throw new Exception("method has not yet been implemented");
        }

        public static BoundingSphere CreateFromPoints(IEnumerable<Vector3> points)
        {
            throw new Exception("method has not yet been implemented");
        }

        public static BoundingSphere CreateMerged(BoundingSphere original, BoundingSphere additional)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void CreateMerged(ref BoundingSphere original, ref BoundingSphere additional, out BoundingSphere result)
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

        public BoundingSphere Transform(Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public void Transform(ref Matrix matrix, out BoundingSphere result)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new Exception("method has not yet been implemented");
        }
        #endregion


        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is BoundingSphere) ? this.Equals((BoundingSphere)obj) : false;
        }
        public bool Equals(BoundingSphere other)
        {
            return this.Center.Equals(other.Center) && this.Radius == other.Radius;
        }
        #endregion


        #region operator overloading
        public static bool operator ==(BoundingSphere a, BoundingSphere b)
        {
            return (a.Center.X == b.Center.X &&
                a.Center.Y == b.Center.Y &&
                a.Center.Z == b.Center.Z &&
                a.Radius == b.Radius);
        }
        public static bool operator !=(BoundingSphere a, BoundingSphere b)
        {
            return (a.Center.X != b.Center.X ||
                a.Center.Y != b.Center.Y ||
                a.Center.Z != b.Center.Z ||
                a.Radius != b.Radius);
        }
        #endregion
    }
}
