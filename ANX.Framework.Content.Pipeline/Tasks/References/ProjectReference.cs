using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks.References
{
    [Serializable]
    public class ProjectReference : Reference
    {
        public Guid Guid
        {
            get;
            set;
        }

        public string Include
        {
            get;
            set;
        }

        public String AssemblyPath
        {
            get;
            set;
        }

        public override void Write(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(this.Include))
            {
                writer.WriteAttributeString("Include", this.Include);
            }

            if (this.Guid != Guid.Empty)
            {
                writer.WriteAttributeString("Guid", this.Guid.ToString("B"));
            }

            base.Write(writer);

            writer.WriteString(this.AssemblyPath);
        }

        public override void Load(System.Xml.XmlReader reader)
        {
            string include = reader.GetAttribute("Include");
            string guid = reader.GetAttribute("Guid");

            base.Load(reader);

            //Skip attributes
            this.AssemblyPath = reader.ReadElementContentAsString();

            this.Include = include;
            if (guid != null)
                this.Guid = new Guid(guid);
        }
    }
}
