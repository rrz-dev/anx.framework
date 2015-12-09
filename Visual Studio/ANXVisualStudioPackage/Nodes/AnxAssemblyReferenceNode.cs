using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Nodes
{
    public class AnxAssemblyReferenceNode : AssemblyReferenceNode
    {
        public AnxAssemblyReferenceNode(ContentProjectNode node, string name, string assemblyPath)
            : base(node, name, assemblyPath)
        {
            this.OriginalAssemblyPath = assemblyPath;
        }

        public string OriginalAssemblyPath
        {
            get;
            private set;
        }

        public new ContentProjectNode ProjectMgr
        {
            get
            {
                return (ContentProjectNode)base.ProjectMgr;
            }
        }

        protected override string ResolveAssemblyPath(string assemblyPath)
        {
            if (!File.Exists(assemblyPath))
            {
                using (var buildDomain = this.ProjectMgr.BuildAppDomain.Aquire())
                {
                    assemblyPath = buildDomain.MakeAbsoluteFromSearchPaths(assemblyPath);
                }
            }
            return assemblyPath;
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            return new AnxAssemblyReferenceProperties(this);
        }

        public string RuntimeVersion
        {
            get
            {
                if (this.IsValid)
                {
                    using (var domain = ProjectMgr.BuildAppDomain.Aquire())
                    {
                        return domain.Proxy.GetAssemblyRuntimeVersion(this.Url);
                    }
                }
                else
                    return string.Empty;
            }
        }

        public override void RefreshReference(bool fileChanged = false)
        {
            if (ProjectMgr.BuildAppDomain.IsDisposed)
                return;

            Uri url;
            if (Uri.TryCreate(this.Url, UriKind.Absolute, out url))
            {
                using (var buildDomain = ProjectMgr.BuildAppDomain.Aquire())
                {
                    if (!File.Exists(this.Url))
                    {
                        buildDomain.RemoveShadowCopyDirectory(url);
                    }

                    base.RefreshReference(fileChanged);

                    if (File.Exists(this.Url))
                    {
                        buildDomain.AddShadowCopyDirectory(new Uri(Path.GetDirectoryName(this.Url)));
                    }
                }
            }

            var container = (ContentProjectReferenceContainer)this.ProjectMgr.GetReferenceContainer();
            if (fileChanged)
                container.RefreshAssemblies();
        }
    }
}
