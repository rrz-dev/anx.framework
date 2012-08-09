#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
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
            VertexElement[] elements = new VertexElement[] { new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                                                             new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                                                             new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
                                                           };
            VertexDeclaration d = new VertexDeclaration(32, elements);
            d.Name = "VertexPositionNormalTexture.VertexDeclaration";
            VertexDeclaration = d;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("{{Position:{0} Normal:{1} TextureCoordinate:{2}}}", this.Position, this.Normal, this.TextureCoordinate);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                return this == (VertexPositionNormalTexture)obj;
            }

            return false;
        }

        public static bool operator ==(VertexPositionNormalTexture lhs, VertexPositionNormalTexture rhs)
        {
            return lhs.Normal.Equals(rhs.Normal) && lhs.Position.Equals(rhs.Position) && lhs.TextureCoordinate.Equals(rhs.TextureCoordinate);
        }

        public static bool operator !=(VertexPositionNormalTexture lhs, VertexPositionNormalTexture rhs)
        {
            return !lhs.Normal.Equals(rhs.Normal) || !lhs.Position.Equals(rhs.Position) || !lhs.TextureCoordinate.Equals(rhs.TextureCoordinate);
        }
    }
}
