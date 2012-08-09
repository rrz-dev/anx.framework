#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.RenderSystem.Windows.DX10;
using ANX.RenderSystem.Windows.GL3;
using System.IO;
using ANX.RenderSystem.Windows.DX11;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace StockShaderCodeGenerator
{
    public static class Compiler
    {
        public static bool GenerateShaders()
        {
            Console.WriteLine("generating shaders...");

            for (int i = 0; i < Configuration.Shaders.Count; i++)
            {
                Shader s = Configuration.Shaders[i];

                Console.WriteLine("-> loading shader for type '{0}' (file: '{1}')", s.Type, s.Source);
                String source = String.Empty;
                if (File.Exists(s.Source))
                {
                    source = File.ReadAllText(s.Source);
                }

                Console.Write("--> compiling shader... ");
                try
                {
                    s.ByteCode = CompileShader(s.RenderSystem, source, Path.GetDirectoryName(s.Source));
                    Console.WriteLine("{0} bytes compiled size", s.ByteCode.Length);
                    s.ShaderCompiled = true;
                }
                catch (Exception ex)
                {
                    s.ShaderCompiled = false;
                    Console.WriteLine("--> error occured while compiling shader: {0}", ex.Message);
                    return false;
                }

                Configuration.Shaders[i] = s;
            }

            Console.WriteLine("finished generating shaders...");
            return true;
        }

        private static Byte[] CompileShader(string RenderSystem, string sourceCode, string directory)
        {
            byte[] byteCode;

            switch (RenderSystem)
            {
                case "ANX.Framework.Windows.DX10":
                    byteCode = Effect_DX10.CompileFXShader(sourceCode, directory);
                    break;
                case "ANX.RenderSystem.Windows.DX11":
                    byteCode = Effect_DX11.CompileFXShader(sourceCode, directory);
                    break;
                case "ANX.Framework.Windows.GL3":
                    byteCode = ShaderHelper.SaveShaderCode(sourceCode);
                    break;
                default:
                    throw new NotImplementedException("compiling shaders for " + RenderSystem + " not yet implemented...");
            }

            return byteCode;
        }
    }
}
