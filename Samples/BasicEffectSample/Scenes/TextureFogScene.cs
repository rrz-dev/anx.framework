using System;
using ANX.Framework.Graphics;
using ANX.Framework.Content;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.Scenes
{
	public class TextureFogScene : BaseScene
	{
	    public override string Name
	    {
	        get { return "Texture with Fog"; }
	    }

		private Texture2D texture;

		public override void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
		{
			texture = content.Load<Texture2D>("Textures/stone_tile");
			effect = new BasicEffect(graphicsDevice);

			vertices = new VertexBuffer(graphicsDevice, VertexPositionTexture.VertexDeclaration, 4, BufferUsage.WriteOnly);
			vertices.SetData(new[]
			{
				new VertexPositionTexture(new Vector3(-5f, 0f, -5f), new Vector2(0, 0)),
				new VertexPositionTexture(new Vector3(-5f, 0f, 5f), new Vector2(0, 1)),
				new VertexPositionTexture(new Vector3(5f, 0f, 5f), new Vector2(1, 1)),
				new VertexPositionTexture(new Vector3(5f, 0f, -5f), new Vector2(1, 0)),
			});

			indices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly);
			indices.SetData(new ushort[] { 0, 2, 1, 0, 3, 2 });
		}

		public override void Draw(GraphicsDevice graphicsDevice)
		{
            ToggleFog(true);
            SetCameraMatrices();
			effect.DiffuseColor = Color.White.ToVector3();
			effect.EmissiveColor = Color.Black.ToVector3();
			effect.LightingEnabled = false;
			effect.VertexColorEnabled = false;
			effect.TextureEnabled = true;
			effect.Texture = texture;
			effect.CurrentTechnique.Passes[0].Apply();

			graphicsDevice.Indices = indices;
			graphicsDevice.SetVertexBuffer(vertices);
			graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
		}
	}
}
