#region Using Statements
using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class VertexDeclaration : GraphicsResource
	{
		private readonly VertexElement[] elements;

	    public int VertexStride { get; private set; }

	    public VertexDeclaration(params VertexElement[] elements)
		{
            if (elements == null || elements.Length == 0)
                throw new ArgumentNullException("elements");

			this.elements = elements;

            for (int i = 0; i < this.elements.Length; i++)
                VertexStride += GetElementStride(this.elements[i].VertexElementFormat);
		}

		public VertexDeclaration(int vertexStride, params VertexElement[] elements)
        {
            if (elements == null || elements.Length == 0)
                throw new ArgumentNullException("elements");

			this.elements = elements;
			VertexStride = vertexStride;
		}

		public VertexElement[] GetVertexElements()
		{
			return elements != null ? (elements.Clone() as VertexElement[]) : null;
		}

		private int GetElementStride(VertexElementFormat format)
		{
			switch (format)
			{
				case VertexElementFormat.NormalizedShort2:
				case VertexElementFormat.Byte4:
				case VertexElementFormat.Color:
				case VertexElementFormat.HalfVector2:
				case VertexElementFormat.Short2:
				case VertexElementFormat.Single:
					return 4;
				case VertexElementFormat.HalfVector4:
				case VertexElementFormat.NormalizedShort4:
				case VertexElementFormat.Short4:
				case VertexElementFormat.Vector2:
					return 8;
				case VertexElementFormat.Vector3:
					return 12;
				case VertexElementFormat.Vector4:
					return 16;
				default:
					throw new ArgumentException("Unknown VertexElementFormat size '" + format + "'.");
			}
		}
	}
}
