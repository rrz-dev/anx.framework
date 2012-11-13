#region Using Statements
using System;
using System.IO;
using ANX.Framework.Content.Pipeline.Helpers.DX11MetroShaderGenerator;
using ANX.Framework.Content.Pipeline.Helpers.GL3;
using System.Diagnostics;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.NonXNA;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Content.Pipeline.Processors;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

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

		private static Byte[] CompileShader(string RenderSystem, string sourceCode, string directory)
		{
			byte[] byteCode;

			switch (RenderSystem)
			{
				case "ANX.RenderSystem.Windows.DX10":
                    byteCode = CompileDXEffect(sourceCode, directory, EffectSourceLanguage.HLSL_FX ,"fx_4_0");
					break;

				case "ANX.RenderSystem.Windows.DX11":
                    byteCode = CompileDXEffect(sourceCode, directory, EffectSourceLanguage.HLSL_FX, "fx_5_0");
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

        private static byte[] CompileDXEffect(string sourceCode, string directory, EffectSourceLanguage sourceLanguage, String targetProfile)
        {
            EffectContent effectContent = new EffectContent()
            {
                EffectCode = sourceCode,
                Identity = new ContentIdentity(null, "StockShaderCodeGenerator", null),
                SourceLanguage = sourceLanguage,
            };

            BuildContent buildContentTask = new BuildContent();
            IContentProcessor instance = buildContentTask.ProcessorManager.GetInstance("EffectProcessor");
            ((EffectProcessor)instance).TargetProfile = targetProfile;
            CompiledEffectContent effect = instance.Process(effectContent, null) as CompiledEffectContent;
            
            return effect.GetEffectCode();
        }

    }
}
