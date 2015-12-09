using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class PackageResourcesDisplayNameAttribute : DisplayNameAttribute
    {
        #region fields
        string name;
        #endregion

        #region ctors
        public PackageResourcesDisplayNameAttribute(string name)
        {
            this.name = name;
        }
        #endregion

        #region properties
        public override string DisplayName
        {
            get
            {
                string result = PackageResources.GetString(this.name, CultureInfo.CurrentUICulture);
                if (result == null)
                {
                    Debug.Assert(false, "String resource '" + this.name + "' is missing");
                    result = this.name;
                }
                return result;
            }
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class PackageResourcesDescriptionAttribute : DescriptionAttribute
    {
        private bool replaced;

        public PackageResourcesDescriptionAttribute(string description)
            : base(description)
        {
        }

        public override string Description
        {
            get
            {
                if (!replaced)
                {
                    replaced = true;
                    DescriptionValue = PackageResources.GetString(base.Description, CultureInfo.CurrentUICulture);
                }
                return base.Description;
            }
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public sealed class PackageResourcesCategoryAttribute : CategoryAttribute
    {

        public PackageResourcesCategoryAttribute(string category)
            : base(category)
        {
        }

        protected override string GetLocalizedString(string value)
        {
            return PackageResources.GetString(value, CultureInfo.CurrentUICulture);
        }
    }

    public class PackageResources
    {
        public const string AssetName = "AssetName";
        public const string AssetNameDescription = "AssetNameDescription";
        public const string BrowseOutputDirectory = "BrowseOutputDirectory";
        public const string BrowseAnxFrameworkDirectory = "BrowseAnxFrameworkDirectory";
        public const string ConfigurableContentProjectSettings = "ConfigurableContentProjectSettings";
        public const string ContentImporter = "ContentImporter";
        public const string ContentImporterDescription = "ContentImporterDescription";
        public const string ContentProcessor = "ContentProcessor";
        public const string ContentProcessorDescription = "ContentProcessorDescription";
        public const string ContentProjectSettings = "ContentProjectSettings";
        public const string InstallOtherFrameworks = "InstallOtherFrameworks";
        public const string InstallOtherFrameworksFWLink = "InstallOtherFrameworksFWLink";
        public const string NeededAssembliesNotLoaded = "NeededAssembliesNotLoaded";
        public const string NeededAssembliesNotLoadedTitle = "NeededAssembliesNotLoadedTitle";
        public const string None = "None";
        public const string ParameterDefaultDescription = "ParameterDefaultDescription";
        public const string PropertyDescriptorResetNotSupported = "PropertyDescriptorResetNotSupported";
        public const string AnxFrameworkAssembliesRedirected = "AnxFrameworkAssembliesRedirected";
        public const string Debugging = "Debugging";
        public const string Building = "Building";
        public const string BuildReferenceContainer = "BuildReferences";
        public const string ContentRoot = "ContentRoot";
        public const string ContentRootDescription = "ContentRootDescription";

        static ResourceManager resources;

        static PackageResources()
        {
            resources = new ResourceManager("ANX.Framework.VisualStudio.Resources.PackageResources", typeof(PackageResources).Assembly);
        }

        public static ResourceManager Resources
        {
            get
            {
                return resources;
            }
        }

        public static string GetString(string name)
        {
            return resources.GetString(name);
        }

        public static string GetString(string name, CultureInfo culture)
        {
            return resources.GetString(name, culture);
        }
    }
}
