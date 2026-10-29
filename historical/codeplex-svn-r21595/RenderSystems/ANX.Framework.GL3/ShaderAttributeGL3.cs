using System;
using OpenTK.Graphics.OpenGL;
using ANX.Framework.Graphics;

namespace ANX.RenderSystem.GL3
{
	public struct ShaderAttributeGL3
	{
		#region Public
		public string Name;
		public int Location;
		public int Size;
		public ActiveAttribType Type;
		#endregion

		#region Constructor
		public ShaderAttributeGL3(int programHandle, int index)
		{
			Name = GL.GetActiveAttrib(programHandle, index, out Size, out Type);
			Location = GL.GetAttribLocation(programHandle, Name);
		}
		#endregion

		#region Bind
		public void Bind(VertexElementUsage usage, int stride, int offset)
		{
			GL.EnableVertexAttribArray(Location);
			ErrorHelper.Check("Failed to bind shader attribute " + Name);

			int size = 0;
			VertexAttribPointerType type = VertexAttribPointerType.Float;
			bool normalized = false;

			switch (usage)
			{
				case VertexElementUsage.Binormal:
				case VertexElementUsage.Normal:
				case VertexElementUsage.Tangent:
				case VertexElementUsage.BlendIndices:
				case VertexElementUsage.BlendWeight:
				case VertexElementUsage.Position:
					size = 3;
					break;

				case VertexElementUsage.Color:
					size = 4;
					type = VertexAttribPointerType.UnsignedByte;
					normalized = true;
					break;

				case VertexElementUsage.TextureCoordinate:
					size = 2;
					break;

				case VertexElementUsage.Fog:
				case VertexElementUsage.PointSize:
				case VertexElementUsage.TessellateFactor:
					size = 1;
					break;

				// TODO
				case VertexElementUsage.Depth:
				case VertexElementUsage.Sample:
					throw new NotImplementedException();
			}

			GL.VertexAttribPointer(Location, size, type, normalized, stride, (IntPtr)offset);
		}
		#endregion

		#region ToString
		public override string ToString()
		{
			return "ShaderAttribute{Name: " + Name +
				", Location: " + Location +
				", Size: " + Size +
				", Type: " + Type + "}";
		}
		#endregion
	}
}
