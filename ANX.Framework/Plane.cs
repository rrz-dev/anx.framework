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
    public struct Plane : IEquatable<Plane>
    {
        #region fields
        public float D;
        public Vector3 Normal;
        #endregion


        #region constructors
        public Plane(float a, float b, float c, float d)
        {
            this.D = d;
            this.Normal = new Vector3(a, b, c);
        }
        public Plane(Vector3 normal, float d)
        {
            this.D = d;
            this.Normal = normal;
        }
        public Plane(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            // calculate 2 vectos spanning the plane and cross them to get the normal, then normalize
            this.Normal = Vector3.Normalize(Vector3.Cross(Vector3.Subtract(point2, point1), Vector3.Subtract(point3, point1)));
           // now calculate d
            this.D = Vector3.Dot(point1, this.Normal);
        }
        public Plane(Vector4 value)
        {
            this.D = value.W;
            this.Normal = new Vector3(value.X, value.Y, value.Z);
        }
        #endregion


        #region public methods
        public float Dot(Vector4 value)
        {
            float result;
            this.Dot(ref value, out result);
            return result;
        }
        public void Dot(ref Vector4 value, out float result)
        {
        //taken from vektor
            result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
        }

        public float DotCoordinate(Vector3 value)
        {
            float result;
            this.DotCoordinate(ref value, out result);
            return result;
        }
        public void DotCoordinate(ref Vector3 value, out float result)
        {
            result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
        }

        public float DotNormal(Vector3 value)
        {
            float result;
            this.DotNormal(ref value, out result);
            return result;
        }
        public void DotNormal(ref Vector3 value, out float result)
        {
            result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z ;
        }

        public override int GetHashCode()
        {
           return this.D.GetHashCode() ^ this.Normal.GetHashCode();
        }

        public PlaneIntersectionType Intersects(BoundingBox box)
        {
            PlaneIntersectionType result;
            this.Intersects(ref box, out result);
            return result;
        }
        public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public PlaneIntersectionType Intersects(BoundingFrustum frustum)
        {
            PlaneIntersectionType result;
            this.Intersects(ref frustum, out result);
            return result; ;
        }
        public void Intersects(ref BoundingFrustum frustum, out PlaneIntersectionType result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public PlaneIntersectionType Intersects(BoundingSphere sphere)
        {
            PlaneIntersectionType result;
            this.Intersects(ref sphere, out result);
            return result;
        }
        public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
        {
            float distanceSquared_Sphere_Origin = Vector3.DistanceSquared(Vector3.Zero, sphere.Center);
            float distanceSquared_Plane_Origin = this.D * this.D;
            //maybe check pointing direktion of normal
            if (distanceSquared_Sphere_Origin > distanceSquared_Plane_Origin)
            {               
                if (distanceSquared_Sphere_Origin - sphere.Radius < distanceSquared_Plane_Origin)
                {
                    result = PlaneIntersectionType.Intersecting;
                    return;
                }
                else
                {
                    result = PlaneIntersectionType.Front;
                    return;
                }
            }
            if (distanceSquared_Sphere_Origin < distanceSquared_Plane_Origin)
            {
                if (distanceSquared_Sphere_Origin + sphere.Radius > distanceSquared_Plane_Origin)
                {
                    result = PlaneIntersectionType.Intersecting;
                    return;
                }
                else
                {
                    result = PlaneIntersectionType.Back;
                    return;
                }
            }
            //else distance sphere == distance plane
            result = PlaneIntersectionType.Intersecting;
            

        }

        public void Normalize()
        {
            throw new Exception("method has not yet been implemented");
        }
        public static Plane Normalize(Plane value)
        {
            Plane result;
            Plane.Normalize(ref value, out result);
            return result;
        }
        public static void Normalize(ref Plane value, out Plane result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public override string ToString()
        {
            return "{{Normal:"+this.Normal.ToString()+" D:"+this.D.ToString()+"}}";

        }

        public static Plane Transform(Plane plane, Matrix matrix)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            throw new Exception("method has not yet been implemented");
        }
        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
        {
            throw new Exception("method has not yet been implemented");
        }
        #endregion


        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            return (obj is Plane) ? this.Equals((Plane)obj) : false;
        }
        public bool Equals(Plane other)
        {
            return this.D == other.D && Normal.Equals(other.Normal);
        }
        #endregion


        #region operator overloading
        public static bool operator ==(Plane lhs, Plane rhs)
        {
            return lhs.D.Equals(rhs.D) && lhs.Normal.Equals(rhs.Normal);
        }
        public static bool operator !=(Plane lhs, Plane rhs)
        {
            return !lhs.D.Equals(rhs.D) || !lhs.Normal.Equals(rhs.Normal);
        }
        #endregion
    }
}
