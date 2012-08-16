using System;
using System.IO;

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ExtendedShaderParameter
	{
		public string Type;
		public string Name;

		public ExtendedShaderParameter(BinaryReader reader)
		{
			Type = reader.ReadString();
			Name = reader.ReadString();
		}
	}
}
