using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Nodes
{
    public class AnxProjectReferenceNode : ProjectReferenceNode
    {
        ContentProjectNode node;
        string oldOutputPath;

        public AnxProjectReferenceNode(ContentProjectNode node, string referencedProjectName, string projectPath, string projectReference)
            : base(node, referencedProjectName, projectPath, projectReference)
        {
            this.node = node;
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            return new AnxProjectReferenceProperties(this);
        }

        public string RuntimeVersion
        {
            get
            {
                if (File.Exists(ReferencedProjectOutputPath))
                {
                    using (var domain = node.BuildAppDomain.Aquire())
                    {
                        return domain.Proxy.GetAssemblyRuntimeVersion(this.ReferencedProjectOutputPath);
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
                if (! string.IsNullOrEmpty(oldOutputPath))
                {
                    buildDomain.RemoveShadowCopyDirectory(new Uri(oldOutputPath));
                }

                base.RefreshReference(fileChanged);

                if (File.Exists(this.ReferencedProjectOutputPath))
                {
                    buildDomain.AddShadowCopyDirectory(new Uri(Path.GetDirectoryName(this.ReferencedProjectOutputPath)));
                }

                oldOutputPath = Path.GetDirectoryName(this.ReferencedProjectOutputPath);
            }


            var container = (ContentProjectReferenceContainer)this.ProjectMgr.GetReferenceContainer();
            if (fileChanged)
                container.RefreshAssemblies();
        }
    }
}
