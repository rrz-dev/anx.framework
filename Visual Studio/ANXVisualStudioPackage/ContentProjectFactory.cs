using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using System.Diagnostics;
using Microsoft.VisualStudio.Project;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.Build.Evaluation;

namespace ANX.Framework.VisualStudio
{
    [Guid(GuidList.guidANXVisualStudio2012ContentProjectFactoryString)]
    public class ContentProjectFactory : ProjectFactory 
    {
        ANXVisualStudioPackage package;

        public ContentProjectFactory(ANXVisualStudioPackage package)
            : base(package)
        {
            this.package = package;
        }

        public override ProjectNode CreateProject()
        {
            var projectNode = new ContentProjectNode(this.package);
            
            return projectNode;
        }

        protected override string ProjectTypeGuids(string file)
        {
            return this.GetType().GUID.ToString("B");
        }

        protected override object PreCreateForOuter(IntPtr outerProjectIUnknown)
        {
            ProjectNode node = this.CreateProject();
            Debug.Assert(node != null, "The project failed to be created");

            node.Package = this.Package as ProjectPackage;
            node.BuildEngine = this.buildEngine;
            node.BuildProject = new Project();
            return node;
        }
    }
}
