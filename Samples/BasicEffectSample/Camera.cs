using System;
using ANX.Framework;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample
{
	public static class Camera
	{
		public static Matrix World;
		public static Matrix View;
		public static Matrix Projection;

		public static void Initialize(GraphicsDevice graphicsDevice)
		{
			World = Matrix.Identity;
			View = Matrix.CreateLookAt(new Vector3(-1f, 5f, 8f), Vector3.Zero, Vector3.Up);
			Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, graphicsDevice.Viewport.AspectRatio, 1f, 100f);
		}
	}
}
