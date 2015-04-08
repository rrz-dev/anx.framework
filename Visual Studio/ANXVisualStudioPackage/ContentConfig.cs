using ANX.Framework.Build;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Graphics;
using ANX.Framework.VisualStudio.Nodes;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANX.Framework.VisualStudio
{
    public class ContentConfig : CommonConfig
    {
        ContentProjectNode node;
        Configuration contentConfiguration;
        ContentBuildableProjectConfig buildableConfig;

        public ContentConfig(ContentProjectNode node, string configuration, string platform)
            : base(node, configuration, platform)
        {
            this.node = node;

            TargetPlatform targetPlatform = Utilities.ParseTargetPlatform(platform);
            if (!node.ContentProject.Configurations.TryGetConfiguration(configuration, targetPlatform, out contentConfiguration))
            {
                contentConfiguration = new Configuration(configuration, targetPlatform);
                contentConfiguration.Profile = GraphicsProfile.Reach;
                contentConfiguration.OutputDirectory = Path.Combine("bin", platform, configuration);

                node.ContentProject.Configurations.Add(contentConfiguration);

                node.SetProjectFileDirty(true);
            }
        }

        public ContentConfig(ContentProjectNode node, Configuration config)
            : base(node, config.Name, Utilities.GetDisplayName(config.Platform))
        {
            this.node = node;
            this.contentConfiguration = config;
        }

        public override string ConfigName
        {
            get
            {
                return this.Configuration.Name;
            }
        }

        public override string PlatformName
        {
            get
            {
                return Utilities.GetDisplayName(this.Configuration.Platform);
            }
        }

        public Configuration Configuration
        {
            get { return contentConfiguration; }
        }

        public new ContentProjectNode ProjectMgr
        {
            get { return this.node; }
        }

        public override int get_Platform(out Guid platform)
        {
            return base.get_Platform(out platform);
        }

        public override int EnumOutputs(out IVsEnumOutputs eo)
        {
            return base.EnumOutputs(out eo);
        }

        /// <summary>
        /// Determines whether the debugger can be launched, given the state of the launch flags.
        /// </summary>
        /// <param name="flags">Flags that determine the conditions under which to launch the debugger. 
        /// For valid grfLaunch values, see __VSDBGLAUNCHFLAGS or __VSDBGLAUNCHFLAGS2.</param>
        /// <param name="fCanLaunch">true if the debugger can be launched, otherwise false</param>
        /// <returns>S_OK if the method succeeds, otherwise an error code</returns>
        public override int QueryDebugLaunch(uint flags, out int fCanLaunch)
        {
            fCanLaunch = 1;
            return VSConstants.S_OK;
        }

        public override int DebugLaunch(uint flags)
        {
            IProjectLauncher starter = node.GetLauncher();

            IVsOutputWindowPane pane;
            if ((flags & 16) != 0)
            {
                pane = this.ProjectMgr.Package.GetOutputPane(GuidList.VsGuids.guidDebugOutputWindowPane, PackageResources.GetString(PackageResources.Debugging));
                flags |= (uint)VSConstants.VSStd2KCmdID.Debug;
            }
            else
                pane = this.ProjectMgr.Package.GetOutputPane(GuidList.VsGuids.guidBuildOutputWindowPane, PackageResources.GetString(PackageResources.Building));

            return starter.LaunchProject(flags | (uint)VSConstants.VSStd2KCmdID.Debug, this, pane);
        }

        public override int get_BuildableProjectCfg(out IVsBuildableProjectCfg pb)
        {
            if (buildableConfig == null)
            {
                buildableConfig = new ContentBuildableProjectConfig(this);
            }
            pb = buildableConfig;
            return VSConstants.S_OK;
        }
    }

    public class ContentBuildableProjectConfig : BuildableProjectConfig
    {
        public const uint SelfInitiatedBuild = 0x80000000;

        ContentConfig config;

        public ContentBuildableProjectConfig(ContentConfig config)
            : base(config)
        {
            this.config = config;
        }

        public override int QueryStartUpToDateCheck(uint options, int[] supported, int[] ready)
        {
            if (supported != null && supported.Length > 0)
                supported[0] = 1;
            if (ready != null && ready.Length > 0)
                ready[0] = (!this.config.ProjectMgr.BuildInProgress && !this.config.ProjectMgr.BuildAppDomain.IsBuildRunning) ? 1 : 0;

            return VSConstants.S_OK;
        }

        public override int StartUpToDateCheck(IVsOutputWindowPane pane, uint options)
        {
            //If we are doing a file build, always force a rebuild.
            if (this.config.ProjectMgr.PendingBuild != null)
                return VSConstants.E_FAIL;

            ErrorLoggingHelper loggingHelper = new ErrorLoggingHelper(config.ProjectMgr, config.ProjectMgr.ErrorListProvider, pane);

            //Call into builder process to see if it returns that everything is up to date.
            using (var domain = config.ProjectMgr.BuildAppDomain.Aquire())
            {
                if (domain.Proxy.IsUpDoDate(config.ProjectMgr.ProjectHome, config.ProjectMgr.ContentProject.BuildItems, config.ProjectMgr.ActiveContentConfiguration, loggingHelper))
                {
                    loggingHelper.LogMessage(null, "Still up to date. Skipping build.");
                    return VSConstants.S_OK;
                }
                else
                    return VSConstants.E_FAIL;
            }
        }

        public override int StartBuild(IVsOutputWindowPane pane, uint options)
        {
            // If we are not asked for a rebuild, then we build the default target (by passing null)
            IProjectLauncher starter = ((ContentProjectNode)this.config.ProjectMgr).GetLauncher();
            //Always output to the build pane, not the debugger pane.
            pane = this.config.ProjectMgr.Package.GetOutputPane(GuidList.VsGuids.guidBuildOutputWindowPane, PackageResources.GetString(PackageResources.Building));

            if (config.ProjectMgr.PendingBuild != null)
            {
                options |= SelfInitiatedBuild;
                if (config.ProjectMgr.PendingBuild.IsDebug)
                    options |= (uint)VSConstants.VSStd2KCmdID.Debug;
            }

            //Find the actually active config instead of just the current, Visual Studio calls the build just an any configuration.
            var activeContentConfig = this.config.ProjectMgr.ActiveContentConfiguration;
            
            IVsCfg cfg;
            ErrorHandler.ThrowOnFailure(this.config.ProjectMgr.ConfigProvider.GetCfgOfName(activeContentConfig.Name, activeContentConfig.Platform.ToString(), out cfg));
            Config activeConfig = (Config)cfg;

            if (config.ProjectMgr.PendingBuild != null && config.ProjectMgr.PendingBuild.IsFileBuild)
            {
                starter.LaunchFiles(config.ProjectMgr.PendingBuild.FilesToBuild, options, activeConfig, pane);
            }
            else
            {
                starter.LaunchProject(options, activeConfig, pane);
            }

            return VSConstants.S_OK;
        }

        protected override void RefreshReferences()
        {
            ContentProjectReferenceContainer references = (ContentProjectReferenceContainer)this.config.ProjectMgr.GetReferenceContainer();

            references.RefreshAssemblies();
        }

        protected override void OnNotifyBuildEnd(MSBuildResult result, string buildTarget, IVsBuildStatusCallback cb, int success)
        {
            //No need for reloading any references as we are automatically reloading if the assemblies get changed.
            try
            {
                ErrorHandler.ThrowOnFailure(cb.BuildEnd(success));
            }
            catch (Exception e)
            {
                // If those who ask for status have bugs in their code it should not prevent the build/notification from happening
                Debug.Fail(String.Format(CultureInfo.CurrentCulture, SR.GetString(SR.BuildEventError, CultureInfo.CurrentUICulture), e.Message));
            }
        }
    }
}
