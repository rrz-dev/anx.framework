#region Using Statements
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.Content.Pipeline.Serialization.Intermediate.SystemTypeSerializers;
using ANX.Framework.NonXNA.Development;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Intermediate
{
    /// <summary>
    /// Provides methods for reading and writing intermediate XML format.
    /// </summary>
    [Developer("KorsarNek")]
    [TestState(TestStateAttribute.TestState.Tested)]
    [PercentageComplete(100)]
    public class IntermediateSerializer
    {
        private Dictionary<Type, Type> genericSerializerTypeHandler = new Dictionary<Type, Type>();
        private Dictionary<Type, ContentTypeSerializer> serializerInstances = new Dictionary<Type, ContentTypeSerializer>();
        private XmlTypeNameContainer xmlTypeNameContainer = new XmlTypeNameContainer();
        private string contentTagName = "AnxContent";

        //Used for the option to rename the anx namespaces to xna.
        private const string XnaFrameworkNamespace = "Microsoft.Xna.Framework";
        private const string AnxFrameworkNamespace = "ANX.Framework";

        private static readonly ContentSerializerAttribute assetAttribute = new ContentSerializerAttribute() { ElementName = "Asset" };

        private static IntermediateSerializer singleton;

        internal static IntermediateSerializer Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new IntermediateSerializer(true, false);
                
                return singleton;
            }
        }

        /// <summary>
        /// Creates a new intermediate serialzier with the option to use xna namespaces and search <see cref="ContentTypeSerializer"/>s.
        /// </summary>
        /// <param name="searchSerializers"></param>
        /// <param name="changeToXnaNamespaces"></param>
        /// <exception cref="System.ArgumentException">Will be thrown if <see cref="ContentTypeSerializer"/>s are loaded that conform to the requirements.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for a <see cref="ContentTypeSerializer"/> can be found.</exception>
#if XNAEXT
        public IntermediateSerializer(bool searchSerializers = true, bool changeToXnaNamespaces = false)
#else
        internal IntermediateSerializer(bool searchSerializers = true, bool changeToXnaNamespaces = false)
#endif
        {
            if (searchSerializers)
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!AssemblyHelper.IsValidForPipeline(assembly.GetName()))
                        continue;

                    this.AddContentTypeSerializerAssembly(assembly);
                }
            }

            if (changeToXnaNamespaces)
            {
                this.ChangeToXnaNamespaces();
            }
        }

        /// <summary>
        /// The name of the main element around the &lt;Assset&gt; element.
        /// If the value is empty or null, no element is used.
        /// </summary>
        public string ContentElementName
        {
            get { return contentTagName; }
            set { contentTagName = value; }
        }

        private void AddContentTypeSerializerAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                ContentTypeSerializerAttribute[] value = (ContentTypeSerializerAttribute[])type.GetCustomAttributes(typeof(ContentTypeSerializerAttribute), true);
                if (value.Length > 0)
                {
                    if (!typeof(ContentTypeSerializer).IsAssignableFrom(type))
                        continue;

                    AddTypeSerializer(type);
                }
            }
        }

        private static Type ValidateGenericSerializer(Type type)
        {
            //The only instance when we can know the type of T is when T is also used as the Parameter for the generic ContentTypeSerializer<T>.
            Type baseType = type.BaseType;
            while (!baseType.IsGenericType || baseType.GetGenericTypeDefinition() != typeof(ContentTypeSerializer<>))
            {
                baseType = baseType.BaseType;
                if (baseType == null)
                {
                    throw new ArgumentException(string.Format("Intermediate {0} {1} is an invalid generic. It should be in the form My{0}<T> : {0}<MyType<T>> .", typeof(ContentTypeSerializer<>).Name, type));
                }
            }

            //The generic argument must be itself generic, that way we can distinguish which of the generic handlers to use.
            //MySerializer<T> : ContentTypeSerializer<MyGeneric<T>> When ever we meet a version of MyGeneric, we can find the responsible serializer.
            Type genericArgument = baseType.GetGenericArguments()[0];
            if (!genericArgument.IsGenericType)
            {
                throw new ArgumentException(string.Format("Intermediate {0} {1} is an invalid generic. It should be in the form My{0}<T> : {0}<MyType<T>> .", typeof(ContentTypeSerializer<>).Name, type));
            }

            if (genericArgument.IsByRef || genericArgument.IsPointer)
            {
                throw new ArgumentException(string.Format("Cannot serialize type {0}. Pointers and references are not supported.", type));
            }

            if (genericArgument.IsSubclassOf(typeof(Delegate)))
            {
                throw new ArgumentException(string.Format("Cannot serialize type {0}. Delegates are not supported.", type));
            }

            if (!type.GetGenericArguments().SequenceEqual(genericArgument.GetGenericArguments()))
            {
                throw new ArgumentException(string.Format("Intermediate {0} {1} is an invalid generic. It should be in the form My{0}<T> : {0}<MyType<T>> .", typeof(ContentTypeSerializer<>).Name, type));
            }

            return genericArgument;
        }

        /// <summary>
        /// Adds a <see cref="ContentTypeSerializer"/> to this intermediate serializer instance.
        /// </summary>
        /// <param name="type"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor can be found.</exception>
        public void AddTypeSerializer(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsByRef || type.IsPointer)
                throw new ArgumentException(string.Format("Intermediate serializer type {0}. Pointers and references are not supported.", type));

            if (!typeof(ContentTypeSerializer).IsAssignableFrom(type))
                throw new ArgumentException(string.Format("\"{0}\" must inherit from {1}.", type.FullName, typeof(ContentTypeSerializer).Name));

            //If it contains open generic parameters like MySerializer<T>. We can't create the instance until we know T.
            if (type.IsGenericTypeDefinition)
            {
                Type genericArgument = ValidateGenericSerializer(type);

                Type genericTypeDefinition = genericArgument.GetGenericTypeDefinition();
                if (genericSerializerTypeHandler.ContainsKey(genericTypeDefinition))
                {
                    throw new InvalidOperationException(string.Format("Intermediate {0} \"{1}\" conflicts with existing handler \"{2}\" for {3}.",
                        typeof(ContentTypeSerializer<>).Name,
                        type.AssemblyQualifiedName,
                        genericSerializerTypeHandler[genericTypeDefinition].AssemblyQualifiedName,
                        genericTypeDefinition));
                }
                genericSerializerTypeHandler.Add(genericTypeDefinition, type);
            }
            else
            {
                try
                {
                    AddTypeSerializer((ContentTypeSerializer)Activator.CreateInstance(type));
                }
                catch (MissingMethodException exc)
                {
                    throw new ArgumentException(string.Format("\"{0}\" doesn't have a public parameterless constructor.", type.AssemblyQualifiedName), exc);
                }
            }
        }

        private void AddTypeSerializer(ContentTypeSerializer serializer)
        {
            if (serializerInstances.ContainsKey(serializer.TargetType))
                throw new InvalidOperationException(string.Format("Intermediate {0} \"{1}\" conflicts with existing handler \"{2}\" for {3}.", typeof(ContentTypeSerializer).Name, serializer.GetType().AssemblyQualifiedName, this.serializerInstances[serializer.TargetType].GetType().AssemblyQualifiedName, serializer.TargetType));

            if (serializer.XmlTypeName != null)
            {
                this.xmlTypeNameContainer.AddSerializer(serializer);
            }
            serializerInstances.Add(serializer.TargetType, serializer);

            serializer.Initialize(this);
        }

        /// <summary>
        /// Serializes an object into an intermediate XML file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="output">The output XML stream.</param>
        /// <param name="value">The object to be serialized.</param>
        /// <param name="referenceRelocationPath">Final name of the output file, used to relative encode external reference filenames.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be thrown if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        public virtual void SerializeObject<T>(XmlWriter output, T value, string referenceRelocationPath)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            bool expectsContentTag = !string.IsNullOrWhiteSpace(ContentElementName);

            IntermediateWriter intermediateWriter = new IntermediateWriter(this, new IntermediateXmlWriter(output), referenceRelocationPath, xmlTypeNameContainer, expectsContentTag);

            if (expectsContentTag)
            {
                output.WriteStartElement(ContentElementName);
            }

            intermediateWriter.WriteObject<object>(value, assetAttribute);
            intermediateWriter.WriteSharedResources();
            intermediateWriter.WriteExternalReferences();

            if (expectsContentTag)
            {
                intermediateWriter.WriteUsedNamespaces();
                intermediateWriter.ComposeXml();
                output.WriteEndElement();
            }
            else
            {
                intermediateWriter.ComposeXml();
            }
        }

        /// <summary>
        /// Serializes an object into an intermediate ANX XML file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="output">The output XML stream.</param>
        /// <param name="value">The object to be serialized.</param>
        /// <param name="referenceRelocationPath">Final name of the output file, used to relative encode external reference filenames.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be thrown if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        public static void Serialize<T>(XmlWriter output, T value, string referenceRelocationPath)
        {
            Singleton.SerializeObject<T>(output, value, referenceRelocationPath);
        }

        /// <summary>
        /// Deserializes an intermediate XML file into a managed object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Intermediate XML file.</param>
        /// <param name="referenceRelocationPath">Final name of the output file used to relative encode external reference filenames.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        public virtual T DeserializeObject<T>(XmlReader input, string referenceRelocationPath)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            bool expectsContentTag = !string.IsNullOrWhiteSpace(ContentElementName);
            if (expectsContentTag)
            {
                if (!input.CheckForElement(ContentElementName))
                    throw ExceptionHelper.CreateInvalidContentException(input, referenceRelocationPath, null, string.Format("XML is not in the intermediate format. Missing {0} root element.", this.ContentElementName));
            }

            input.ReadStartElement();

            IntermediateReader intermediateReader = new IntermediateReader(this, new IntermediateXmlReader(input), referenceRelocationPath, xmlTypeNameContainer);

            T resultObject = intermediateReader.ReadObject<T>(assetAttribute);

            intermediateReader.ReadSharedResources();
            intermediateReader.ReadExternalReferences();

            if (expectsContentTag)
            {
                input.ReadEndElement();
            }

            return resultObject;
        }

        /// <summary>
        /// Deserializes an intermediate ANX XML file into a managed object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">Intermediate XML file.</param>
        /// <param name="referenceRelocationPath">Final name of the output file used to relative encode external reference filenames.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        public static T Deserialize<T>(XmlReader input, string referenceRelocationPath)
        {
            return Singleton.DeserializeObject<T>(input, referenceRelocationPath);
        }

        /// <summary>
        /// Retrieves the worker serializer for a specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.RankException">Will be triggered if a multidimensional array is passed.</exception>
        /// <exception cref="System.MissingMethodException">Will be thrown if no parameterless constructor for the corresponding <see cref="ContentTypeSerializer"/> can be found.</exception>
        public virtual ContentTypeSerializer GetTypeSerializer(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsByRef || type.IsPointer)
                throw new ArgumentException(string.Format("Cannot serialize type {0}. Pointers and references are not supported.", type));

            if (type.ContainsGenericParameters)
                throw new ArgumentException(string.Format("Type {0} cannot be serialized because not all the generic type parameters have been specified.", type));

            if (type.IsSubclassOf(typeof(Delegate)))
                throw new ArgumentException(string.Format("Cannot serialize type {0}. Delegates are not supported.", type));

            ContentTypeSerializer contentTypeSerializer = null;
            if (serializerInstances.TryGetValue(type, out contentTypeSerializer))
            {
                return contentTypeSerializer;
            }

            Type genericTypeHandler; 
            if (type.IsGenericType && genericSerializerTypeHandler.TryGetValue(type.GetGenericTypeDefinition(), out genericTypeHandler))
            {
                contentTypeSerializer = BuildGenericSerializerVariant(genericTypeHandler, type);
            }
            else if (type.IsArray)
            {
                if (type.GetArrayRank() != 1)
                    throw new RankException("Can't serialize multidimensional arrays.");

                contentTypeSerializer = (ContentTypeSerializer)Activator.CreateInstance(typeof(ArraySerializer<>).MakeGenericType(type.GetElementType()));
            }
            else if (type.IsEnum)
            {
                contentTypeSerializer = new EnumSerializer(type);
            }
            else
            {
                contentTypeSerializer = new ReflectiveSerializer(type);
            }

            AddTypeSerializer(contentTypeSerializer);
            return contentTypeSerializer;
        }

        private ContentTypeSerializer BuildGenericSerializerVariant(Type genericTypeHandler, Type targetType)
        {
            Type[] targetArguments = targetType.GetGenericArguments();
            Type genericHandlerDefinition = genericTypeHandler.GetGenericTypeDefinition();

            return (ContentTypeSerializer)Activator.CreateInstance(genericHandlerDefinition.MakeGenericType(targetArguments));
        }

        /// <summary>
        /// Changes the ContentElementName to XnaContent and changes the namespaces for the ANX classes to match the name for the XNA classes.
        /// </summary>
        /// <exception cref="System.Reflection.ReflectionTypeLoadException"></exception>
        private void ChangeToXnaNamespaces()
        {
            foreach (Type type in Assembly.GetAssembly(typeof(Game)).GetTypes())
            {
                if (type.IsPublic && type.Namespace.StartsWith(AnxFrameworkNamespace) && !type.Namespace.Contains("NonXNA"))
                {
                    this.xmlTypeNameContainer.SetNamespaceRename(type, XnaFrameworkNamespace + type.Namespace.Substring(AnxFrameworkNamespace.Length));
                }
            }

            foreach (Type type in Assembly.GetAssembly(typeof(IntermediateSerializer)).GetTypes())
            {
                if (type.IsPublic && type.Namespace.StartsWith(AnxFrameworkNamespace) && !type.Namespace.Contains("NonXNA"))
                {
                    this.xmlTypeNameContainer.SetNamespaceRename(type, XnaFrameworkNamespace + type.Namespace.Substring(AnxFrameworkNamespace.Length));
                }
            }

            ContentElementName = "XnaContent";
        }
    }
}
