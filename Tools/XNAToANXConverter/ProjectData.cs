using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace XNAToANXConverter
{
	public static class ProjectData
	{
		private const string XnaBaseName = "Microsoft.Xna.Framework";
		private const string AnxBaseName = "ANX.Framework";

		#region Convert
		public static void Convert(string sourceFilepath, string destinationFilepath)
		{
			string sourceFolderPath =
				sourceFilepath.Replace(Path.GetFileName(sourceFilepath), "");
			string destinationFolderPath =
				destinationFilepath.Replace(Path.GetFileName(destinationFilepath), "");

			if (Directory.Exists(destinationFolderPath) == false)
			{
				Directory.CreateDirectory(destinationFolderPath);
			}

			XDocument doc = XDocument.Load(sourceFilepath);
			XElement root = doc.Root;
			foreach (XElement propertyGroup in root.Elements())
			{
				if (propertyGroup.Name.LocalName != "ItemGroup")
				{
					continue;
				}

				List<XElement> elements = new List<XElement>(propertyGroup.Elements());
				foreach (XElement item in elements)
				{
					if (item.Name.LocalName == "Reference")
					{
						#region Process Reference
						string assemblyPath = item.Attribute("Include").Value;
						if (assemblyPath.Contains(XnaBaseName))
						{
							item.Remove();

							string anxPath = assemblyPath.Replace(XnaBaseName, AnxBaseName);
							if (anxPath.Contains(", Version"))
							{
								anxPath = anxPath.Substring(0, anxPath.IndexOf(',')) + ".dll";
							}
							propertyGroup.Add(new XElement("Reference",
								new XAttribute("Include", anxPath)));
						}
						#endregion
					}
					else if (item.Name.LocalName == "Compile")
					{
						#region Process Compile
						string codeFilepath = item.Attribute("Include").Value;
						string absolutePath = Path.Combine(sourceFolderPath, codeFilepath);
						string text = File.ReadAllText(absolutePath);

						text = text.Replace(XnaBaseName, AnxBaseName);

						string destCodeFolderPath = codeFilepath.Replace(
							Path.GetFileName(codeFilepath), "");

						destCodeFolderPath = Path.Combine(destinationFolderPath,
							destCodeFolderPath);
						if (Directory.Exists(destCodeFolderPath) == false)
						{
							Directory.CreateDirectory(destCodeFolderPath);
						}

						File.WriteAllText(Path.Combine(destinationFolderPath,
							codeFilepath), text);
						#endregion
					}
					else if (item.Name.LocalName == "BootstrapperPackage")
					{
						#region Process BootstrapperPackage
						// Remove all bootstrapper tasks for XNA.
						string includeName = item.Attribute("Include").Value;
						if (includeName.Contains(XnaBaseName))
						{
							item.Remove();
						}
						#endregion
					}
				}
			}

			doc.Save(destinationFilepath);
		}
		#endregion
	}
}
