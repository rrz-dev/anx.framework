using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace DX11MetroShaderGenerator
{
	public class CompiledPass
	{
		public byte[] VertexShaderCode
		{
			get;
			private set;
		}

		public byte[] PixelShaderCode
		{
			get;
			private set;
		}

		public CompiledPass(byte[] setVertexShaderCode, byte[] setPixelShaderCode)
		{
			VertexShaderCode = setVertexShaderCode;
			PixelShaderCode = setPixelShaderCode;
		}
	}
}
