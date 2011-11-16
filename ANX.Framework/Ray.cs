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
        /// <summary>
        /// The direction this ray is pointing to.
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// Starting position of the ray.
        /// </summary>
        public Vector3 Position;
        #endregion


        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Ray"/> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction.</param>
        public Ray(Vector3 position, Vector3 direction)
        {
            this.Direction = direction;
            this.Position = position;
        }
        #endregion


        #region public methods
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Position.GetHashCode() + Direction.GetHashCode();

        }
        /*
         *  Source for implementation :
         *  http://www-gs.informatik.tu-cottbus.de/projektstudium2006/doku/Strahlen_in_der_CG.pdf
         */
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <returns>Distance from <see cref="Ray"/> start to interesection points</returns>
        public Nullable<float> Intersects(BoundingBox box)
        {
            Nullable<float> result;
            this.Intersects(ref box, out result);
            return result;
        }
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="BoundingBox"/>.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="result">The result.</param>
        public void Intersects(ref BoundingBox box, out Nullable<float> result)
        {
            throw new Exception("method has not yet been implemented");

        }
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="BoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">The box.</param>
        /// <returns>Distance from <see cref="Ray"/> start to interesection points</returns>
        public Nullable<float> Intersects(BoundingFrustum frustum)
        {
            Nullable<float> result;
            this.Intersects(ref frustum, out result);
            return result;
        }
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="BoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">The frustum.</param>
        /// <param name="result">The result.</param>
        public void Intersects(ref BoundingFrustum frustum, out Nullable<float> result)
        {
            throw new Exception("method has not yet been implemented");
        }
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="BoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The sphere.</param>
        /// <returns>
        /// Distance from <see cref="Ray"/> start to interesection points
        /// </returns>
        public Nullable<float> Intersects(BoundingSphere sphere)
        {
            Nullable<float> result;
            this.Intersects(ref sphere, out result);
            return result;
        }
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="BoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The sphere.</param>
        /// <param name="result">The result.</param>
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

        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="Plane"/>.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <returns>
        /// Distance from <see cref="Ray"/> start to interesection points
        /// </returns>
        public Nullable<float> Intersects(Plane plane)
        {
            Nullable<float> result;
            this.Intersects(ref plane, out result);
            return result;
        }
        /// <summary>
        /// Test if this <see cref="Ray"/> intersects with the specified <see cref="Plane"/>.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <param name="result">The result.</param>
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

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;
            return string.Format(currentCulture, "{{Position:{0} Direction:{1}}}", new object[]
	        {
		        this.Position.ToString(), 
		        this.Direction.ToString()
	        });
        }
        #endregion


        #region operator overloading
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Ray a, Ray b)
        {
            return a.Direction == b.Direction && a.Position == b.Position;
        }
        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Ray a, Ray b)
        {
            return a.Direction != b.Direction || a.Position != b.Position;
        }
        #endregion


        #region IEquatable implementation
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(Object obj)
        {
            if (obj is Ray)
            {
                return this.Equals((Ray)obj);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Ray"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Ray"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="Ray"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Ray other)
        {
            return this.Direction == other.Direction && this.Position == other.Position;
        }
        #endregion
    }
}
