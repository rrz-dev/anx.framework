using ANX.Framework.VisualStudio.Converters;
using ANX.Framework.VisualStudio.PropertyDescriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace ANX.Framework.VisualStudio
{
    /// <summary>
    /// Context that creates a proxy that can be send over AppDomain boundaries.
    /// </summary>
    public class TypeDescriptorContextWrapper : ITypeDescriptorContext
    {
        private class Proxy : MarshalByRefObject, IProxy
        {
            ITypeDescriptorContext context;
            IProxy converterProxy;

            public Proxy(ITypeDescriptorContext context, IProxy converterProxy)
            {
                this.context = context;
                this.converterProxy = converterProxy;
            }

            public override object InitializeLifetimeService()
            {
                return null;
            }

            public IProxy ConverterProxy
            {
                get { return converterProxy; }
            }
            
            public IContainer Container
            {
                get 
                { 
                    var container = context.Container;
                    if (container != null && container.GetType().IsSerializable || container.GetType().IsMarshalByRef)
                    {
                        return container;
                    }
                    return null; 
                }
            }

            public object Instance
            {
                //We are always handling with strings in the visual studio appDomain, so this value is expected to be a string too.
                get { return (string)context.Instance; }
            }

            public void OnComponentChanged()
            {
                context.OnComponentChanged();
            }

            public bool OnComponentChanging()
            {
                return context.OnComponentChanging();
            }

            public IProxy PropertyDescriptor
            {
                get 
                {
                    //we are currently in the visual studio appDomain.
                    //The parameterDescriptors we get here are always in the visual studio appDomain.
                    //They are either ParameterDescriptor that wraps the descriptors from the build appDomain or we have a ContentProcessorParameterDescriptor.
                    var descriptor = context.PropertyDescriptor;
                    if (descriptor.Converter is WrappedConverter)
                    {
                        return ParameterDescriptor.CreateProxy(descriptor, converterProxy, ((WrappedConverter)descriptor.Converter).ProxyInstance);
                    }
                    else
                    {
                        Debugger.Break(); //If we have a wrapped descriptor, we have to find the original type.
                        return ParameterDescriptor.CreateProxy(descriptor, converterProxy, WrappedConverter.CreateProxy(descriptor.Converter.GetType(), descriptor.PropertyType));
                    }
                }
            }

            public IProxy GetService(Type serviceType)
            {
                if (serviceType == typeof(IWindowsFormsEditorService))
                {
                    return IWindowsFormsEditorServiceWrapper.CreateProxy((IWindowsFormsEditorService)context.GetService(serviceType));
                }

                return null;
            }

            public object OriginalInstance
            {
                get { return context; }
            }

            public Type WrapperType
            {
                get { return typeof(TypeDescriptorContextWrapper); }
            }
        }

        private Proxy proxy;
        TypeConverter converter;

        /// <summary>
        /// Creates a new proxy for an <see cref="ITypeDescriptorContext"/>. Can only be executed on the visual studio appDomain.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="converterProxy"></param>
        /// <returns></returns>
        public static IProxy CreateProxy(ITypeDescriptorContext context, IProxy converterProxy)
        {
            if (AppDomain.CurrentDomain.IsBuildAppDomain())
                throw new InvalidOperationException(string.Format("A proxy for {0} is only allowed to be created on the main appDomain.", typeof(TypeDescriptorContextWrapper).Name));

            //The proxy is always meant to be created on the visual studio appDomain, we only send the proxy over to the build AppDomain and wrap it so that the custom converters can use it.
            return new Proxy(context, converterProxy);
        }

        public TypeDescriptorContextWrapper(IProxy proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException("proxy");

            if (proxy.GetType() != typeof(Proxy))
                throw new ArgumentException(string.Format("The given proxy must be of type {0}.", typeof(Proxy).FullName));

            this.proxy = (Proxy)proxy;
            this.converter = new WrappedConverter(this.proxy.ConverterProxy);
        }

        public IProxy ProxyInstance
        {
            get { return this.proxy; }
        }

        public IContainer Container
        {
            get { return proxy.Container; }
        }

        public object Instance
        {
            get { return converter.ConvertFromString((string)proxy.Instance); }
        }

        public void OnComponentChanged()
        {
            proxy.OnComponentChanged();
        }

        public bool OnComponentChanging()
        {
            return proxy.OnComponentChanging();
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return new ParameterDescriptor(proxy.PropertyDescriptor); }
        }

        public object GetService(Type serviceType)
        {
            IProxy service = proxy.GetService(serviceType);
            if (service == null)
                return null;
            else
            {
                return Activator.CreateInstance(service.WrapperType, service);
            }
        }
    }
}
