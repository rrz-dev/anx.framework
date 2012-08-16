using System;
using System.Collections.Generic;
using System.IO;

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ExtendedShader
	{
		public Dictionary<string, ExtendedShaderPass[]> Techniques;
		public List<ExtendedShaderParameter> Parameters;

		public ExtendedShader(Stream stream)
		{
			Techniques = new Dictionary<string, ExtendedShaderPass[]>();
			Parameters = new List<ExtendedShaderParameter>();

			BinaryReader reader = new BinaryReader(stream);

			int numberOfVariables = reader.ReadInt32();
			for (int index = 0; index < numberOfVariables; index++)
			{
				Parameters.Add(new ExtendedShaderParameter(reader));
			}

			int numberOfStructures = reader.ReadInt32();
			for (int index = 0; index < numberOfStructures; index++)
			{
				string name = reader.ReadString();
				int numberOfStructVariables = reader.ReadInt32();
				for (int varIndex = 0; varIndex < numberOfStructVariables; varIndex++)
				{
					string varType = reader.ReadString();
					string varName = reader.ReadString();
					string varSemantic = reader.ReadString();
				}
			}

			int numberOfTechniques = reader.ReadInt32();
			for (int index = 0; index < numberOfTechniques; index++)
			{
				string name = reader.ReadString();
				int numberOfPasses = reader.ReadInt32();
				ExtendedShaderPass[] passes = new ExtendedShaderPass[numberOfPasses];
				Techniques.Add(name, passes);

				for (int passIndex = 0; passIndex < numberOfPasses; passIndex++)
				{
					passes[passIndex] = new ExtendedShaderPass(reader);
				}
			}
		}
	}
}
