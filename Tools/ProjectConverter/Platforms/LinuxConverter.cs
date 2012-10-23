using System;
using System.IO;
using System.Xml.Linq;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
	public class LinuxConverter : Converter
	{
		public override string Postfix
		{
			get { return "Linux"; }
		}

        public override string Name
        {
            get { return "linux"; }
        }

		#region ConvertImport
		protected override void ConvertImport(XElement element, XAttribute projectAttribute)
		{
			if (projectAttribute != null &&
				(projectAttribute.Value.EndsWith(XnaGameStudioTarget) ||
				projectAttribute.Value.EndsWith(XnaPipelineExtensionTarget)))
			{
				element.Remove();
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
