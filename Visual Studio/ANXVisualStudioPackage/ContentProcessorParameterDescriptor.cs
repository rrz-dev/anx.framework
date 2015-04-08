using ANX.Framework.VisualStudio.Converters;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.PropertyDescriptors
{
    internal sealed class ContentProcessorParameterDescriptor : PropertyDescriptor
    {
        private ProcessorParameter processorParameter;
        private string parameterName;
        private IEnumerable<AssetFileNode> fileNodes;
        private WrappedConverter converter;
        private ContentProjectNode projectNode;
        private string processorTypeName;

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

        public override TypeConverter Converter
        {
            get
            {
                return this.converter;
            }
        }

        internal static ContentProcessorParameterDescriptor Create(string processorName, ContentProjectNode projectNode, ProcessorParameter processorParameter, IEnumerable<AssetFileNode> fileNodes)
        {
            if (processorParameter == null)
            {
                throw new System.ArgumentNullException("processorParameter");
            }

            if (fileNodes == null)
            {
                throw new System.ArgumentNullException("fileNode");
            }

            if (fileNodes.Count() == 0)
                throw new ArgumentException("fileNodes is empty.");

            string displayName = processorParameter.DisplayName;
            if (string.IsNullOrWhiteSpace(displayName))
            {
                displayName = processorParameter.PropertyName;
            }

            string description = processorParameter.Description;
            if (string.IsNullOrEmpty(description))
            {
                description = string.Format(PackageResources.GetString(PackageResources.ParameterDefaultDescription), processorParameter.PropertyName);
            }

            return new ContentProcessorParameterDescriptor(displayName, description, processorParameter, fileNodes, projectNode, processorName);
        }

        private ContentProcessorParameterDescriptor(string name, string description, ProcessorParameter processorParameter, IEnumerable<AssetFileNode> fileNodes, ContentProjectNode projectNode, string processorName)
            : base(name, new Attribute[] { new DescriptionAttribute(description), new HelpKeywordAttribute("Parameterized Processors") })
        {
            this.processorParameter = processorParameter;
            this.fileNodes = fileNodes;
            this.parameterName = processorParameter.PropertyName;

            string converterTypeName;
            using (var buildDomain = projectNode.BuildAppDomain.Aquire())
            {
                processorTypeName = buildDomain.Proxy.GetProcessorTypeName(processorName);
                converterTypeName = buildDomain.Proxy.GetConverterTypeName(processorTypeName, processorParameter.PropertyName);
            }

            converter = new WrappedConverter(converterTypeName, processorParameter.PropertyType);

            this.projectNode = projectNode;
        }

        public override bool CanResetValue(object component)
        {
            return this.processorParameter.DefaultValue != null && !this.processorParameter.DefaultValue.Equals(this.GetValue(component));
        }

        public override object GetValue(object component)
        {
            object result;
            fileNodes.First().ProcessorParameters.TryGetValue(this.parameterName, out result);

            foreach (var node in fileNodes.Skip(1))
            {
                object value;
                node.ProcessorParameters.TryGetValue(this.parameterName, out value);

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
                throw new InvalidOperationException(PackageResources.GetString(PackageResources.PropertyDescriptorResetNotSupported));
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

            string attributeValue=  this.Converter.ConvertTo(null, CultureInfo.InvariantCulture, value, typeof(string)) as string;

            foreach (var node in fileNodes)
                node.ProcessorParameters[this.parameterName] = attributeValue;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return this.CanResetValue(component) || this.processorParameter.DefaultValue == null;
        }

        public override object GetEditor(Type editorBaseType)
        {
            if (editorBaseType == typeof(UITypeEditor))
            {
                IProxy editor = null;
                using (var buildDomain = projectNode.BuildAppDomain.Aquire())
                {
                    editor = buildDomain.Proxy.GetEditor(editorBaseType, this.processorTypeName, this.processorParameter.PropertyName, this.converter.ProxyInstance);
                }
                
                if (editor != null)
                {
                    return new UITypeEditorWrapper(editor);
                }
            }
            return base.GetEditor(editorBaseType);
        }
    }
}
