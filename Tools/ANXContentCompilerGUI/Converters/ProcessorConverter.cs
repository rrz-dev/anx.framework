using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using ANX.ContentCompiler.GUI.Nodes;
using ANX.ContentCompiler.GUI.Descriptors;

namespace ANX.ContentCompiler.GUI.Converters
{
    class ProcessorConverter : StringConverter
    {
        ProcessorManager processorManager = new ProcessorManager();

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //From real name to display name.
            if (value is string)
            {
                if (string.IsNullOrEmpty((string)value))
                    return (string)value;
                else
                    return processorManager.GetProcessorDisplayName((string)value);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //From display name to real name.
            if (value is string)
            {
                return processorManager.GetProcessorName((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            BuildItemNodeProperties[] fileNodeProperties;
            if (context.Instance is BuildItemNodeProperties)
                fileNodeProperties = new BuildItemNodeProperties[] { (BuildItemNodeProperties)context.Instance };
            else if (context.Instance is Array)
                fileNodeProperties = ((Array)context.Instance).Cast<BuildItemNodeProperties>().ToArray();
            else
                throw new ArgumentException("Unknown context.Instance.");

            if (fileNodeProperties.Length == 0)
                return false;

            var previousProcessor = fileNodeProperties[0].Processor;
            foreach (var property in fileNodeProperties)
            {
                if (previousProcessor != property.Processor)
                    return false;
            }

            if (string.IsNullOrEmpty(previousProcessor))
                return false;

            ICollection<ProcessorParameter> processorParameters = processorParameters = processorManager.GetProcessorParameters(fileNodeProperties[0].Processor);
            
            return processorParameters != null && processorParameters.Count > 0;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(processorManager.AvailableProcessors.Select((x) => x.Key).ToArray());
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            BuildItemNodeProperties[] fileNodeProperties;
            if (context.Instance is BuildItemNodeProperties)
                fileNodeProperties = new BuildItemNodeProperties[] { (BuildItemNodeProperties)context.Instance };
            else if (context.Instance is Array)
                fileNodeProperties = ((Array)context.Instance).Cast<BuildItemNodeProperties>().ToArray();
            else
                throw new ArgumentException("Unknown context.Instance.");

            if (fileNodeProperties.Length != 0)
            {
                var previousProcessor = fileNodeProperties[0].Processor;
                foreach (var property in fileNodeProperties)
                {
                    ICollection<ProcessorParameter> processorParameters = processorManager.GetProcessorParameters((string)value);

                    if (processorParameters != null && processorParameters.Count > 0 && fileNodeProperties != null)
                    {
                        List<PropertyDescriptor> list = new List<PropertyDescriptor>(processorParameters.Count);
                        foreach (var parameter in processorParameters)
                        {
                            list.Add(new ProcessorParameterDescriptor(TypeDescriptor.CreateProperty(context.Instance.GetType(), parameter.PropertyName, Type.GetType(parameter.PropertyType, true)), parameter, fileNodeProperties));
                        }
                        return new PropertyDescriptorCollection(list.ToArray());
                    }

                }
            }

            return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        }
    }
}
