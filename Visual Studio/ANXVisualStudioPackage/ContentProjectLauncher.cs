using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class ContentProjectLauncher : IProjectLauncher
    {
        ContentProjectNode node;

        public ContentProjectLauncher(ContentProjectNode node)
        {
            this.node = node;
        }

        public int LaunchProject(uint options, Config config, IVsOutputWindowPane pane)
        {
            IVsBuildableProjectCfg buildableConfig;
            config.get_BuildableProjectCfg(out buildableConfig);

            config.PrepareBuild(options, false);

            string target = MsBuildTarget.Build;
            if ((options & (uint)VSConstants.VSStd2KCmdID.Debug) != 0 || (options & 1) != 0)
                target = MsBuildTarget.Rebuild;

            ((ContentBuildableProjectConfig)buildableConfig).Build(options, pane, target, null);
            
            return VSConstants.S_OK;
        }

        public int LaunchFiles(IEnumerable<string> files, uint options, Config config, IVsOutputWindowPane pane)
        {
            IVsBuildableProjectCfg buildableConfig;
            config.get_BuildableProjectCfg(out buildableConfig);

            config.PrepareBuild(options, false);

            this.node.SetOutputLogger(pane);
            ((ContentBuildableProjectConfig)buildableConfig).Build(options, pane, MsBuildTarget.Rebuild, files);

            return VSConstants.S_OK;
        }
    }
}
