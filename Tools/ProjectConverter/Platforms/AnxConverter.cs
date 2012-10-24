using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ProjectConverter.Platforms
{
    public class AnxConverter : AbstractXna2AnxConverter
    {
        public AnxConverter()
        {
            // add default XNA references
            assemblyReferencesToAdd.Add("Microsoft.Xna.Framework");
            assemblyReferencesToAdd.Add("Microsoft.Xna.Framework.Game");
            assemblyReferencesToAdd.Add("Microsoft.Xna.Framework.Input");
            assemblyReferencesToAdd.Add("Microsoft.Xna.Framework.Graphics");
        }

        public override string Name
        {
            get { return "anx2xna"; }
        }

        protected internal override MappingDirection MappingDirection
        {
            get { return ProjectConverter.MappingDirection.Anx2Xna; }
        }

        protected override void ConvertProjectReference(XElement element)
        {
            XAttribute includeAttribute = element.Attribute("Include");
            if (includeAttribute != null)
            {
                string reference = Path.GetFileNameWithoutExtension(includeAttribute.Value);    // sometimes the node contains a relative path to the reference

                if (NamespaceMapper.IsProjectReference(MappingDirection, reference))
                {
                    string referenceAssembly = NamespaceMapper.GetReferencingAssemblyName(MappingDirection, reference);
                    if (!string.IsNullOrEmpty(referenceAssembly))
                    {
                        assemblyReferencesToAdd.Add(referenceAssembly);
                    }

                    element.Remove();
                }
            }
        }

        protected internal override string ReplaceInlineNamespaces(string input)
        {
            return input.Replace("ANX.Framework.Game", "Microsoft.Xna.Framework.Game");
        }
    }
}
