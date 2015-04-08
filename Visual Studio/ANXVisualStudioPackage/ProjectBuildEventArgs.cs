using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class ProjectBuildEventArgs : EventArgs
    {
        public ProjectBuildEventArgs(Project project, string projectConfig, string platform, string solutionConfig)
        {
            this.Project = project;
            this.ProjectConfig = projectConfig;
            this.Platform = platform;
            this.SolutionConfig = solutionConfig;
        }

        public Project Project
        {
            get;
            private set;
        }
        
        public string ProjectConfig
        {
            get;
            private set;
        }
        
        public string Platform
        {
            get;
            private set;
        }

        public string SolutionConfig
        {
            get;
            private set;
        }
    }
}
