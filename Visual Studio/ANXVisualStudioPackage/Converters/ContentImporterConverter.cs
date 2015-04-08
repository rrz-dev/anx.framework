using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Converters
{
    public class ContentImporterConverter : StringConverter
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
                string displayName = null;

                var projectNode = Utilities.GetCurrentProject();
                if (projectNode != null)
                {
                    using (var buildDomain = projectNode.BuildAppDomain.Aquire())
                    {
                        displayName = buildDomain.Proxy.GetImporterDisplayName((string)value);
                    }
                }
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
                string importerName = null;
                using (var domainLock = Utilities.GetCurrentProject().BuildAppDomain.Aquire())
                {
                    importerName = domainLock.Proxy.GetImporterName((string)value);
                }
               
                //The importer has no display name, that means we never converted it to the display name.
                if (importerName == null)
                    importerName = (string)value;

                return importerName;
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

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            using (var buildDomain = Utilities.GetCurrentProject().BuildAppDomain.Aquire())
            {
                return new StandardValuesCollection(new List<string>(buildDomain.Proxy.GetImporterNames()));
            }
        }
    }
}
