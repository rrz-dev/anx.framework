#region Using Statements
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections.Generic;
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
    /// Provides an implementation of many of the methods of IntermediateSerializer. 
    /// Deserializes and tracks state for shared resources and external references.
    /// </summary>
    [Developer("KorsarNek")]
    [TestState(TestStateAttribute.TestState.InProgress)]
    [PercentageComplete(100)]
    public sealed class IntermediateReader
    {
        private struct ExternalReferenceFixup
        {
            public string ID;
            public Type TargetType;
            public Action<string> Fixup;
        }

        private string basePath;
        private Dictionary<string, List<Action<object>>> sharedResourceFixups = new Dictionary<string, List<Action<object>>>();
        private List<IntermediateReader.ExternalReferenceFixup> externalReferenceFixups = new List<IntermediateReader.ExternalReferenceFixup>();
        private XmlTypeNameReader xmlTypeNameReader;

        /// <summary>
        /// Gets the parent serializer.
        /// </summary>
        public IntermediateSerializer Serializer
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the XML input stream.
        /// </summary>
        public IntermediateXmlReader Xml
        {
            get;
            private set;
        }

        internal string BasePath
        {
            get { return this.basePath; }
        }

        internal IntermediateReader(IntermediateSerializer serializer, IntermediateXmlReader xmlReader, string basePath, XmlTypeNameContainer container)
        {
            this.Serializer = serializer;
            this.Xml = xmlReader;
            this.xmlTypeNameReader = new XmlTypeNameReader(container, xmlReader);

            if (basePath != null)
            {
                basePath = Path.GetFullPath(basePath);
            }

            this.basePath = basePath;
        }

        /// <summary>
        /// Reads a single object from the input XML stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format expected by the type serializer.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be triggered if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadObject<T>(ContentSerializerAttribute format)
        {
            return this.ReadObjectInternal<T>(format, this.Serializer.GetTypeSerializer(typeof(T)), null);
        }

        /// <summary>
        /// Reads a single object from the input XML stream, optionally specifying an existing instance to receive the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <param name="existingInstance">The object receiving the data, or null if a new instance should be created.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be triggered if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadObject<T>(ContentSerializerAttribute format, T existingInstance)
        {
            return this.ReadObjectInternal<T>(format, this.Serializer.GetTypeSerializer(typeof(T)), existingInstance);
        }

        /// <summary>
        /// Reads a single object from the input XML stream, using the specified type hint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format expected by the type serializer.</param>
        /// <param name="typeSerializer">The type serializer.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadObject<T>(ContentSerializerAttribute format, ContentTypeSerializer typeSerializer)
        {
            return this.ReadObjectInternal<T>(format, typeSerializer, null);
        }

        /// <summary>
        /// Reads a single object from the input XML stream using the specified type hint, optionally specifying an existing instance to receive the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <param name="typeSerializer">The type serializer.</param>
        /// <param name="existingInstance">The object receiving the data, or null if a new instance should be created.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadObject<T>(ContentSerializerAttribute format, ContentTypeSerializer typeSerializer, T existingInstance)
        {
            return this.ReadObjectInternal<T>(format, typeSerializer, existingInstance);
        }

        private T ReadObjectInternal<T>(ContentSerializerAttribute format, ContentTypeSerializer typeSerializer, object existingInstance)
        {
            if (typeSerializer == null)
                throw new ArgumentNullException("typeSerializer");

            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            if (!format.FlattenContent)
            {
                if (!this.Xml.CheckForElement(format.ElementName))
                    throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" not found.", format.ElementName));
                
                string attribute = this.Xml.GetAttribute("Null");
                if (attribute != null && XmlConvert.ToBoolean(attribute))
                {
                    if (!format.AllowNull)
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" is not allowed to be null.", format.ElementName));
                    
                    this.Xml.Skip();
                    return default(T);
                }
                else if (this.Xml.MoveToAttribute("Type"))
                {
                    Type type = this.ReadTypeName();
                    if (!typeSerializer.TargetType.IsAssignableFrom(type))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML \"Type\" attribute is invalid. Expecting a subclass of {0}, but XML contains {1}.", typeSerializer.TargetType, type));

                    typeSerializer = this.Serializer.GetTypeSerializer(type);
                    this.Xml.MoveToElement();
                }
                else if (typeSerializer.TargetType == typeof(object))
                {
                    throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, "XML is missing a \"Type\" attribute.");
                }
            }
            return this.ReadRawObjectInternal<T>(format, typeSerializer, existingInstance);
        }

        /// <summary>
        /// Reads a single object from the input XML stream as an instance of the specified type, optionally specifying an existing instance to receive the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be triggered if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadRawObject<T>(ContentSerializerAttribute format)
        {
            return this.ReadRawObjectInternal<T>(format, this.Serializer.GetTypeSerializer(typeof(T)), null);
        }

        /// <summary>
        /// Reads a single object from the input XML stream, as an instance of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <param name="existingInstance">The object receiving the data, or null if a new instance should be created.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be triggered if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadRawObject<T>(ContentSerializerAttribute format, T existingInstance)
        {
            return this.ReadRawObjectInternal<T>(format, this.Serializer.GetTypeSerializer(typeof(T)), existingInstance);
        }

        /// <summary>
        /// Reads a single object from the input XML stream as an instance of the specified type using the specified type hint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <param name="typeSerializer">The type serializer.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadRawObject<T>(ContentSerializerAttribute format, ContentTypeSerializer typeSerializer)
        {
            return this.ReadRawObjectInternal<T>(format, typeSerializer, null);
        }

        /// <summary>
        /// Reads a single object from the input XML stream as an instance of the specified type using the specified type hint, optionally specifying an existing instance to receive the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <param name="typeSerializer">The type serializer.</param>
        /// <param name="existingInstance">The object receiving the data, or null if a new instance should be created.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public T ReadRawObject<T>(ContentSerializerAttribute format, ContentTypeSerializer typeSerializer, T existingInstance)
        {
            return this.ReadRawObjectInternal<T>(format, typeSerializer, existingInstance);
        }

        private T ReadRawObjectInternal<T>(ContentSerializerAttribute format, ContentTypeSerializer typeSerializer, object existingInstance)
        {
            if (typeSerializer == null)
                throw new ArgumentNullException("typeSerializer");

            if (format == null)
                throw new ArgumentNullException("format");
            
            object obj;
            if (format.FlattenContent)
            {
                this.Xml.MoveToContent();
                obj = typeSerializer.Deserialize(this, format, existingInstance);
            }
            else
            {
                if (!this.Xml.CheckForElement(format.ElementName))
                    throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" not found.", format.ElementName));

                IntermediateXmlReader xmlReader = this.Xml;
                //When reading an empty element, XNA was giving a fake reader where it doesn't matter what you do.
                if (this.Xml.IsEmptyElement)
                {
                    this.Xml = EmptyElementReader.Instance;
                }
                xmlReader.ReadStartElement();
                obj = typeSerializer.Deserialize(this, format, existingInstance);
                if (this.Xml == xmlReader)
                {
                    this.Xml.ReadEndElement();
                }
                else
                {
                    this.Xml = xmlReader;
                }
            }

            if (obj == null)
                throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("Intermediate ContentTypeSerializer {0} (handling type {1}) returned a null value from its Deserialize method.", typeSerializer.GetType(), typeSerializer.TargetType));

            if (existingInstance != null && obj != existingInstance)
                throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("Intermediate ContentTypeSerializer {0} (handling type {1}) returned a new object instance from its Deserialize method. This should have loaded data into the existingInstance parameter.", typeSerializer.GetType(), typeSerializer.TargetType));

            if (!(obj is T))
                throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML \"Type\" attribute is invalid. Expecting a subclass of {0}, but XML contains {1}.", typeof(T), obj.GetType()));

            return (T)obj;
        }

        /// <summary>
        /// Reads a shared resource ID and records it for subsequent operations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format of the XML.</param>
        /// <param name="fixup">The fixup operation to perform.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="ANX.Framework.Content.Pipeline.InvalidContentException"></exception>
        public void ReadSharedResource<T>(ContentSerializerAttribute format, Action<T> fixup)
        {
            if (format == null)
                throw new ArgumentNullException("format");
            
            if (fixup == null)
                throw new ArgumentNullException("fixup");
            
            string text;
            if (format.FlattenContent)
            {
                text = this.Xml.ReadContentAsString();
            }
            else
            {
                if (!this.Xml.CheckForElement(format.ElementName))
                    throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" not found.", format.ElementName));

                text = this.Xml.ReadElementContentAsString();
            }

            if (string.IsNullOrEmpty(text))
            {
                if (!format.AllowNull)
                    throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" is not allowed to be null.", format.ElementName));
            }
            else
            {
                if (!this.sharedResourceFixups.ContainsKey(text))
                {
                    this.sharedResourceFixups.Add(text, new List<Action<object>>());
                }

                this.sharedResourceFixups[text].Add((value) =>
                    {
                        if (!(value is T))
                            throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML specifies invalid type for shared resource. Expecting a subclass of \"{0}\", but XML contains \"{1}\".", typeof(T), value.GetType()));

                        fixup((T)value);
                    });
            }
        }

        internal void ReadSharedResources()
        {
            if (this.Xml.CheckForElement("Resources"))
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                ContentSerializerAttribute contentSerializerAttribute = new ContentSerializerAttribute();
                contentSerializerAttribute.ElementName = "Resource";
                this.Xml.ReadStartElement();
                while (this.Xml.CheckForElement("Resource"))
                {
                    string id = this.Xml.GetAttribute("ID");
                    if (string.IsNullOrEmpty(id))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML attribute \"{0}\" was not found.", "ID"));

                    if (dictionary.ContainsKey(id))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("Duplicate XML ID attribute \"{0}\".", id));

                    object value = this.ReadObject<object>(contentSerializerAttribute);
                    dictionary.Add(id, value);
                }
                this.Xml.ReadEndElement();

                foreach (var fixup in this.sharedResourceFixups)
                {
                    object obj;
                    if (!dictionary.TryGetValue(fixup.Key, out obj))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("Missing shared resource \"{0}\".", fixup.Key));

                    foreach (var action in fixup.Value)
                    {
                        action(obj);
                    }
                }
            }
            else if (this.sharedResourceFixups.Count > 0)
            {
                throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" not found.", "Resources"));
            }
        }

        /// <summary>
        /// Reads an external reference ID and records it for subsequent operations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="existingInstance">The object receiving the data, or null if a new instance of the object should be created.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public void ReadExternalReference<T>(ExternalReference<T> existingInstance)
        {
            if (existingInstance == null)
                throw new ArgumentNullException("existingInstance");
            
            if (!this.Xml.CheckForElement("Reference"))
            {
                return;
            }

            string text = this.Xml.ReadElementContentAsString();
            if (!string.IsNullOrEmpty(text))
            {
                IntermediateReader.ExternalReferenceFixup item;
                item.ID = text;
                item.TargetType = typeof(T);
                item.Fixup = (x) => existingInstance.Filename = x;

                this.externalReferenceFixups.Add(item);
            }
        }

        internal void ReadExternalReferences()
        {
            if (this.Xml.CheckForElement("ExternalReferences"))
            {
                Dictionary<string, Type> idTypeDictionary = new Dictionary<string, Type>();
                Dictionary<string, string> idFilenameDictionary = new Dictionary<string, string>();

                this.Xml.ReadStartElement();
                while (this.Xml.CheckForElement("ExternalReference"))
                {
                    string id = this.Xml.GetAttribute("ID");
                    if (string.IsNullOrEmpty(id))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML attribute \"{0}\" was not found.", "ID"));

                    if (idTypeDictionary.ContainsKey(id))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("Duplicate XML ID attribute \"{0}\".", id));

                    if (!this.Xml.MoveToAttribute("TargetType"))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML attribute \"{0}\" was not found.", "TargetType"));

                    idTypeDictionary.Add(id, this.ReadTypeName());
                    this.Xml.MoveToElement();
                    string filename = this.Xml.ReadElementString();
                    string absolutePath = Path.Combine(this.basePath, filename);
                    idFilenameDictionary.Add(id, absolutePath);
                }

                this.Xml.ReadEndElement();
                foreach (var refFixup in externalReferenceFixups)
                {
                    if (!idTypeDictionary.ContainsKey(refFixup.ID))
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("Missing external reference \"{0}\".", refFixup.ID));

                    if (idTypeDictionary[refFixup.ID] != refFixup.TargetType)
                        throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML specifies wrong type for external reference \"{0}\".", refFixup.ID));

                    refFixup.Fixup(idFilenameDictionary[refFixup.ID]);
                }
            }
            else if (this.externalReferenceFixups.Count > 0)
            {
                throw ExceptionHelper.CreateInvalidContentException(this.Xml, this.BasePath, null, string.Format("XML element \"{0}\" not found.", "ExternalReferences"));
            }
        }

        /// <summary>
        /// Reads and decodes a type descriptor from the XML input stream.
        /// </summary>
        /// <returns></returns>
        public Type ReadTypeName()
        {
            string typeName = this.Xml.ReadContentAsString();
            return this.xmlTypeNameReader.GetTypeFromXmlName(typeName);
        }

        /// <summary>
        /// Moves to the specified element if the element name exists.
        /// </summary>
        /// <param name="elementName">The element name.</param>
        /// <returns></returns>
        public bool MoveToElement(string elementName)
        {
            return this.Xml.CheckForElement(elementName);
        }
    }
}
