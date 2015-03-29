using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ProjectConverter.Platforms.Metro;
using System.Collections.Generic;
using ProjectConverter;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
    public class MetroConverter : Converter
    {
        private const string postfix = "WindowsMetro";

        private const string OpenSSLToolPath = "../../lib/OpenSSL/openssl.exe";

        private bool isCurrentProjectExecutable;

        private List<ProjectReference> metroAssemblies = new List<ProjectReference>
        {
            //Handled differently, we are always replacing a reference to this one.
            new ProjectReference("ANX.Framework", "6899F0C9-70B9-4EB0-9DD3-E598D4BE3E35", "", postfix),
            //In case of the others, we remove all other ANX references and add these here, when it's an executable.
            //If it's just a class library, these are not interesting.
            new ProjectReference("ANX.InputDevices.Windows.ModernUI", "628AB80A-B1B9-4878-A810-7A58D4840F60", "InputSystems", postfix),
            new ProjectReference("ANX.InputSystem.Standard", "49066074-3B7B-4A55-B122-6BD33AB73558", "InputSytems", postfix),
            new ProjectReference("ANX.PlatformSystem.Metro", "04F6041E-475E-4B2A-A889-6A33EABD718B", "PlatformSystems", postfix),
            new ProjectReference("ANX.RenderSystem.Windows.Metro", "FF0AB665-2796-4354-9630-76C2751DB3C2", "RenderSystems", postfix),
            new ProjectReference("ANX.SoundSystem.Windows.XAudio", "6A582788-C4D2-410C-96CD-177F75712D65", "SoundSystems", postfix),
        };

        public override string Postfix
        {
            get { return postfix; }
        }

        public override string Name
        {
            get { return "windowsmetro"; }
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

        #region ConvertOutputPath
        protected override string ConvertOutputPath(string path)
        {
            return Path.Combine(base.ConvertOutputPath(path), "ModernUI");
        }
        #endregion

        #region ConvertProjectReference
        private static String[] IgnoreAssemblies = new[] { "ANX.RenderSystem.Windows.DX10",
                                                           "ANX.RenderSystem.GL3",
                                                           "ANX.PlatformSystem.Windows",
                                                           "ANX.RenderSystem.Windows.DX11",
                                                           "ANX.SoundSystem.OpenAL",
                                                           "ANX.InputDevices.Windows.XInput",
                                                           "System.Windows.Forms",
                                                           "OggUtils",
                                                         };

        protected override void ConvertProjectReference(XElement element)
        {
            XAttribute includeAttribute = element.Attribute("Include");
            if (includeAttribute != null)
            {
                string value = includeAttribute.Value;
                foreach (string ignoreAssembly in IgnoreAssemblies)
                {
                    if (value.Contains(ignoreAssembly))
                    {
                        element.Remove();
                    }
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
            
            ChangeOrCreateNodeValue(element, "MinimumVisualStudioVersion", "12");

            ChangeOrCreateNodeValue(element, "TargetPlatformVersion", "8.1");
            DeleteNodeIfExists(element, "TargetFrameworkVersion");

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

            if (element.IsEmpty)
            {
                element.Remove();
            }
        }
        #endregion

        protected override void ConvertReference(XElement element)
        {
            if (element.Value.ToLowerInvariant().Contains("standard-net40"))
            {
                var attribute = element.Attribute("Include");
                attribute.Value = attribute.Value.Split(',').First();
                foreach (var nodeElement in element.Elements().ToList())
                {

                    if (nodeElement.Name.LocalName == "SpecificVersion")
                    {
                        nodeElement.Remove();
                    }
                    if (nodeElement.Name.LocalName == "HintPath")
                    {
                        nodeElement.Value = nodeElement.Value.ToLowerInvariant().Replace("standard-net40", "standard-winrt");
                    }

                }
            }
        }

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

            RemoveIncompatibleReferences();

            ChangeOutputPath();

            if (isCurrentProjectExecutable)
            {
                RemoveIncompatibleResources(namespaceName);
                AddPlatformSpecificReferences();
            }

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

        //Having build an assembly for normal windows and having the same output path makes the compile process fail when changing to win rt as it
        //doesn't automatically recompile the assemblies.
        private void ChangeOutputPath()
        {
            foreach (var element in CurrentProject.Root.Descendants(XName.Get("OutputPath", CurrentProject.ProjectXmlNamespace)))
            {
                if (element.Value == null)
                    continue;

                if (!element.Value.Contains("ModernUI"))
                {
                    element.Value = Path.Combine(element.Value, "ModernUI");
                }
            }
        }

        private void RemoveIncompatibleResources(string namespaceName)
        {
            List<XElement> culprits = new List<XElement>();
            //Remove the App.config element, which usually contains an entry for supportedRuntime, which makes the WinRT app not startable.
            foreach (var node in CurrentProject.Root.Descendants(XName.Get("None", namespaceName)))
            {
                var attribute = node.Attribute(XName.Get("Include"));
                if (String.Equals(attribute.Value, "App.config", StringComparison.OrdinalIgnoreCase))
                {
                    //Don't remove the culprits in here, they would just cause null-reference turmoil when iterating.
                    //Move them outside and then get rid of them.
                    culprits.Add(node);
                }
            }

            foreach (var culprit in culprits)
            {
                culprit.Remove();
            }
        }

        #region GenerateAppxManifest
        private void GenerateAppxManifest(XElement itemGroup)
        {
            AppxManifest manifest = new AppxManifest(CurrentProject);
            manifest.AddNode(itemGroup);
            manifest.Save();
        }
        #endregion

        private void RemoveIncompatibleReferences()
        {
            List<XElement> unwantedReferences = new List<XElement>();
            List<XElement> transformableReferences = new List<XElement>();
            foreach (var reference in EnumerateReferences())
            {
                var attribute = reference.Attribute(XName.Get("Include"));
                if (attribute != null && attribute.Value != null && attribute.Value.StartsWith("ANX."))
                {
                    if (metroAssemblies.Any((x) => x.Name == attribute.Value))
                    {
                        transformableReferences.Add(reference);
                    }
                    else
                    {
                        unwantedReferences.Add(reference);
                    }
                }
            }
            
            //Mostly for converting the samples. In a normal project, there shouldn't be any ProjectReferences to ANX projects.
            foreach (var reference in EnumerateProjectReferences())
            {
                var name = reference.Element(XName.Get("Name", CurrentProject.ProjectXmlNamespace));
                if (name != null && name.Value != null && name.Value.StartsWith("ANX."))
                {
                    if (metroAssemblies.Any((x) => x.Name == name.Value))
                    {
                        transformableReferences.Add(reference);
                    }
                    else
                    {
                        unwantedReferences.Add(reference);
                    }
                }
            }

            foreach (var reference in unwantedReferences)
            {
                reference.Remove();
            }

            foreach (var reference in transformableReferences)
            {
                var libraryName = GetLibraryName(reference);
                var projectReference = metroAssemblies.First((x) => x.Name == libraryName);

                reference.Parent.Add(CreateFrameworkReferenceNode(projectReference));
                reference.Remove();
            }
        }

        private bool IsProjectReference(XElement reference)
        {
            return reference.Element(XName.Get("Project", CurrentProject.ProjectXmlNamespace)) != null;
        }

        private string GetLibraryName(XElement reference)
        {
            if (IsProjectReference(reference))
            {
                var name = reference.Element(XName.Get("Name", CurrentProject.ProjectXmlNamespace));
                if (name == null)
                    throw new InvalidOperationException("The reference \"" + reference.ToString() + "\" is not a valid project reference.");

                return name.Value;
            }
            else
            {
                var include = reference.Attribute(XName.Get("Include"));
                if (include == null)
                    throw new InvalidOperationException("The reference \"" + reference.ToString() + "\" is not a valid reference.");

                return include.Value;
            }
        }

        private IEnumerable<XElement> EnumerateReferences()
        {
            foreach (var element in CurrentProject.Root.Descendants(XName.Get("Reference", CurrentProject.ProjectXmlNamespace)))
            {
                yield return element;
            }
        }

        private IEnumerable<XElement> EnumerateProjectReferences()
        {
            foreach (var reference in CurrentProject.Root.Descendants(XName.Get("ProjectReference", CurrentProject.ProjectXmlNamespace)))
            {
                yield return reference;
            }
        }

        private void AddPlatformSpecificReferences()
        {
            var itemGroup = new XElement(XName.Get("ItemGroup", CurrentProject.ProjectXmlNamespace));

            foreach (var library in metroAssemblies)
            {
                if (!HasReference(library.Name))
                {
                    itemGroup.Add(CreateFrameworkReferenceNode(library.Name, library.Guid, library.RelativeProjectPath));
                }
            }

            CurrentProject.Root.Add(itemGroup);
        }

        private bool HasReference(string libraryName)
        {
            foreach (var reference in EnumerateReferences())
            {
                var include = reference.Attribute(XName.Get("Include"));
                if (include != null && include.Value != null && include.Value == libraryName)
                {
                    return true;
                }
            }

            foreach (var projectReference in EnumerateProjectReferences())
            {
                var name = projectReference.Element(XName.Get("Name", CurrentProject.ProjectXmlNamespace));
                if (name != null && name.Value != null && name.Value == libraryName)
                {
                    return true;
                }
            }

            return false;
        }

        private XElement CreateFrameworkReferenceNode(ProjectReference projectReference)
        {
            if (projectReference == null)
                throw new ArgumentNullException("projectReference");

            return CreateFrameworkReferenceNode(projectReference.Name, projectReference.Guid, projectReference.RelativeProjectPath);
        }

        private XElement CreateFrameworkReferenceNode(string libraryName, Guid projectGuid, Uri relativeProjectPath)
        {
            return CreateFrameworkReferenceNode(libraryName, libraryName + ".dll", projectGuid, relativeProjectPath);
        }

        private XElement CreateFrameworkReferenceNode(string libraryName, string libraryFileName, Guid projectGuid, Uri relativeProjectPath)
        {
            XElement reference;
            //Practically only for internal usage, when we are converting our own projects in our sln, we actually want project references instead of just a reference to the path where the ANX assemblies have been installed to.
            if (IsProjectReferencesFlagSet)
            {
                string frameworkPath = ".";

                KeyValuePair<string, string> frameworkPathData = Program.KeyValueParameters.FirstOrDefault((x) => x.Key.Equals("FrameworkPath", StringComparison.InvariantCultureIgnoreCase));
                if (frameworkPathData.Value != null)
                {
                    frameworkPath = frameworkPathData.Value;
                }

                var currentDir = new Uri(Environment.CurrentDirectory + "\\", UriKind.Absolute);
                var frameworkUri = new Uri(frameworkPath, UriKind.RelativeOrAbsolute);
                if (!frameworkUri.IsAbsoluteUri)
                {
                    frameworkUri = new Uri(currentDir, frameworkUri);
                }

                var currentProjectUri = new Uri(CurrentProject.FullDestinationPath, UriKind.RelativeOrAbsolute);
                if (!currentProjectUri.IsAbsoluteUri)
                {
                    currentProjectUri = new Uri(currentDir, currentProjectUri);
                }

                reference = new XElement(XName.Get("ProjectReference", CurrentProject.ProjectXmlNamespace));
                reference.SetAttributeValue(XName.Get("Include"), currentProjectUri.MakeRelativeUri(new Uri(frameworkUri, relativeProjectPath)));

                var projectGuidElement = new XElement(XName.Get("Project", CurrentProject.ProjectXmlNamespace), projectGuid.ToString("B"));

                var nameElement = new XElement(XName.Get("Name", CurrentProject.ProjectXmlNamespace), libraryName);

                reference.Add(projectGuidElement);
                reference.Add(nameElement);
            }
            else
            {
                reference = new XElement(XName.Get("Reference", CurrentProject.ProjectXmlNamespace));
                reference.SetAttributeValue(XName.Get("Include"), libraryName);

                XElement hintPath = new XElement(XName.Get("HintPath", CurrentProject.ProjectXmlNamespace));
                hintPath.Value = @"$([MSBuild]::GetRegistryValueFromView('HKEY_LOCAL_MACHINE\Software\Microsoft\.NETFramework\AssemblyFolders\ANX.Framework for WindowsStore', '', null, RegistryView.Registry64, RegistryView.Registry32))" + libraryFileName;

                reference.Add(hintPath);
            }

            return reference;
        }

        private bool IsProjectReferencesFlagSet
        {
            get
            {
                return Program.Switches.Contains("ProjectReferences", StringComparer.InvariantCultureIgnoreCase);
            }
        }

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
