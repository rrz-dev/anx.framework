#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
