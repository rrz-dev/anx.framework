using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(95)]
    [TestState(TestStateAttribute.TestState.Untested)]
	public struct VertexPositionTexture : IVertexType
	{
		public Vector3 Position;
		public Vector2 TextureCoordinate;

		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration
		{
			get { return VertexDeclaration; }
		}

		public VertexPositionTexture(Vector3 position, Vector2 textureCoordinate)
		{
			this.Position = position;
			this.TextureCoordinate = textureCoordinate;
		}

		static VertexPositionTexture()
		{
			VertexElement[] elements = new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
			};
			VertexDeclaration = new VertexDeclaration(20, elements);
			VertexDeclaration.Name = "VertexPositionTexture.VertexDeclaration";
		}

		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return String.Format("{{Position:{0} TextureCoordinate:{1}}}", Position, TextureCoordinate);
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is VertexPositionTexture)
				return this == (VertexPositionTexture)obj;

			return false;
		}

		public static bool operator ==(VertexPositionTexture lhs, VertexPositionTexture rhs)
		{
			return lhs.TextureCoordinate.Equals(rhs.TextureCoordinate) && lhs.Position.Equals(rhs.Position);
		}

		public static bool operator !=(VertexPositionTexture lhs, VertexPositionTexture rhs)
		{
			return !lhs.TextureCoordinate.Equals(rhs.TextureCoordinate) || !lhs.Position.Equals(rhs.Position);
		}
	}
}
