using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    internal class EmptyElementReader : IntermediateXmlReader
    {
        public static readonly EmptyElementReader Instance = new EmptyElementReader();

        public override int AttributeCount
        {
            get
            {
                return 0;
            }
        }

        public override string BaseURI
        {
            get
            {
                return null;
            }
        }

        public override int Depth
        {
            get
            {
                return 0;
            }
        }

        public override bool EOF
        {
            get
            {
                return false;
            }
        }

        public override bool HasValue
        {
            get
            {
                return false;
            }
        }

        public override bool IsEmptyElement
        {
            get
            {
                return true;
            }
        }

        public override string LocalName
        {
            get
            {
                return null;
            }
        }

        public override XmlNameTable NameTable
        {
            get
            {
                return null;
            }
        }

        public override string NamespaceURI
        {
            get
            {
                return null;
            }
        }

        public override XmlNodeType NodeType
        {
            get
            {
                return XmlNodeType.Text;
            }
        }

        public override string Prefix
        {
            get
            {
                return null;
            }
        }

        public override ReadState ReadState
        {
            get
            {
                return ReadState.EndOfFile;
            }
        }

        public override string Value
        {
            get
            {
                return string.Empty;
            }
        }

        public override void Close()
        {
        }

        public override string GetAttribute(int i)
        {
            return null;
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return null;
        }

        public override string GetAttribute(string name)
        {
            return null;
        }

        public override string LookupNamespace(string prefix)
        {
            return null;
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            return false;
        }

        public override bool MoveToAttribute(string name)
        {
            return false;
        }

        public override bool MoveToElement()
        {
            return false;
        }

        public override bool MoveToFirstAttribute()
        {
            return false;
        }

        public override bool MoveToNextAttribute()
        {
            return false;
        }

        public override bool Read()
        {
            return false;
        }

        public override bool ReadAttributeValue()
        {
            return false;
        }

        public override void ResolveEntity()
        {

        }
    }
}
