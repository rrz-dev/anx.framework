using System;
using System.Collections.Generic;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ExtendedShader
    {
        #region Private
        private Dictionary<string, ExtendedShaderPass[]> techniques;
        #endregion

        #region Public
        public ExtendedShaderPass[] this[string name]
        {
            get
            {
                return techniques[name];
            }
        }

        public string[] TechniqueNames
        {
            get
            {
                return new List<string>(techniques.Keys).ToArray();
            }
        }

        public ExtendedShaderParameter[] Parameters
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public ExtendedShader(Stream stream)
		{
			techniques = new Dictionary<string, ExtendedShaderPass[]>();

			BinaryReader reader = new BinaryReader(stream);

			int numberOfVariables = reader.ReadInt32();
            Parameters = new ExtendedShaderParameter[numberOfVariables];
			for (int index = 0; index < numberOfVariables; index++)
			{
				Parameters[index] = new ExtendedShaderParameter(reader);
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
				techniques.Add(name, passes);

				for (int passIndex = 0; passIndex < numberOfPasses; passIndex++)
				{
					passes[passIndex] = new ExtendedShaderPass(reader);
				}
			}
        }
        #endregion
	}
}
