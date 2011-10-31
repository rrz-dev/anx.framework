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

namespace ANX.Framework.Graphics
{
    public struct Viewport
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private float near;
        private float far;

        public Viewport(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.near = 0f;
            this.far = 1f;
        }

        public Viewport(Rectangle bounds)
        {
            this.x = bounds.X;
            this.y = bounds.Y;
            this.width = bounds.Width;
            this.height = bounds.Height; this.near = 0f;
            this.far = 1f;
        }

        public Vector3 Project(Vector3 source, Matrix projection, Matrix view, Matrix world)
        {
            throw new NotImplementedException();
        }

        public Vector3 Unproject(Vector3 source, Matrix projection, Matrix view, Matrix world)
        {
            throw new NotImplementedException();
        }

        public float AspectRatio
        {
            get
            {
                if (this.width != 0 && this.height != 0)
                {
                    return (float)width / (float)height;
                }

                return 0f;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(x, y, width, height);
            }
            set
            {
                this.x = value.X;
                this.y = value.Y;
                this.width = value.Width;
                this.height = value.Height;
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        public float MaxDepth
        {
            get
            {
                return this.far;
            }
            set
            {
                this.far = value;
            }
        }

        public float MinDepth
        {
            get
            {
                return this.near;
            }
            set
            {
                this.near = value;
            }
        }

        public Rectangle TitleSafeArea
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public override string ToString()
        {
            return String.Format("Viewport X: {0} Y:{1} Width: {2} Height: {3} AspectRatio: {4} MinDepth: {5} MaxDepth: {6}", X, Y, Width, Height, AspectRatio, MinDepth, MaxDepth);
        }
    }
}
