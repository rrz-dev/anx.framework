using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSLangProj;

namespace ANX.Framework.VisualStudio
{
    public class ProjectBuildListener
    {
        private Project targetProject;
        private DTE dte;

        public event EventHandler<ProjectBuildEventArgs> BuildStarted;
        public event EventHandler<ProjectBuildEndEventArgs> BuildEnded;

        public ProjectBuildListener(IServiceProvider serviceProvider, Project targetProject)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException("serviceProvider");

            if (targetProject == null)
                throw new ArgumentNullException("targetProject");

            dte = serviceProvider.GetService(typeof(DTE)) as DTE;
            if (dte == null)
                throw new ArgumentException("serviceProvider doesn't provide a DTE object.");

            
            this.targetProject = targetProject;

            dte.Events.BuildEvents.OnBuildProjConfigBegin +=BuildEvents_OnBuildProjConfigBegin;
            dte.Events.BuildEvents.OnBuildProjConfigDone += BuildEvents_OnBuildProjConfigDone;
        }

        void BuildEvents_OnBuildProjConfigBegin(string Project, string ProjectConfig, string Platform, string SolutionConfig)
        {
            if (Project == targetProject.UniqueName)
            {
                this.OnBuildStarted(new ProjectBuildEventArgs(targetProject, ProjectConfig, Platform, SolutionConfig));
            }
        }

        void BuildEvents_OnBuildProjConfigDone(string Project, string ProjectConfig, string Platform, string SolutionConfig, bool Success)
        {
            if (Project == targetProject.UniqueName)
            {
                this.OnBuildEnded(new ProjectBuildEndEventArgs(targetProject, ProjectConfig, Platform, SolutionConfig, Success));
            }
        }

        protected virtual void OnBuildStarted(ProjectBuildEventArgs args)
        {
            RaiseEventIfNotNull(BuildStarted, this, args);
        }

        protected virtual void OnBuildEnded(ProjectBuildEndEventArgs args)
        {
            RaiseEventIfNotNull(BuildEnded, this, args);
        }

        private void RaiseEventIfNotNull<T>(EventHandler<T> handler, object sender, T args) where T : EventArgs
        {
            if (handler != null)
                handler(sender, args);
        }
    }
}
