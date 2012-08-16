using System;
using System.IO;

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ExtendedShaderPass
	{
		public string Name;
		public byte[] VertexCode;
		public byte[] PixelCode;

		public ExtendedShaderPass(BinaryReader reader)
		{
			Name = reader.ReadString();
			int vertexCodeLength = reader.ReadInt32();
			VertexCode = reader.ReadBytes(vertexCodeLength);
			int pixelCodeLength = reader.ReadInt32();
			PixelCode = reader.ReadBytes(pixelCodeLength);
		}
	}
}
