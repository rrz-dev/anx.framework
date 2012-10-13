using System;
using ANX.Framework;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.VertexTypes
{
    public struct VertexPositionNormalColorTexture : IVertexType
    {
		public Vector3 Position;
		public Vector3 Normal;
        public Color Color;
        public Vector2 TextureCoordinate;

		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration
		{
			get { return VertexDeclaration; }
		}

		public VertexPositionNormalColorTexture(Vector3 position, Vector3 normal, Color color, Vector2 texcoord)
		{
			Position = position;
		    Normal = normal;
			Color = color;
            TextureCoordinate = texcoord;
		}

        static VertexPositionNormalColorTexture()
		{
			VertexElement[] elements =
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
				new VertexElement(24, VertexElementFormat.Color, VertexElementUsage.Color, 0),
				new VertexElement(28, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
			};

			VertexDeclaration = new VertexDeclaration(36, elements);
            VertexDeclaration.Name = "VertexPositionNormalColorTexture.VertexDeclaration";
		}
    }
}
