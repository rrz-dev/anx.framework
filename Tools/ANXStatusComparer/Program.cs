using System;
using System.Diagnostics;
using System.IO;
using ANXStatusComparer.Data;
using ANXStatusComparer.Output;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANXStatusComparer
{
	public static class Program
	{
		#region Main
		/// <summary>
		/// The main entry point of the tool.
		/// </summary>
		/// <param name="args">Arguments passed to the application.</param>
		static void Main(string[] args)
		{
			#region Validation
			string configFilepath = args.Length == 0 ?
				"./SampleConfigFile.xml" :
				args[0];

            Configuration config;

            if (File.Exists(configFilepath) == true)
            {
                // Load the config
                config = new Configuration(configFilepath);
            } 
            else
			{
                string currentAssemblyDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                config = new Configuration(new String[] { Path.Combine(currentAssemblyDirectoryName, "ANX.Framework.dll"),
                                                          Path.Combine(currentAssemblyDirectoryName, "ANX.Framework.Content.Pipeline.dll")
                                                        },
                                           GetDefaultXnaAssemblies(),
                                           Path.Combine(currentAssemblyDirectoryName, "SummaryStyle.css")
                                          );
			}
			#endregion

			// Now load the actual assemblies and preparse them:
			// for xna...
			AssembliesData xnaAssemblies = new AssembliesData(config.XnaAssemblies);
			// ...and for anx.
			AssembliesData anxAssemblies = new AssembliesData(config.AnxAssemblies);

			// Everything before was easy...now comes the main part.
			ResultData result = AssemblyComparer.Compare(xnaAssemblies, anxAssemblies, CheckType.All, config.Excludes);

			switch(config.OutputType)
			{
				default:
				case "text":
					Console.WriteLine(TextOutput.GenerateOutput(result));
					Console.Read();
					break;

				case "html":
					HtmlOutput.GenerateOutput(result, config.StylesheetFile);
					Process.Start(HtmlOutput.HtmlFilepath);
					break;
			}
		}
		#endregion

        private static string[] GetDefaultXnaAssemblies()
        {
            HashSet<string> assemblies = new HashSet<string>();

            var xna32 = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\XNA\Game Studio\v4.0", "InstallPath", null);
            var xna64 = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\XNA\Game Studio\v4.0", "InstallPath", null);

            if (xna32 != null)
            {
                foreach (string file in Directory.EnumerateFiles(Path.Combine(xna32.ToString(), @"References\Windows\x86"), "*.dll", SearchOption.AllDirectories))
                {
                    assemblies.Add(file);
                }
            }

            if (xna64 != null)
            {
                foreach (string file in Directory.EnumerateFiles(Path.Combine(xna64.ToString(), @"References\Windows\x86"), "*.dll", SearchOption.AllDirectories))
                {
                    assemblies.Add(file);
                }
            }

            return assemblies.ToArray();
        }
	}
}
