using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Tasks.References
{
    [Serializable]
    public class AssemblyReference : Reference
    {
        public String AssemblyPath
        {
            get;
            set;
        }

        public override void Write(XmlWriter writer)
        {
            base.Write(writer);

            writer.WriteString(this.AssemblyPath);
        }

        public override void Load(XmlReader reader)
        {
            base.Load(reader);

            this.AssemblyPath = reader.ReadElementContentAsString();
        }
    }
}
