using System;
using ANX.Framework;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace BasicEffectSample.VertexTypes
{
    public struct VertexPositionNormalColor : IVertexType
    {
		public Vector3 Position;
		public Vector3 Normal;
		public Color Color;

		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration
		{
			get { return VertexDeclaration; }
		}

		public VertexPositionNormalColor(Vector3 position, Vector3 normal, Color color)
		{
			Position = position;
		    Normal = normal;
			Color = color;
		}

        static VertexPositionNormalColor()
		{
			VertexElement[] elements =
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
				new VertexElement(24, VertexElementFormat.Color, VertexElementUsage.Color, 0)
			};

			VertexDeclaration = new VertexDeclaration(28, elements);
            VertexDeclaration.Name = "VertexPositionNormalColor.VertexDeclaration";
		}
    }
}
