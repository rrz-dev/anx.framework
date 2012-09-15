using System;
using SharpDX.D3DCompiler;
using System.IO;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#endif
#if DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
	public partial class EffectDX
	{
		protected Effect managedEffect;

		#region Constructor
		protected EffectDX(Effect managedEffect)
		{
			if (managedEffect == null)
				throw new ArgumentNullException("managedEffect");
			this.managedEffect = managedEffect;
		}
		#endregion

		#region GetByteCode
		protected ShaderBytecode GetByteCode(Stream stream)
		{
			if (stream.CanSeek)
				stream.Seek(0, SeekOrigin.Begin);

			return ShaderBytecode.FromStream(stream);
		}
		#endregion

		#region CompileVertexShader (TODO)
		public static byte[] CompileVertexShader(string effectCode, string directory = "")
		{
			// TODO: not all entry points are named VS!
			ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(effectCode, "VS", "vs_4_0", ShaderFlags.None,
				EffectFlags.None, null, new IncludeHandler(directory), "unknown");
			byte[] bytecode = new byte[vertexShaderByteCode.BufferSize];
			vertexShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
			return bytecode;
		}
		#endregion

		#region CompilePixelShader (TODO)
		public static byte[] CompilePixelShader(string effectCode, string directory = "")
		{
			// TODO: not all entry points are named PS!
			ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(effectCode, "PS", "ps_4_0", ShaderFlags.None,
				EffectFlags.None, null, new IncludeHandler(directory), "unknown");
			byte[] bytecode = new byte[pixelShaderByteCode.BufferSize];
			pixelShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
			return bytecode;
		}
		#endregion

		protected static byte[] CompileShader(string profile, string effectCode, string directory = "")
		{
			ShaderBytecode effectByteCode = ShaderBytecode.Compile(effectCode, profile, ShaderFlags.None, EffectFlags.None,
				null, new IncludeHandler(directory), "unknown");
			byte[] bytecode = new byte[effectByteCode.BufferSize];
			effectByteCode.Data.Read(bytecode, 0, bytecode.Length);
			return bytecode;
		}
	}
}
