#region Using Statements
using System;
using System.Text;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    public class BoundingFrustum : IEquatable<BoundingFrustum>
    {
        #region fields
        public const int CornerCount = 8;
        #endregion

        #region properties
        private Vector3[] corners;

        private Matrix matrix;
        public Matrix Matrix 
        { 
            get { return this.matrix; } 
            set 
            { 
                this.matrix = value;
                this.CreatePlanes();
                this.CreateCorners();
            } 
        }

        private Plane near;
        public Plane Near { get { return this.near; } }

        private Plane far;
        public Plane Far { get { return this.far; } }

        private Plane top;
        public Plane Top { get { return this.top; } }

        private Plane bottom;
        public Plane Bottom { get { return this.bottom; } }

        private Plane right;
        public Plane Right { get { return this.right; } }

        private Plane left;
        public Plane Left { get { return this.left; } }
        #endregion

        #region constructors
        public BoundingFrustum(Matrix value)
        {
            corners = new Vector3[CornerCount];
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
            Vector3[] boxCorners = box.GetCorners();

            result = ContainmentType.Contains;

            Plane plane = Bottom;
            Vector3 normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);
            float planeDistance = plane.D;

            Vector3 pVertex = box.Min;
            if (normal.X >= 0)
                pVertex.X = box.Max.X;
            if (normal.Y >= 0)
                pVertex.Y = box.Max.Y;
            if (normal.Z < 0)
                pVertex.Z = box.Max.Z;

            float tempDistP;
            Vector3.Dot(ref normal, ref pVertex, out tempDistP);
            float distanceP = tempDistP - planeDistance;

            Vector3 nVertex = box.Max;
            if (normal.X >= 0)
                nVertex.X = box.Min.X;
            if (normal.Y >= 0)
                nVertex.Y = box.Min.Y;
            if (normal.Z < 0)
                nVertex.Z = box.Min.Z;

            float tempDistN;
            Vector3.Dot(ref normal, ref pVertex, out tempDistN);
            float distanceN = tempDistN - planeDistance;

            if (distanceN < 0 && distanceP < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distanceN < 0 || distanceP < 0)
            {
                result = ContainmentType.Intersects;
                return;
            } 
            
            plane = Top;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);
            planeDistance = plane.D;

            pVertex = box.Min;
            if (normal.X >= 0)
                pVertex.X = box.Max.X;
            if (normal.Y >= 0)
                pVertex.Y = box.Max.Y;
            if (normal.Z < 0)
                pVertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistP);
            distanceP = tempDistP - planeDistance;

            nVertex = box.Max;
            if (normal.X >= 0)
                nVertex.X = box.Min.X;
            if (normal.Y >= 0)
                nVertex.Y = box.Min.Y;
            if (normal.Z < 0)
                nVertex.Z = box.Min.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistN);
            distanceN = tempDistN - planeDistance;

            if (distanceN < 0 && distanceP < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distanceN < 0 || distanceP < 0)
            {
                result = ContainmentType.Intersects;
                return;
            }

            plane = Left;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);
            planeDistance = plane.D;

            pVertex = box.Min;
            if (normal.X >= 0)
                pVertex.X = box.Max.X;
            if (normal.Y >= 0)
                pVertex.Y = box.Max.Y;
            if (normal.Z < 0)
                pVertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistP);
            distanceP = tempDistP - planeDistance;

            nVertex = box.Max;
            if (normal.X >= 0)
                nVertex.X = box.Min.X;
            if (normal.Y >= 0)
                nVertex.Y = box.Min.Y;
            if (normal.Z < 0)
                nVertex.Z = box.Min.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistN);
            distanceN = tempDistN - planeDistance;

            if (distanceN < 0 && distanceP < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distanceN < 0 || distanceP < 0)
            {
                result = ContainmentType.Intersects;
                return;
            }

            plane = Right;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);
            planeDistance = plane.D;

            pVertex = box.Min;
            if (normal.X >= 0)
                pVertex.X = box.Max.X;
            if (normal.Y >= 0)
                pVertex.Y = box.Max.Y;
            if (normal.Z < 0)
                pVertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistP);
            distanceP = tempDistP - planeDistance;

            nVertex = box.Max;
            if (normal.X >= 0)
                nVertex.X = box.Min.X;
            if (normal.Y >= 0)
                nVertex.Y = box.Min.Y;
            if (normal.Z < 0)
                nVertex.Z = box.Min.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistN);
            distanceN = tempDistN - planeDistance;

            if (distanceN < 0 && distanceP < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distanceN < 0 || distanceP < 0)
            {
                result = ContainmentType.Intersects;
                return;
            }

            plane = Near;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);
            planeDistance = plane.D;

            pVertex = box.Min;
            if (normal.X >= 0)
                pVertex.X = box.Max.X;
            if (normal.Y >= 0)
                pVertex.Y = box.Max.Y;
            if (normal.Z < 0)
                pVertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistP);
            distanceP = tempDistP - planeDistance;

            nVertex = box.Max;
            if (normal.X >= 0)
                nVertex.X = box.Min.X;
            if (normal.Y >= 0)
                nVertex.Y = box.Min.Y;
            if (normal.Z < 0)
                nVertex.Z = box.Min.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistN);
            distanceN = tempDistN - planeDistance;

            if (distanceN < 0 && distanceP < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distanceN < 0 || distanceP < 0)
            {
                result = ContainmentType.Intersects;
                return;
            }

            plane = Far;
            normal = plane.Normal;
            //Vector3.Negate(ref normal, out normal);
            planeDistance = plane.D;

            pVertex = box.Min;
            if (normal.X >= 0)
                pVertex.X = box.Max.X;
            if (normal.Y >= 0)
                pVertex.Y = box.Max.Y;
            if (normal.Z < 0)
                pVertex.Z = box.Max.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistP);
            distanceP = tempDistP - planeDistance;

            nVertex = box.Max;
            if (normal.X >= 0)
                nVertex.X = box.Min.X;
            if (normal.Y >= 0)
                nVertex.Y = box.Min.Y;
            if (normal.Z < 0)
                nVertex.Z = box.Min.Z;

            Vector3.Dot(ref normal, ref pVertex, out tempDistN);
            distanceN = tempDistN - planeDistance;

            if (distanceN < 0 && distanceP < 0)
            {
                result = ContainmentType.Disjoint;
                return;
            }
            else if (distanceN < 0 || distanceP < 0)
            {
                result = ContainmentType.Intersects;
                return;
            }
        }

        public ContainmentType Contains(BoundingFrustum frustum)
        {
            throw new NotImplementedException();
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
					return "{Near:" + Near.ToString() +
						" Far:" + Far.ToString() +
						" Left:" + Left.ToString() +
						" Right:" + Right.ToString() +
						" Top:" + Top.ToString() +
						" Bottom:" + Bottom.ToString() + "}";

					////source: monoxna
					//  var currentCulture = System.Globalization.CultureInfo.CurrentCulture;
					//  return string.Format(currentCulture, "{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", new object[]
					//{
					//  this.Near.ToString(), 
					//  this.Far.ToString(), 
					//  this.Left.ToString(), 
					//  this.Right.ToString(), 
					//  this.Top.ToString(), 
					//  this.Bottom.ToString()
					//});
        }
        #endregion

        #region private methods
        //algorithm based on but normals were pointing to outside instead of inside: http://crazyjoke.free.fr/doc/3D/plane%20extraction.pdf
        private void CreatePlanes()
        {
            this.near.Normal.X   = -this.matrix.M13;
            this.near.Normal.Y   = -this.matrix.M23;
            this.near.Normal.Z   = -this.matrix.M33;
            this.near.D          = -this.matrix.M43;

            this.far.Normal.X    = this.matrix.M14 - this.matrix.M13;
            this.far.Normal.Y    = this.matrix.M24 - this.matrix.M23;
            this.far.Normal.Z    = this.matrix.M34 - this.matrix.M33;
            this.far.D           = this.matrix.M44 - this.matrix.M43;

            this.left.Normal.X    = -this.matrix.M14 - this.matrix.M11;
            this.left.Normal.Y    = -this.matrix.M24 - this.matrix.M21;
            this.left.Normal.Z    = -this.matrix.M34 - this.matrix.M31;
            this.left.D           = -this.matrix.M44 - this.matrix.M41;

            this.right.Normal.X   = -this.matrix.M14 + this.matrix.M11;
            this.right.Normal.Y   = -this.matrix.M24 + this.matrix.M21;
            this.right.Normal.Z   = -this.matrix.M34 + this.matrix.M31;
            this.right.D          = -this.matrix.M44 + this.matrix.M41;

            this.top.Normal.X    = -this.matrix.M14 + this.matrix.M12;
            this.top.Normal.Y    = -this.matrix.M24 + this.matrix.M22;
            this.top.Normal.Z    = -this.matrix.M34 + this.matrix.M32;
            this.top.D           = -this.matrix.M44 + this.matrix.M42;

            this.bottom.Normal.X = -this.matrix.M14 - this.matrix.M12;
            this.bottom.Normal.Y = -this.matrix.M24 - this.matrix.M22;
            this.bottom.Normal.Z = -this.matrix.M34 - this.matrix.M32;
            this.bottom.D        = -this.matrix.M44 - this.matrix.M42;

            NormalizePlane(ref this.left);
            NormalizePlane(ref this.right);
            NormalizePlane(ref this.bottom);
            NormalizePlane(ref this.top);
            NormalizePlane(ref this.near);
            NormalizePlane(ref this.far);
        }

        //source: monoxna
        private void NormalizePlane(ref Plane p)
        {
            float factor = 1f / p.Normal.Length();
            p.Normal.X *= factor;
            p.Normal.Y *= factor;
            p.Normal.Z *= factor;
            p.D *= factor;
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

        //source: monoxna
        private void CreateCorners()
        {
            this.corners = new Vector3[8];
            this.corners[0] = IntersectionPoint(ref this.near, ref this.left, ref this.top);
            this.corners[1] = IntersectionPoint(ref this.near, ref this.right, ref this.top);
            this.corners[2] = IntersectionPoint(ref this.near, ref this.right, ref this.bottom);
            this.corners[3] = IntersectionPoint(ref this.near, ref this.left, ref this.bottom);
            this.corners[4] = IntersectionPoint(ref this.far, ref this.left, ref this.top);
            this.corners[5] = IntersectionPoint(ref this.far, ref this.right, ref this.top);
            this.corners[6] = IntersectionPoint(ref this.far, ref this.right, ref this.bottom);
            this.corners[7] = IntersectionPoint(ref this.far, ref this.left, ref this.bottom);
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
