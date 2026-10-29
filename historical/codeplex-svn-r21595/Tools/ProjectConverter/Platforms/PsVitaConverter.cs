using System;
using System.Xml.Linq;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
	public class PsVitaConverter : Converter
	{
		public override string Postfix
		{
			get { return "PSVita"; }
		}


        public override string Name
        {
            get { return "PSVita"; }
        }

		#region ConvertImport
		protected override void ConvertImport(XElement element, XAttribute projectAttribute)
		{
			if (projectAttribute != null)
			{
				if (projectAttribute.Value.EndsWith("Microsoft.CSharp.targets"))
				{
					projectAttribute.Value =
						@"$(MSBuildExtensionsPath)\Sce\Sce.Psm.CSharp.targets";
				}
				else if (projectAttribute.Value.EndsWith(XnaGameStudioTarget) ||
					projectAttribute.Value.EndsWith(XnaPipelineExtensionTarget))
				{
					element.Remove();
				}
			}
		}
		#endregion

		#region ConvertItemGroup
		protected override void ConvertItemGroup(XElement element)
		{
			if (element.Element(GetXName("Reference")) != null)
			{
				var systemCoreReference = new XElement(GetXName("Reference"));
				systemCoreReference.Add(new XAttribute("Include", "System.Core"));
				element.Add(systemCoreReference);
			}
		}
		#endregion

		#region ConvertReference
		protected override void ConvertReference(XElement element)
		{
			XAttribute includeAttribute = element.Attribute("Include");
			if (includeAttribute != null)
			{
				string value = includeAttribute.Value;
				if (value == "System.Net")
					element.Remove();
			}
		}
		#endregion

		#region ConvertMainPropertyGroup
		protected override void ConvertMainPropertyGroup(XElement element)
		{
			ChangeOrCreateNodeValue(element, "ProjectTypeGuids",
				"{69878862-DA7D-4DC6-B0A1-50D8FAB4242F};" +
				"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}");
			ChangeOrCreateNodeValue(element, "ProductVersion", "10.0.0");
			ChangeOrCreateNodeValue(element, "SchemaVersion", "2.0");
			DeleteNodeIfExists(element, "TargetFrameworkVersion");
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
