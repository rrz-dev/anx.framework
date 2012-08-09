using System;
using System.IO;
using System.Xml.Linq;

namespace ProjectConverter.Platforms
{
	public class LinuxConverter : Converter
	{
		public override string Postfix
		{
			get
			{
				return "Linux";
			}
		}

		#region ConvertImport
		protected override void ConvertImport(XElement element)
		{
			XAttribute projectAttribute = element.Attribute("Project");
			if (projectAttribute != null)
			{
				if (projectAttribute.Value.EndsWith("Microsoft.Xna.GameStudio.targets"))
				{
					element.Remove();
				}
			}
		}
		#endregion

		#region ConvertMainPropertyGroup
		protected override void ConvertMainPropertyGroup(XElement element)
		{
			DeleteNodeIfExists(element, "ProjectTypeGuids");
			DeleteNodeIfExists(element, "TargetFrameworkProfile");

			XElement outputTypeNode = GetOrCreateNode(element, "OutputType");
			if (outputTypeNode.Value == "WinExe" ||
				outputTypeNode.Value == "appcontainerexe")
			{
				outputTypeNode.Value = "Exe";
			}
			else if (String.IsNullOrEmpty(outputTypeNode.Value))
			{
				outputTypeNode.Value = "Library";
			}
		}
		#endregion
	}
}
