using System;
using ANX.Framework.Graphics;
using ANX.Framework.Content;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.Scenes
{
	public class LitDiffuseFogScene : BaseScene
	{
	    public override string Name
	    {
	        get { return mode + "VertexLighting with DiffuseColor and Fog"; }
	    }

        private readonly LightingMode mode;

        public LitDiffuseFogScene(LightingMode setMode)
        {
            mode = setMode;
        }

		public override void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
		{
			effect = new BasicEffect(graphicsDevice);

			var elements = new[]
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			};
			var declaration = new VertexDeclaration(24, elements);

			vertices = new VertexBuffer(graphicsDevice, declaration, 6, BufferUsage.WriteOnly);
			vertices.SetData(new[]
			{
				new Vector3(0f, 0f, -5f), new Vector3(-1f, 0f, 0f),
				new Vector3(0f, 0f, 5f), new Vector3(-1f, 0f, 0f),
				new Vector3(0f, 2f, -5f), Vector3.Normalize(new Vector3(-1f, 1f, 0f)),
				new Vector3(0f, 2f, 5f), Vector3.Normalize(new Vector3(-1f, 1f, 0f)),
				new Vector3(5f, 2f, -5f), new Vector3(0f, 1f, 0f),
				new Vector3(5f, 2f, 5f), new Vector3(0f, 1f, 0f),
			});

			indices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 12, BufferUsage.WriteOnly);
			indices.SetData(new ushort[] { 0, 2, 1, 2, 3, 1, 2, 4, 3, 4, 5, 3 });
		}

		public override void Draw(GraphicsDevice graphicsDevice)
        {
            ToggleFog(true);
            SetCameraMatrices();
			effect.DiffuseColor = Color.LightGreen.ToVector3();
			effect.EmissiveColor = Color.Black.ToVector3();

		    EnableLightingMode(mode);

		    effect.CurrentTechnique.Passes[0].Apply();

			graphicsDevice.Indices = indices;
			graphicsDevice.SetVertexBuffer(vertices);
			graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 4);
		}
	}
}
