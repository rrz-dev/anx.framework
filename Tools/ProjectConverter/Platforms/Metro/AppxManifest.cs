using System;
using System.IO;
using System.Xml.Linq;

namespace ProjectConverter.Platforms.Metro
{
	public class AppxManifest
	{
		private string filepath;
		private ProjectPath project;

		public AppxManifest(ProjectPath setProject)
		{
			project = setProject;
			filepath = Path.Combine(project.FullSourceDirectoryPath, "Manifest.appxmanifest");
		}

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

		public void Save()
		{
			// TODO: set name etc.
			File.WriteAllText(filepath, 
					@"<?xml version=""1.0"" encoding=""utf-8""?>
<Package xmlns=""http://schemas.microsoft.com/appx/2010/manifest"">
  <Identity Name=""f191dd44-caad-4a71-b1f4-07eb177508db"" Publisher=""CN=ANX-Team"" Version=""1.0.0.0"" />
  <Properties>
    <DisplayName>App1</DisplayName>
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
      <VisualElements DisplayName=""WindowsGame1"" Logo=""Assets\Logo.png"" SmallLogo=""Assets\SmallLogo.png"" Description=""WindowsGame1"" ForegroundText=""light"" BackgroundColor=""#464646"">
        <DefaultTile ShowName=""allLogos"" />
        <SplashScreen Image=""Assets\SplashScreen.png"" />
      </VisualElements>
    </Application>
  </Applications>
  <Capabilities>
    <Capability Name=""internetClient"" />
  </Capabilities>
</Package>");
		}
	}
}
