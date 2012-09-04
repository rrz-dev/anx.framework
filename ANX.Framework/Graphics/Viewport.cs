using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public struct Viewport
	{
		#region Private
		private int x;
        private int y;
        private int width;
        private int height;
        private float near;
        private float far;
		#endregion

		#region Public
		public float AspectRatio
		{
			get
			{
				if (this.width != 0 && this.height != 0)
					return (float)width / (float)height;

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
				// On Windows Xna simply returns the rectangle. Check if we need any other implementation on other platforms!
				return new Rectangle(x, y, width, height);
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
		#endregion

		#region Constructor
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
            this.height = bounds.Height;
			this.near = 0f;
            this.far = 1f;
        }
		#endregion

		#region Project
		public Vector3 Project(Vector3 source, Matrix projection, Matrix view, Matrix world)
		{
			Matrix wv;
			Matrix wvp;
			Matrix.Multiply(ref world, ref view, out wv);
			Matrix.Multiply(ref wv, ref projection, out wvp);
			Vector3 vector;
			Vector3.Transform(ref source, ref wvp, out vector);
			float num = source.X * wvp.M14 + source.Y * wvp.M24 + source.Z * wvp.M34 + wvp.M44;
			if (WithinEpsilon(num) == false)
				vector /= num;
			vector.X = (vector.X + 1f) * 0.5f * (float)width + (float)x;
			vector.Y = (-vector.Y + 1f) * 0.5f * (float)height + (float)y;
			vector.Z = vector.Z * (far - near) + near;
			return vector;
		}
		#endregion

		#region Unproject
		public Vector3 Unproject(Vector3 source, Matrix projection, Matrix view, Matrix world)
		{
			Matrix wv;
			Matrix wvp;
			Matrix.Multiply(ref world, ref view, out wv);
			Matrix.Multiply(ref wv, ref projection, out wvp);
			wvp = Matrix.Invert(wvp);
			source.X = (source.X - (float)x) / (float)width * 2f - 1f;
			source.Y = -((source.Y - (float)y) / (float)height * 2f - 1f);
			source.Z = (source.Z - near) / (far - near);
			Vector3 vector;
			Vector3.Transform(ref source, ref wvp, out vector);
			float num = source.X * wvp.M14 + source.Y * wvp.M24 + source.Z * wvp.M34 + wvp.M44;
			return (WithinEpsilon(num) == false) ? vector /= num : vector;
		}
		#endregion

		#region WithinEpsilon
		private static bool WithinEpsilon(float num)
		{
			num -= 1f;
			return -1.401298E-45f <= num && num <= 1.401298E-45f;
		}
		#endregion

		#region ToString
		public override string ToString()
        {
            return String.Format("Viewport X: {0} Y:{1} Width: {2} Height: {3} AspectRatio: {4} MinDepth: {5} MaxDepth: {6}",
				X, Y, Width, Height, AspectRatio, MinDepth, MaxDepth);
        }
		#endregion
    }
}
