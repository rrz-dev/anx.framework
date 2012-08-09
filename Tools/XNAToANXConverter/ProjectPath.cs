using System;
using System.IO;
using NUnit.Framework;
using System.Xml.Linq;
using ProjectConverter.Platforms;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter
{
	public class ProjectPath
	{
		public string RelativeSourcePath
		{
			get;
			private set;
		}

		public string FullSourcePath
		{
			get;
			private set;
		}

		public string RelativeDestinationPath
		{
			get;
			private set;
		}

		public string FullDestinationPath
		{
			get;
			private set;
		}

		public XDocument Document
		{
			get;
			private set;
		}

		public XElement Root
		{
			get
			{
				return Document.Root;
			}
		}

		public ProjectPath(Converter converter, string relativeSourcePath, string basePath)
		{
			RelativeSourcePath = relativeSourcePath;
			FullSourcePath = Path.Combine(basePath, relativeSourcePath);

			RelativeDestinationPath = BuildTargetFilepath(converter);
			FullDestinationPath = Path.Combine(basePath, RelativeDestinationPath);

			try
			{
				LoadProjectFile();
			}
			catch
			{
			}
		}

		#region Save
		public void Save()
		{
			Document.Save(FullDestinationPath, SaveOptions.None);
		}
		#endregion

		#region BuildTargetFilepath
		private string BuildTargetFilepath(Converter converter)
		{
			string basePath = Path.GetDirectoryName(RelativeSourcePath);
			string filename = Path.GetFileNameWithoutExtension(RelativeSourcePath);
			if (filename.Contains("_"))
			{
				filename = filename.Substring(0, filename.IndexOf('_'));
			}

			filename += "_" + converter.Postfix;

			return Path.Combine(basePath, filename + ".csproj");
		}
		#endregion

		#region LoadProjectFile
		private void LoadProjectFile()
		{
			string documentText = File.ReadAllText(FullSourcePath);
			Document = XDocument.Parse(documentText);
		}
		#endregion

		private class ProjectPathTests
		{
			#region TestBuildTargetFilepath
			[Test]
			public static void TestBuildTargetFilepath()
			{
				string testBasePath = "C:\\code\\";
				string testRelativeSourcePath = "ANX.Framework.csproj";

				var projPath = new ProjectPath(new PsVitaConverter(),
					testRelativeSourcePath, testBasePath);
				Assert.AreEqual(projPath.RelativeDestinationPath,
					"ANX.Framework_PSVita.csproj");

				projPath = new ProjectPath(new LinuxConverter(),
					"ANX.Framework_IOS.csproj", testBasePath);

				Assert.AreEqual(projPath.RelativeDestinationPath,
					"ANX.Framework_Linux.csproj");

				projPath = new ProjectPath(new MetroConverter(),
					"ANX.Framework_IOS_Android_WindowsXNA.csproj", testBasePath);
				Assert.AreEqual(projPath.RelativeDestinationPath,
					"ANX.Framework_WindowsMetro.csproj");
			}
			#endregion
		}
	}
}
