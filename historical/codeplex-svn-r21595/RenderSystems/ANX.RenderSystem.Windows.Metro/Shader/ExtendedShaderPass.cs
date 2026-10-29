using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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
