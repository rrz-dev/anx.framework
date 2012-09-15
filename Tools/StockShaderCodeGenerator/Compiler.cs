#region Using Statements
using System;
using System.IO;
using ANX.RenderSystem.Windows.DX10;
using ANX.RenderSystem.Windows.DX11;
using ANX.RenderSystem.GL3;
using DX11MetroShaderGenerator;
using System.Diagnostics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using EffectDX10 = ANX.RenderSystem.Windows.DX10.EffectDX;
using EffectDX11 = ANX.RenderSystem.Windows.DX11.EffectDX;

namespace StockShaderCodeGenerator
{
	public static class Compiler
	{
		#region GenerateShaders
		public static bool GenerateShaders()
		{
			Program.TraceListener.WriteLine("generating shaders...");

			for (int i = 0; i < Configuration.Shaders.Count; i++)
			{
				Shader s = Configuration.Shaders[i];

				Program.TraceListener.WriteLine(String.Format("-> loading shader for type '{0}' (file: '{1}')", s.Type, s.Source));
				String source = String.Empty;
				if (File.Exists(s.Source))
				{
					source = File.ReadAllText(s.Source);
				}

				Program.TraceListener.Write("--> compiling shader... ");
				try
				{
					s.ByteCode = CompileShader(s.RenderSystem, source, Path.GetDirectoryName(s.Source));
					Program.TraceListener.WriteLine(String.Format("{0} bytes compiled size", s.ByteCode.Length));
					s.ShaderCompiled = true;
				}
				catch (Exception ex)
				{
					s.ShaderCompiled = false;
					Program.TraceListener.WriteLine("--> error occured while compiling shader: {0}", ex.Message);
					return false;
				}

				Configuration.Shaders[i] = s;
			}

			Program.TraceListener.WriteLine("finished generating shaders...");
			return true;
		}
		#endregion

		#region CompileShader
		private static Byte[] CompileShader(string RenderSystem,
			string sourceCode, string directory)
		{
			byte[] byteCode;

			switch (RenderSystem)
			{
				case "ANX.RenderSystem.Windows.DX10":
					byteCode = EffectDX10.CompileFXShader(sourceCode, directory);
					break;

				case "ANX.RenderSystem.Windows.DX11":
					byteCode = EffectDX11.CompileFXShader(sourceCode, directory);
					break;

				case "ANX.RenderSystem.Windows.Metro":
					var metroGenerator = new MetroCodeGenerator(sourceCode);
					byteCode = metroGenerator.ResultByteCode;
					break;

				case "ANX.RenderSystem.GL3":
					byteCode = ShaderHelper.SaveShaderCode(sourceCode);
					break;

				default:
					throw new NotImplementedException("compiling shaders for " +
						RenderSystem + " not yet implemented...");
			}

			return byteCode;
		}
		#endregion
	}
}
