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
    public struct Ray : IEquatable<Ray>
    {
        #region fields
        public Vector3 Direction;
        public Vector3 Position;
        #endregion


        #region constructors
        public Ray(Vector3 position, Vector3 direction)
        {
            this.Direction = direction;
            this.Position = position;
        }
        #endregion


        #region public methods
        public override int GetHashCode()
        {
            return Position.GetHashCode() ^ Direction.GetHashCode();

        }
        /*
         *  Source for implementation :
         *  http://www-gs.informatik.tu-cottbus.de/projektstudium2006/doku/Strahlen_in_der_CG.pdf
         */
        public Nullable<float> Intersects(BoundingBox box)
        {
            Nullable<float> result;
            this.Intersects(ref box, out result);
            return result;
        }
        public void Intersects(ref BoundingBox box, out Nullable<float> result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public Nullable<float> Intersects(BoundingFrustum frustum)
        {
            Nullable<float> result;
            this.Intersects(ref frustum, out result);
            return result;
        }
        public void Intersects(ref BoundingFrustum frustum, out Nullable<float> result)
        {
            throw new Exception("method has not yet been implemented");
        }
        public Nullable<float> Intersects(BoundingSphere sphere)
        {
            Nullable<float> result;
            this.Intersects(ref sphere, out result);
            return result;
        }
        public void Intersects(ref BoundingSphere sphere, out Nullable<float> result)
        {
            Vector3 toSphere = Vector3.Subtract(sphere.Center, this.Position);
            float lengthSquaredToSphere = toSphere.LengthSquared();
            float sphereRadiusSquared = sphere.Radius * sphere.Radius;

            //project the distance to the Sphere onto the Ray
            float toSphereOnRay = Vector3.Dot(this.Direction, toSphere);

            //ray starts in sphere
            if (lengthSquaredToSphere <= sphereRadiusSquared)
            {
                result = 0;
                return;
            }
            //if toSphere and this.Direction pointing in different directions
            if (toSphereOnRay < 0)
            {
                result = null;
                return;
            }

            float dist = sphereRadiusSquared + toSphereOnRay * toSphereOnRay - lengthSquaredToSphere;

            result = (dist < 0) ? null : toSphereOnRay - (float?)Math.Sqrt(dist);



        }
        public Nullable<float> Intersects(Plane plane)
        {
            Nullable<float> result;
            this.Intersects(ref plane, out result);
            return result;
        }
        public void Intersects(ref Plane plane, out Nullable<float> result)
        {
            //http://www.cs.toronto.edu/~smalik/418/tutorial8_ray_primitive_intersections.pdf

            float vd = Vector3.Dot(plane.Normal, this.Direction);
          //As plane and Ray are infinitiv it intersects in every case, except the ray is parrales to the plane
            //no intersection if ray direction and plane normal are orthogional to each other
            if (vd == 0)
            {
                result = null;
                return;
            }
            float v0 = -Vector3.Dot(plane.Normal, this.Position) + plane.D;
            float t = v0 / vd;
            result=(this.Direction*t).Length();
        }

        public override string ToString()
        {
            return "{{Position:"+Position.ToString()+" Direction:"+Direction.ToString()+"}}";

        }
        #endregion


        #region operator overloading
        public static bool operator ==(Ray a, Ray b)
        {
            return a.Direction == b.Direction && a.Position == b.Position;
        }
        public static bool operator !=(Ray a, Ray b)
        {
            return a.Direction != b.Direction || a.Position != b.Position;
        }
        #endregion


        #region IEquatable implementation
        public override bool Equals(Object obj)
        {
            if (obj is Ray)
            {
                return this.Equals((Ray)obj);
            }
            return false;
        }

        public bool Equals(Ray other)
        {
            return this.Direction == other.Direction && this.Position == other.Position;
        }
        #endregion
    }
}
