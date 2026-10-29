using System;
using ANX.Framework.Graphics;
using ANX.Framework.Content;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.Scenes
{
	public class DiffuseNoFogScene : BaseScene
	{
	    public override string Name
	    {
	        get { return "DiffuseColor"; }
	    }

		public override void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
		{
			effect = new BasicEffect(graphicsDevice);
			var declaration = new VertexDeclaration(12,
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0));

			vertices = new VertexBuffer(graphicsDevice, declaration, 4, BufferUsage.WriteOnly);
			vertices.SetData(new[]
			{
				new Vector3(-5f, 0f, -5f),
				new Vector3(-5f, 0f, 5f),
				new Vector3(5f, 0f, 5f),
				new Vector3(5f, 0f, -5f),
			});

			indices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly);
			indices.SetData(new ushort[] { 0, 2, 1, 0, 3, 2 });
		}

		public override void Draw(GraphicsDevice graphicsDevice)
        {
            SetCameraMatrices();
			effect.DiffuseColor = Color.Red.ToVector3();
			effect.EmissiveColor = Color.Black.ToVector3();
            effect.LightingEnabled = false;
            ToggleFog(false);
			effect.CurrentTechnique.Passes[0].Apply();

			graphicsDevice.Indices = indices;
			graphicsDevice.SetVertexBuffer(vertices);
			graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
		}
	}
}
