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
    [TestState(TestStateAttribute.TestState.InProgress)]
	public struct VertexPositionColor : IVertexType
	{
		public Vector3 Position;
		public Color Color;

		public static readonly VertexDeclaration VertexDeclaration;

		VertexDeclaration IVertexType.VertexDeclaration
		{
			get { return VertexDeclaration; }
		}

		public VertexPositionColor(Vector3 position, Color color)
		{
			this.Position = position;
			this.Color = color;
		}

		static VertexPositionColor()
		{
			VertexElement[] elements =
			{
				new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
				new VertexElement(12, VertexElementFormat.Color, VertexElementUsage.Color, 0),
			};

		    VertexDeclaration = new VertexDeclaration(16, elements)
		    {
		        Name = "VertexPositionColor.VertexDeclaration"
		    };
		}

		public override int GetHashCode()
		{
            return HashHelper.GetGCHandleHashCode(this);
		}

		public override string ToString()
		{
			return String.Format("{{Position:{0} Color:{1}}}", Position, Color);
		}

		public override bool Equals(object obj)
		{
		    return obj is VertexPositionColor && this == (VertexPositionColor)obj;
		}

	    public static bool operator ==(VertexPositionColor lhs, VertexPositionColor rhs)
		{
			return lhs.Color.Equals(rhs.Color) && lhs.Position.Equals(rhs.Position);
		}

		public static bool operator !=(VertexPositionColor lhs, VertexPositionColor rhs)
		{
			return lhs.Color.Equals(rhs.Color) == false || lhs.Position.Equals(rhs.Position) == false;
		}
	}
}
