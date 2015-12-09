using ANX.ContentCompiler.GUI.Nodes;
using ANX.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI.Descriptors
{
    class ProcessorParameterDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor innerDescriptor;
        private ProcessorParameter processorParameter;
        private IEnumerable<BuildItemNodeProperties> nodeProperties;

        public override Type ComponentType
        {
            get
            {
                return typeof(string);
            }
        }

        //A condition for a processor parameter to be even created is that it is writeable.
        public override bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return typeof(string);
            }
        }

        public ProcessorParameterDescriptor(PropertyDescriptor innerDescriptor, ProcessorParameter processorParameter, IEnumerable<BuildItemNodeProperties> nodeProperties)
            : base((processorParameter.DisplayName ?? processorParameter.PropertyName), new Attribute[] { new DescriptionAttribute(processorParameter.Description) })
        {
            this.innerDescriptor = innerDescriptor;
            this.processorParameter = processorParameter;
            this.nodeProperties = nodeProperties;
        }

        public override bool CanResetValue(object component)
        {
            return this.processorParameter.DefaultValue != null && !this.processorParameter.DefaultValue.Equals(this.GetValue(component));
        }

        public override object GetValue(object component)
        {
            object result;
            nodeProperties.First().BuildItem.ProcessorParameters.TryGetValue(this.processorParameter.PropertyName, out result);

            foreach (var node in nodeProperties.Skip(1))
            {
                object value;
                node.BuildItem.ProcessorParameters.TryGetValue(this.processorParameter.PropertyName, out value);

                if (value == null)
                    if (result == null)
                        continue;
                    else
                        return null;
                else if (!value.Equals(result))
                    return null;
            }

            if (result == null)
                result = this.processorParameter.DefaultValue;

            return result;
        }

        public override void ResetValue(object component)
        {
            if (!this.CanResetValue(component))
            {
                throw new InvalidOperationException("Reset not supported.");
            }

            this.SetValue(component, this.processorParameter.DefaultValue);
        }

        public override void SetValue(object component, object value)
        {
            object originalValue = this.GetValue(component);
            if (originalValue == value)
            {
                return;
            }
            if (originalValue != null && originalValue.Equals(value))
            {
                return;
            }

            string attributeValue = this.Converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(string)) as string;

            foreach (var node in nodeProperties)
                node.BuildItem.ProcessorParameters[this.processorParameter.PropertyName] = attributeValue;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return this.CanResetValue(component) || this.processorParameter.DefaultValue == null;
        }

        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            return innerDescriptor.GetChildProperties(instance, filter);
        }

        public override object GetEditor(Type editorBaseType)
        {
            return innerDescriptor.GetEditor(editorBaseType);
        }

        public override TypeConverter Converter
        {
            get
            {
                return innerDescriptor.Converter;
            }
        }

        public override string Category
        {
            get
            {
                return innerDescriptor.Category;
            }
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return innerDescriptor.Attributes;
            }
        }
    }
}
