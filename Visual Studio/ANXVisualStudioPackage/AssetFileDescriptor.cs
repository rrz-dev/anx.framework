using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ANX.Framework.VisualStudio.Nodes;

namespace ANX.Framework.VisualStudio
{
    public class AssetFileDescriptor : DesignPropertyDescriptor
    {
        ContentProjectNode currentProject;

        public AssetFileDescriptor(PropertyDescriptor prop)
            : base(prop)
        {
            currentProject = Utilities.GetCurrentProject();
        }

        public override void SetValue(object component, object value)
        {
            var prop = (IncludedAssetFileNodeProperties)component;
            switch (this.Name)
            {
                case "AssetName":
                    if (IsAssetNameValid((string)value))
                        prop.AssetName = (string)value;
                    break;
                default:
                    base.SetValue(component, value);
                    break;
            }

            //By refreshing the property grid, we make sure if a converter didn't have any properties and we changed to a converter with properties, it would refresh.
            TypeDescriptor.Refresh(component);
        }

        private bool IsAssetNameValid(string value)
        {
            if (value == null)
                return true;

            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (value.Contains(c))
                    return false;
            }

            return true;
        }

        public override bool CanResetValue(object component)
        {
            var prop = (IncludedAssetFileNodeProperties)component;
            switch (this.Name)
            {
                case "AssetName":
                    return Path.GetFileNameWithoutExtension(prop.FileName) != prop.AssetName;
                case "ContentImporter":
                    using (var domain = currentProject.BuildAppDomain.Aquire())
                    {
                        var importer = domain.Proxy.GuessImporterByFileExtension(prop.FileName);
                        return importer != null && prop.ContentImporter != importer;
                    }
                case "ContentProcessor":
                    if (string.IsNullOrEmpty(prop.ContentImporter))
                        return false;

                    using (var domain = currentProject.BuildAppDomain.Aquire())
                    {
                        return prop.ContentProcessor != domain.Proxy.GetDefaultProcessorForImporter(prop.ContentImporter) || 
                            this.Converter.GetProperties(new TypeDescriptorContext(null, component, this), prop.ContentProcessor).Cast<PropertyDescriptor>().Any((x) => x.CanResetValue(prop.ContentProcessor));
                    }
                case "FileName":
                    return false;
                default:
                    return base.CanResetValue(component);
            }
        }

        public override void ResetValue(object component)
        {
            var prop = (IncludedAssetFileNodeProperties)component;
            switch (this.Name)
            {
                case "AssetName":
                    this.SetValue(component, Path.GetFileNameWithoutExtension(prop.FileName));
                    return;
                case "ContentImporter":
                    using (var domain = currentProject.BuildAppDomain.Aquire())
                    {
                        this.SetValue(component, domain.Proxy.GuessImporterByFileExtension(prop.FileName));
                    }
                    return;
                case "ContentProcessor":
                    using (var domain = currentProject.BuildAppDomain.Aquire())
                    {
                        this.SetValue(component, domain.Proxy.GetDefaultProcessorForImporter(prop.ContentImporter));
                        prop.ProcessorParameters.Clear();
                    }
                    return;
            }

            base.ResetValue(component);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return this.CanResetValue(component);
        }

        public override object GetValue(object component)
        {
            var prop = (IncludedAssetFileNodeProperties)component;

            //if no values are present, create default values, a none value is not allowed.
            if (string.IsNullOrEmpty(prop.ContentImporter))
                using (var buildDomain = currentProject.BuildAppDomain.Aquire())
                {
                    prop.ContentImporter = buildDomain.Proxy.GuessImporterByFileExtension(prop.Extension);
                }

            if (string.IsNullOrEmpty(prop.ContentProcessor))
                using (var buildDomain = currentProject.BuildAppDomain.Aquire())
                {
                    prop.ContentProcessor = buildDomain.Proxy.GetDefaultProcessorForImporter(prop.ContentImporter);
                }

            return base.GetValue(component);
        }
    }
}
