using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    public class IntermediateXmlWriter : XmlWriter
    {
        XmlWriter internalWriter;
        bool firstPart = true;

        public IntermediateXmlWriter(XmlWriter internalWriter)
        {
            if (internalWriter == null)
                throw new ArgumentNullException("internalWriter");

            this.internalWriter = internalWriter;
        }

        public override void Close()
        {
            this.internalWriter.Close();
        }

        public override void Flush()
        {
            this.internalWriter.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return this.internalWriter.LookupPrefix(ns);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            this.internalWriter.WriteBase64(buffer, index, count);

            StopPartWriting();
        }

        public override void WriteCData(string text)
        {
            this.internalWriter.WriteCData(text);

            StopPartWriting();
        }

        public override void WriteCharEntity(char ch)
        {
            this.internalWriter.WriteCharEntity(ch);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            this.internalWriter.WriteChars(buffer, index, count);
        }

        public override void WriteComment(string text)
        {
            this.internalWriter.WriteComment(text);

            StopPartWriting();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            internalWriter.WriteDocType(name, pubid, sysid, subset);

            StopPartWriting();
        }

        public override void WriteEndAttribute()
        {
            internalWriter.WriteEndAttribute();

            StopPartWriting();
        }

        public override void WriteEndDocument()
        {
            internalWriter.WriteEndDocument();

            StopPartWriting();
        }

        public override void WriteEndElement()
        {
            internalWriter.WriteEndElement();

            StopPartWriting();
        }

        public override void WriteEntityRef(string name)
        {
            internalWriter.WriteEntityRef(name);
        }

        public override void WriteFullEndElement()
        {
            internalWriter.WriteFullEndElement();

            StopPartWriting();
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            internalWriter.WriteProcessingInstruction(name, text);
        }

        public override void WriteRaw(string data)
        {
            internalWriter.WriteRaw(data);

            StopPartWriting();
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            internalWriter.WriteRaw(buffer, index, count);

            StopPartWriting();
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            internalWriter.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteStartDocument(bool standalone)
        {
            internalWriter.WriteStartDocument(standalone);

            StopPartWriting();
        }

        public override void WriteStartDocument()
        {
            internalWriter.WriteStartDocument();

            StopPartWriting();
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            internalWriter.WriteStartElement(prefix, localName, ns);

            StopPartWriting();
        }

        public override WriteState WriteState
        {
            get { return internalWriter.WriteState; }
        }

        public override void WriteString(string text)
        {
            internalWriter.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            this.internalWriter.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteWhitespace(string ws)
        {
            this.internalWriter.WriteWhitespace(ws);
        }

        private void StopPartWriting()
        {
            firstPart = true;
        }

        public void WriteStringPart(string text)
        {
            foreach (var c in IntermediateXmlReader.listSeparators)
            {
                if (text.Contains(c))
                    throw new ArgumentException("text must not contain a whitespace or linebreak.");
            }

            if (firstPart)
                firstPart = false;
            else
                this.internalWriter.WriteWhitespace(" ");

            this.internalWriter.WriteString(text);
        }

        public void WritePart(int value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(uint value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(float value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(bool value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(byte value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(sbyte value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(double value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(char value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(short value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(ushort value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(long value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }

        public void WritePart(ulong value)
        {
            WriteStringPart(XmlConvert.ToString(value));
        }
    }
}
