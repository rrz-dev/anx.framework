using System;
using System.Xml.Linq;

namespace ProjectConverter.Platforms
{
	public class MetroConverter : Converter
	{
		private bool isCurrentProjectExecutable;

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
		
		#region ConvertMainPropertyGroup
		protected override void ConvertMainPropertyGroup(XElement element)
		{
			ChangeOrCreateNodeValue(element, "ProjectTypeGuids",
				"{BC8A1FFA-BEE3-4634-8014-F334798102B3};" +
				"{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}");

			//ChangeOrCreateNodeValue(element, "TargetFrameworkVersion", "4.5");
			//ChangeOrCreateNodeValue(element, "TargetPlatformVersion", "8.0");
			DeleteNodeIfExists(element, "TargetFrameworkVersion");
			DeleteNodeIfExists(element, "TargetPlatformVersion");

			DeleteNodeIfExists(element, "TargetFrameworkProfile");
			DeleteNodeIfExists(element, "XnaFrameworkVersion");
			DeleteNodeIfExists(element, "XnaPlatform");
			DeleteNodeIfExists(element, "XnaProfile");
			DeleteNodeIfExists(element, "XnaCrossPlatformGroupID");
			DeleteNodeIfExists(element, "XnaOutputType");
			DeleteNodeIfExists(element, "ApplicationIcon");
			DeleteNodeIfExists(element, "Thumbnail");
			DeleteNodeIfExists(element, "PublishUrl");
			DeleteNodeIfExists(element, "Install");
			DeleteNodeIfExists(element, "UpdateEnabled");
			DeleteNodeIfExists(element, "UpdateMode");
			DeleteNodeIfExists(element, "UpdateInterval");
			DeleteNodeIfExists(element, "UpdateIntervalUnits");
			DeleteNodeIfExists(element, "UpdatePeriodically");
			DeleteNodeIfExists(element, "UpdateRequired");
			DeleteNodeIfExists(element, "MapFileExtensions");
			DeleteNodeIfExists(element, "ApplicationRevision");
			DeleteNodeIfExists(element, "ApplicationVersion");
			DeleteNodeIfExists(element, "IsWebBootstrapper");
			DeleteNodeIfExists(element, "UseApplicationTrust");
			DeleteNodeIfExists(element, "BootstrapperEnabled");

			XElement outputTypeNode = GetOrCreateNode(element, "OutputType");
			string outputTypeValue = outputTypeNode.Value.ToLower();

			isCurrentProjectExecutable =
				outputTypeValue == "winexe" ||
				outputTypeValue == "appcontainerexe" ||
				outputTypeValue == "exe";
			if (isCurrentProjectExecutable)
			{
				outputTypeNode.Value = "AppContainerExe";
			}
		}
		#endregion

		#region ConvertItemGroup
		protected override void ConvertItemGroup(XElement element)
		{
			XName bootstrapperPackageName = XName.Get("BootstrapperPackage",
				element.Name.NamespaceName);
			
			var bootstrappers = element.Elements(bootstrapperPackageName);
			foreach (XElement bootstrapper in bootstrappers)
			{
				bootstrapper.Remove();
			}
		}
		#endregion

		#region ConvertPropertyGroup
		protected override void ConvertPropertyGroup(XElement element)
		{
			DeleteNodeIfExists(element, "NoStdLib");
			DeleteNodeIfExists(element, "XnaCompressContent");
		}
		#endregion

		#region Convert
		protected override void PostConvert()
		{
			string namespaceName = currentProject.Root.Name.NamespaceName;
			
			AddMetroVersionNode(namespaceName);
			AddAppManifestNode(namespaceName);
		}
		#endregion

		#region AddMetroVersionNode
		private void AddMetroVersionNode(string namespaceName)
		{
			XName propertyGroupName = XName.Get("PropertyGroup", namespaceName);
			XName vsVersionName = XName.Get("VisualStudioVersion", namespaceName);

			XElement metroVersionElement = new XElement(propertyGroupName);
			metroVersionElement.Add(new XAttribute("Condition",
				" '$(VisualStudioVersion)' == '' or '$(VisualStudioVersion)' < '11.0' "));
			metroVersionElement.Add(new XElement(vsVersionName, "11.0"));
			currentProject.Root.Add(metroVersionElement);
		}
		#endregion

		#region AddAppManifestNode
		private void AddAppManifestNode(string namespaceName)
		{
			if(isCurrentProjectExecutable == false)
				return;

			XName itemGroupName = XName.Get("ItemGroup", namespaceName);
			XName appxManifestName = XName.Get("AppxManifest", namespaceName);
			XName subTypeName = XName.Get("SubType", namespaceName);

			XElement newItemGroup = new XElement(itemGroupName);
			XElement appManifestElement = new XElement(appxManifestName);
			newItemGroup.Add(appManifestElement);
			appManifestElement.Add(new XAttribute("Include", "Manifest.appxmanifest"));
			appManifestElement.Add(new XElement(subTypeName, "Designer"));
			currentProject.Root.Add(newItemGroup);
		}
		#endregion
	}
}
