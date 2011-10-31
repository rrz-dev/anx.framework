using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace ANXStatusComparer
{
	/// <summary>
	/// Configuration class, loading the xml configuration which contains the
	/// filepaths to the anx and xna assemblies.
	/// </summary>
	public class Configuration
	{
		#region Public
		/// <summary>
		/// Array of filepaths to the ANX assemblies.
		/// </summary>
		public string[] AnxAssemblies
		{
			get;
			private set;
		}

		/// <summary>
		/// Array of filepaths to the XNA assemblies.
		/// </summary>
		public string[] XnaAssemblies
		{
			get;
			private set;
		}

		/// <summary>
		/// The output type of the comparison.
		/// </summary>
		public string OutputType
		{
			get;
			private set;
		}

		/// <summary>
		/// Filepath to a stylesheet file when html output is chosen.
		/// </summary>
		public string StylesheetFile
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Load a new configuration file from the specified filepath.
		/// </summary>
		/// <param name="filepath">Path to the config xml file.</param>
		public Configuration(string filepath)
		{
			if (File.Exists(filepath) == false)
			{
				throw new FileNotFoundException("Failed to load configuration because " +
					"the file doesn't exist! Aborting.");
			}

			XDocument doc = XDocument.Load(filepath);
			if (doc.Root.Name.LocalName != "Config")
			{
				throw new FormatException("Failed to load configuration because the " +
					"file has no Config-Node as the root element! Aborting.");
			}

			XElement anxNode = doc.Root.Element("ANXAssemblies");
			XElement xnaNode = doc.Root.Element("XNAAssemblies");
			if (anxNode == null ||
				xnaNode == null)
			{
				throw new FormatException("Failed to load configuration because the " +
					"file must have both ANXAssemblies and XNAAssemblies nodes! Aborting.");
			}

			List<string> anxPaths = new List<string>();
			foreach (XElement node in anxNode.Elements("Assembly"))
			{
				string assemblyPath = ValidateAssemblyPath(node.Value);
				anxPaths.Add(assemblyPath);
			}
			AnxAssemblies = anxPaths.ToArray();

			List<string> xnaPaths = new List<string>();
			foreach (XElement node in xnaNode.Elements("Assembly"))
			{
				string assemblyPath = ValidateAssemblyPath(node.Value);
				xnaPaths.Add(assemblyPath);
			}
			XnaAssemblies = xnaPaths.ToArray();

			// Set the default values
			OutputType = "text";
			StylesheetFile = "SummaryStyle.css";

			XElement outputNode = doc.Root.Element("Output");
			if(outputNode != null)
			{
				XElement outputTypeNode = outputNode.Element("OutputType");
				if(outputTypeNode != null)
				{
					string value = outputTypeNode.Value.ToLower();
					if(value == "html" || value == "text")
					{
						OutputType = value;
					}
				}

				XElement stylesheetFileNode = outputNode.Element("StylesheetFile");
				if (stylesheetFileNode != null)
				{
					if (File.Exists(stylesheetFileNode.Value) == false)
					{
						StylesheetFile = stylesheetFileNode.Value;
					}
				}
			}
		}
		#endregion

		#region ValidateAssemblyPath
		/// <summary>
		/// Validate the specified assembly path by creating an absolute filepath
		/// if the path is relative and checking if the file exists at all.
		/// </summary>
		/// <param name="path">Loaded path to an assembly.</param>
		/// <returns>Validated path.</returns>
		private string ValidateAssemblyPath(string path)
		{
			string assemblyPath = path;
			if(Path.IsPathRooted(assemblyPath) == false)
			{
				assemblyPath = Path.Combine(
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
					assemblyPath);
			}

			if (File.Exists(assemblyPath) == false)
			{
				throw new FileNotFoundException("Failed to find all assemblies " +
					"from the configuration file. '" + assemblyPath + "'. Aborting.");
			}

			return assemblyPath;
		}
		#endregion

		#region Tests
		private class Tests
		{
			public static void TestLoadConfiguration()
			{
				Configuration config = new Configuration("./SampleConfigFile.xml");
				Console.WriteLine("---------- ANX");
				foreach (string assemblyPath in config.AnxAssemblies)
				{
					Console.WriteLine(assemblyPath);
				}
				Console.WriteLine("---------- XNA");
				foreach (string assemblyPath in config.XnaAssemblies)
				{
					Console.WriteLine(assemblyPath);
				}

				Console.WriteLine("---------- Output");
				Console.WriteLine(config.OutputType);
				Console.WriteLine(config.StylesheetFile);
			}
		}
		#endregion
	}
}
