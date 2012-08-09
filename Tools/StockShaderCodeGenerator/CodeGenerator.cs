#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.RenderSystem.Windows.DX10;
using System.IO;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace StockShaderCodeGenerator
{
	public static class CodeGenerator
	{
		public static void Generate()
		{
			Console.WriteLine("generating output...");

			using (StreamWriter target = new StreamWriter(Configuration.Target, false))
			{
				//
				// write header
				//
				target.WriteLine("#region Using Statements");
				target.WriteLine("using System;");
				target.WriteLine("#endregion // Using Statements");
				target.WriteLine();
				target.WriteLine(
					"// This file is part of the ANX.Framework created by the");
				target.WriteLine(
					"// \"ANX.Framework developer group\" and released under the Ms-PL license.");
				target.WriteLine(
					"// For details see: http://anxframework.codeplex.com/license");
				target.WriteLine();
				target.WriteLine("namespace {0}", Configuration.Namespace);
				target.WriteLine("{");
				target.WriteLine("\tinternal static class ShaderByteCode");
				target.WriteLine("\t{");

				for (int i = 0; i < Configuration.Shaders.Count; i++)
				{
					Shader s = Configuration.Shaders[i];

					target.WriteLine("\t\t#region {0}Shader", s.Type);
					target.WriteLine("\t\tinternal static byte[] {0}ByteCode = new byte[]", s.Type);
					target.WriteLine("\t\t{");

					target.Write("\t\t\t");
					for (int j = 0; j < s.ByteCode.Length; j++)
					{
						target.Write(s.ByteCode[j].ToString("D3"));
						if (j < s.ByteCode.Length - 1)
						{
							target.Write(", ");
						}

						if (j % 20 == 0)
						{
							target.WriteLine();
							target.Write("\t\t\t");
						}
					}

					target.WriteLine();
					target.WriteLine("\t\t};");
					target.WriteLine("\t\t#endregion //{0}Shader", s.Type);
					target.WriteLine();
				}

				target.WriteLine("\t}");
				target.WriteLine("}");
			}

			Console.WriteLine("finished generating output...");
		}
	}
}
