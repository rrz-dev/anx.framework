﻿using System;
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