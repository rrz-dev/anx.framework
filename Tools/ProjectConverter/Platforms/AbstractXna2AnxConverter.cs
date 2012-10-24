#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ProjectConverter.Platforms
{
    public abstract class AbstractXna2AnxConverter : Converter
    {
        protected internal List<string> csharpFiles = new List<string>();
        protected internal HashSet<string> filesToCopy = new HashSet<string>();
        protected internal HashSet<string> assemblyReferencesToAdd = new HashSet<string>();

        public override string Postfix
        {
            get { return string.Empty; }
        }

        protected internal abstract MappingDirection MappingDirection { get; }

        protected internal abstract string ReplaceInlineNamespaces(string input);

        protected override void ConvertItemGroup(System.Xml.Linq.XElement element)
        {
            var groups = element.Elements().ToList();
            foreach (var group in groups)
            {
                if (group.Name.LocalName.Equals("compile", StringComparison.InvariantCultureIgnoreCase))
                {
                    var attributes = group.Attributes().ToList();
                    foreach (var attribute in attributes)
                    {
                        if (attribute.Name.LocalName.Equals("include", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (attribute.Value.EndsWith(".cs", true, System.Globalization.CultureInfo.InvariantCulture))
                            {
                                csharpFiles.Add(attribute.Value);
                            }
                        }
                    }
                }
                else if (group.Name.LocalName.Equals("content", StringComparison.InvariantCultureIgnoreCase) ||
                         group.Name.LocalName.Equals("none", StringComparison.InvariantCultureIgnoreCase))
                {
                    var attributes = group.Attributes().ToList();
                    foreach (var attribute in attributes)
                    {
                        if (attribute.Name.LocalName.Equals("include", StringComparison.InvariantCultureIgnoreCase))
                        {
                            filesToCopy.Add(attribute.Value);
                        }
                    }
                }
            }

            base.ConvertItemGroup(element);
        }

        protected override void PostConvert()
        {
            string destinationPath = System.IO.Path.GetDirectoryName(base.CurrentProject.FullDestinationPath);

            foreach (var file in csharpFiles)
            {
                string target = string.Empty;
                ConvertUsingDirectives(file, ref target);

                string destinationFile = System.IO.Path.Combine(destinationPath, file);
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationFile));
                System.IO.File.WriteAllText(destinationFile, ReplaceInlineNamespaces(target));
            }

            string namespaceName = CurrentProject.Root.Name.NamespaceName;
            XName referenceName = XName.Get("Reference", namespaceName);
            XName itemGroupName = XName.Get("ItemGroup", namespaceName);
            var groups = CurrentProject.Root.Elements().ToList();

            foreach (var group in groups)
            {
                if (group.Name == itemGroupName)
                {
                    foreach (string reference in assemblyReferencesToAdd)
                    {
                        if (group.Element(GetXName("Reference")) != null)
                        {
                            var referenceElement = new XElement(GetXName("Reference"));
                            referenceElement.Add(new XAttribute("Include", reference));
                            group.Add(referenceElement);
                        }
                    }
                }
            }

            foreach (var file in filesToCopy)
            {
                string destinationFile = System.IO.Path.Combine(destinationPath, file);
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destinationFile));
                System.IO.File.Copy(System.IO.Path.Combine(CurrentProject.FullSourceDirectoryPath, file), destinationFile, true);
            }

            base.PostConvert();
        }

        protected void ConvertUsingDirectives(string file, ref string target)
        {
            string content = System.IO.File.ReadAllText(System.IO.Path.Combine(CurrentProject.FullSourceDirectoryPath, file));

            int lastPos = 0;
            int currentPos = -1;
            int endPos = 0;
            int tokenLength = 5;

            do
            {
                currentPos = content.IndexOf("using", currentPos + 1, StringComparison.InvariantCultureIgnoreCase);
                if (currentPos >= 0)
                {
                    endPos = NextTokenPos(ref content, currentPos + tokenLength, ref tokenLength);
                    string directive = content.Substring(currentPos + 6, endPos - currentPos - 6);

                    target += content.Substring(lastPos, (currentPos + 6) - lastPos);

                    directive = NamespaceMapper.TryMapNamespace(MappingDirection, directive);

                    target += directive;
                    lastPos = endPos;
                }
            } while (currentPos >= 0);

            target += content.Substring(lastPos);
        }

        private int NextTokenPos(ref string content, int startPos, ref int tokenLength)
        {
            int semicolonPos = content.IndexOf(";", startPos, StringComparison.InvariantCultureIgnoreCase);
            int usingPos = content.IndexOf("using", startPos, StringComparison.InvariantCultureIgnoreCase);
            int endPos = content.Length;
            int ret = 0;

            if (semicolonPos < 0) semicolonPos = int.MaxValue;
            if (usingPos < 0) usingPos = int.MaxValue;

            if (semicolonPos < usingPos)
            {
                ret = semicolonPos;
                tokenLength = 1;
            }
            else
            {
                ret = usingPos;
                tokenLength = 5;
            }

            if (ret < endPos)
            {
                return ret;
            }

            tokenLength = endPos - startPos;
            return endPos;
        }
    }
}
