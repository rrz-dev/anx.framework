using ANX.Framework.Content.Pipeline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    public partial class IntermediateXmlReader : XmlReader
    {
        internal static readonly char[] listSeparators = new char[]
		{
			' ',
			'\t',
			'\r',
			'\n'
		};

        XmlReader internalReader;
        IEnumerator<string> enumerator;
        bool atEnd = false;
        bool readingParts = false;

        protected IntermediateXmlReader()
        {

        }

        public IntermediateXmlReader(XmlReader internalReader)
        {
            if (internalReader == null)
                throw new ArgumentNullException("internalReader");

            this.internalReader = internalReader;

            if (internalReader is IntermediateXmlReader)
            {
                var reader = (IntermediateXmlReader)internalReader;
                this.enumerator = reader.enumerator;
                this.atEnd = reader.atEnd;
                this.readingParts = reader.readingParts;
            }
        }

        public override void ReadEndElement()
        {
            internalReader.ReadEndElement();

            ShutdownPartReading();
        }

        public override void ReadStartElement()
        {
            internalReader.ReadStartElement();

            ShutdownPartReading();
        }

        public override void ReadStartElement(string localname, string ns)
        {
            internalReader.ReadStartElement(localname, ns);

            ShutdownPartReading();
        }

        public override void ReadStartElement(string name)
        {
            internalReader.ReadStartElement(name);

            ShutdownPartReading();
        }

        public override int AttributeCount
        {
            get { return internalReader.AttributeCount; }
        }

        public override string BaseURI
        {
            get { return internalReader.BaseURI; }
        }

        public override void Close()
        {
            internalReader.Close();

            ShutdownPartReading();
        }

        public override int Depth
        {
            get { return internalReader.Depth; }
        }

        public override bool EOF
        {
            get { return internalReader.EOF; }
        }

        public override string GetAttribute(int i)
        {
            return internalReader.GetAttribute(i);
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            return internalReader.GetAttribute(name, namespaceURI);
        }

        public override string GetAttribute(string name)
        {
            return internalReader.GetAttribute(name);
        }

        public override bool IsEmptyElement
        {
            get { return internalReader.IsEmptyElement; }
        }

        public override string LocalName
        {
            get { return internalReader.LocalName; }
        }

        public override string LookupNamespace(string prefix)
        {
            return internalReader.LookupNamespace(prefix);
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            ShutdownPartReading();

            return internalReader.MoveToAttribute(name, ns);
        }

        public override bool MoveToAttribute(string name)
        {
            ShutdownPartReading();

            return internalReader.MoveToAttribute(name);
        }

        public override bool MoveToElement()
        {
            return internalReader.MoveToElement();
        }

        public override bool MoveToFirstAttribute()
        {
            ShutdownPartReading();

            return internalReader.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            ShutdownPartReading();

            return internalReader.MoveToNextAttribute();
        }

        public override XmlNameTable NameTable
        {
            get { return internalReader.NameTable; }
        }

        public override string NamespaceURI
        {
            get { return internalReader.NamespaceURI; }
        }

        public override XmlNodeType NodeType
        {
            get 
            {
                return internalReader.NodeType;
            }
        }

        public override string Prefix
        {
            get { return internalReader.Prefix; }
        }

        public override bool Read()
        {
            if (readingParts)
            {
                bool movedNext = this.enumerator.MoveNext();
                this.atEnd = !movedNext;
                return movedNext;
            }
            else
            {
                return internalReader.Read();
            }
        }

        public override bool ReadAttributeValue()
        {
            return internalReader.ReadAttributeValue();
        }

        public override ReadState ReadState
        {
            get { return internalReader.ReadState; }
        }

        public override bool CanResolveEntity
        {
            get
            {
                return internalReader.CanResolveEntity;
            }
        }

        public override void ResolveEntity()
        {
            internalReader.ResolveEntity();
        }

        public override string Value
        {
            get 
            {
                return internalReader.Value;
            }
        }

        private void InitializePartReading()
        {
            IEnumerable<string> enumerable = internalReader.ReadContentAsString().Split(listSeparators, StringSplitOptions.RemoveEmptyEntries);
            this.enumerator = enumerable.GetEnumerator();
            this.atEnd = !this.enumerator.MoveNext();
            this.readingParts = true;
        }

        private void ShutdownPartReading()
        {
            this.readingParts = false;
        }

        public string ReadStringPart()
        {
            if (readingParts == false)
            {
                InitializePartReading();
            }

            if (this.atEnd)
            {
                throw new InvalidOperationException("XML node doesn't contain enough text parts.");
            }

            if (this.ReadState != ReadState.Interactive)
            {
                return string.Empty;
            }

            string value = this.enumerator.Current;

            this.atEnd = !this.enumerator.MoveNext();

            return value;
        }

        public bool HasMoreParts
        {
            get
            {
                if (readingParts)
                {
                    return !atEnd;
                }
                else
                {
                    return !this.IsEmptyElement;
                }
            }
        }
    }
}
