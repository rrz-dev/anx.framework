#region Using Statements
using System;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
            if ((this.Max.X < box.Min.X || this.Min.X > box.Max.X) ||
                (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y) ||
                (this.Max.Z < box.Min.Z || this.Min.Z > box.Max.Z)
               )
            {
                result = ContainmentType.Disjoint;
                return;
            }

            if ((this.Min.X <= box.Min.X && box.Max.X <= this.Max.X) &&
                (this.Min.Y <= box.Min.Y && box.Max.Y <= this.Max.Y) &&
                (this.Min.Z <= box.Min.Z && box.Max.Z <= this.Max.Z)
               )
            {
                result = ContainmentType.Contains;
                return;
            }
                
            result = ContainmentType.Intersects;
        }

        public ContainmentType Contains(BoundingFrustum frustum)
        {
            Vector3[] points = frustum.GetCorners();

            int pointsIn = 0;
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 point = points[i];

                if (point.X < this.Min.X ||
                    point.Z < this.Min.Z ||
                    point.Y < this.Min.Y ||
                    point.X > this.Max.X ||
                    point.Y > this.Max.Y ||
                    point.Z > this.Max.Z)
                {
                    continue;
                }

                if (i != 0 && pointsIn == 0)
                    return ContainmentType.Intersects;

                pointsIn++;
            }

            if (pointsIn == points.Length)
                return ContainmentType.Contains;

            return ContainmentType.Disjoint;
        }

        public ContainmentType Contains(BoundingSphere sphere)
        {
            ContainmentType result;
            this.Contains(ref sphere, out result);
            return result;
        }

        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            Vector3 clampedSphereCenter;
            clampedSphereCenter.X = MathHelper.Clamp(sphere.Center.X, this.Min.X, this.Max.X);
            clampedSphereCenter.Y = MathHelper.Clamp(sphere.Center.Y, this.Min.Y, this.Max.Y);
            clampedSphereCenter.Z = MathHelper.Clamp(sphere.Center.Z, this.Min.Z, this.Max.Z);

            if (Vector3.DistanceSquared(sphere.Center, clampedSphereCenter) > (sphere.Radius * sphere.Radius))
            {
                result = ContainmentType.Disjoint;
            }
            else if ((this.Min.X + sphere.Radius <= sphere.Center.X && sphere.Center.X <= this.Max.X - sphere.Radius) &&
                     (this.Max.X - this.Min.X > sphere.Radius && this.Min.Y + sphere.Radius <= sphere.Center.Y) &&
                     (sphere.Center.Y <= this.Max.Y - sphere.Radius && this.Max.Y - this.Min.Y > sphere.Radius) &&
                     (this.Min.Z + sphere.Radius <= sphere.Center.Z && sphere.Center.Z <= this.Max.Z - sphere.Radius) &&
                     (this.Max.X - this.Min.X > sphere.Radius)
                    )
            {
                result = ContainmentType.Contains;
            }
            else
            {
                result = ContainmentType.Intersects;
            }
        }

        public ContainmentType Contains(Vector3 point)
        {
            ContainmentType result;
            this.Contains(ref point, out result);
            return result;
        }

        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            result = ContainmentType.Disjoint;

            if (point.X < this.Min.X ||
                point.Z < this.Min.Z ||
                point.Y < this.Min.Y ||
                point.X > this.Max.X ||
                point.Y > this.Max.Y ||
                point.Z > this.Max.Z)
            {
                result = ContainmentType.Disjoint;
            }
            else if (point.X == this.Min.X ||
                point.Z == this.Min.Z ||
                point.Y == this.Min.Y ||
                point.X == this.Max.X ||
                point.Y == this.Max.Y ||
                point.Z == this.Max.Z)
            {
                result = ContainmentType.Intersects;
            }
            else
            {
                result = ContainmentType.Contains;
            }
        }

        public static BoundingBox CreateFromPoints(IEnumerable<Vector3> points)
        {
            if (points == null)
                throw new ArgumentNullException("Points must not be null");

            Vector3 min = new Vector3();
            Vector3 max = new Vector3();

            int p = 0;

            foreach (Vector3 point in points)
            {
                if (p == 0)
                {
                    min = point;
                    max = point;
                }
                else
                {
                    min.X = Math.Min(point.X, min.X);
                    min.Y = Math.Min(point.Y, min.Y);
                    min.Z = Math.Min(point.Z, min.Z);

                    max.X = Math.Max(point.X, max.X);
                    max.Y = Math.Max(point.Y, max.Y);
                    max.Z = Math.Max(point.Z, max.Z);
                }

                p++;
            }

            return new BoundingBox(min, max);
        }

        public static BoundingBox CreateFromSphere(BoundingSphere sphere)
        {
            BoundingBox result;
            CreateFromSphere(ref sphere, out result);
            return result;
        }

        public static void CreateFromSphere(ref BoundingSphere sphere, out BoundingBox result)
        {
            Vector3 min = new Vector3();
            Vector3 max = new Vector3();

            min.X = sphere.Center.X - sphere.Radius;
            min.Y = sphere.Center.Y - sphere.Radius;
            min.Z = sphere.Center.Z - sphere.Radius;

            max.X = sphere.Center.X + sphere.Radius;
            max.Y = sphere.Center.Y + sphere.Radius;
            max.Z = sphere.Center.Z + sphere.Radius;

            result = new BoundingBox(min, max);
        }

        public static BoundingBox CreateMerged(BoundingBox original, BoundingBox additional)
        {
            BoundingBox result;
            CreateMerged(ref original, ref additional, out result);
            return result;
        }

        public static void CreateMerged(ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
        {
            Vector3 min = new Vector3();
            Vector3 max = new Vector3();

            min.X = Math.Min(original.Min.X, additional.Min.X);
            min.Y = Math.Min(original.Min.Y, additional.Min.Y);
            min.Z = Math.Min(original.Min.Z, additional.Min.Z);

            max.X = Math.Max(original.Max.X, additional.Max.X);
            max.Y = Math.Max(original.Max.Y, additional.Max.Y);
            max.Z = Math.Max(original.Max.Z, additional.Max.Z);

            result = new BoundingBox(min, max);
        }

        public Vector3[] GetCorners()
        {
            Vector3[] corners = new Vector3[BoundingBox.CornerCount];
            corners[0].X = this.Min.X;
            corners[0].Y = this.Max.Y;
            corners[0].Z = this.Max.Z;
            corners[1].X = this.Max.X;
            corners[1].Y = this.Max.Y;
            corners[1].Z = this.Max.Z;
            corners[2].X = this.Max.X;
            corners[2].Y = this.Min.Y;
            corners[2].Z = this.Max.Z;
            corners[3].X = this.Min.X;
            corners[3].Y = this.Min.Y;
            corners[3].Z = this.Max.Z;
            corners[4].X = this.Min.X;
            corners[4].Y = this.Max.Y;
            corners[4].Z = this.Min.Z;
            corners[5].X = this.Max.X;
            corners[5].Y = this.Max.Y;
            corners[5].Z = this.Min.Z;
            corners[6].X = this.Max.X;
            corners[6].Y = this.Min.Y;
            corners[6].Z = this.Min.Z;
            corners[7].X = this.Min.X;
            corners[7].Y = this.Min.Y;
            corners[7].Z = this.Min.Z;

            return corners;
        }

        public void GetCorners(Vector3[] corners)
        {
            if (corners.Length != BoundingBox.CornerCount)
                throw new ArgumentException("Corners has to have a Length of" + BoundingBox.CornerCount.ToString());

            corners[0].X = this.Min.X;
            corners[0].Y = this.Max.Y;
            corners[0].Z = this.Max.Z;
            corners[1].X = this.Max.X;
            corners[1].Y = this.Max.Y;
            corners[1].Z = this.Max.Z;
            corners[2].X = this.Max.X;
            corners[2].Y = this.Min.Y;
            corners[2].Z = this.Max.Z;
            corners[3].X = this.Min.X;
            corners[3].Y = this.Min.Y;
            corners[3].Z = this.Max.Z;
            corners[4].X = this.Min.X;
            corners[4].Y = this.Max.Y;
            corners[4].Z = this.Min.Z;
            corners[5].X = this.Max.X;
            corners[5].Y = this.Max.Y;
            corners[5].Z = this.Min.Z;
            corners[6].X = this.Max.X;
            corners[6].Y = this.Min.Y;
            corners[6].Z = this.Min.Z;
            corners[7].X = this.Min.X;
            corners[7].Y = this.Min.Y;
            corners[7].Z = this.Min.Z;
        }

        public override int GetHashCode()
        {
            return this.Min.GetHashCode() + this.Max.GetHashCode();
        }

        public bool Intersects(BoundingBox box)
        {
            bool result;
            Intersects(ref box, out result);
            return result;
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            if ((this.Max.X < box.Min.X || this.Min.X > box.Max.X) ||
                (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y) ||
                (this.Max.Z < box.Min.Z || this.Min.Z > box.Max.Z)
               )
            {
                result = false;
            }
            else
            {
                result = true;
            }
        }

        public bool Intersects(BoundingFrustum frustum)
        {
            Vector3[] points = frustum.GetCorners();

            for (int i = 0; i < points.Length; i++)
            {
                Vector3 point = points[i];

                if (point.X < this.Min.X ||
                    point.Z < this.Min.Z ||
                    point.Y < this.Min.Y ||
                    point.X > this.Max.X ||
                    point.Y > this.Max.Y ||
                    point.Z > this.Max.Z)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool Intersects(BoundingSphere sphere)
        {
            bool result;
            Intersects(ref sphere, out result);
            return result;
        }

        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            result = Vector3.DistanceSquared(sphere.Center, Vector3.Clamp(sphere.Center, this.Min, this.Max)) <= sphere.Radius * sphere.Radius;
        }

        public PlaneIntersectionType Intersects(Plane plane)
        {
            PlaneIntersectionType result;
            Intersects(ref plane, out result);
            return result;
        }

        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            Vector3 p;

            p.X = plane.Normal.X >= 0f ? this.Min.X : this.Max.X;
            p.Y = plane.Normal.Y >= 0f ? this.Min.Y : this.Max.X;
            p.Z = plane.Normal.Z >= 0f ? this.Min.Z : this.Max.X;

            float dot = plane.Normal.X * p.X + plane.Normal.Y * p.Y + plane.Normal.Z * p.Z;

            if (dot + plane.D > 0f)
            {
                result = PlaneIntersectionType.Front;
                return;
            }

            p.X = plane.Normal.X >= 0f ? this.Max.X : this.Min.X;
            p.Y = plane.Normal.Y >= 0f ? this.Max.Y : this.Min.X;
            p.Z = plane.Normal.Z >= 0f ? this.Max.Z : this.Min.X;

            dot = plane.Normal.X * p.X + plane.Normal.Y * p.Y + plane.Normal.Z * p.Z;

            if (dot + plane.D < 0f)
            {
                result = PlaneIntersectionType.Back;
                return;
            }

            result = PlaneIntersectionType.Intersecting;
        }

        public Nullable<float> Intersects(Ray ray)
        {
            Nullable<float> result;
            Intersects(ref ray, out result);
            return result;
        }

        // source for implementation:
        // http://courses.csusm.edu/cs697exz/ray_box.htm
        public void Intersects(ref Ray ray, out Nullable<float> result)
        {
            float tnear = float.NegativeInfinity;
            float tfar = float.PositiveInfinity;

            float t1 = (this.Min.X - ray.Position.X) - ray.Direction.X;
            float t2 = (this.Max.X - ray.Position.X) - ray.Direction.X;

            if (t1 > t2)
            {
                float t = t1;
                t1 = t2;
                t2 = t;
            }

            if (t1 > tnear)
                tnear = t1;

            if (t2 < tfar)
                tfar = t2;

            if (tnear > tfar || tfar < 0)
            {
                result = null;
                return;
            }

            t1 = (this.Min.Y - ray.Position.Y) - ray.Direction.Y;
            t2 = (this.Max.Y - ray.Position.Y) - ray.Direction.Y;

            if (t1 > t2)
            {
                float t = t1;
                t1 = t2;
                t2 = t;
            }

            if (t1 > tnear)
                tnear = t1;

            if (t2 < tfar)
                tfar = t2;

            if (tnear > tfar || tfar < 0)
            {
                result = null;
                return;
            }
            
            t1 = (this.Min.Z - ray.Position.Z) - ray.Direction.Z;
            t2 = (this.Max.Z - ray.Position.Z) - ray.Direction.Z;

            if (t1 > t2)
            {
                float t = t1;
                t1 = t2;
                t2 = t;
            }

            if (t1 > tnear)
                tnear = t1;

            if (t2 < tfar)
                tfar = t2;

            if (tnear > tfar || tfar < 0)
            {
                result = null;
                return;
            }

            result = tfar;
        }

        public override string ToString()
				{
					// This may look a bit more ugly, but String.Format should
					// be avoided cause of it's bad performance!
					return "{Min:" + Min.ToString() +
						" Max:" + Max.ToString() + "}";

					//  return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Min:{0} Max:{1}}}", new object[]
					//{
					//  this.Min.ToString(), 
					//      this.Max.ToString()
					//});
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
            return a.Max != b.Max || a.Min != b.Min;
        }
        #endregion
    }
}
