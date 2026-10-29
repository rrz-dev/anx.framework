#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Reflection;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
    public class XnaContentProjectConverter : Converter
    {
        #region Private Members
        private ContentProject targetContentProject = null;
        string targetProjectFile;

        #endregion

        public override string Name
        {
            get { return "content2anx"; }
        }

        public override string Postfix
        {
            get { return string.Empty; }
        }

        public override bool WriteSourceProjectToDestination
        {
            get { return false; }
        }

        protected override void PreConvert()
        {
            this.targetContentProject = new ContentProject(CurrentProject.ProjectName);
            this.targetContentProject.Creator = String.Format("ANX {0} v{1}", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version);
            this.targetProjectFile = Path.Combine(Path.GetDirectoryName(CurrentProject.FullDestinationPath),
                                                  Path.GetFileNameWithoutExtension(CurrentProject.FullDestinationPath) + ".cproj");
        }

        protected override void PostConvert()
        {
            targetContentProject.Save(this.targetProjectFile);
        }

        protected override void ConvertMainPropertyGroup(XElement element)
        {
            ConvertPropertyGroup(element);
        }

        protected override void ConvertPropertyGroup(XElement element)
        {
            targetContentProject.ContentRoot = GetSubNodeValue(element, "RootNamespace");
        }

        protected override void ConvertItemGroup(System.Xml.Linq.XElement element)
        {
            var groups = element.Elements().ToList();
            foreach (var group in groups)
            {
                var attributes = group.Attributes().ToList();

                if (group.Name.LocalName.Equals("reference", StringComparison.InvariantCultureIgnoreCase))
                {
                    string include = GetAttributeValue(attributes, "include");
                    targetContentProject.References.Add(include);
                }
                else if (group.Name.LocalName.Equals("compile", StringComparison.InvariantCultureIgnoreCase))
                {
                    string include = GetAttributeValue(attributes, "include");
                    string name = GetSubNodeValue(group, "Name");
                    string importer = GetSubNodeValue(group, "importer");
                    string processor = GetSubNodeValue(group, "processor");
                    Dictionary<string, string> parameters = GetSubNodeValues(group, "ProcessorParameters", "_");

                    BuildItem buildItem = new BuildItem()
                    {
                        AssetName = name,
                        SourceFilename = include,
                        OutputFilename = name + ".xnb",
                        ImporterName = importer,
                        ProcessorName = processor,
                    };

                    foreach (KeyValuePair<string, string> parameter in parameters)
                    {
                        buildItem.ProcessorParameters.Add(parameter.Key, parameter.Value);
                    }

                    targetContentProject.BuildItems.Add(buildItem);
                }
            }

            base.ConvertItemGroup(element);
        }

        private string GetAttributeValue(IEnumerable<XAttribute> attributes, string name)
        {
            foreach (XAttribute attribute in attributes)
            {
                if (attribute.Name.LocalName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return attribute.Value;
                }
            }

            return string.Empty;
        }

        private string GetSubNodeValue(XElement node, string subNodeName)
        {
            var elements = node.Elements().ToList();
            foreach (var element in elements)
            {
                if (element.Name.LocalName.Equals(subNodeName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return element.Value;
                }
            }

            return string.Empty;
        }

        private Dictionary<string, string> GetSubNodeValues(XElement node, string subNodeNameStartingWith, string splitter)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            var elements = node.Elements().ToList();
            foreach (var element in elements)
            {
                if (element.Name.LocalName.StartsWith(subNodeNameStartingWith, StringComparison.InvariantCultureIgnoreCase))
                {
                    string[] parts = element.Name.LocalName.Split(new string[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
                    values[parts[1]] = element.Value;
                }
            }

            return values;
        }
    }
}
