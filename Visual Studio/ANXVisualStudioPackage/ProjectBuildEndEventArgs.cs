using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class ProjectBuildEndEventArgs : ProjectBuildEventArgs
    {
        public ProjectBuildEndEventArgs(Project project, string projectConfig, string platform, string solutionConfig, bool successfull)
            : base(project, projectConfig, platform, solutionConfig)
        {
            this.Successfull = successfull;
        }

        public bool Successfull
        {
            get;
            private set;
        }
    }
}
