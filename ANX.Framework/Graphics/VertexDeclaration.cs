#region Using Statements
using System;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class VertexDeclaration : GraphicsResource
    {
        private int vertexStride;
        private VertexElement[] elements;

        public VertexDeclaration(params VertexElement[] elements)
        {
            this.elements = elements;


            for (int i = 0; i < this.elements.Length; i++)
            {
                this.vertexStride += GetElementStride(this.elements[i].VertexElementFormat);
            }
        }

        public VertexDeclaration(int vertexStride, params VertexElement[] elements)
        {
            this.elements = elements;
            this.vertexStride = vertexStride;
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public int VertexStride
        {
            get
            {
                return this.vertexStride;
            }
        }

        public VertexElement[] GetVertexElements()
        {
            if (elements != null)
            {
                return elements.Clone() as VertexElement[];
            }
            else
            {
                return null;
            }
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            throw new NotImplementedException();
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
                    throw new ArgumentException("unknown VertexElementFormat size '" + format.ToString() + "'");
            }
        }
    }
}
