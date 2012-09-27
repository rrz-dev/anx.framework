using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ProjectConverter.Platforms.Metro;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
	public class MetroConverter : Converter
	{
		private const string OpenSSLToolPath = "../../lib/OpenSSL/openssl.exe";

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

		#region ConvertProjectReference
		protected override void ConvertProjectReference(XElement element)
		{
			XAttribute includeAttribute = element.Attribute("Include");
			if (includeAttribute != null)
			{
				string value = includeAttribute.Value;
				if (value.Contains("ANX.RenderSystem.Windows.DX10") ||
					value.Contains("ANX.RenderSystem.GL3") ||
					value.Contains("ANX.PlatformSystem.Windows") ||
					value.Contains("ANX.RenderSystem.Windows.DX11") ||
					value.Contains("ANX.SoundSystem.OpenAL"))
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
			
			var bootstrappers = element.Elements(bootstrapperPackageName).ToList();
			bootstrappers.Remove();

			XName noneName = XName.Get("None", element.Name.NamespaceName);
			
			var noneElements = element.Elements(noneName);
			foreach (XElement noneNode in noneElements)
			{
				if (noneNode.Attribute("Include").Value == "app.config")
				{
					noneNode.Remove();
				}
			}
            XName referenceName = XName.Get("Reference", element.Name.NamespaceName);

            var referenceElements = element.Elements(referenceName);
            foreach (XElement referenceNode in referenceElements)
            {
                if (referenceNode.Value.Contains("Standard-net20"))
                {
                   var attribute=  referenceNode.Attribute("Include");
                   attribute.Value = attribute.Value.Split(',').First();
                   foreach (var nodeElement in referenceNode.Elements().ToList())
                   {

                       if (nodeElement.Name.LocalName=="SpecificVersion")
                       {
                           nodeElement.Remove();
                       }
                       if (nodeElement.Name.LocalName=="HintPath")
                       {
                           nodeElement.Value = nodeElement.Value.Replace("Standard-net20", "Win8Metro");
                       }

                   }                    
                }
            }
			if (element.IsEmpty)
			{
				element.Remove();
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
			string namespaceName = CurrentProject.Root.Name.NamespaceName;

			AddMetroResources(namespaceName);

			AddMetroVersionNode(namespaceName);
			AddCommonPropsNode(namespaceName);

			//GenerateTestCertificate();
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
			CurrentProject.Root.Add(commonPropsNode);
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
			CurrentProject.Root.Add(metroVersionElement);
		}
		#endregion

		#region AddMetroResources
		private void AddMetroResources(string namespaceName)
		{
			if(isCurrentProjectExecutable == false)
				return;

			XName itemGroupName = XName.Get("ItemGroup", namespaceName);
			XElement newItemGroup = new XElement(itemGroupName);
			CurrentProject.Root.Add(newItemGroup);

			XName noneName = XName.Get("None", namespaceName);
			XElement noneGroup = new XElement(noneName);
			noneGroup.Add(new XAttribute("Include", "Test_TemporaryKey.pfx"));
			newItemGroup.Add(noneGroup);

			GenerateAppxManifest(newItemGroup);

			MetroAssets assets = new MetroAssets(CurrentProject);
			assets.AddAssetsToProject(newItemGroup);
		}
		#endregion

		#region GenerateAppxManifest
		private void GenerateAppxManifest(XElement itemGroup)
		{
			AppxManifest manifest = new AppxManifest(CurrentProject);
			manifest.AddNode(itemGroup);
			manifest.Save();
		}
		#endregion

		// TODO: not working yet
		#region GenerateTestCertificate
		private void GenerateTestCertificate()
		{
			//string tempKeyFilepath = Path.GetTempFileName() + ".key";
			//string tempFilepath = Path.GetTempFileName() + ".pem";
			string tempKeyFilepath = "C:\\test.key";
			string tempFilepath = "C:\\test.pem";
			string pfxFilepath = Path.Combine(CurrentProject.FullSourceDirectoryPath,
				"Test_TemporaryKey.pfx");
			string dir = Directory.GetCurrentDirectory();
			string toolPath = Path.Combine(dir, OpenSSLToolPath);

			//string cmd = "req -x509 -nodes -days 365 -newkey rsa:2048 -keyout \"" +
			//  tempFilepath + "\" -out \"" + tempFilepath + "\"";
			string cmd = "req -new -sha256 -keyout C:\\test.pem -out C:\\test2.pem -days 1500 -newkey rsa:2048";
			string output = Execute(toolPath, cmd);

			// # export mycert.pem as PKCS#12 file, mycert.pfx
			// openssl pkcs12 -export -out mycert.pfx -in mycert.pem -name "testkey"
			cmd =
				"pkcs12 -export -out \"" + pfxFilepath + "\" -in \"" + tempFilepath +
				"\" -name \"testkey\"";
			output = Execute(toolPath, cmd);

			File.Delete(tempFilepath);
		}
		#endregion

		#region Execute
		private string Execute(string filepath, string args)
		{
			string result = "";

			Process process = new Process();
			process.StartInfo.FileName = filepath;
			process.StartInfo.Arguments = args;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.RedirectStandardOutput = true;
			DataReceivedEventHandler dataReceived =
				delegate(object sender, DataReceivedEventArgs e)
			{
				result += e.Data + "\n";
			};
			process.OutputDataReceived += dataReceived;
			process.ErrorDataReceived += dataReceived;
			process.Start();
			process.BeginErrorReadLine();
			process.BeginOutputReadLine();
			process.WaitForExit();

			return result;
		}
		#endregion
	}
}
