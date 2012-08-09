using System;
using System.Diagnostics;
using System.IO;
using ANXStatusComparer.Data;
using ANXStatusComparer.Output;

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

			if (File.Exists(configFilepath) == false)
			{
				throw new FileNotFoundException("Failed to generate the status " +
					"because the configuration file '" + configFilepath +
					"' doesn't exist! Aborting.");
			}
			#endregion

			// Load the config
			Configuration config = new Configuration(configFilepath);

			// Now load the actual assemblies and preparse them:
			// for xna...
			AssembliesData xnaAssemblies = new AssembliesData(config.XnaAssemblies);
			// ...and for anx.
			AssembliesData anxAssemblies = new AssembliesData(config.AnxAssemblies);

			// Everything before was easy...now comes the main part.
			ResultData result = AssemblyComparer.Compare(xnaAssemblies, anxAssemblies,
				CheckType.All);

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
	}
}
