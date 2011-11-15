using System.Collections.Generic;
using System.IO;
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
								propertyGroup.Add(new XElement("Reference",
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
								propertyGroup.Add(new XElement("Reference",
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
