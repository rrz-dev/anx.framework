using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro.Shader
{
	public class ExtendedShaderParameter
    {
        #region Public
        public string Type
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public int ArraySize
        {
            get;
            private set;
        }

        public int[] TypeDimensions
        {
            get;
            private set;
        }

        public int SizeInBytes
        {
            get;
            private set;
        }

        public bool IsTexture
        {
            get;
            private set;
        }
        #endregion

        #region Constructor
        public ExtendedShaderParameter(BinaryReader reader)
		{
			Type = reader.ReadString();
			Name = reader.ReadString();
            ArraySize = reader.ReadInt32();

            IsTexture = Type.ToLower().Contains("texture");
            SizeInBytes = GetParameterTypeSize();
            if (ArraySize > 0)
                SizeInBytes *= ArraySize;

            int numberOfDimensions = reader.ReadByte();
            TypeDimensions = new int[numberOfDimensions];
            for (int dimIndex = 0; dimIndex < numberOfDimensions; dimIndex++)
            {
                TypeDimensions[dimIndex] = (int)reader.ReadByte();
                SizeInBytes *= TypeDimensions[dimIndex];
            }
		}
        #endregion

        #region GetParameterTypeSize
        private int GetParameterTypeSize()
        {
            if (IsTexture)
                return 0;

            if (Type == "float" ||
                Type == "int" ||
                Type == "uint" ||
                Type == "dword")
                return 4;
            if (Type == "double")
                return 8;
            if (Type == "bool")
                return 1;
            if (Type == "half")
                return 2;

            throw new NotImplementedException("Parameter type " + Type + " has no size value!");
        }
        #endregion
    }
}
