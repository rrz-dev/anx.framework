#region Using Statements
using System;
using System.Globalization;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
           return this.D.GetHashCode() + this.Normal.GetHashCode();
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
            if ((this.Normal * 2).LengthSquared() < this.Normal.LengthSquared())
            {
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
            }
            else
            {

                if (distanceSquared_Sphere_Origin > distanceSquared_Plane_Origin)
                {
                    if (distanceSquared_Sphere_Origin - sphere.Radius < distanceSquared_Plane_Origin)
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
                if (distanceSquared_Sphere_Origin < distanceSquared_Plane_Origin)
                {
                    if (distanceSquared_Sphere_Origin + sphere.Radius > distanceSquared_Plane_Origin)
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
					var culture = CultureInfo.CurrentCulture;
					// This may look a bit more ugly, but String.Format should
					// be avoided cause of it's bad performance!
					return "{Normal:" + Normal.ToString() +
						" D:" + D.ToString(culture) + "}";

					//return string.Format(culture, "{{Normal:{0} D:{1}}}", new object[]
					//{
					//  this.Normal.ToString(), 
					//  this.D.ToString(culture)
					//});
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
