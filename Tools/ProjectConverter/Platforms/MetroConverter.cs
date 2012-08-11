using System;
using System.Xml.Linq;

namespace ProjectConverter.Platforms
{
	public class MetroConverter : Converter
	{
		public override string Postfix
		{
			get
			{
				return "WindowsMetro";
			}
		}

		#region ConvertImport
		protected override void ConvertImport(XElement element, XAttribute projectAttribute)
		{
			if (projectAttribute != null)
			{
				if (projectAttribute.Value.EndsWith("Microsoft.CSharp.targets"))
				{
					projectAttribute.Value =
						@"$(MSBuildExtensionsPath)\Microsoft\WindowsXaml\" +
						@"v$(VisualStudioVersion)\Microsoft.Windows.UI.Xaml.CSharp.targets";
				}
				else if (projectAttribute.Value.EndsWith(XnaGameStudioTarget) ||
					projectAttribute.Value.EndsWith(XnaPipelineExtensionTarget))
				{
					element.Remove();
				}
			}
		}
		#endregion

		#region ConvertReference
		protected override void ConvertReference(XElement element)
		{
			XName hintPathName = XName.Get("HintPath", element.Name.NamespaceName);
			XElement hintPath = element.Element(hintPathName);
			if (hintPath != null)
			{
				if (hintPath.Value.Contains("lib\\SharpDX\\Bin\\"))
				{
					hintPath.Value = hintPath.Value.Replace("lib\\SharpDX\\Bin\\",
					 "lib\\SharpDX-Win8\\Bin\\");
				}
			}
		}
		#endregion

		#region ConvertMainPropertyGroup
		protected override void ConvertMainPropertyGroup(XElement element)
		{
			ChangeOrCreateNodeValue(element, "ProjectTypeGuids",
				"{BC8A1FFA-BEE3-4634-8014-F334798102B3};" +
				"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}");
			ChangeOrCreateNodeValue(element, "TargetFrameworkVersion", "4.5");
			ChangeOrCreateNodeValue(element, "TargetPlatformVersion", "8.0");
		}
		#endregion

		#region Convert
		protected override void PostConvert()
		{
			string namespaceName = currentProject.Root.Name.NamespaceName;
			XName propertyGroupName = XName.Get("PropertyGroup", namespaceName);
			XName vsVersionName = XName.Get("VisualStudioVersion", namespaceName);

			XElement metroVersionElement = new XElement(propertyGroupName);
			metroVersionElement.Add(new XAttribute("Condition",
				" '$(VisualStudioVersion)' == '' or '$(VisualStudioVersion)' < '11.0' "));
			metroVersionElement.Add(new XElement(vsVersionName, "11.0"));
			currentProject.Root.Add(metroVersionElement);
		}
		#endregion
	}
}
