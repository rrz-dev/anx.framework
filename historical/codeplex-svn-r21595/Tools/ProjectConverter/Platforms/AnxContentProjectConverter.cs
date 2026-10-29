#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;
using System.Xml.Linq;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
    public class AnxContentProjectConverter : Converter
    {
        private ContentProject sourceContentProject = null;

        public override string Name
        {
            get { return "content2xna"; }
        }

        public override string Postfix
        {
            get { return string.Empty; }
        }

        public override string TargetFileExtension
        {
            get
            {
                return ".contentproj";
            }
        }

        public override bool WriteSourceProjectToDestination
        {
            get { return true; }
        }

        protected override void PreConvert()
        {
            this.sourceContentProject = ContentProject.Load(CurrentProject.FullSourcePath);
        }

        protected override void ConvertMainPropertyGroup(XElement element)
        {
            var rootNameSpaceNode = GetOrCreateNode(element, "RootNamespace");
            rootNameSpaceNode.Value = sourceContentProject.ContentRoot;
        }

        protected override void ConvertProject(XElement element)
        {
            var itemGroupName = GetXName("ItemGroup");
            var includeName = XName.Get("Include");
            var referenceName = GetXName("Reference");

            //
            // add build items
            //
            foreach (var buildItem in sourceContentProject.BuildItems)
            {
                XElement buildItemElement = new XElement(itemGroupName);
                
                var compileElement = GetOrCreateNode(buildItemElement, "Compile");
                compileElement.SetAttributeValue(includeName, buildItem.SourceFilename);
                
                var nameElement = GetOrCreateNode(compileElement, "Name");
                nameElement.Value = buildItem.AssetName;

                var importerElement = GetOrCreateNode(compileElement, "Importer");
                importerElement.Value = buildItem.ImporterName;

                var processorElement = GetOrCreateNode(compileElement, "Processor");
                processorElement.Value = buildItem.ProcessorName;

                foreach (var processorParameter in buildItem.ProcessorParameters)
                {
                    var parameterElement = GetOrCreateNode(compileElement, "ProcessorParamters_" + processorParameter.Key);
                    parameterElement.Value = processorParameter.Value.ToString();
                }

                element.Add(buildItemElement);
            }

            //
            // add references
            //
            XElement referenceItemGroup = new XElement(itemGroupName);

            foreach (var reference in sourceContentProject.References)
            {
                var referenceElement = new XElement(referenceName);
                referenceElement.SetAttributeValue(includeName, reference);
                referenceItemGroup.Add(referenceElement);
            }

            element.Add(referenceItemGroup);
        }

        protected override void PostConvert()
        {
            
        }



        public override string ProjectFileTemplate
        {
            get
            {
                return @"<?xml version=""1.0"" encoding=""utf-8""?>
<Project DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"" ToolsVersion=""4.0"">
  <PropertyGroup>
    <ProjectGuid>{FA6E229D-4504-47B1-8A23-2D3FCC13F778}</ProjectGuid>
    <ProjectTypeGuids>{96E2B04D-8817-42c6-938A-82C39BA4D311};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">x86</Platform>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <TargetFrameworkVersion>v4.0</TargetFrameworkVersion>
    <XnaFrameworkVersion>v4.0</XnaFrameworkVersion>
    <OutputPath>bin\$(Platform)\$(Configuration)</OutputPath>
  </PropertyGroup>
  <PropertyGroup>
    <RootNamespace />
  </PropertyGroup>
  <Import Project=""$(MSBuildExtensionsPath)\Microsoft\XNA Game Studio\$(XnaFrameworkVersion)\Microsoft.Xna.GameStudio.ContentPipeline.targets"" />
  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. 
       Other similar extension points exist, see Microsoft.Common.targets.
  <Target Name=""BeforeBuild"">
  </Target>
  <Target Name=""AfterBuild"">
  </Target>
  -->
</Project>
                        ";
            }
        }
    }
}
