using ANX.Framework.Content.Pipeline.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    class XmlTypeNameReader
    {
        private XmlTypeNameContainer _container;
        private XmlReader _xmlReader;

        public XmlTypeNameReader(XmlTypeNameContainer container, XmlReader xmlReader)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            if (xmlReader == null)
                throw new ArgumentNullException("xmlReader");

            this._container = container;
            this._xmlReader = xmlReader;
        }

        public Type GetTypeFromXmlName(string xmlTypeName)
        {
            xmlTypeName = this.ExpandXmlTypeName(xmlTypeName);

            xmlTypeName = _container.TranslateXmlName(xmlTypeName);

            Type type;
            if (_container.TryGetType(xmlTypeName, out type))
                return type;

            if (xmlTypeName.EndsWith("[]"))
            {
                type = this.GetTypeFromXmlName(xmlTypeName.Substring(0, xmlTypeName.Length - "[]".Length)).MakeArrayType();
            }
            else
            {
                //Parse also works with non generic types, but not with arrays.
                type = ParseGenericArguments(xmlTypeName);
            }

            _container.AddEntry(type, xmlTypeName);

            return type;
        }

        private Type ParseGenericArguments(string xmlTypeName)
        {
            string baseName;
            var genericArguments = this.ExtractGenericArgumentList(xmlTypeName, out baseName).Select((x) => this.GetTypeFromXmlName(x)).ToArray();

            if (genericArguments.Length > 0)
            {
                baseName += "`" + genericArguments.Length;
                Type genericType = FindType(baseName);

                return genericType.MakeGenericType(genericArguments);
            }
            else
            {
                return FindType(baseName);
            }
        }

        /// <summary>
        /// Returns the type with the given clr type name.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private Type FindType(string typeName)
        {
            Type type = Type.GetType(typeName);
            if (type != null)
                return type;

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!AssemblyHelper.IsValidForPipeline(assembly.GetName()))
                    continue;

                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            throw new ArgumentException(string.Format("Cannot find type \"{0}\".", typeName));
        }

        private string[] ExtractGenericArgumentList(string xmlTypeName, out string genericTypeName)
        {
            List<string> genericArguments = new List<string>();
            int bracketsCount = 0;
            int argumentSeparatorPos = -1;

            genericTypeName = xmlTypeName;

            for (int i = 0, length = xmlTypeName.Length; i < length; i++)
            {
                if (xmlTypeName[i] == '[')
                {
                    if (bracketsCount == 0)
                    {
                        genericTypeName = xmlTypeName.Substring(0, i);
                        argumentSeparatorPos = i;
                    }

                    bracketsCount++;
                }
                else if (xmlTypeName[i] == ']')
                {
                    bracketsCount--;
                    if (bracketsCount == 0)
                    {
                        return genericArguments.ToArray();
                    }
                    else
                        throw new ArgumentException("Not matching number of brackets for type name: " + xmlTypeName);
                }
                else if (xmlTypeName[i] == ',' && bracketsCount == 1)
                {
                    genericArguments.Add(xmlTypeName.Substring(argumentSeparatorPos + 1, i - argumentSeparatorPos));
                }
            }

            return genericArguments.ToArray();
        }

        private string ExpandXmlTypeName(string xmlTypeName)
        {
            int abbreviationIndex = xmlTypeName.IndexOf(":");
            if (abbreviationIndex != -1)
            {
                string abbreviation = xmlTypeName.Substring(0, abbreviationIndex);
                string remainingTypeName = xmlTypeName.Substring(abbreviationIndex + ":".Length);

                return this._xmlReader.LookupNamespace(abbreviation) + "." + remainingTypeName;
            }

            return xmlTypeName;
        }
    }
}
