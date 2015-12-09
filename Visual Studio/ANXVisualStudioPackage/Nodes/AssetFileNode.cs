using ANX.Framework.Build;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio.Nodes
{
    [Guid("B9675D94-62A9-4C7D-AC3A-FB3B4A51400B")]
    public class AssetFileNode : CommonNonCodeFileNode
    {
        private bool isDirty;
        private string assetName;
        private string contentProcessor;
        private string contentImporter;
        private ProcessorParameterDictionary processorParameters = new ProcessorParameterDictionary();

        public AssetFileNode(CommonProjectNode root, ProjectElement e)
            : base(root, e)
        {
            processorParameters.Invalidate += processorParameters_Invalidated;
        }

        void processorParameters_Invalidated(object sender, EventArgs e)
        {
            this.IsDirty = true;
        }

        public new ContentProjectNode ProjectMgr
        {
            get { return (ContentProjectNode)base.ProjectMgr; }
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            if (IsLinkFile)
            {
                return new LinkFileNodeProperties(this);
            }
            else if (IsNonMemberItem)
            {
                return new ExcludedFileNodeProperties(this);
            }
            
            return new IncludedAssetFileNodeProperties(this);
        }

        public IDictionary<string, object> ProcessorParameters
        {
            get { return processorParameters; }
        }

        public string AssetName
        {
            get { return assetName; }
            set
            {
                if (assetName != value)
                {
                    assetName = value;

                    this.IsDirty = true;
                }
            }
        }

        public string ContentProcessor
        {
            get
            {
                return contentProcessor;
            }
            set
            {
                if (contentProcessor != value)
                {
                    contentProcessor = value;
                    ProcessorParameters.Clear();

                    this.IsDirty = true;
                }
            }
        }

        public string ContentImporter
        {
            get
            {
                return contentImporter;
            }
            set
            {
                if (contentImporter != value)
                {
                    contentImporter = value;

                    this.IsDirty = true;
                }
            }
        }

        public bool Loading
        {
            get;
            set;
        }

        public bool IsDirty
        {
            get { return isDirty; }
            set
            {
                if (Loading == false)
                {
                    isDirty = value;
                    if (value)
                    {
                        this.ProjectMgr.SetProjectFileDirty(true);
                    }
                }
            }
        }

        public override int QueryStatusOnNode(Guid guidCmdGroup, uint cmd, IntPtr pCmdText, ref QueryStatusResult result)
        {
            if (guidCmdGroup == GuidList.guidANXVisualStudio2012PackageCmdSet)
            {
                var buildInProgress = this.ProjectMgr.DTE.Solution.SolutionBuild.BuildState == EnvDTE.vsBuildState.vsBuildStateInProgress;

                switch (cmd)
                {
                    case AnxMenuCommands.DebugFile:
                        result |= QueryStatusResult.SUPPORTED;
                        if (!buildInProgress)
                            result |= QueryStatusResult.ENABLED;
                        return VSConstants.S_OK;
                    case AnxMenuCommands.BuildFile:
                        result |= QueryStatusResult.SUPPORTED;
                        if (!buildInProgress)
                            result |= QueryStatusResult.ENABLED;
                        return VSConstants.S_OK;
                }
            }

            return base.QueryStatusOnNode(guidCmdGroup, cmd, pCmdText, ref result);
        }

        public override int ExecCommandOnNode(Guid cmdGroup, uint cmd, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (cmdGroup == GuidList.guidANXVisualStudio2012PackageCmdSet)
            {
                switch (cmd)
                {
                    case AnxMenuCommands.DebugFile:
                        return BuildSingleFile(true);
                    case AnxMenuCommands.BuildFile:
                        return BuildSingleFile(false);
                }
            }

            return base.ExecCommandOnNode(cmdGroup, cmd, nCmdexecopt, pvaIn, pvaOut);
        }

        private int BuildSingleFile(bool debug)
        {
            if (!VsUtilities.SaveDirtyFiles())
            {
                return VSConstants.E_ABORT;
            }

            var selected = this.ProjectMgr.GetSelectedNodes();

            if (selected.Count > 1)
                throw new InvalidOperationException("Number of selected files is bigger than 1.");
            else if (selected.Count == 0)
                return VSConstants.E_FAIL;

            this.ProjectMgr.InitiateFileBuild(selected.OfType<AssetFileNode>().Select((x) => x.Url), debug);

            return VSConstants.S_OK;
        }
    }
}
