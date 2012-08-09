using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace XNAToANXConverter
{
	public static class ProjectData
	{
		private const string XnaBaseName = "Microsoft.Xna.Framework";
		private const string AnxBaseName = "ANX.Framework";

		#region Convert
		public static void Convert(string target, string sourceFilepath,
			string destinationFilepath)
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
						if (target == "anx")
						{
							#region ANX
							if (assemblyPath.Contains(XnaBaseName))
							{
								item.Remove();

								string anxPath = assemblyPath.Replace(XnaBaseName, AnxBaseName);
								if (anxPath.Contains(", Version"))
								{
									anxPath = anxPath.Substring(0, anxPath.IndexOf(',')) + ".dll";
								}
								propertyGroup.Add(new XElement(XName.Get("Reference",
									propertyGroup.Name.NamespaceName),
									new XAttribute("Include", anxPath)));
							}
							#endregion
						}
						else
						{
							#region XNA
							if (assemblyPath.Contains(AnxBaseName))
							{
								item.Remove();

								// TODO: FQN of the xna assemby

								string xnaPath = assemblyPath.Replace(AnxBaseName, XnaBaseName);
								propertyGroup.Add(new XElement(XName.Get("Reference",
									propertyGroup.Name.NamespaceName),
									new XAttribute("Include", xnaPath)));
							}
							#endregion
						}
						#endregion
					}
					else if (item.Name.LocalName == "Compile")
					{
						#region Process Compile
						string codeFilepath = item.Attribute("Include").Value;
						string absolutePath = Path.Combine(sourceFolderPath, codeFilepath);
						string text = File.ReadAllText(absolutePath);

						if (target == "anx")
						{
							text = text.Replace(XnaBaseName, AnxBaseName);
						}
						else
						{
							text = text.Replace(AnxBaseName, XnaBaseName);
						}

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
						if (target == "xna")
						{
							continue;
						}

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
