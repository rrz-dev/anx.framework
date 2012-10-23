using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectConverter.Platforms
{
    public abstract class AbstractXna2AnxConverter : Converter
    {
        protected internal List<string> csharpFiles = new List<string>();

        public override string Postfix
        {
            get { return string.Empty; }
        }

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
            }

            base.ConvertItemGroup(element);
        }

        protected override void PostConvert()
        {
            foreach (var file in csharpFiles)
            {
                string target = string.Empty;
                ConvertUsingDirectives(file, ref target);
            }

            base.PostConvert();
        }

        protected abstract void ConvertUsingDirectives(string file, ref string target);

        protected void ConvertUsingDirectivesImpl(ref string content, ref string target, string sourceNamespace, string targetNamespace, string[] namespaces)
        {
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
                    string directive = content.Substring(currentPos + 5, endPos - currentPos - 5);

                    target += content.Substring(lastPos, (currentPos + 6) - lastPos);

                    if (namespaces.Contains<string>(directive.Trim()))
                    {
                        directive = directive.Replace(sourceNamespace, targetNamespace);
                    }

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
