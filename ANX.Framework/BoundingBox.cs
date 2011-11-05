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
            result = ContainmentType.Disjoint;

            if (box.Min.X >= this.Min.X && box.Min.X <= this.Max.X &&
                box.Min.Y >= this.Min.Y && box.Min.Y <= this.Max.Y &&
                box.Min.Z >= this.Min.Z && box.Min.Z <= this.Max.Z)
            {
                result = ContainmentType.Intersects;
            }

            if (box.Max.X >= this.Min.X && box.Max.X <= this.Max.X &&
                box.Max.Y >= this.Min.Y && box.Max.Y <= this.Max.Y &&
                box.Max.Z >= this.Min.Z && box.Max.Z <= this.Max.Z)
            {
                if (result == ContainmentType.Intersects)
                    result = ContainmentType.Contains;
                else
                    result = ContainmentType.Intersects;
            }
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

        //source: monoxna
        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            //TODO: Find an other way, this one often is wrong!
            if (sphere.Center.X - Min.X > sphere.Radius
                && sphere.Center.Y - Min.Y > sphere.Radius
                && sphere.Center.Z - Min.Z > sphere.Radius
                && Max.X - sphere.Center.X > sphere.Radius
                && Max.Y - sphere.Center.Y > sphere.Radius
                && Max.Z - sphere.Center.Z > sphere.Radius)
            {
                result = ContainmentType.Contains;
                return;
            }

            double dmin = 0;

            if (sphere.Center.X - Min.X <= sphere.Radius)
                dmin += (sphere.Center.X - Min.X) * (sphere.Center.X - Min.X);
            else if (Max.X - sphere.Center.X <= sphere.Radius)
                dmin += (sphere.Center.X - Max.X) * (sphere.Center.X - Max.X);
            if (sphere.Center.Y - Min.Y <= sphere.Radius)
                dmin += (sphere.Center.Y - Min.Y) * (sphere.Center.Y - Min.Y);
            else if (Max.Y - sphere.Center.Y <= sphere.Radius)
                dmin += (sphere.Center.Y - Max.Y) * (sphere.Center.Y - Max.Y);
            if (sphere.Center.Z - Min.Z <= sphere.Radius)
                dmin += (sphere.Center.Z - Min.Z) * (sphere.Center.Z - Min.Z);
            else if (Max.Z - sphere.Center.Z <= sphere.Radius)
                dmin += (sphere.Center.Z - Max.Z) * (sphere.Center.Z - Max.Z);

            if (dmin <= sphere.Radius * sphere.Radius)
                result = ContainmentType.Intersects;
            else
                result = ContainmentType.Disjoint;
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
            throw new Exception("method has not yet been implemented");
        }

        public bool Intersects(BoundingBox box)
        {
            bool result;
            Intersects(ref box, out result);
            return result;
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            result = false;

            if (box.Min.X >= this.Min.X && box.Min.X <= this.Max.X &&
                box.Min.Y >= this.Min.Y && box.Min.Y <= this.Max.Y &&
                box.Min.Z >= this.Min.Z && box.Min.Z <= this.Max.Z)
            {
                result = true;
                return;
            }

            if (box.Max.X >= this.Min.X && box.Max.X <= this.Max.X &&
                box.Max.Y >= this.Min.Y && box.Max.Y <= this.Max.Y &&
                box.Max.Z >= this.Min.Z && box.Max.Z <= this.Max.Z)
            {
                result = true;
                return;
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

        //source: monoxna
        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            //TODO: Find an other way, this one often is wrong!
            if (sphere.Center.X - Min.X > sphere.Radius
                && sphere.Center.Y - Min.Y > sphere.Radius
                && sphere.Center.Z - Min.Z > sphere.Radius
                && Max.X - sphere.Center.X > sphere.Radius
                && Max.Y - sphere.Center.Y > sphere.Radius
                && Max.Z - sphere.Center.Z > sphere.Radius)
            {
                result = true;
                return;
            }

            double dmin = 0;

            if (sphere.Center.X - Min.X <= sphere.Radius)
                dmin += (sphere.Center.X - Min.X) * (sphere.Center.X - Min.X);
            else if (Max.X - sphere.Center.X <= sphere.Radius)
                dmin += (sphere.Center.X - Max.X) * (sphere.Center.X - Max.X);
            if (sphere.Center.Y - Min.Y <= sphere.Radius)
                dmin += (sphere.Center.Y - Min.Y) * (sphere.Center.Y - Min.Y);
            else if (Max.Y - sphere.Center.Y <= sphere.Radius)
                dmin += (sphere.Center.Y - Max.Y) * (sphere.Center.Y - Max.Y);
            if (sphere.Center.Z - Min.Z <= sphere.Radius)
                dmin += (sphere.Center.Z - Min.Z) * (sphere.Center.Z - Min.Z);
            else if (Max.Z - sphere.Center.Z <= sphere.Radius)
                dmin += (sphere.Center.Z - Max.Z) * (sphere.Center.Z - Max.Z);

            if (dmin <= sphere.Radius * sphere.Radius)
                result = true;
            else
                result = false;
        }

        public PlaneIntersectionType Intersects(Plane plane)
        {
            PlaneIntersectionType result;
            Intersects(ref plane, out result);
            return result;
        }

        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            Vector3 p = this.Min;

            if (plane.Normal.X >= 0)
                p.X = this.Max.X;
            if (plane.Normal.Y >= 0)
                p.Y = this.Max.Y;
            if (plane.Normal.Z < 0)
                p.Z = this.Max.Z;

            float distance;
            Vector3 planeNormal = -plane.Normal;

            Vector3.Dot(ref planeNormal, ref p, out distance);
            distance -= plane.D;

            if (distance < 0)
                result = PlaneIntersectionType.Front;
            else if (distance > 0)
                result = PlaneIntersectionType.Back;
            else
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
            return "Min:" + Min.ToString() + " Max:" + Max.ToString();
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
