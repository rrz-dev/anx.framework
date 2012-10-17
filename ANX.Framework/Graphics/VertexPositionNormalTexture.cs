#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
#if !WINDOWSMETRO      //TODO: search replacement for Win8
    [Serializable]
#endif
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public struct VertexPositionNormalTexture : IVertexType
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TextureCoordinate;

        public static readonly VertexDeclaration VertexDeclaration;

        VertexDeclaration IVertexType.VertexDeclaration
        {
            get { return VertexDeclaration; }
        }

        public VertexPositionNormalTexture(Vector3 position, Vector3 normal, Vector2 textureCoordinate)
        {
            this.Position = position;
            this.Normal = normal;
            this.TextureCoordinate = textureCoordinate;
        }

        static VertexPositionNormalTexture()
        {
            VertexElement[] elements =
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
				new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
			};
            VertexDeclaration = new VertexDeclaration(32, elements)
            {
                Name = "VertexPositionNormalTexture.VertexDeclaration"
            };
        }

        public override int GetHashCode()
        {
            return HashHelper.GetGCHandleHashCode(this);
        }

        public override string ToString()
        {
            return String.Format("{{Position:{0} Normal:{1} TextureCoordinate:{2}}}", Position, Normal, TextureCoordinate);
        }

        public override bool Equals(object obj)
        {
            return obj is VertexPositionNormalTexture && this == (VertexPositionNormalTexture)obj;
        }

        public static bool operator ==(VertexPositionNormalTexture lhs, VertexPositionNormalTexture rhs)
        {
            return lhs.Normal.Equals(rhs.Normal) && lhs.Position.Equals(rhs.Position) &&
				lhs.TextureCoordinate.Equals(rhs.TextureCoordinate);
        }

        public static bool operator !=(VertexPositionNormalTexture lhs, VertexPositionNormalTexture rhs)
        {
			return lhs.Normal.Equals(rhs.Normal) == false || lhs.Position.Equals(rhs.Position) == false ||
				lhs.TextureCoordinate.Equals(rhs.TextureCoordinate) == false;
        }
    }
}
