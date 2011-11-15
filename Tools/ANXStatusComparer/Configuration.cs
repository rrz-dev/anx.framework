using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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
