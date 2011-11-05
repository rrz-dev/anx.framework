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
            throw new Exception("method has not yet been implemented");
        }

        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public ContainmentType Contains(BoundingFrustum frustum)
        {
            throw new Exception("method has not yet been implemented");
        }

        public ContainmentType Contains(BoundingSphere sphere)
        {
            throw new Exception("method has not yet been implemented");
        }

        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public ContainmentType Contains(Vector3 point)
        {
            throw new Exception("method has not yet been implemented");
        }

        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            throw new Exception("method has not yet been implemented");
        }

        public Vector3[] GetCorners()
        {
            return this.corners;
        }

        public void GetCorners(Vector3[] corners)
        {
            corners = this.corners;
        }

        public override int GetHashCode()
        {
            //TODO: implement
            return base.GetHashCode();
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

        #region private methods
        //algorithm from: http://crazyjoke.free.fr/doc/3D/plane%20extraction.pdf
        private void CreatePlanes()
        {
            this.left.Normal.X = this.matrix.M14 + this.matrix.M11;
            this.left.Normal.Y = this.matrix.M24 + this.matrix.M21;
            this.left.Normal.Z = this.matrix.M34 + this.matrix.M31;
            this.left.D = this.matrix.M44 + this.matrix.M41;

            this.right.Normal.X = this.matrix.M14 - this.matrix.M11;
            this.right.Normal.Y = this.matrix.M24 - this.matrix.M21;
            this.right.Normal.Z = this.matrix.M34 - this.matrix.M31;
            this.right.D = this.matrix.M44 - this.matrix.M41;

            this.bottom.Normal.X = this.matrix.M14 + this.matrix.M12;
            this.bottom.Normal.Y = this.matrix.M24 + this.matrix.M22;
            this.bottom.Normal.Z = this.matrix.M34 + this.matrix.M32;
            this.bottom.D = this.matrix.M44 + this.matrix.M42;

            this.top.Normal.X = this.matrix.M14 - this.matrix.M12;
            this.top.Normal.Y = this.matrix.M24 - this.matrix.M22;
            this.top.Normal.Z = this.matrix.M34 - this.matrix.M32;
            this.top.D = this.matrix.M44 - this.matrix.M42;

            this.near.Normal.X = this.matrix.M13;
            this.near.Normal.Y = this.matrix.M23;
            this.near.Normal.Z = this.matrix.M33;
            this.near.D = this.matrix.M43;

            this.far.Normal.X = this.matrix.M14 - this.matrix.M13;
            this.far.Normal.Y = this.matrix.M24 - this.matrix.M23;
            this.far.Normal.Z = this.matrix.M34 - this.matrix.M33;
            this.far.D = this.matrix.M44 - this.matrix.M43;

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
