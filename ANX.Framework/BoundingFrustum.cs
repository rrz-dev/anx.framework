#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;


#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.InProgress)]
    [Developer("xToast, Glatzemann")]
    public class BoundingFrustum : IEquatable<BoundingFrustum>
    {
        #region fields
        public const int CornerCount = 8;

        private Vector3[] corners;
        private Plane[] planes = new Plane[6];
        private Matrix matrix;

        private enum PlanePosition : int
        {
            Near = 0,
            Far = 1,
            Left = 2,
            Right = 3,
            Top = 4,
            Bottom = 5,
        }

        #endregion

        #region constructors
        public BoundingFrustum(Matrix value)
        {
            this.matrix = value;

            CreatePlanes();
            CreateCorners();
        }
        #endregion

        #region public methods
        public ContainmentType Contains(BoundingBox box)
        {
            ContainmentType result;
            Contains(ref box, out result);
            return result;
        }

        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            bool flag = false;
            Plane[] array = this.planes;
            for (int i = 0; i < array.Length; i++)
            {
                Plane plane = array[i];
                PlaneIntersectionType planeIntersectionType = box.Intersects(plane);
                if (planeIntersectionType == PlaneIntersectionType.Front)
                {
                    result = ContainmentType.Disjoint;
                    return;
                }
                if (planeIntersectionType == PlaneIntersectionType.Intersecting)
                {
                    flag = true;
                }
            }

            if (!flag)
            {
                result = ContainmentType.Contains;
                return;
            }

            result = ContainmentType.Intersects;
        }

        public ContainmentType Contains(BoundingFrustum frustum)
        {
            if (frustum == null)
            {
                throw new ArgumentNullException("frustum");
            }

            ContainmentType result = ContainmentType.Disjoint;
            if (this.Intersects(frustum))
            {
                result = ContainmentType.Contains;
                for (int i = 0; i < this.corners.Length; i++)
                {
                    if (this.Contains(frustum.corners[i]) == ContainmentType.Disjoint)
                    {
                        result = ContainmentType.Intersects;
                        break;
                    }
                }
            }

            return result;
        }

        public ContainmentType Contains(BoundingSphere sphere)
        {
            ContainmentType result;
            Contains(ref sphere, out result);
            return result;
        }

        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            Vector3 center = sphere.Center;

            result = ContainmentType.Contains;

            float distance = Bottom.Normal.X * center.X + Bottom.Normal.Y * center.Y + Bottom.Normal.Z * center.Z + Bottom.D;
            if (distance > sphere.Radius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance > -sphere.Radius)
            {
                result = ContainmentType.Intersects;
            }

            distance = Top.Normal.X * center.X + Top.Normal.Y * center.Y + Top.Normal.Z * center.Z + Top.D;
            if (distance > sphere.Radius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance > -sphere.Radius)
            {
                result = ContainmentType.Intersects;
            }

            distance = Left.Normal.X * center.X + Left.Normal.Y * center.Y + Left.Normal.Z * center.Z + Left.D;
            if (distance > sphere.Radius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance > -sphere.Radius)
            {
                result = ContainmentType.Intersects;
            }

            distance = Right.Normal.X * center.X + Right.Normal.Y * center.Y + Right.Normal.Z * center.Z + Right.D;
            if (distance > sphere.Radius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance > -sphere.Radius)
            {
                result = ContainmentType.Intersects;
            }

            distance = Near.Normal.X * center.X + Near.Normal.Y * center.Y + Near.Normal.Z * center.Z + Near.D;
            if (distance > sphere.Radius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance > -sphere.Radius)
            {
                result = ContainmentType.Intersects;
            }

            distance = Far.Normal.X * center.X + Far.Normal.Y * center.Y + Far.Normal.Z * center.Z + Far.D;
            if (distance > sphere.Radius)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance > -sphere.Radius)
            {
                result = ContainmentType.Intersects;
            }
        }

        public ContainmentType Contains(Vector3 point)
        {
            ContainmentType result;
            Contains(ref point, out result);
            return result;
        }

        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            result = ContainmentType.Contains;

            Plane plane = Bottom;
            Vector3 normal = plane.Normal;

            float planeDistance = plane.D;

            float tempDist;
            Vector3.Dot(ref normal, ref point, out tempDist);
            float distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance == 0)
            {
                result = ContainmentType.Intersects;
            }

            plane = Top;
            normal = plane.Normal;

            planeDistance = plane.D;

            Vector3.Dot(ref normal, ref point, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance == 0)
            {
                result = ContainmentType.Intersects;
            }

            plane = Left;
            normal = plane.Normal;

            planeDistance = plane.D;

            Vector3.Dot(ref normal, ref point, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance == 0)
            {
                result = ContainmentType.Intersects;
            }

            plane = Right;
            normal = plane.Normal;

            planeDistance = plane.D;

            Vector3.Dot(ref normal, ref point, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance == 0)
            {
                result = ContainmentType.Intersects;
            }

            plane = Near;
            normal = plane.Normal;

            planeDistance = plane.D;

            Vector3.Dot(ref normal, ref point, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance == 0)
            {
                result = ContainmentType.Intersects;
            }

            plane = Far;
            normal = plane.Normal;

            planeDistance = plane.D;

            Vector3.Dot(ref normal, ref point, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distance == 0)
            {
                result = ContainmentType.Intersects;
            }
        }

        public Vector3[] GetCorners()
        {
            return this.corners;
        }

        public void GetCorners(Vector3[] corners)
        {
            if (corners == null)
            {
                throw new ArgumentNullException("corners");
            }

            if (corners.Length < 8)
            {
                throw new ArgumentOutOfRangeException("corners", "The array to be filled with corner vertices needs at least have a length of 8 Vector3");
            }

            this.corners.CopyTo(corners, 0);
        }

        public override int GetHashCode()
        {
            return this.matrix.GetHashCode();
        }

        public bool Intersects(BoundingBox box)
        {
            bool result;
            Intersects(ref box, out result);
            return result;
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            Vector3[] boxCorners = box.GetCorners();

            result = true;

            Plane plane = Bottom;
            Vector3 normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);

            float planeDistance = plane.D;
            Vector3 vertex = box.Min;

            if (normal.X >= 0)
                vertex.X = box.Max.X;
            if (normal.Y >= 0)
                vertex.Y = box.Max.Y;
            if (normal.Z < 0)
                vertex.Z = box.Max.Z;

            float tempDist; 
            Vector3.Dot(ref normal, ref vertex, out tempDist);
            float distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = false;
                return;
            }

            plane = Top;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);

            planeDistance = plane.D;
            vertex = box.Min;

            if (normal.X >= 0)
                vertex.X = box.Max.X;
            if (normal.Y >= 0)
                vertex.Y = box.Max.Y;
            if (normal.Z < 0)
                vertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref vertex, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = false;
                return;
            } 
            
            plane = Left;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);

            planeDistance = plane.D;
            vertex = box.Min;

            if (normal.X >= 0)
                vertex.X = box.Max.X;
            if (normal.Y >= 0)
                vertex.Y = box.Max.Y;
            if (normal.Z < 0)
                vertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref vertex, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = false;
                return;
            }
            
            plane = Right;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);

            planeDistance = plane.D;
            vertex = box.Min;

            if (normal.X >= 0)
                vertex.X = box.Max.X;
            if (normal.Y >= 0)
                vertex.Y = box.Max.Y;
            if (normal.Z < 0)
                vertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref vertex, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = false;
                return;
            } 
            
            plane = Near;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);

            planeDistance = plane.D;
            vertex = box.Min;

            if (normal.X >= 0)
                vertex.X = box.Max.X;
            if (normal.Y >= 0)
                vertex.Y = box.Max.Y;
            if (normal.Z < 0)
                vertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref vertex, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = false;
                return;
            } 
            
            plane = Far;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);

            planeDistance = plane.D;
            vertex = box.Min;

            if (normal.X >= 0)
                vertex.X = box.Max.X;
            if (normal.Y >= 0)
                vertex.Y = box.Max.Y;
            if (normal.Z < 0)
                vertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref vertex, out tempDist);
            distance = tempDist - planeDistance;

            if (distance < 0)
            {
                result = false;
                return;
            }
        }

        public bool Intersects(BoundingFrustum frustum)
        {
            throw new NotImplementedException();
        }

        public bool Intersects(BoundingSphere sphere)
        {
            bool result;
            Intersects(ref sphere, out result);
            return result;
        }

        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            Vector3 center = sphere.Center;

            result = true;

            float distance = Bottom.Normal.X * center.X + Bottom.Normal.Y * center.Y + Bottom.Normal.Z * center.Z + Bottom.D;
            if (distance > sphere.Radius)
            {
                result = false;
                return;
            } 
            
            distance = Top.Normal.X * center.X + Top.Normal.Y * center.Y + Top.Normal.Z * center.Z + Top.D;
            if (distance > sphere.Radius)
            {
                result = false;
                return;
            }

            distance = Left.Normal.X * center.X + Left.Normal.Y * center.Y + Left.Normal.Z * center.Z + Left.D;
            if (distance > sphere.Radius)
            {
                result = false;
                return;
            }

            distance = Right.Normal.X * center.X + Right.Normal.Y * center.Y + Right.Normal.Z * center.Z + Right.D;
            if (distance > sphere.Radius)
            {
                result = false;
                return;
            }

            distance = Near.Normal.X * center.X + Near.Normal.Y * center.Y + Near.Normal.Z * center.Z + Near.D;
            if (distance > sphere.Radius)
            {
                result = false;
                return;
            }

            distance = Far.Normal.X * center.X + Far.Normal.Y * center.Y + Far.Normal.Z * center.Z + Far.D;
            if (distance > sphere.Radius)
            {
                result = false;
                return;
            }
        }

        public PlaneIntersectionType Intersects(Plane plane)
        {
            PlaneIntersectionType result;
            Intersects(ref plane, out result);
            return result;
        }

        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            throw new NotImplementedException();
        }

        public Nullable<float> Intersects(Ray ray)
        {
            Nullable<float> result;
            Intersects(ref ray, out result);
            return result;
        }

        public void Intersects(ref Ray ray, out Nullable<float> result)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
		{
			// This may look a bit more ugly, but String.Format should
			// be avoided cause of it's bad performance!
			return "{Near:"   + Near.ToString()   +
				   " Far:"    + Far.ToString()    +
				   " Left:"   + Left.ToString()   +
				   " Right:"  + Right.ToString()  +
				   " Top:"    + Top.ToString()    +
				   " Bottom:" + Bottom.ToString() + "}";
        }
        #endregion

        #region public properties
        public Matrix Matrix
        {
            get
            {
                return this.matrix;
            }
            set
            {
                this.matrix = value;
                this.CreatePlanes();
                this.CreateCorners();
            }
        }

        public Plane Near
        {
            get
            {
                return this.planes[0];
            }
        }

        public Plane Far
        {
            get
            {
                return this.planes[1];
            }
        }

        public Plane Top
        {
            get
            {
                return this.planes[4];
            }
        }

        public Plane Bottom
        {
            get
            {
                return this.planes[5];
            }
        }

        public Plane Right
        {
            get
            {
                return this.planes[3];
            }
        }

        public Plane Left
        {
            get
            {
                return this.planes[2];
            }
        }
        #endregion

        #region private methods
        //algorithm based on but normals were pointing to outside instead of inside: http://crazyjoke.free.fr/doc/3D/plane%20extraction.pdf
        private void CreatePlanes()
        {
            int idx;

            idx = (int)PlanePosition.Near;
            this.planes[idx].Normal.X = -this.matrix.M13;
            this.planes[idx].Normal.Y = -this.matrix.M23;
            this.planes[idx].Normal.Z = -this.matrix.M33;
            this.planes[idx].D        = -this.matrix.M43;

            idx = (int)PlanePosition.Far;
            this.planes[idx].Normal.X = -this.matrix.M14 + this.matrix.M13;
            this.planes[idx].Normal.Y = -this.matrix.M24 + this.matrix.M23;
            this.planes[idx].Normal.Z = -this.matrix.M34 + this.matrix.M33;
            this.planes[idx].D        = -this.matrix.M44 + this.matrix.M43;

            idx = (int)PlanePosition.Left;
            this.planes[idx].Normal.X = -this.matrix.M14 - this.matrix.M11;
            this.planes[idx].Normal.Y = -this.matrix.M24 - this.matrix.M21;
            this.planes[idx].Normal.Z = -this.matrix.M34 - this.matrix.M31;
            this.planes[idx].D        = -this.matrix.M44 - this.matrix.M41;

            idx = (int)PlanePosition.Right;
            this.planes[idx].Normal.X = -this.matrix.M14 + this.matrix.M11;
            this.planes[idx].Normal.Y = -this.matrix.M24 + this.matrix.M21;
            this.planes[idx].Normal.Z = -this.matrix.M34 + this.matrix.M31;
            this.planes[idx].D        = -this.matrix.M44 + this.matrix.M41;

            idx = (int)PlanePosition.Top;
            this.planes[idx].Normal.X = -this.matrix.M14 + this.matrix.M12;
            this.planes[idx].Normal.Y = -this.matrix.M24 + this.matrix.M22;
            this.planes[idx].Normal.Z = -this.matrix.M34 + this.matrix.M32;
            this.planes[idx].D        = -this.matrix.M44 + this.matrix.M42;

            idx = (int)PlanePosition.Bottom;
            this.planes[idx].Normal.X = -this.matrix.M14 - this.matrix.M12;
            this.planes[idx].Normal.Y = -this.matrix.M24 - this.matrix.M22;
            this.planes[idx].Normal.Z = -this.matrix.M34 - this.matrix.M32;
            this.planes[idx].D        = -this.matrix.M44 - this.matrix.M42;

            for (int i = 0; i < this.planes.Length; i++)
            {
                this.planes[i].Normalize();
            }
        }

        private void CreateCorners()
        {
            Ray rnl = BoundingFrustum.EdgeIntersection(ref this.planes[(int)PlanePosition.Near], ref this.planes[(int)PlanePosition.Left]);
            Ray rrn = BoundingFrustum.EdgeIntersection(ref this.planes[(int)PlanePosition.Right], ref this.planes[(int)PlanePosition.Near]);
            Ray rlf = BoundingFrustum.EdgeIntersection(ref this.planes[(int)PlanePosition.Left], ref this.planes[(int)PlanePosition.Far]);
            Ray rfr = BoundingFrustum.EdgeIntersection(ref this.planes[(int)PlanePosition.Far], ref this.planes[(int)PlanePosition.Right]);

            this.corners = new[]
            {
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Top], ref rnl),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Top], ref rrn),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Bottom], ref rrn),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Bottom], ref rnl),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Top], ref rlf),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Top], ref rfr),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Bottom], ref rfr),
                BoundingFrustum.PointIntersection(ref this.planes[(int)PlanePosition.Bottom], ref rlf),
            };
        }

        //source: monoxna
        private static Vector3 IntersectionPoint(ref Plane a, ref Plane b, ref Plane c)
        {
            // Formula used
            //                d1 ( N2 * N3 ) + d2 ( N3 * N1 ) + d3 ( N1 * N2 )
            //P =       -------------------------------------------------------------------------
            //                             N1 . ( N2 * N3 )
            //
            // Note: N refers to the normal, d refers to the displacement. '.' means dot product. '*' means cross product

            Vector3 v1, v2, v3;
            float f = -Vector3.Dot(a.Normal, Vector3.Cross(b.Normal, c.Normal));

            v1 = (a.D * (Vector3.Cross(b.Normal, c.Normal)));
            v2 = (b.D * (Vector3.Cross(c.Normal, a.Normal)));
            v3 = (c.D * (Vector3.Cross(a.Normal, b.Normal)));

            Vector3 vec = new Vector3(v1.X + v2.X + v3.X, v1.Y + v2.Y + v3.Y, v1.Z + v2.Z + v3.Z);
            return vec / f;
        }

        private static Ray EdgeIntersection(ref Plane p1, ref Plane p2)
        {
            Ray result = default(Ray);
            result.Direction = Vector3.Cross(p1.Normal, p2.Normal);
            float divider = result.Direction.LengthSquared();
            result.Position = Vector3.Cross(-p1.D * p2.Normal + p2.D * p1.Normal, result.Direction) / divider;
            return result;
        }

        private static Vector3 PointIntersection(ref Plane plane, ref Ray ray)
        {
            float scaleFactor = (-plane.D - Vector3.Dot(plane.Normal, ray.Position)) / Vector3.Dot(plane.Normal, ray.Direction);
            return ray.Position + ray.Direction * scaleFactor;
        }

        #endregion

        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            var frustum = obj as BoundingFrustum;
            if (frustum != null)
            {
                return this.Matrix == frustum.Matrix;
            }
            return false;
        }
        public bool Equals(BoundingFrustum other)
        {
            if (other != null)
            {
                return this.Matrix == other.Matrix;
            }
            return false;
        }
        #endregion

        #region operator overloading
        public static bool operator ==(BoundingFrustum a, BoundingFrustum b)
        {
            return object.Equals(a, b);
        }

        public static bool operator !=(BoundingFrustum a, BoundingFrustum b)
        {
            return !object.Equals(a, b);
        }
        #endregion
    }
}
