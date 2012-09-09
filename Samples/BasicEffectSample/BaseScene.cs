using System;
using ANX.Framework.Content;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample
{
	public abstract class BaseScene
	{
		public abstract string Name { get; }
		public abstract void Initialize(ContentManager content, GraphicsDevice graphicsDevice);

		public abstract void Draw(GraphicsDevice graphicsDevice);
	}
}
