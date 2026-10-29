using System;
using ANX.Framework.Graphics;
using ANX.Framework.Content;
using ANX.Framework;
using BasicEffectSample.VertexTypes;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.Scenes
{
    public class LitVertexColorTextureFogScene : BaseScene
	{
        public override string Name
        {
            get { return mode + " with VertexColor, Texture and Fog"; }
        }

        private Texture2D texture;
	    private readonly LightingMode mode;

        public LitVertexColorTextureFogScene(LightingMode setMode)
        {
            mode = setMode;
        }

		public override void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            texture = content.Load<Texture2D>("Textures/stone_tile");
			effect = new BasicEffect(graphicsDevice);

            vertices = new VertexBuffer(graphicsDevice, VertexPositionNormalColorTexture.VertexDeclaration, 6, BufferUsage.WriteOnly);
            float textureFactor = 1f / 7f;
			vertices.SetData(new[]
			{
                new VertexPositionNormalColorTexture(new Vector3(0f, 0f, -5f), new Vector3(-1f, 0f, 0f), Color.OrangeRed, new Vector2(0, 0)),
                new VertexPositionNormalColorTexture(new Vector3(0f, 0f, 5f), new Vector3(-1f, 0f, 0f), Color.LightGreen, new Vector2(0, 1)),
                new VertexPositionNormalColorTexture(new Vector3(0f, 2f, -5f), Vector3.Normalize(new Vector3(-1f, 1f, 0f)), Color.OrangeRed, new Vector2(textureFactor * 2f, 0)),
                new VertexPositionNormalColorTexture(new Vector3(0f, 2f, 5f), Vector3.Normalize(new Vector3(-1f, 1f, 0f)), Color.LightGreen, new Vector2(textureFactor * 2f, 1)),
                new VertexPositionNormalColorTexture(new Vector3(5f, 2f, -5f), new Vector3(0f, 1f, 0f), Color.OrangeRed, new Vector2(1, 0)),
                new VertexPositionNormalColorTexture(new Vector3(5f, 2f, 5f), new Vector3(0f, 1f, 0f), Color.LightGreen, new Vector2(1, 1))
			});

			indices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 12, BufferUsage.WriteOnly);
			indices.SetData(new ushort[] { 0, 2, 1, 2, 3, 1, 2, 4, 3, 4, 5, 3 });
		}

		public override void Draw(GraphicsDevice graphicsDevice)
        {
            ToggleFog(true);
            SetCameraMatrices();
			effect.DiffuseColor = Color.White.ToVector3();
			effect.EmissiveColor = Color.Black.ToVector3();
		    effect.VertexColorEnabled = true;
            effect.TextureEnabled = true;
            effect.Texture = texture;

            EnableLightingMode(mode);

			effect.CurrentTechnique.Passes[0].Apply();

			graphicsDevice.Indices = indices;
			graphicsDevice.SetVertexBuffer(vertices);
			graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 4);
		}
	}
}
