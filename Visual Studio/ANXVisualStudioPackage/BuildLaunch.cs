using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class BuildLaunch
    {
        public bool IsDebug
        {
            get;
            private set;
        }

        public IEnumerable<string> FilesToBuild
        {
            get;
            private set;
        }

        public bool IsFileBuild
        {
            get { return FilesToBuild.Any(); }
        }

        public BuildLaunch(bool debug)
        {
            this.IsDebug = debug;
        }

        public BuildLaunch(bool debug, IEnumerable<string> filesToBuild)
            : this(debug)
        {
            this.FilesToBuild = filesToBuild;
        }
    }
}
