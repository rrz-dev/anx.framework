using System;
using System.IO;
using System.Xml.Linq;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms.Metro
{
	public class AppxManifest
	{
		#region Private
		private string filepath;
		private ProjectPath project;
		#endregion

		#region Constructor
		public AppxManifest(ProjectPath setProject)
		{
			project = setProject;
			filepath = Path.Combine(project.FullSourceDirectoryPath, "Manifest.appxmanifest");
		}
		#endregion

		#region AddNode
		public void AddNode(XElement itemGroup)
		{
			string namespaceName = itemGroup.Name.NamespaceName;
			XName appxManifestName = XName.Get("AppxManifest", namespaceName);
			XName subTypeName = XName.Get("SubType", namespaceName);

			XElement appManifestElement = new XElement(appxManifestName);
			appManifestElement.Add(new XAttribute("Include", "Manifest.appxmanifest"));
			appManifestElement.Add(new XElement(subTypeName, "Designer"));

			itemGroup.Add(appManifestElement);
		}
		#endregion

		#region Save
		public void Save()
		{
			string projectName = Path.GetFileNameWithoutExtension(project.FullSourcePath);
			// TODO: set name etc.
			File.WriteAllText(filepath,
					@"<?xml version=""1.0"" encoding=""utf-8""?>
<Package xmlns=""http://schemas.microsoft.com/appx/2010/manifest"">
  <Identity Name=""" + projectName + @""" Publisher=""CN=ANX-Team"" Version=""1.0.0.0"" />
  <Properties>
    <DisplayName>" + projectName + @"</DisplayName>
    <PublisherDisplayName>ANX Developer Team</PublisherDisplayName>
    <Logo>Assets\StoreLogo.png</Logo>
  </Properties>
  <Prerequisites>
    <OSMinVersion>6.2.0</OSMinVersion>
    <OSMaxVersionTested>6.2.0</OSMaxVersionTested>
  </Prerequisites>
  <Resources>
    <Resource Language=""x-generate"" />
  </Resources>
  <Applications>
    <Application Id=""App"" Executable=""$targetnametoken$.exe"" EntryPoint=""WindowsGame1.Program"">
      <VisualElements DisplayName=""" + projectName + @""" Logo=""Assets\Logo.png"" SmallLogo=""Assets\SmallLogo.png"" Description=""" + projectName + @""" ForegroundText=""light"" BackgroundColor=""#464646"">
        <DefaultTile ShowName=""allLogos"" ShortName=""" + projectName + @""" />
        <SplashScreen Image=""Assets\SplashScreen.png"" />
      </VisualElements>
    </Application>
  </Applications>
  <Capabilities>
    <Capability Name=""internetClient"" />
  </Capabilities>
</Package>");
		}
		#endregion
	}
}
