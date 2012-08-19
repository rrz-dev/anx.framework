using System;
using System.IO;

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ExtendedShaderParameter
	{
		public string Type;
		public string Name;
        public int ArraySize;
        public int[] TypeDimensions;

		public ExtendedShaderParameter(BinaryReader reader)
		{
			Type = reader.ReadString();
			Name = reader.ReadString();
            ArraySize = reader.ReadInt32();

            int numberOfDimensions = reader.ReadByte();
            TypeDimensions = new int[numberOfDimensions];
            for (int dimIndex = 0; dimIndex < numberOfDimensions; dimIndex++)
            {
                TypeDimensions[dimIndex] = (int)reader.ReadByte();
            }
		}
	}
}
