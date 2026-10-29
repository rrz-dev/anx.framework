using System;
using System.IO;
using System.Diagnostics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace StockShaderCodeGenerator
{
	public static class CodeGenerator
	{
		public static void Generate()
		{
			Program.TraceListener.WriteLine("generating output...");

			using (StreamWriter target = new StreamWriter(Configuration.Target, false))
			{
				//
				// write header
				//
				target.WriteLine("using System;");
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

						bool isNextLineNeeded = (j + 1) % 15 == 0;
						if (j < s.ByteCode.Length - 1)
						{
							target.Write("," + (isNextLineNeeded ? "" : " "));
						}

						if (isNextLineNeeded)
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

			Program.TraceListener.WriteLine("finished generating output...");
		}
	}
}
