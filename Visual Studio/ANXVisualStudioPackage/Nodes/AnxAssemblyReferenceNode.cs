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
        ContentProjectNode node;

        public AnxAssemblyReferenceNode(ContentProjectNode node, string name, string assemblyPath)
            : base(node, name, assemblyPath)
        {
            this.node = node;
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
                    using (var domain = node.BuildAppDomain.Aquire())
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
            if (node.BuildAppDomain.IsDisposed)
                return;


            using (var buildDomain = node.BuildAppDomain.Aquire())
            {
                if (!File.Exists(this.Url))
                {
                    buildDomain.RemoveShadowCopyDirectory(new Uri(this.Url));
                }

                base.RefreshReference(fileChanged);

                if (File.Exists(this.Url))
                {
                    buildDomain.AddShadowCopyDirectory(new Uri(Path.GetDirectoryName(this.Url)));
                }
            }

            var container = (ContentProjectReferenceContainer)this.ProjectMgr.GetReferenceContainer();
            if (fileChanged)
                container.RefreshAssemblies();
        }
    }
}
