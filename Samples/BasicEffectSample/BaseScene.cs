using System;
using ANX.Framework;
using ANX.Framework.Content;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample
{
	public abstract class BaseScene
    {
        protected BasicEffect effect;
        protected VertexBuffer vertices;
        protected IndexBuffer indices;

		public abstract string Name { get; }

		public abstract void Initialize(ContentManager content, GraphicsDevice graphicsDevice);

		public abstract void Draw(GraphicsDevice graphicsDevice);

        protected void EnableLightingMode(LightingMode mode)
        {
            effect.LightingEnabled = true;
            if (mode == LightingMode.VertexLighting)
            {
                effect.EnableDefaultLighting();
                effect.PreferPerPixelLighting = false;
            }
            else if (mode == LightingMode.OneLight)
            {
                effect.DirectionalLight0.Enabled = true;
                effect.DirectionalLight1.Enabled = false;
                effect.DirectionalLight2.Enabled = false;
                effect.PreferPerPixelLighting = false;
            }
            else if (mode == LightingMode.PixelLighting)
            {
                effect.EnableDefaultLighting();
                effect.PreferPerPixelLighting = true;
            }
        }

        protected void ToggleFog(bool enabled)
        {
            if (enabled)
            {
                effect.FogStart = 1f;
                effect.FogEnd = 15f;
                effect.FogColor = Color.Gray.ToVector3();
            }

            effect.FogEnabled = enabled;
        }

        protected void SetCameraMatrices()
        {
            effect.World = Camera.World;
            effect.View = Camera.View;
            effect.Projection = Camera.Projection;
        }
	}
}
