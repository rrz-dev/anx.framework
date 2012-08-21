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
            return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
        }
        public void Dot(ref Vector4 value, out float result)
        {
            result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
        }

        public float DotCoordinate(Vector3 value)
        {
            return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
        }
        public void DotCoordinate(ref Vector3 value, out float result)
        {
            result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
        }

        public float DotNormal(Vector3 value)
        {
            return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
        }
        public void DotNormal(ref Vector3 value, out float result)
        {
            result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
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
            Vector3 p;

            p.X = this.Normal.X >= 0f ? box.Min.X : box.Max.X;
            p.Y = this.Normal.Y >= 0f ? box.Min.Y : box.Max.X;
            p.Z = this.Normal.Z >= 0f ? box.Min.Z : box.Max.X;

            float dot = this.Normal.X * p.X + this.Normal.Y * p.Y + this.Normal.Z * p.Z;

            if (dot + this.D > 0f)
            {
                result = PlaneIntersectionType.Front;
                return;
            }

            p.X = this.Normal.X >= 0f ? box.Max.X : box.Min.X;
            p.Y = this.Normal.Y >= 0f ? box.Max.Y : box.Min.X;
            p.Z = this.Normal.Z >= 0f ? box.Max.Z : box.Min.X;

            dot = this.Normal.X * p.X + this.Normal.Y * p.Y + this.Normal.Z * p.Z;

            if (dot + this.D < 0f)
            {
                result = PlaneIntersectionType.Back;
                return;
            }

            result = PlaneIntersectionType.Intersecting;
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
            float l = Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z;
            if (Math.Abs(1.0f - l) < float.Epsilon)
            {
                return;
            }
            l = 1.0f / (float)Math.Sqrt(l);
            Normal.X = Normal.X * l;
            Normal.Y = Normal.Y * l;
            Normal.Z = Normal.Z * l;
            this.D = this.D * l;
        }

        public static Plane Normalize(Plane value)
        {
            Vector3 n = value.Normal;
            float l = n.X * n.X + n.Y * n.Y + n.Z * n.Z;
            if (Math.Abs(1.0f - l) < float.Epsilon)
            {
                return new Plane(n, value.D);
            }
            l = 1.0f / (float)Math.Sqrt(l);
            Plane result;
            result.Normal.X = value.Normal.X * l;
            result.Normal.Y = value.Normal.Y * l;
            result.Normal.Z = value.Normal.Z * l;
            result.D = value.D * l;
            return result;
        }

        public static void Normalize(ref Plane value, out Plane result)
        {
            Vector3 n = value.Normal;
            float l = n.X * n.X + n.Y * n.Y + n.Z * n.Z;
            if (Math.Abs(1.0f - l) < float.Epsilon)
            {
                result.Normal = n;
                result.D = value.D;
                return;
            }
            l = 1.0f / (float)Math.Sqrt(l);
            result.Normal.X = value.Normal.X * l;
            result.Normal.Y = value.Normal.Y * l;
            result.Normal.Z = value.Normal.Z * l;
            result.D = value.D * l;
        }

        public override string ToString()
        {
            var culture = CultureInfo.CurrentCulture;
            // This may look a bit more ugly, but String.Format should
            // be avoided cause of it's bad performance!
            return "{Normal:" + Normal.ToString() +
                    " D:" + D.ToString(culture) + "}";
        }

        public static Plane Transform(Plane plane, Matrix matrix)
        {
            // multiply by the inverse transpose of the matrix
            Matrix m;
            Matrix.Invert(ref matrix, out m);

            Vector3 n = plane.Normal;
            Plane result;
            result.Normal.X = n.X * m.M11 + n.Y * m.M12 + n.Z * m.M13 + plane.D * m.M14;
            result.Normal.Y = n.X * m.M21 + n.Y * m.M22 + n.Z * m.M23 + plane.D * m.M24;
            result.Normal.Z = n.X * m.M31 + n.Y * m.M32 + n.Z * m.M33 + plane.D * m.M34;
            result.D = n.X * m.M41 + n.Y * m.M42 + n.Z * m.M43 + plane.D * m.M44;
            return result;
        }

        public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
        {
            // multiply by the inverse transpose of the matrix
            Matrix m;
            Matrix.Invert(ref matrix, out m);

            Vector3 n = plane.Normal;
            result.Normal.X = n.X * m.M11 + n.Y * m.M12 + n.Z * m.M13 + plane.D * m.M14;
            result.Normal.Y = n.X * m.M21 + n.Y * m.M22 + n.Z * m.M23 + plane.D * m.M24;
            result.Normal.Z = n.X * m.M31 + n.Y * m.M32 + n.Z * m.M33 + plane.D * m.M34;
            result.D = n.X * m.M41 + n.Y * m.M42 + n.Z * m.M43 + plane.D * m.M44;
        }

        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            float twoX = rotation.X + rotation.X;
            float twoY = rotation.Y + rotation.Y;
            float twoZ = rotation.Z + rotation.Z;

            float twoXX = twoX * rotation.X;
            float twoXY = twoX * rotation.Y;
            float twoXZ = twoX * rotation.Z;
            float twoXW = twoX * rotation.W;

            float twoYY = twoY * rotation.Y;
            float twoYZ = twoY * rotation.Z;
            float twoYW = twoY * rotation.W;

            float twoZZ = twoZ * rotation.Z;
            float twoZW = twoZ * rotation.W;

            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;

            Plane result;
            result.Normal.X = x * (1.0f - twoYY - twoZZ) + y * (twoXY - twoZW) + z * (twoXZ + twoYW);
            result.Normal.Y = x * (twoXY + twoZW) + y * (1.0f - twoXX - twoZZ) + z * (twoYZ - twoXW);
            result.Normal.Z = x * (twoXZ - twoYW) + y * (twoYZ + twoXW) + z * (1.0f - twoXX - twoYY);
            result.D = plane.D;
            return result;
        }

        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
        {
            float twoX = rotation.X + rotation.X;
            float twoY = rotation.Y + rotation.Y;
            float twoZ = rotation.Z + rotation.Z;

            float twoXX = twoX * rotation.X;
            float twoXY = twoX * rotation.Y;
            float twoXZ = twoX * rotation.Z;
            float twoXW = twoX * rotation.W;

            float twoYY = twoY * rotation.Y;
            float twoYZ = twoY * rotation.Z;
            float twoYW = twoY * rotation.W;

            float twoZZ = twoZ * rotation.Z;
            float twoZW = twoZ * rotation.W;

            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;

            result.Normal.X = x * (1.0f - twoYY - twoZZ) + y * (twoXY - twoZW) + z * (twoXZ + twoYW);
            result.Normal.Y = x * (twoXY + twoZW) + y * (1.0f - twoXX - twoZZ) + z * (twoYZ - twoXW);
            result.Normal.Z = x * (twoXZ - twoYW) + y * (twoYZ + twoXW) + z * (1.0f - twoXX - twoYY);
            result.D = plane.D;
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
