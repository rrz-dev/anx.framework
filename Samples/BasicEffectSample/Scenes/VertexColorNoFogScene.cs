using System;
using ANX.Framework.Graphics;
using ANX.Framework.Content;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.Scenes
{
	public class VertexColorNoFogScene : BaseScene
	{
	    public override string Name
	    {
	        get { return "VertexColor"; }
	    }

		public override void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
		{
			effect = new BasicEffect(graphicsDevice);

			vertices = new VertexBuffer(graphicsDevice, VertexPositionColor.VertexDeclaration, 4, BufferUsage.WriteOnly);
			vertices.SetData(new[]
			{
				new VertexPositionColor(new Vector3(-5f, 0f, -5f), Color.Red),
				new VertexPositionColor(new Vector3(-5f, 0f, 5f), Color.Green),
				new VertexPositionColor(new Vector3(5f, 0f, 5f), Color.Blue),
				new VertexPositionColor(new Vector3(5f, 0f, -5f), Color.Yellow),
			});

			indices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly);
			indices.SetData<ushort>(new ushort[] { 0, 2, 1, 0, 3, 2 });
		}

		public override void Draw(GraphicsDevice graphicsDevice)
        {
            SetCameraMatrices();
			effect.VertexColorEnabled = true;
			effect.LightingEnabled = false;
            effect.EmissiveColor = Color.Black.ToVector3();
            ToggleFog(false);
			effect.CurrentTechnique.Passes[0].Apply();

			graphicsDevice.Indices = indices;
			graphicsDevice.SetVertexBuffer(vertices);
			graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
		}
	}
}
