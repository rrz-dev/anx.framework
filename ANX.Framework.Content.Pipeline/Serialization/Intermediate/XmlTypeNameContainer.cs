using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    class XmlTypeNameContainer
    {
        private Dictionary<Type, string> _contentSerializerMappings = new Dictionary<Type, string>();
        private Dictionary<string, Type> _xmlNameToType = new Dictionary<string,Type>();
        private Dictionary<Type, string> _typeToXmlName = new Dictionary<Type,string>();
        private Dictionary<string, string> _namespaceRenames = new Dictionary<string, string>();

        public void AddEntry(Type type, string xmlName)
        {
            _xmlNameToType.Add(xmlName, type);
            _typeToXmlName.Add(type, xmlName);
        }

        public void SetNamespaceRename(Type type, string @namespace)
        {
            _namespaceRenames[type.FullName] = @namespace;
            _namespaceRenames[@namespace + "." + type.Name] = type.Namespace;
        }

        /// <summary>
        /// Returns the namespace for the given type.
        /// Could be changed by <see cref="SetNamespaceRename"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetNamespace(Type type)
        {
            string fullTypeName = type.FullName;
            if (_namespaceRenames.ContainsKey(fullTypeName))
                return _namespaceRenames[fullTypeName];

            return type.Namespace;
        }

        /// <summary>
        /// Returns the namespace for the given type.
        /// Could be changed by <see cref="SetNamespaceRename"/>.
        /// </summary>
        /// <param name="fullTypeName">The CLR type name.</param>
        /// <returns></returns>
        public string GetNamespace(string fullTypeName)
        {
            string typeName;
            return GetNamespace(fullTypeName, out typeName);
        }

        public string GetNamespace(string fullTypeName, out string typeName)
        {
            int typeStart = fullTypeName.LastIndexOf(".");
            if (typeStart != -1)
                typeName = fullTypeName.Substring(typeStart + ".".Length);
            else
            {
                typeName = fullTypeName;
                return "";
            }

            if (_namespaceRenames.ContainsKey(fullTypeName))
                return _namespaceRenames[fullTypeName];

            return fullTypeName.Substring(0, typeStart);
        }

        public void AddSerializer(ContentTypeSerializer serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer");

            Type targetType = serializer.TargetType;
            string xmlTypeName = serializer.XmlTypeName;
            if (String.IsNullOrWhiteSpace(xmlTypeName))
                throw new ArgumentException("The ContentTypeSerializer \"{0}\" doesn't return a XmlTypeName.", serializer.XmlTypeName);

            if (this._contentSerializerMappings.ContainsValue(xmlTypeName))
            {
                throw new InvalidOperationException(
                    string.Format("Intermediate ContentTypeSerializer \"{0}\" conflicts with existing handler \"{1}\" for XmlTypeName \"{2}\".",
                        serializer.GetType().AssemblyQualifiedName,
                        this._contentSerializerMappings.First((x) => x.Value == xmlTypeName).Key.AssemblyQualifiedName,
                        xmlTypeName
                    )
                );
            }

            //ContentSerializer has precedence over type mappings that might have been dynamically generated.
            this._typeToXmlName[targetType] = xmlTypeName;
            this._xmlNameToType[xmlTypeName] = targetType;
            this._contentSerializerMappings.Add(targetType, xmlTypeName);
        }

        public bool TryGetType(string xmlName, out Type type)
        {
            return _xmlNameToType.TryGetValue(xmlName, out type);
        }

        public bool TryGetXmlName(Type type, out string xmlName, out bool withoutNamespace)
        {
            if (this._contentSerializerMappings.TryGetValue(type, out xmlName))
            {
                withoutNamespace = true;
                return true;
            }
            else
            {
                withoutNamespace = false;
                return _typeToXmlName.TryGetValue(type, out xmlName);
            }
        }

        public string TranslateXmlName(string xmlTypeName)
        {
            string typeName;
            return GetNamespace(xmlTypeName, out typeName) + "." + typeName;
        }
    }
}
