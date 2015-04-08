using ANX.Framework.VisualStudio.Converters;
using ANX.Framework.VisualStudio.Nodes;
using ANX.Framework.VisualStudio.PropertyDescriptors;
using System;
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
    public class ContentProcessorConverter : StringConverter
    {
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
                ContentProjectNode node = Utilities.GetCurrentProject();
                if (node == null) //Can happen if the selected project is not a content project and we just moved from one file within a content project to file outside a content project.
                {
                    return value;
                }

                string displayName = null;

                try
                {
                    using (var buildDomain = node.BuildAppDomain.Aquire())
                    {
                        displayName = buildDomain.Proxy.GetProcessorDisplayName((string)value);
                    }
                }
                catch { }

                //If there is no display name, just return the normal name.
                if (displayName == null)
                    displayName = (string)value;

                return displayName;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //From display name to real name.
            if (value is string)
            {
                var node = Utilities.GetCurrentProject();
                if (node == null)
                {
                    return value;
                }

                string processorName = null;
                using (var buildDomain = node.BuildAppDomain.Aquire())
                {
                    processorName = buildDomain.Proxy.GetProcessorName((string)value);
                }
                //The processor has no display name, that means we never converted it to the display name.
                if (processorName == null)
                    processorName = (string)value;

                return processorName;
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

            IncludedAssetFileNodeProperties[] fileNodeProperties;
            if (context.Instance is IncludedAssetFileNodeProperties)
                fileNodeProperties = new IncludedAssetFileNodeProperties[] { (IncludedAssetFileNodeProperties)context.Instance };
            else if (context.Instance is Array)
                fileNodeProperties = ((Array)context.Instance).Cast<IncludedAssetFileNodeProperties>().ToArray();
            else
                throw new ArgumentException("Unknown context.Instance.");

            if (fileNodeProperties.Length == 0)
                return false;

            var previousProcessor = fileNodeProperties[0].ContentProcessor;
            foreach (var property in fileNodeProperties)
            {
                if (previousProcessor != property.ContentProcessor)
                    return false;
            }

            ContentProjectNode projectNode = Utilities.GetCurrentProject();
            ICollection<ProcessorParameter> processorParameters = null;

            using (var buildDomain = projectNode.BuildAppDomain.Aquire())
            {
                processorParameters = buildDomain.Proxy.GetProcessorParameters(fileNodeProperties[0].ContentProcessor);
            }
            return processorParameters != null && processorParameters.Count > 0;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            using (var buildDomain = Utilities.GetCurrentProject().BuildAppDomain.Aquire())
            {
                return new StandardValuesCollection(new List<string>(buildDomain.Proxy.GetProcessorNames()));
            }
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return base.IsValid(context, value);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            IncludedAssetFileNodeProperties[] fileNodeProperties;
            if (context.Instance is IncludedAssetFileNodeProperties)
                fileNodeProperties = new IncludedAssetFileNodeProperties[] { (IncludedAssetFileNodeProperties)context.Instance };
            else if (context.Instance is Array)
                fileNodeProperties = ((Array)context.Instance).Cast<IncludedAssetFileNodeProperties>().ToArray();
            else
                throw new ArgumentException("Unknown context.Instance.");

            ContentProjectNode projectNode = Utilities.GetCurrentProject();
            if (projectNode != null && fileNodeProperties.Length != 0)
            {
                var assetFileNodes = fileNodeProperties.Select((x) => x.AssetFileNode).Where((x) => x != null).ToArray();

                var previousProcessor = fileNodeProperties[0].ContentProcessor;
                foreach (var property in fileNodeProperties)
                {
                    ICollection<ProcessorParameter> processorParameters = null;
                    using (var buildDomain = projectNode.BuildAppDomain.Aquire())
                    {
                        processorParameters = buildDomain.Proxy.GetProcessorParameters((string)value);
                    }

                    if (processorParameters != null && processorParameters.Count > 0 && fileNodeProperties != null)
                    {
                        List<PropertyDescriptor> list = new List<PropertyDescriptor>(processorParameters.Count);
                        foreach (var parameter in processorParameters)
                        {
                            list.Add(ContentProcessorParameterDescriptor.Create((string)value, projectNode, parameter, assetFileNodes));
                        }
                        return new PropertyDescriptorCollection(list.ToArray());
                    }

                }
            }

            return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        }
    }
}
