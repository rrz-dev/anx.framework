using ANX.Framework.Build;
using ANX.Framework.VisualStudio.Nodes;
using ANX.Framework.VisualStudio.PropertyDescriptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Converters
{
    /// <summary>
    /// A TypeConverter that gets used as a proxy between the real converter and visual studio.
    /// </summary>
    public class WrappedConverter : StringConverter
    {
        private class Proxy : MarshalByRefObject, IProxy
        {
            TypeConverter innerConverter;
            Type propertyType;

            public override object InitializeLifetimeService()
            {
                return null;
            }

            public void Initialize(Type converterType, Type propertyType)
            {
                if (converterType == null)
                    throw new ArgumentNullException("converterType");

                if (propertyType == null)
                    throw new ArgumentNullException("propertyType");

                this.propertyType = propertyType;
                if (propertyType.IsEnum)
                {
                    this.innerConverter = new EnumConverter(propertyType);
                }
                else
                {
                    var parameterTypes = new Type[] { typeof(Type) };

                    ConstructorInfo constructor = converterType.GetConstructor(parameterTypes);
                    if (constructor != null)
                    {
                        this.innerConverter = (TypeConverter)TypeDescriptor.CreateInstance(null, converterType, parameterTypes, new object[] { propertyType });
                    }
                    else
                    {
                        this.innerConverter = (TypeConverter)TypeDescriptor.CreateInstance(null, converterType, null, null);
                    }

                    if (this.innerConverter == null)
                    {
                        throw new InvalidOperationException(string.Format("Unable to create TypeConverter for {0}", converterType.FullName));
                    }
                }
            }

            public void Initialize(TypeConverter converter)
            {
                if (converter == null)
                    throw new ArgumentNullException("converter");

                this.innerConverter = converter;
            }

            public void Initialize(string converterType, string propertyType)
            {
                bool buildAppDomain = AppDomain.CurrentDomain.IsBuildAppDomain();

                Initialize(Type.GetType(converterType, true), Type.GetType(propertyType, true));
            }

            public string ConverterTypeName
            {
                get { return innerConverter.GetType().AssemblyQualifiedName; }
            }

            public bool CanConvertFrom(IProxy context, Type sourceType)
            {
                return innerConverter.CanConvertFrom(new TypeDescriptorContextWrapper(context), sourceType);
            }

            public bool CanConvertTo(IProxy context, Type destinationType)
            {
                return innerConverter.CanConvertTo(new TypeDescriptorContextWrapper(context), destinationType);
            }

            public object ConvertTo(IProxy context, CultureInfo culture, object value, Type destinationType)
            {
                TypeDescriptorContextWrapper wrappedContext = null;
                if (context != null)
                {
                    wrappedContext = new TypeDescriptorContextWrapper(context);
                }

                if (value is string && propertyType != destinationType)
                {
                    value = innerConverter.ConvertFromString((string)value);
                }

                return innerConverter.ConvertTo(wrappedContext, culture, value, destinationType);
            }

            public object ConvertFrom(IProxy context, CultureInfo culture, object value, bool convertToString)
            {
                TypeDescriptorContextWrapper wrappedContext = null;
                if (context != null)
                {
                    wrappedContext = new TypeDescriptorContextWrapper(context);
                }

                object result = innerConverter.ConvertFrom(wrappedContext, culture, value);

                if (convertToString)
                {
                    result = innerConverter.ConvertToString(wrappedContext, culture, result);
                }
                return result;
            }

            public string CreateInstance(IProxy context, Dictionary<object, object> propertyValues)
            {
                var wrappedContext = new TypeDescriptorContextWrapper(context);
                //I didn't find a way to get the current value in a general way, so I just hope passing null won't cause problems.
                var properties = innerConverter.GetProperties(wrappedContext, null, wrappedContext.PropertyDescriptor.Attributes.Cast<Attribute>().ToArray());
                if (propertyValues.Count != properties.Count)
                    throw new ArgumentException(string.Format("The amount of returned properties for the converter {0} isn't equal to the properties used to create a new instance.", this.ConverterTypeName));

                Dictionary<object, object> convertedProperties = new Dictionary<object,object>();
                //Convert the propertyValues to their correct types.
                var propDescriptorsEnumerator = properties.GetEnumerator();
                var propValuesEnumerator = propertyValues.GetEnumerator();
                while (propDescriptorsEnumerator.MoveNext() && propValuesEnumerator.MoveNext())
                {
                    var descriptor = (PropertyDescriptor)propDescriptorsEnumerator.Current;
                    var propPair = propValuesEnumerator.Current;

                    //The value that just been changed is probably in the original value and not converted to a string yet.
                    //TODO: check why that happens, it could cause problems if the subtype is defined in one of the referenced assemblies that are referenced by the project.
                    var value = propPair.Value;
                    if (value != null && value.GetType() != descriptor.PropertyType)
                    {
                        value = descriptor.Converter.ConvertFrom(wrappedContext, CultureInfo.CurrentUICulture, propPair.Value);
                    }
                    convertedProperties.Add(propPair.Key, value);
                }

                return innerConverter.ConvertToString(innerConverter.CreateInstance(wrappedContext, convertedProperties));
            }

            public bool GetCreateInstanceSupported(IProxy context)
            {
                return innerConverter.GetCreateInstanceSupported(new TypeDescriptorContextWrapper(context));
            }

            public IProxy[] GetProperties(IProxy context, string value, Attribute[] attributes)
            {
                var wrappedContext = new TypeDescriptorContextWrapper(context);

                var descriptors = innerConverter.GetProperties(wrappedContext, this.innerConverter.ConvertFromString(wrappedContext, CultureInfo.CurrentUICulture, value), attributes);

                IProxy[] result = new IProxy[descriptors.Count];
                for (int i = 0; i < result.Length; i++)
                {
                    var proxy = new Proxy();
                    proxy.Initialize(descriptors[i].Converter.GetType().AssemblyQualifiedName, descriptors[i].PropertyType.AssemblyQualifiedName);

                    result[i] = ParameterDescriptor.CreateProxy(descriptors[i], proxy, this);
                }

                return result;
            }

            public bool GetPropertiesSupported(IProxy context)
            {
                return innerConverter.GetPropertiesSupported(new TypeDescriptorContextWrapper(context));
            }

            public ICollection GetStandardValues(IProxy context)
            {
                List<object> standardValues = new List<object>();
                foreach (var obj in innerConverter.GetStandardValues(new TypeDescriptorContextWrapper(context)))
                {
                    standardValues.Add(obj);
                }
                return standardValues;
            }

            public bool GetStandardValuesExclusive(IProxy context)
            {
                return innerConverter.GetStandardValuesExclusive(new TypeDescriptorContextWrapper(context));
            }

            public bool GetStandardValuesSupported(IProxy context)
            {
                return innerConverter.GetStandardValuesSupported(new TypeDescriptorContextWrapper(context));
            }

            public bool IsValid(IProxy context, string value)
            {
                var wrappedContext = new TypeDescriptorContextWrapper(context);

                return innerConverter.IsValid(wrappedContext, this.innerConverter.ConvertFromString(wrappedContext, CultureInfo.CurrentUICulture, value));
            }

            public object OriginalInstance
            {
                get { return innerConverter; }
            }

            public Type WrapperType
            {
                get { return typeof(WrappedConverter); }
            }
        }

        public static IProxy CreateProxy(Type converterType, Type propertyType)
        {
            if (!AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException("Proxies can only be created by the build appDomain.");

            var proxy = new Proxy();
            proxy.Initialize(converterType, propertyType);

            return proxy;
        }

        private Proxy proxyInstance;

        public IProxy ProxyInstance
        {
            get { return proxyInstance; }
        }

        public WrappedConverter(IProxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            if (proxy.GetType() != typeof(Proxy))
                throw new ArgumentException(string.Format("The given proxy must be of type {0}.", typeof(Proxy).FullName));

            this.proxyInstance = (Proxy)proxy;
        }

        public WrappedConverter(string converterType, string propertyType)
        {
            if (AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException("WrappedConverter constructor with string parameters can only be called from the main appDomain.");

            ContentProjectNode node = Utilities.GetCurrentProject();

            using (var domainLock = node.BuildAppDomain.Aquire())
            {
                proxyInstance = domainLock.CreateInstanceAndUnwrap<Proxy>();
            }
            proxyInstance.Initialize(converterType, propertyType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return proxyInstance.CanConvertFrom(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance), sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return proxyInstance.CanConvertTo(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance), destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
                Debugger.Break();

            IProxy wrappedContext = null;
            if (context != null)
            {
                wrappedContext = TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance);
            }

            //Because we are outputting the values always as string, value can only be string.
            return proxyInstance.ConvertTo(wrappedContext, culture, value, destinationType);
        }

        /// <summary>
        /// Converts the given object to the type of the converter. Is restricted to strings and should only be used to interact with the Visual Studio shell.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            IProxy contextProxy = null;
            if (context != null)
            {
                if (context is TypeDescriptorContextWrapper)
                {
                    contextProxy = ((TypeDescriptorContextWrapper)context).ProxyInstance;
                }
                else
                {
                    contextProxy = TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance);
                }
            }

            return proxyInstance.ConvertFrom(contextProxy, culture, value, !AppDomain.CurrentDomain.IsBuildAppDomain());
        }

        public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
        {
            Dictionary<object, object> dictionary = new Dictionary<object, object>();
            foreach (DictionaryEntry value in propertyValues)
            {
                dictionary.Add(value.Key, value.Value);
            }

            return proxyInstance.CreateInstance(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance), dictionary);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return proxyInstance.GetCreateInstanceSupported(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance));
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            List<Attribute> serializables = new List<Attribute>();
            foreach (var attribute in attributes)
            {
                if (attribute != null && attribute.GetType().IsSerializable)
                {
                    serializables.Add(attribute);
                }
            }

            //Get the names of the properties to keep the sorting.
            List<string> names = new List<string>();
            List<ParameterDescriptor> parameters = new List<ParameterDescriptor>();

            foreach (var proxy in proxyInstance.GetProperties(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance), this.ConvertToString(value), serializables.ToArray()))
            {
                var parameter = new ParameterDescriptor(proxy);

                names.Add(parameter.Name);
                parameters.Add(parameter);
            }

            var result = new PropertyDescriptorCollection(parameters.ToArray());
            //TODO: support using a IComparer.
            return result.Sort(names.ToArray());
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return proxyInstance.GetPropertiesSupported(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance));
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(proxyInstance.GetStandardValues(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance)));
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return proxyInstance.GetStandardValuesExclusive(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance));
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return proxyInstance.GetStandardValuesSupported(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance));
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return proxyInstance.IsValid(TypeDescriptorContextWrapper.CreateProxy(context, this.ProxyInstance), this.ConvertToString(value));
        }
    }
}
