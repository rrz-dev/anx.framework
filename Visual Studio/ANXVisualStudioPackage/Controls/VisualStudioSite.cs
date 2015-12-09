using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Controls
{
    class VisualStudioSite : ISite
    {
        ProjectNode node;

        public VisualStudioSite(ProjectNode node)
        {
            this.node = node;
        }

        public IComponent Component
        {
            get { return null; }
        }

        public IContainer Container
        {
            get { return null; }
        }

        public bool DesignMode
        {
            get { return VsShellUtilities.IsVisualStudioInDesignMode(node.Site); }
        }

        public string Name
        {
            get;
            set;
        }

        public object GetService(Type serviceType)
        {
            return node.Site.GetService(serviceType);
        }
    }
}
