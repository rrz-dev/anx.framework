#region Using Statements
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    /// <summary>
    /// Provides an implementation of many of the methods of IntermediateSerializer including serialization and state tracking for shared resources and external references.
    /// </summary>
    [Developer("KorsarNek")]
    [TestState(TestStateAttribute.TestState.Tested)]
    [PercentageComplete(100)]
    public sealed class IntermediateWriter
    {
        private struct ExternalReference
        {
            public Type TargetType;
            public string Filename;
            public string ID;
        }

        private struct RecursionIdentifier
        {
            public object instance;
            public Type targetType;
        }

        private string basePath;
        private List<Type> writtenTypes = new List<Type>();
        private Dictionary<RecursionIdentifier, bool> recurseDetector = new Dictionary<RecursionIdentifier, bool>();
        private Dictionary<object, string> sharedResourceNames = new Dictionary<object, string>(new ReferenceEqualityComparer<object>());
        private Queue<object> sharedResources = new Queue<object>();
        private List<IntermediateWriter.ExternalReference> externalReferences = new List<IntermediateWriter.ExternalReference>();
        private IntermediateXmlWriter realXml;
        private StringBuilder inMemoryXml = new StringBuilder();
        private XmlTypeNameWriter xmlTypeNameWriter;

        /// <summary>
        /// Gets the parent serializer.
        /// </summary>
        public IntermediateSerializer Serializer
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the XML output stream.
        /// </summary>
        public IntermediateXmlWriter Xml
        {
            get;
            private set;
        }

        internal IntermediateWriter(IntermediateSerializer serializer, IntermediateXmlWriter xmlWriter, string basePath, XmlTypeNameContainer xmlTypeNameContainer, bool shortenNamespaces)
        {
            this.Serializer = serializer;
            this.realXml = xmlWriter;

            this.xmlTypeNameWriter = new XmlTypeNameWriter(xmlTypeNameContainer, shortenNamespaces);

            StringWriter memoryWriter = new StringWriter(inMemoryXml);
            //The xml declaration will already be contained in the given xmlWriter.
            this.Xml = new IntermediateXmlWriter(XmlWriter.Create(memoryWriter, new XmlWriterSettings() { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Auto }));

            //We need a full path to be able to make the compiled files relative to it.
            if (basePath != null)
            {
                basePath = Path.GetFullPath(basePath);
            }
            this.basePath = basePath;
        }

        /// <summary>
        /// Writes a single object to the output XML stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format of the XML.</param>
        public void WriteObject<T>(T value, ContentSerializerAttribute format)
        {
            this.WriteObject<T>(value, format, this.Serializer.GetTypeSerializer(typeof(T)));
        }

        /// <summary>
        /// Writes a single object to the output XML stream, using the specified type hint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format of the XML.</param>
        /// <param name="typeSerializer">The type serializer.</param>
        public void WriteObject<T>(T value, ContentSerializerAttribute format, ContentTypeSerializer typeSerializer)
        {
            if (typeSerializer == null)
                throw new ArgumentNullException("typeSerializer");

            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            
            if (!format.FlattenContent)
            {
                if (string.IsNullOrEmpty(format.ElementName))
                    throw new ArgumentException("ContentSerializerAttribute has a null ElementName property.");
                
                this.Xml.WriteStartElement(format.ElementName);
            }
            if (value == null)
            {
                if (format.FlattenContent)
                    throw new InvalidOperationException("Cannot serialize null values when the ContentSerializerAttribute.FlattenContent flag is set.");
                
                this.Xml.WriteAttributeString("Null", "true");
            }
            else
            {
                Type type = value.GetType();
                if (type.IsSubclassOf(typeof(Type)))
                {
                    type = typeof(Type); 
                }

                //Special handling for nullables. Reason is that calling GetType() on a nullable will return the underlying type, not the nullable, therefore we would
                //wrongly write the underlying type into the xml even though it is already known.
                Type targetType = typeSerializer.TargetType;
                if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    targetType = Nullable.GetUnderlyingType(targetType);
                }

                if (type != targetType)
                {
                    if (format.FlattenContent)
                        throw new InvalidOperationException("Cannot serialize derived types when the ContentSerializerAttribute.FlattenContent flag is set.");

                    typeSerializer = this.Serializer.GetTypeSerializer(type);
                    this.Xml.WriteStartAttribute("Type");
                    this.WriteTypeName(typeSerializer.TargetType);
                    this.Xml.WriteEndAttribute();
                }

                RecursionIdentifier recursion = new RecursionIdentifier()
                    {
                        instance = value,
                        targetType = typeof(T)
                    };

                if (this.recurseDetector.ContainsKey(recursion))
                    throw new InvalidOperationException(string.Format("Cyclic reference found while serializing {0}. You may be missing a ContentSerializerAttribute.SharedResource flag.", value));

                this.recurseDetector.Add(recursion, true);
                typeSerializer.Serialize(this, value, format);
                this.recurseDetector.Remove(recursion);

                writtenTypes.Add(typeSerializer.TargetType);
            }

            if (!format.FlattenContent)
            {
                this.Xml.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes a single object to the output XML stream using the specified serializer worker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format of the XML.</param>
        public void WriteRawObject<T>(T value, ContentSerializerAttribute format)
        {
            if (value == null)
                throw new ArgumentException("value");

            this.WriteRawObject<T>(value, format, this.Serializer.GetTypeSerializer(value.GetType()));
        }

        /// <summary>
        /// Writes a single object to the output XML stream as an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format of the XML.</param>
        /// <param name="typeSerializer">The type serializer.</param>
        public void WriteRawObject<T>(T value, ContentSerializerAttribute format, ContentTypeSerializer typeSerializer)
        {
            if (value == null)
                throw new ArgumentException("value");
            
            if (typeSerializer == null)
                throw new ArgumentNullException("typeSerializer");

            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            
            if (!format.FlattenContent)
            {
                if (string.IsNullOrEmpty(format.ElementName))
                {
                    throw new ArgumentException("\"format\" must have an ElementName if FlattenContent is false.");
                }
                this.Xml.WriteStartElement(format.ElementName);
            }

            typeSerializer.Serialize(this, value, format);

            if (!format.FlattenContent)
            {
                this.Xml.WriteEndElement();
            }
        }

        /// <summary>
        /// Adds a shared reference to the output XML and records the object to be serialized later.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to write.</param>
        /// <param name="format">The format of the XML.</param>
        public void WriteSharedResource<T>(T value, ContentSerializerAttribute format)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            
            if (!format.FlattenContent)
            {
                if (string.IsNullOrEmpty(format.ElementName))
                    throw new ArgumentException("ContentSerializerAttribute has a null ElementName property.");
                
                this.Xml.WriteStartElement(format.ElementName);
            }

            if (value != null)
            {
                string text;
                if (!this.sharedResourceNames.TryGetValue(value, out text))
                {
                    text = "#Resource" + (this.sharedResourceNames.Count + 1).ToString(CultureInfo.InvariantCulture);
                    this.sharedResourceNames.Add(value, text);
                    this.sharedResources.Enqueue(value);
                }
                this.Xml.WriteString(text);
            }

            if (!format.FlattenContent)
            {
                this.Xml.WriteEndElement();
            }
        }

        internal void WriteSharedResources()
        {
            if (this.sharedResources.Count > 0)
            {
                this.Xml.WriteStartElement("Resources");
                ContentSerializerAttribute contentSerializerAttribute = new ContentSerializerAttribute();
                contentSerializerAttribute.ElementName = "Resource";
                contentSerializerAttribute.FlattenContent = true;

                while (this.sharedResources.Count > 0)
                {
                    object obj = this.sharedResources.Dequeue();
                    Type type = obj.GetType();
                    ContentTypeSerializer typeSerializer = this.Serializer.GetTypeSerializer(type);
                    this.Xml.WriteStartElement("Resource");
                    this.Xml.WriteAttributeString("ID", this.sharedResourceNames[obj]);
                    this.Xml.WriteStartAttribute("Type");
                    this.WriteTypeName(type);
                    this.Xml.WriteEndAttribute();
                    this.WriteRawObject<object>(obj, contentSerializerAttribute, typeSerializer);
                    this.Xml.WriteEndElement();
                }
                this.Xml.WriteEndElement();
            }
        }

        /// <summary>
        /// Adds an external reference to the output XML, and records the filename to be serialized later.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The external reference to add.</param>
        public void WriteExternalReference<T>(ExternalReference<T> value)
        {
            if (value == null || value.Filename == null)
            {
                return;
            }

            //TODO: It's currently not possible to reference files that are a directory higher, only that are lower than the current file or at the same level.
            //Should think about making these paths relative with URIs which would allow reference files that are higher, but would always make a relative path if a basePath is given.
            string relativePath = value.Filename;
            if (relativePath != null && basePath != null && relativePath.StartsWith(basePath))
            {
                relativePath = relativePath.Substring(basePath.Length).Trim(Path.DirectorySeparatorChar);
            }
            
            string id = null;
            foreach (IntermediateWriter.ExternalReference current in this.externalReferences)
            {
                if (current.TargetType == typeof(T) && current.Filename == relativePath)
                {
                    id = current.ID;
                    break;
                }
            }

            if (id == null)
            {
                id = "#External" + (this.externalReferences.Count + 1).ToString(CultureInfo.InvariantCulture);
                IntermediateWriter.ExternalReference item;
                item.TargetType = typeof(T);
                item.Filename = relativePath;
                item.ID = id;
                this.externalReferences.Add(item);
            }

            this.Xml.WriteElementString("Reference", id);
        }

        internal void WriteExternalReferences()
        {
            if (this.externalReferences.Count > 0)
            {
                this.Xml.WriteStartElement("ExternalReferences");
                foreach (IntermediateWriter.ExternalReference current in this.externalReferences)
                {
                    this.Xml.WriteStartElement("ExternalReference");
                    this.Xml.WriteAttributeString("ID", current.ID);
                    this.Xml.WriteStartAttribute("TargetType");
                    this.WriteTypeName(current.TargetType);
                    this.Xml.WriteEndAttribute();
                    this.Xml.WriteString(current.Filename);
                    this.Xml.WriteEndElement();
                }
                this.Xml.WriteEndElement();
            }
        }

        internal void WriteUsedNamespaces()
        {
            foreach (var xmlNamespace in this.xmlTypeNameWriter.NamespaceAbbreviations)
            {
                this.realXml.WriteAttributeString("xmlns", xmlNamespace.Value, null, xmlNamespace.Key);
            }
        }

        internal void ComposeXml()
        {
            this.Xml.Flush();
            this.realXml.WriteRaw(this.inMemoryXml.ToString());
        }

        /// <summary>
        /// Writes a managed type descriptor to the XML output stream.
        /// </summary>
        /// <param name="type">The type.</param>
        public void WriteTypeName(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            
            this.Xml.WriteString(this.xmlTypeNameWriter.getXmlTypeName(type));
        }
    }
}
