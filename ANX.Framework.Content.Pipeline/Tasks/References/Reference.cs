using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Tasks.References
{
    [Serializable]
    public abstract class Reference
    {
        public string Name
        {
            get;
            set;
        }

        public virtual void Write(XmlWriter writer)
        {
            writer.WriteStartAttribute("Name");
            writer.WriteString(this.Name);
            writer.WriteEndAttribute();
        }

        public virtual void Load(XmlReader reader)
        {
            this.Name = reader.GetAttribute("Name");
        }
    }
}
