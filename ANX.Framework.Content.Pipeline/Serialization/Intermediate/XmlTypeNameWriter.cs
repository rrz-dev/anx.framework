using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    class XmlTypeNameWriter
    {
        private XmlTypeNameContainer _container;
        private Dictionary<string, string> _namespaceAbbreviations = new Dictionary<string, string>();
        private bool _shortenNamespaces;

        public ReadOnlyCollection<KeyValuePair<string, string>> NamespaceAbbreviations
        {
            get { return new ReadOnlyCollection<KeyValuePair<string, string>>(_namespaceAbbreviations.ToList()); }
        }

        public XmlTypeNameWriter(XmlTypeNameContainer container, bool shortenNamespaces)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this._container = container;
            this._shortenNamespaces = shortenNamespaces;
        }

        private static Type GetDeclaringType(Type type)
        {
            Type declaringType = type.DeclaringType;
            if (declaringType == null)
                return null;

            //If the declaring type isn't defined with all generic arguments, use the arguments of the given type for it.
            if (declaringType.IsGenericTypeDefinition)
            {
                int length = declaringType.GetGenericArguments().Length;
                Type[] genericArgumentsForDeclaringType = new Type[length];
                Array.Copy(type.GetGenericArguments(), genericArgumentsForDeclaringType, length);

                return declaringType.MakeGenericType(genericArgumentsForDeclaringType);
            }
            else
                return declaringType;
        }

        public string getXmlTypeName(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsGenericTypeDefinition)
                throw new ArgumentException("Can't convert generic type defintions to a XML representation.");

            if (type.IsArray && type.GetArrayRank() > 1)
                throw new ArgumentException("Can't convert multidimensional arrays to a XML representation.");

            if (type.IsPointer)
                throw new ArgumentException("Can't convert pointer types to a XMl representation.");

            if (type.IsByRef)
                throw new ArgumentException("Can't convert ref argument types to a XMl representation.");

            string xmlTypeName;
            bool withoutNamespace;
            if (_container.TryGetXmlName(type, out xmlTypeName, out withoutNamespace))
            {
                if (!withoutNamespace)
                    return TranslateNamespace(xmlTypeName, _container.GetNamespace(type));
                return xmlTypeName;
            }
            else
            {
                bool isArray;
                string typeName = this.ResolveTypeName(type, out isArray);
                //In case of an array, we don't want to write the namespace of the array down, we only want the namespace of the element.
                if (!isArray)
                    xmlTypeName = TranslateNamespace(typeName, _container.GetNamespace(type));
                else
                    xmlTypeName = typeName;

                _container.AddEntry(type, typeName);

                return xmlTypeName;
            }
        }

        private string TranslateNamespace(string typeName, string @namespace)
        {
            string abbreviation;
            string remainingNamespace;
            if (GetNamespaceAbbreviation(@namespace, out abbreviation, out remainingNamespace))
            {
                return abbreviation + ":" + remainingNamespace + typeName;
            }
            else
            {
                return remainingNamespace + typeName;
            }
        }

        private bool GetNamespaceAbbreviation(string @namespace, out string abbreviation, out string remainingNamespace)
        {
            if (!_shortenNamespaces)
            {
                abbreviation = "";
                remainingNamespace = @namespace;
                return false;
            }

            remainingNamespace = "";

            if (_namespaceAbbreviations.TryGetValue(@namespace, out abbreviation))
                return true;

            var originalNamespace = @namespace;
            //create a new namespace abbreviation.
            while (@namespace.Length > 0)
            {
                var lastPartIndex = @namespace.LastIndexOf(".");
                if (lastPartIndex == -1)
                {
                    abbreviation = "";
                    remainingNamespace = originalNamespace + ".";
                    return false;
                }
                else
                {
                    abbreviation = @namespace.Substring(lastPartIndex + ".".Length);
                    if (!_namespaceAbbreviations.ContainsValue(abbreviation))
                    {
                        _namespaceAbbreviations.Add(@namespace, abbreviation);
                        return true;
                    }
                    else
                    {
                        remainingNamespace += abbreviation + ".";
                    }

                    @namespace = @namespace.Substring(0, lastPartIndex);
                }
            }

            return false;
        }

        private string ResolveTypeName(Type type)
        {
            bool isArray;
            return ResolveTypeName(type, out isArray);
        }

        private string ResolveTypeName(Type type, out bool isArray)
        {
            isArray = false;
            //remove the generic marker from the type name. We represent them with brackets and resolved type parameters.
            Type declaringType = null;
            if (type.IsNested)
                declaringType = GetDeclaringType(type);

            string typeName = type.Name;
            if (type.IsGenericType)
            {
                //TODO: remove type arguments from declaring classes.
                var genericMarkerPos = typeName.LastIndexOf('`');
                typeName = typeName.Substring(0, genericMarkerPos);
                typeName += "[";
                List<string> typeParameterNames = new List<string>();
                var genericArguments = type.GetGenericArguments();
                if (type.IsNested)
                {
                    //Ignore the generic arguments of the declaring types, we will write them on the declaring type.
                    genericArguments = genericArguments.Skip(declaringType.GetGenericArguments().Length).ToArray();
                }

                foreach (var typeParameter in genericArguments)
                {
                    typeParameterNames.Add(this.getXmlTypeName(typeParameter));
                }
                typeName += string.Join(",", typeParameterNames) + "]";
            }
            else if (type.IsArray)
            {
                typeName = this.getXmlTypeName(type.GetElementType()) + "[]";
                isArray = true;
            }
            
            if (type.IsNested)
            {
                return this.ResolveTypeName(declaringType) + "+" + typeName;
            }
            else
            {
                return typeName;
            }
        }
    }
}
