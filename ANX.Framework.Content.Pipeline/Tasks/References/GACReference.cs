using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks.References
{
    [Serializable]
    public class GACReference : Reference
    {
        public string AssemblyName
        {
            get;
            set;
        }

        public override void Write(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("AssemblyName", this.AssemblyName);

            base.Write(writer);
        }

        public override void Load(System.Xml.XmlReader reader)
        {
            this.AssemblyName = reader.GetAttribute("AssemblyName");

            base.Load(reader);
        }
    }
}
