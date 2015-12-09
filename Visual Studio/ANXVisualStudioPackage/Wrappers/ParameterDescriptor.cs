using ANX.Framework.VisualStudio.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.PropertyDescriptors
{
    public class ParameterDescriptor : PropertyDescriptor
    {
        /// <summary>
        /// The proxy should only run on the build appDomain, because we are wrapping the custom PropertyDescriptors of the referenced assemblies.
        /// </summary>
        private class Proxy : MarshalByRefObject, IProxy
        {
            PropertyDescriptor innerDescriptor;
            //Inside a proxy, we don't need to use WrappedConverters because we work on the same domain as them.
            IProxy converterProxy;
            IProxy componentConverterProxy;
            TypeConverter converter;
            TypeConverter componentConverter;

            MethodInfo onValueChanged;
            PropertyInfo nameHashCode;
            MethodInfo createAttributeCollection;
            MethodInfo fillAttributes;
            PropertyInfo attributeArray;
            MethodInfo getInvocationTarget;

            public void Initialize(PropertyDescriptor descriptor, IProxy converterProxy, IProxy componentConverterProxy)
            {
                this.innerDescriptor = descriptor;
                this.converterProxy = converterProxy;
                this.converter = new WrappedConverter(this.converterProxy);
                this.componentConverterProxy = componentConverterProxy;
                this.componentConverter = new WrappedConverter(this.componentConverterProxy);

                Type type = descriptor.GetType();
                onValueChanged = type.GetMethod("OnValueChanged", BindingFlags.Instance | BindingFlags.NonPublic, null, new [] { typeof(object), typeof(EventArgs) }, null);
                nameHashCode = type.GetProperty("NameHashCode", BindingFlags.Instance | BindingFlags.NonPublic, null, typeof(int), Type.EmptyTypes, null);
                createAttributeCollection = type.GetMethod("CreateAttributeCollection", BindingFlags.Instance | BindingFlags.NonPublic);
                fillAttributes = type.GetMethod("FillAttributes", BindingFlags.Instance | BindingFlags.NonPublic, null, new [] { typeof(IList) }, null);
                attributeArray = type.GetProperty("AttributeArray", BindingFlags.Instance | BindingFlags.NonPublic, null, typeof(Attribute[]), Type.EmptyTypes, null);
                getInvocationTarget = type.GetMethod("GetInvocationTarget", BindingFlags.Instance | BindingFlags.NonPublic, null, new [] { typeof(Type), typeof(object) }, null);
            }

            public override object InitializeLifetimeService()
            {
                return null;
            }

            public IProxy ConverterProxy
            {
                get { return converterProxy; }
            }

            public IProxy ComponentConverterProxy
            {
                get { return componentConverterProxy; }
            }

            public bool CanResetValue(string component)
            {
                return innerDescriptor.CanResetValue(componentConverter.ConvertFromString(component));
            }

            public string GetValue(string component)
            {
                return converter.ConvertToString(innerDescriptor.GetValue(componentConverter.ConvertFromString(component)));
            }

            public bool IsReadOnly
            {
                get { return innerDescriptor.IsReadOnly; }
            }

            public void ResetValue(string component)
            {
                innerDescriptor.ResetValue(componentConverter.ConvertFromString(component));
            }

            public void SetValue(string component, string value)
            {
                innerDescriptor.SetValue(componentConverter.ConvertFromString(component), converter.ConvertFromString(value));
            }

            public bool ShouldSerializeValue(string component)
            {
                return innerDescriptor.ShouldSerializeValue(componentConverter.ConvertFromString(component));
            }

            public void AddValueChanged(string component, EventHandler handler)
            {
                innerDescriptor.AddValueChanged(componentConverter.ConvertFromString(component), handler);
            }

            public string Category
            {
                get
                {
                    return innerDescriptor.Category;
                }
            }

            public TypeConverter Converter
            {
                get
                {
                    return converter;
                }
            }

            public string Description
            {
                get
                {
                    return innerDescriptor.Description;
                }
            }

            public bool DesignTimeOnly
            {
                get
                {
                    return innerDescriptor.DesignTimeOnly;
                }
            }

            public string DisplayName
            {
                get
                {
                    return innerDescriptor.DisplayName;
                }
            }

            public bool IsBrowsable
            {
                get
                {
                    return innerDescriptor.IsBrowsable;
                }
            }

            public bool IsLocalizable
            {
                get
                {
                    return innerDescriptor.IsLocalizable;
                }
            }

            public string Name
            {
                get
                {
                    return innerDescriptor.Name;
                }
            }

            public bool SupportsChangeEvents
            {
                get
                {
                    return innerDescriptor.SupportsChangeEvents;
                }
            }

            public void RemoveValueChanged(string component, EventHandler handler)
            {
                innerDescriptor.RemoveValueChanged(componentConverter.ConvertFromString(component), handler);
            }

            public int NameHashCode
            {
                get
                {
                    return (int)nameHashCode.GetValue(innerDescriptor, null);
                }
            }

            public void OnValueChanged(string component)
            {
                onValueChanged.Invoke(innerDescriptor, new object[] { componentConverter.ConvertFromString(component), EventArgs.Empty });
            }

            public string GetInvocationTarget(Type type, string instance)
            {
                return this.converter.ConvertToString(getInvocationTarget.Invoke(innerDescriptor, new object[] { type, converter.ConvertFromString(instance) }));
            }

            public object GetEditor(Type editorBaseType)
            {
                var editor = innerDescriptor.GetEditor(editorBaseType);
                if (editor == null || !(editor is UITypeEditor))
                    return null;

                //Create an outgoing proxy for the custom editor.
                return UITypeEditorWrapper.CreateProxy((UITypeEditor)editor, converterProxy);
            }

            public IProxy[] GetChildProperties(string instance, Attribute[] filter)
            {
                var descriptors = innerDescriptor.GetChildProperties(converter.ConvertFromString(instance), filter);

                List<IProxy> result = new List<IProxy>();
                foreach (PropertyDescriptor descriptor in descriptors)
                {
                    result.Add(ParameterDescriptor.CreateProxy(descriptor, WrappedConverter.CreateProxy(descriptor.Converter.GetType(), descriptor.PropertyType), this.ConverterProxy));
                }
                return result.ToArray();
            }

            public void FillAttributes(List<Attribute> attributeList)
            {
                fillAttributes.Invoke(innerDescriptor, new object[] { attributeList });
            }

            public List<Attribute> CreateAttributeCollection()
            {
                return FilterAttributes((AttributeCollection)createAttributeCollection.Invoke(innerDescriptor, new object[0]));
            }

            public Attribute[] Attributes
            {
                get
                {
                    return FilterAttributes(innerDescriptor.Attributes).ToArray();
                }
            }

            public Attribute[] AttributeArray
            {
                get
                {
                    return FilterAttributes((Attribute[])attributeArray.GetValue(innerDescriptor)).ToArray();
                }
                set
                {
                    attributeArray.SetValue(innerDescriptor, value);
                }
            }

            public object OriginalInstance
            {
                get { return innerDescriptor; }
            }

            public Type WrapperType
            {
                get { return typeof(ParameterDescriptor); }
            }
        }

        private static List<Attribute> FilterAttributes(IEnumerable attributes)
        {
            List<Attribute> result = new List<Attribute>();
            if (attributes != null)
            {
                foreach (var attribute in attributes)
                {
                    if (attribute is Attribute && attribute.GetType().IsSerializable)
                    {
                        result.Add((Attribute)attribute);
                    }
                }
            }

            return result;
        }

        Proxy proxy;
        WrappedConverter converter;
        WrappedConverter componentConverter;

        //This method can be called inside the visual studi appDomain for the contentProcessorParameterDescriptor or a proxy to this wrapper or 
        //it can be called from the build appDomain to wrap a custom PropertyDescriptor.
        public static IProxy CreateProxy(PropertyDescriptor descriptor, IProxy converterProxy, IProxy componentConverterProxy)
        {
            if (descriptor == null)
                throw new ArgumentNullException("descriptor");

            if (converterProxy == null)
                throw new ArgumentNullException("converterProxy");

            if (componentConverterProxy == null)
                throw new ArgumentNullException("componentConverterProxy");

            Proxy proxy = new Proxy();
            proxy.Initialize(descriptor, converterProxy, componentConverterProxy);

            return proxy;
        }

        public ParameterDescriptor(IProxy proxy)
            : base(((Proxy)proxy).Name, ((Proxy)proxy).Attributes)
        {
            this.proxy = (Proxy)proxy;

            this.converter = new WrappedConverter(this.proxy.ConverterProxy);
            this.componentConverter = new WrappedConverter(this.proxy.ComponentConverterProxy);
        }

        public ParameterDescriptor(PropertyDescriptor descriptor, WrappedConverter converter, WrappedConverter componentConverter)
            : base(descriptor)
        {
            if (converter == null)
                throw new ArgumentNullException("converter");

            if (componentConverter == null)
                throw new ArgumentNullException("componentConverter");

            if (AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException("WrappedConverter constructor with string parameters can only be called from the main appDomain.");

            this.converter = converter;
            this.componentConverter = componentConverter;
            this.proxy = new Proxy();
            this.proxy.Initialize(descriptor, converter.ProxyInstance, componentConverter.ProxyInstance);
        }

        public override bool CanResetValue(object component)
        {
            return proxy.CanResetValue(componentConverter.ConvertToString(component));
        }

        public override Type ComponentType
        {
            get { return typeof(string); }
        }

        public override object GetValue(object component)
        {
            return (string)proxy.GetValue((string)component);
        }

        public override bool IsReadOnly
        {
            get { return proxy.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return typeof(string); }
        }

        public override void ResetValue(object component)
        {
            proxy.ResetValue(componentConverter.ConvertToString(component));
        }

        public override void SetValue(object component, object value)
        {
            proxy.SetValue(componentConverter.ConvertToString(component), converter.ConvertToString(value));
        }

        public override bool ShouldSerializeValue(object component)
        {
            return proxy.ShouldSerializeValue(componentConverter.ConvertToString(component));
        }

        public override void AddValueChanged(object component, EventHandler handler)
        {
            base.AddValueChanged(component, handler);
        }

        public override string Category
        {
            get
            {
                return proxy.Category;
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                return this.converter;
            }
        }

        public override string Description
        {
            get
            {
                return proxy.Description;
            }
        }

        public override bool DesignTimeOnly
        {
            get
            {
                return proxy.DesignTimeOnly;
            }
        }

        public override string DisplayName
        {
            get
            {
                return proxy.DisplayName;
            }
        }

        public override bool IsBrowsable
        {
            get
            {
                return proxy.IsBrowsable;
            }
        }

        public override bool IsLocalizable
        {
            get
            {
                return proxy.IsLocalizable;
            }
        }

        public override string Name
        {
            get
            {
                return proxy.Name;
            }
        }

        public override bool SupportsChangeEvents
        {
            get
            {
                return proxy.SupportsChangeEvents;
            }
        }

        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            proxy.RemoveValueChanged(componentConverter.ConvertToString(component), handler);
        }

        protected override int NameHashCode
        {
            get
            {
                return proxy.NameHashCode;
            }
        }

        protected override void OnValueChanged(object component, EventArgs e)
        {
            proxy.OnValueChanged(componentConverter.ConvertToString(component));
        }

        protected override object GetInvocationTarget(Type type, object instance)
        {
            return converter.ConvertFromString(proxy.GetInvocationTarget(type, converter.ConvertToString(instance)));
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (editorBaseType != typeof(UITypeEditor))
                return null;

            var editor = (UITypeEditor)proxy.GetEditor(editorBaseType);
            if (editor == null)
                return null;

            return new UITypeEditorWrapper(editor, this.converter);
        }

        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            //Get the names of the properties to keep the sorting.
            List<string> names = new List<string>();
            List<ParameterDescriptor> parameters = new List<ParameterDescriptor>();
            foreach (var propertyProxy in proxy.GetChildProperties(converter.ConvertToString(instance), FilterAttributes(filter).ToArray()))
            {
                var parameter = new ParameterDescriptor(propertyProxy);

                names.Add(parameter.Name);
                parameters.Add(parameter);
            }

            var result = new PropertyDescriptorCollection(parameters.ToArray());
            //TODO: support using an IComparer.
            return result.Sort(names.ToArray());
        }

        protected override void FillAttributes(System.Collections.IList attributeList)
        {
            proxy.FillAttributes(FilterAttributes(attributeList));
        }

        protected override AttributeCollection CreateAttributeCollection()
        {
            return new AttributeCollection(proxy.CreateAttributeCollection().ToArray());
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(proxy.Attributes);
            }
        }

        protected override Attribute[] AttributeArray
        {
            get
            {
                return proxy.AttributeArray;
            }
            set
            {
                proxy.AttributeArray = FilterAttributes(value).ToArray();
            }
        }
    }
}
