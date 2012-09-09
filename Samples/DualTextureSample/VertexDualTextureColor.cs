using System;
using ANX.Framework;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace DualTextureSample
{
	public struct VertexDualTextureColor : IVertexType
	{
		public Vector3 Position;
		public Vector2 TextureCoordinate;
		public Vector2 TextureCoordinate2;
		public Color Color;

		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration
		{
			get { return VertexDeclaration; }
		}

		public VertexDualTextureColor(Vector3 position, Vector2 textureCoordinate, Vector2 textureCoordinate2, Color color)
		{
			Position = position;
			TextureCoordinate = textureCoordinate;
			TextureCoordinate2 = textureCoordinate2;
			Color = color;
		}

		static VertexDualTextureColor()
		{
			var elements = new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
				new VertexElement(20, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),
				new VertexElement(28, VertexElementFormat.Color, VertexElementUsage.Color, 0),
			};
			VertexDeclaration = new VertexDeclaration(32, elements);
			VertexDeclaration.Name = "VertexDualTextureColor.VertexDeclaration";
		}
	}
}
