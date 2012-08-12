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
			ChangeOrCreateNodeValue(element, "DefaultLanguage", "en-US");
			ChangeOrCreateNodeValue(element, "FileAlignment", "512");

			// TODO: generate cert
			ChangeOrCreateNodeValue(element, "PackageCertificateKeyFile",
				"Test_TemporaryKey.pfx");
			
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
			DeleteNodeIfExists(element, "InstallFrom");

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

			XName noneName = XName.Get("None", element.Name.NamespaceName);
			
			var noneElements = element.Elements(noneName);
			foreach (XElement noneNode in noneElements)
			{
				if (noneNode.Attribute("Include").Value == "app.config")
				{
					noneNode.Remove();
				}
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

			AddAppManifestAndCertNode(namespaceName);

			AddMetroVersionNode(namespaceName);
			AddCommonPropsNode(namespaceName);
		}
		#endregion

		#region AddCommonPropsNode
		private void AddCommonPropsNode(string namespaceName)
		{
			XName importName = XName.Get("Import", namespaceName);
			XElement commonPropsNode = new XElement(importName);
			commonPropsNode.Add(new XAttribute("Project",
				 @"$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"));
			commonPropsNode.Add(new XAttribute("Condition",
				@"Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\" +
				"Microsoft.Common.props')"));
			currentProject.Root.Add(commonPropsNode);
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

		#region AddAppManifestAndCertNode
		private void AddAppManifestAndCertNode(string namespaceName)
		{
			if(isCurrentProjectExecutable == false)
				return;

			XName itemGroupName = XName.Get("ItemGroup", namespaceName);
			XName appxManifestName = XName.Get("AppxManifest", namespaceName);
			XName subTypeName = XName.Get("SubType", namespaceName);
			XName noneName = XName.Get("None", namespaceName);

			XElement newItemGroup = new XElement(itemGroupName);

			XElement noneGroup = new XElement(noneName);
			noneGroup.Add(new XAttribute("Include", "Test_TemporaryKey.pfx"));

			XElement appManifestElement = new XElement(appxManifestName);
			appManifestElement.Add(new XAttribute("Include", "Manifest.appxmanifest"));
			appManifestElement.Add(new XElement(subTypeName, "Designer"));

			newItemGroup.Add(noneGroup);
			newItemGroup.Add(appManifestElement);
			currentProject.Root.Add(newItemGroup);
		}
		#endregion
	}
}
