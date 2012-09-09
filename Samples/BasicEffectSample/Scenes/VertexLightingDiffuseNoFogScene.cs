using System;
using ANX.Framework.Graphics;
using ANX.Framework.Content;
using ANX.Framework;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.Scenes
{
	public class VertexLightingDiffuseNoFogScene : BaseScene
	{
		public override string Name
		{
			get
			{
				return "VertexLighting with DiffuseColor";
			}
		}

		private BasicEffect effect;
		private VertexBuffer vertices;
		private IndexBuffer indices;

		public override void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
		{
			effect = new BasicEffect(graphicsDevice);

			var elements = new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			};
			var declaration = new VertexDeclaration(24, elements);

			vertices = new VertexBuffer(graphicsDevice, declaration, 6, BufferUsage.WriteOnly);
			vertices.SetData<Vector3>(new Vector3[]
			{
				new Vector3(0f, 0f, -5f), new Vector3(-1f, 0f, 0f),
				new Vector3(0f, 0f, 5f), new Vector3(-1f, 0f, 0f),
				new Vector3(0f, 2f, -5f), Vector3.Normalize(new Vector3(-1f, 1f, 0f)),
				new Vector3(0f, 2f, 5f), Vector3.Normalize(new Vector3(-1f, 1f, 0f)),
				new Vector3(5f, 2f, -5f), new Vector3(0f, 1f, 0f),
				new Vector3(5f, 2f, 5f), new Vector3(0f, 1f, 0f),
			});

			indices = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 12, BufferUsage.WriteOnly);
			indices.SetData<ushort>(new ushort[] { 0, 2, 1, 2, 3, 1, 2, 4, 3, 4, 5, 3 });
		}

		public override void Draw(GraphicsDevice graphicsDevice)
		{
			effect.World = Camera.World;
			effect.View = Camera.View;
			effect.Projection = Camera.Projection;
			effect.DiffuseColor = Color.LightGreen.ToVector3();
			effect.EmissiveColor = Color.Black.ToVector3();
			effect.LightingEnabled = true;
			effect.PreferPerPixelLighting = false;
			effect.FogEnabled = false;

			effect.EnableDefaultLighting();

			effect.CurrentTechnique.Passes[0].Apply();

			graphicsDevice.Indices = indices;
			graphicsDevice.SetVertexBuffer(vertices);
			graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 6, 0, 4);
		}
	}
}
