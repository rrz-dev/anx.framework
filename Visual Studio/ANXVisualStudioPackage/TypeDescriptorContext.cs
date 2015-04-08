using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    class TypeDescriptorContext : ITypeDescriptorContext
    {
        IContainer container;
        object instance;
        PropertyDescriptor descriptor;

        public TypeDescriptorContext(IContainer container, object instance, PropertyDescriptor descriptor)
        {
            this.container = container;
            this.instance = instance;
            this.descriptor = descriptor;
        }

        public IContainer Container
        {
            get { return container; }
        }

        public object Instance
        {
            get { return instance; }
        }

        public void OnComponentChanged()
        {
            
        }

        public bool OnComponentChanging()
        {
            return false;
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return descriptor; }
        }

        public object GetService(Type serviceType)
        {
            return null;
        }
    }
}
