using ANX.Framework.VisualStudio.Converters;
using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Nodes
{
    [ComVisible(true)]
    public class IncludedAssetFileNodeProperties : FileNodeProperties
    {
        AssetFileNode fileNode;

        public IncludedAssetFileNodeProperties(AssetFileNode node)
            : base(node)
        {
            this.fileNode = node;
        }

        [Browsable(false)]
        [AutomationBrowsable(false)]
        internal AssetFileNode AssetFileNode
        {
            get { return fileNode; }
        }

        [PackageResourcesDisplayName(PackageResources.AssetName)]
        [PackageResourcesDescriptionAttribute(PackageResources.AssetNameDescription)]
        public string AssetName
        {
            get
            {
                return fileNode.AssetName;
            }
            set
            {
                fileNode.AssetName = value;
            }
        }

        [PackageResourcesDisplayName(PackageResources.ContentProcessor)]
        [PackageResourcesDescriptionAttribute(PackageResources.ContentProcessorDescription)]
        [TypeConverter(typeof(ContentProcessorConverter))]
        public string ContentProcessor
        {
            get
            {
                return fileNode.ContentProcessor;
            }
            set
            {
                fileNode.ContentProcessor = value;
            }
        }

        [PackageResourcesDisplayName(PackageResources.ContentImporter)]
        [PackageResourcesDescriptionAttribute(PackageResources.ContentImporterDescription)]
        [TypeConverter(typeof(ContentImporterConverter))]
        public string ContentImporter
        {
            get
            {
                return fileNode.ContentImporter;
            }
            set
            {
                fileNode.ContentImporter = value;
            }
        }

        [Browsable(false)]
        [AutomationBrowsable(false)]
        public IDictionary<string, object> ProcessorParameters
        {
            get { return fileNode.ProcessorParameters; }
        }

        [Browsable(false)]
        public string SourceControlStatus
        {
            get
            {
                // remove STATEICON_ and return rest of enum
                return HierarchyNode.StateIconIndex.ToString().Substring(10);
            }
        }

        public override DesignPropertyDescriptor CreateDesignPropertyDescriptor(PropertyDescriptor propertyDescriptor)
        {
            return new AssetFileDescriptor(propertyDescriptor);
        }
    }

    [ComVisible(true)]
    public class AnxAssemblyReferenceProperties : ReferenceNodeProperties
    {
        #region fields

        AnxAssemblyReferenceNode node;

        #endregion

        #region ctors
        public AnxAssemblyReferenceProperties(AnxAssemblyReferenceNode node)
            : base(node)
        {
            this.node = node;
        }
        #endregion

        #region properties

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.RuntimeVersion)]
        [SRDescriptionAttribute(SR.RuntimeVersionDescription)]
        public virtual string RuntimeVersion
        {
            get
            {
                return this.node.RuntimeVersion;
            }
        }

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.Resolved)]
        [SRDescriptionAttribute(SR.ResolvedDescription)]
        public bool Resolved
        {
            get { return this.node.IsValid; }
        }

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.AssemblyCulture)]
        [SRDescriptionAttribute(SR.AssemblyCultureDescription)]
        public string Culture
        {
            get 
            {
                if (this.node.AssemblyName != null)
                {
                    return this.node.AssemblyName.CultureName;
                }
                return string.Empty;
            }
        }

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.StrongName)]
        [SRDescriptionAttribute(SR.StrongNameDescription)]
        public bool StrongName
        {
            get 
            {
                if (this.node.AssemblyName != null)
                {
                    return this.node.AssemblyName.KeyPair != null && this.node.AssemblyName.KeyPair.PublicKey != null && this.node.AssemblyName.KeyPair.PublicKey.Length > 0;
                }
                return false;
            }
        }

        #endregion
    }

    [ComVisible(true)]
    public class AnxProjectReferenceProperties : ProjectReferencesProperties
    {
        #region fields

        AnxProjectReferenceNode node;

        #endregion

        #region ctors
        public AnxProjectReferenceProperties(AnxProjectReferenceNode node)
            : base(node)
        {
            this.node = node;
        }
        #endregion

        #region properties

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.RuntimeVersion)]
        [SRDescriptionAttribute(SR.RuntimeVersionDescription)]
        public virtual string RuntimeVersion
        {
            get
            {
                return this.node.RuntimeVersion;
            }
        }

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.Resolved)]
        [SRDescriptionAttribute(SR.ResolvedDescription)]
        public bool Resolved
        {
            get { return this.node.IsValid; }
        }

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.AssemblyCulture)]
        [SRDescriptionAttribute(SR.AssemblyCultureDescription)]
        public string Culture
        {
            get
            {
                if (File.Exists(this.node.ReferencedProjectOutputPath))
                {
                    try
                    {
                        return AssemblyName.GetAssemblyName(this.node.ReferencedProjectOutputPath).CultureName;
                    }
                    catch { }
                }
                return string.Empty;
            }
        }

        [SRCategoryAttribute(SR.Misc)]
        [LocDisplayName(SR.StrongName)]
        [SRDescriptionAttribute(SR.StrongNameDescription)]
        public bool StrongName
        {
            get
            {
                if (File.Exists(this.node.ReferencedProjectOutputPath))
                {
                    try
                    {
                        var assemblyName = AssemblyName.GetAssemblyName(this.node.ReferencedProjectOutputPath);

                        return assemblyName.KeyPair != null && assemblyName.KeyPair.PublicKey != null && assemblyName.KeyPair.PublicKey.Length > 0;
                    }
                    catch { }
                }
                return false;
            }
        }

        #endregion
    }
}
