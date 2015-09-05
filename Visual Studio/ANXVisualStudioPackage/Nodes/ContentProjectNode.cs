using ANX.Framework.Build;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Project;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OleConstants = Microsoft.VisualStudio.OLE.Interop.Constants;
using VsCommands = Microsoft.VisualStudio.VSConstants.VSStd97CmdID;
using VsCommands2K = Microsoft.VisualStudio.VSConstants.VSStd2KCmdID;
using Microsoft.Build.Utilities;
using ANX.Framework.VisualStudio;
using Microsoft.Win32;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Content.Pipeline.Tasks.References;
using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Helpers;
using Microsoft.VisualStudio.Project.Automation;
using ContentBuilder;

namespace ANX.Framework.VisualStudio.Nodes
{
    [Guid(GuidList.guidANXVisualStudio2012ContentProjectString)]
    public class ContentProjectNode : CommonProjectNode
    {
        ContentProject anxContentProject = null;
        BuildAppDomain buildAppDomain;

        /// <summary>
        /// List of output groups names and their associated target
        /// </summary>
        private static KeyValuePair<string, string>[] outputGroupNames =
        {                                      // Name                    Target (MSBuild)
            new KeyValuePair<string, string>("Built",                 "BuiltProjectOutputGroup"),
            new KeyValuePair<string, string>("ContentFiles",          "ContentFilesProjectOutputGroup"),
        };

        public ContentProjectNode(CommonProjectPackage package)
            : base(package, VsUtilities.GetImageList(File.OpenRead(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "anx.ico"))))
        {
            buildAppDomain = new BuildAppDomain(this);

            //Also allows using the delete key on a node.
            this.CanProjectDeleteItems = true;

            this.InitilalizeCATIDs();
        }

        private void InitilalizeCATIDs()
        {
            //AddCATIDMapping(typeof(ContentProjectSettingsPage), typeof(ContentProjectSettingsPage).GUID);
            AddCATIDMapping(typeof(ConfigurableContentProjectSettingsPage), typeof(ConfigurableContentProjectSettingsPage).GUID);
            //AddCATIDMapping(typeof(BuildEventsPropPageComClass), typeof(BuildEventsPropPageComClass).GUID);
            //AddCATIDMapping(typeof(ReferencePathsPropPageComClass), typeof(ReferencePathsPropPageComClass).GUID);
        }

        public BuildLaunch PendingBuild
        {
            get;
            private set;
        }

        public EnvDTE._DTE DTE
        {
            get
            {
                return this.Site.GetService(typeof(EnvDTE._DTE)) as EnvDTE._DTE;
            }
        }

        public void InitiateFileBuild(IEnumerable<string> files, bool debug)
        {
            var activeConfig = this.ActiveContentConfiguration;

            var dte = this.Site.GetService(typeof(EnvDTE._DTE)) as EnvDTE._DTE;
            //WaitForBuildToFinish = false is important because otherwise, the debugger hangs when started with this build.
            string solutionConfiguration = activeConfig.Name;
            string projectUniqueName = ((OAProject)this.GetAutomationObject()).UniqueName;

            this.PendingBuild = new BuildLaunch(debug, files);

            dte.Solution.SolutionBuild.BuildProject(solutionConfiguration, projectUniqueName, WaitForBuildToFinish: false);
        }

        public void InitiateBuild(bool debug)
        {
            var activeConfig = this.ActiveContentConfiguration;

            var dte = this.Site.GetService(typeof(EnvDTE._DTE)) as EnvDTE._DTE;
            //WaitForBuildToFinish = false is important because otherwise, the debugger hangs when started with this build.
            string solutionConfiguration = activeConfig.Name;

            this.PendingBuild = new BuildLaunch(debug);

            dte.Solution.SolutionBuild.BuildProject(solutionConfiguration, ((OAProject)this.GetAutomationObject()).UniqueName, false);
        }

        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            return new Guid[] 
            { 
                //typeof(ContentProjectSettingsPage).GUID,  
            };
        }

        protected override Guid[] GetConfigurationDependentPropertyPages()
        {
            return new[] 
            { 
                typeof(ConfigurableContentProjectSettingsPage).GUID, 
                //typeof(BuildEventsPropPageComClass).GUID, 
            };
        }

        public ContentProject ContentProject
        {
            get { return anxContentProject; }
        }

        public override int ImageIndex
        {
            get
            {
                return 52;
            }
        }

        public virtual string AbsoluteProjectFilePath
        {
            get
            {
                return new Uri(new Uri(this.ProjectHome, UriKind.Absolute), new Uri(this.ProjectFile, UriKind.Relative)).LocalPath;
            }
        }

        public ErrorListProvider ErrorListProvider
        {
            get;
            protected set;
        }

        public BuildAppDomain BuildAppDomain
        {
            get { return buildAppDomain; }
        }

        public override Guid ProjectGuid
        {
            get { return GuidList.guidANXVisualStudio2012ContentProjectFactory; }
        }

        public override string ProjectType
        {
            get { return "ANX Content Project"; }
        }

        public override string Caption
        {
            get
            {
                // Default to file name
                string caption = this.GetProjectProperty(ProjectFileConstants.Name);
                if (String.IsNullOrEmpty(caption))
                {
                    caption = Path.GetFileNameWithoutExtension(this.FileName);
                }

                return caption;
            }
        }

        public override IList<KeyValuePair<string, string>> GetOutputGroupNames()
        {
            return outputGroupNames;
        }

        public Configuration ActiveContentConfiguration
        {
            get
            {
                var activeConfig = this.GetProjectProperty(ProjectFileConstants.Configuration);
                var activePlatform = this.GetProjectProperty(ProjectFileConstants.Platform);

                var config = this.ConfigProvider.FirstOrDefault((x) => x.ConfigName == activeConfig && x.PlatformName == activePlatform) as ContentConfig;
                if (config == null)
                    throw new KeyNotFoundException(string.Format("Error when searching for configuration with name \"{0}\" and platform \"{1}\".", activeConfig, activePlatform));

                return config.Configuration;
            }
        }

        /// <summary>
        /// Factory method for configuration provider
        /// </summary>
        /// <returns>Configuration provider created</returns>
        protected override ConfigProvider CreateConfigProvider()
        {
            return new ContentConfigProvider(this);
        }

        /// <summary>
        /// Initialize common project properties with default value if they are empty
        /// </summary>
        /// <remarks>The following common project properties are defaulted to projectName (if empty):
        ///    AssemblyName, Name and RootNamespace.
        /// If the project filename is not set then no properties are set</remarks>
        protected override void InitializeProjectProperties()
        {
            //base tried to access properties of the project file like assembly name, but we don't load the old ms project.
        }

        protected override void SaveMSBuildProjectFile(string filename)
        {
            SaveProjectFile(filename);
        }

        /// <summary>
        /// Saves project file related information to the new file name. It also calls msbuild API to save the project file.
        /// It is called by the SaveAs method and the SetEditLabel before the project file rename related events are triggered. 
        /// An implementer can override this method to provide specialized semantics on how the project file is renamed in the msbuild file.
        /// </summary>
        /// <param name="newFileName">The new full path of the project file</param>
        protected override void SaveMSBuildProjectFileAs(string newFileName)
        {
            Debug.Assert(!String.IsNullOrEmpty(newFileName), "Cannot save project file for an empty or null file name");

            this.FileName = newFileName;

            string newFileNameWithoutExtension = Path.GetFileNameWithoutExtension(newFileName);

            // Refresh solution explorer
            this.SetProjectProperty(ProjectFileConstants.Name, newFileNameWithoutExtension);

            // Saves the project file on disk.
            this.SaveProjectFile(newFileName);

        }

        private void SaveProjectFile(string filename)
        {
            Debug.Assert(!String.IsNullOrEmpty(filename), "Cannot save project file for an empty or null file name");

            string newFileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);

            anxContentProject.Name = newFileNameWithoutExtension;

            // Refresh solution explorer
            this.SetProjectProperty(ProjectFileConstants.Name, newFileNameWithoutExtension);

            Uri projectUri = new Uri(ProjectHome);
            
            //Convert from MSBuild back to ContentProject.
            anxContentProject.References.Clear();
            foreach (var reference in this.GetReferenceContainer().EnumReferences())
            {
                if (reference is AnxAssemblyReferenceNode)
                {
                    AnxAssemblyReferenceNode assemblyReference = (AnxAssemblyReferenceNode)reference;

                    if (assemblyReference.IsFrameworkAssembly)
                    {
                        GACReference anxReference = new GACReference();
                        anxReference.Name = assemblyReference.Caption;
                        anxReference.AssemblyName = assemblyReference.AssemblyName.ToString();

                        anxContentProject.References.Add(anxReference);
                    }
                    else
                    {
                        AssemblyReference anxReference = new AssemblyReference();
                        anxReference.Name = assemblyReference.Caption;
                        anxReference.AssemblyPath = assemblyReference.OriginalAssemblyPath;

                        anxContentProject.References.Add(anxReference);
                    }
                }
                else if (reference is AnxProjectReferenceNode)
                {
                    AnxProjectReferenceNode projectReferenceNode = (AnxProjectReferenceNode)reference;

                    ProjectReference anxReference = new ProjectReference()
                    {
                        Guid = projectReferenceNode.ReferencedProjectGuid,
                        Name = projectReferenceNode.ReferencedProjectName,
                        Include = projectReferenceNode.RelativeUrl,
                    };

                    if (!string.IsNullOrEmpty(projectReferenceNode.ReferencedProjectOutputPath))
                    {
                        using (var buildDomain = this.BuildAppDomain.Aquire())
                        {
                            anxReference.AssemblyPath = buildDomain.MakeRelativeToSearchPaths(projectReferenceNode.ReferencedProjectOutputPath);
                        }

                        if (Path.IsPathRooted(anxReference.AssemblyPath))
                        {
                            Uri uri = new Uri(projectReferenceNode.ReferencedProjectOutputPath);

                            anxReference.AssemblyPath = Uri.UnescapeDataString(projectUri.MakeRelativeUri(new Uri(projectReferenceNode.ReferencedProjectOutputPath)).OriginalString);
                        }
                    }

                    anxContentProject.References.Add(anxReference);
                }

            }

            anxContentProject.BuildItems.Clear();
            foreach (var file in this.EnumNodesOfType<AssetFileNode>())
            {
                if (file.IsNonMemberItem)
                    continue;
                
                BuildItem item = new BuildItem();
                item.SourceFilename = CommonUtils.GetRelativeFilePath(this.ProjectFolder, file.Url);
                item.AssetName = file.AssetName;

                if (string.IsNullOrEmpty(file.ContentImporter))
                {
                    try
                    {
                        using (var buildDomain = this.BuildAppDomain.Aquire())
                        {
                            file.ContentImporter = buildDomain.Proxy.GuessImporterByFileExtension(file.FileName);
                        }
                    }
                    catch (Exception exc)
                    {
                        Trace.TraceWarning(exc.Message);
                    }
                }

                if (string.IsNullOrEmpty(file.ContentProcessor))
                {
                    try
                    {
                        using (var buildDomain = this.BuildAppDomain.Aquire())
                        {
                            file.ContentProcessor = buildDomain.Proxy.GetDefaultProcessorForImporter(file.ContentImporter);
                        }
                    }
                    catch (Exception exc)
                    {
                        Trace.TraceWarning(exc.Message);
                    }
                }

                item.ProcessorName = file.ContentProcessor;
                item.ImporterName = file.ContentImporter;

                foreach (var pair in file.ProcessorParameters)
                {
                    item.ProcessorParameters.Add(pair.Key, pair.Value);
                }

                anxContentProject.BuildItems.Add(item);
            }

            anxContentProject.Save(filename);

            this.SetProjectFileDirty(false);
            foreach (var fileNode in this.EnumNodesOfType<AssetFileNode>())
            {
                fileNode.IsDirty = false;
            }
        }

        public override void Load(string fileName, string location, string name, uint flags, ref Guid iidProject, out int canceled)
        {
            try
            {
                this.disableQueryEdit = true;

                // set up internal members and icons
                canceled = 0;

                this.ProjectMgr = this;
                Utilities.Initialize((EnvDTE.DTE)this.GetService(typeof(EnvDTE.DTE)));

                // based on the passed in flags, this either reloads/loads a project, or tries to create a new one
                // now we create a new project... we do that by loading the template and then saving under a new name
                // we also need to copy all the associated files with it.					
                if ((flags & (uint)__VSCREATEPROJFLAGS.CPF_CLONEFILE) == (uint)__VSCREATEPROJFLAGS.CPF_CLONEFILE)
                {
                    Debug.Assert(!String.IsNullOrEmpty(fileName) && File.Exists(fileName), "Invalid filename passed to load the project. A valid filename is expected");

                    // Compute the file name
                    // We try to solve two problems here. When input comes from a wizzard in case of zipped based projects 
                    // the parameters are different.
                    // In that case the filename has the new filename in a temporay path.

                    // First get the extension from the template.
                    // Then get the filename from the name.
                    // Then create the new full path of the project.
                    string extension = Path.GetExtension(fileName);

                    string tempName = String.Empty;

                    // We have to be sure that we are not going to loose data here. If the project name is a.b.c then for a project that was based on a zipped template(the wizzard calls us) GetFileNameWithoutExtension will suppress "c".
                    // We are going to check if the parameter "name" is extension based and the extension is the same as the one from the "filename" parameter.
                    string tempExtension = Path.GetExtension(name);
                    if (!String.IsNullOrEmpty(tempExtension))
                    {
                        bool isSameExtension = (String.Compare(tempExtension, extension, StringComparison.OrdinalIgnoreCase) == 0);

                        if (isSameExtension)
                        {
                            tempName = Path.GetFileNameWithoutExtension(name);
                        }
                        // If the tempExtension is not the same as the extension that the project name comes from then assume that the project name is a dotted name.
                        else
                        {
                            tempName = Path.GetFileName(name);
                        }
                    }
                    else
                    {
                        tempName = Path.GetFileName(name);
                    }

                    Debug.Assert(!String.IsNullOrEmpty(tempName), "Could not compute project name");
                    string tempProjectFileName = tempName + extension;
                    this.FileName = Path.Combine(location, tempProjectFileName);

                    this.anxContentProject = ContentProject.Load(fileName);

                    // Initialize the common project properties.
                    this.InitializeProjectProperties();

                    ErrorHandler.ThrowOnFailure(this.Save(this.FileName, 1, 0));

                    // now we do have the project file saved. we need to create embedded files.
                    foreach (BuildItem item in this.ContentProject.BuildItems)
                    {
                        string strRelFilePath = item.SourceFilename;
                        string basePath = Path.GetDirectoryName(fileName);
                        string strPathToFile;
                        string newFileName;
                        // taking the base name from the project template + the relative pathname,
                        // and you get the filename
                        strPathToFile = Path.Combine(basePath, strRelFilePath);
                        // the new path should be the base dir of the new project (location) + the rel path of the file
                        newFileName = Path.Combine(location, strRelFilePath);
                        // now the copy file
                        AddFileFromTemplate(strPathToFile, newFileName);
                    }
                }
                else
                {
                    this.FileName = fileName;
                }

                this.Reload();
            }
            finally
            {
                this.disableQueryEdit = false;
            }
        }

        protected override void LoadProjectFile(string filename)
        {
            this.anxContentProject = ContentProject.Load(filename);

            //The project might have been loaded before and someone wanted to reload it. If we don't check, an error would be thrown if we try to set the fullPath.
            var project = ProjectCollection.GlobalProjectCollection.GetLoadedProjects(this.FileName).FirstOrDefault();
            if (project != null)
                this.BuildProject = project;
            else
            {
                this.BuildProject = new Project();
                this.BuildProject.FullPath = this.FileName;
            }
            
            base.SetProjectProperty(ProjectFileConstants.ProjectGuid, this.ProjectGuid.ToString("B"));
            base.SetProjectProperty(ProjectFileConstants.Name, this.anxContentProject.Name);
            this.CurrentConfig = this.BuildProject.CreateProjectInstance();
            
            using (var buildDomain = this.BuildAppDomain.Aquire())
            {
                buildDomain.SearchPaths.Clear();
                buildDomain.SearchPaths.Add(new Uri(this.ProjectHome));

                Uri anxFrameworkPath;
                if (BuildHelper.TryGetAnxFrameworkPath(out anxFrameworkPath))
                {
                    buildDomain.SearchPaths.Add(anxFrameworkPath);
                }

                buildDomain.Initialize(this.ProjectGuid.ToString());
            }

            if (ErrorListProvider != null)
                ErrorListProvider.Dispose();

            ErrorListProvider = new ErrorListProvider(this.Site);
            ErrorListProvider.ProviderName = this.ContentProject.Name;
            ErrorListProvider.ProviderGuid = new Guid("{90A94AFA-C6E3-42D4-ABD8-B663BEE015F0}"); //custom random guid.
        }

        protected override ReferenceContainerNode CreateReferenceContainerNode()
        {
            var referenceContainer = new ContentProjectReferenceContainer(this);

            var listener = new SolutionListenerForProjectReferences(this.Site, referenceContainer);
            this.Package.SolutionListeners.Add(listener);

            listener.Init();

            return referenceContainer;
        }

        public override void ProcessReferences()
        {
            ContentProjectReferenceContainer container = (ContentProjectReferenceContainer)GetReferenceContainer();
            if (null == container)
            {
                // This project type does not support references or there is a problem
                // creating the reference container node.
                // In both cases there is no point to try to process references, so exit.
                return;
            }
            
            // Load the referernces.
            container.LoadReferencesFromContentProject();
        }

        protected override void ProcessConfigurations()
        {
            base.ProcessConfigurations();

            var dte = (EnvDTE.DTE)this.GetService(typeof(EnvDTE.DTE));
            var configurations = dte.Solution.SolutionBuild.SolutionConfigurations.AsEnumerable();
            var names = configurations.Select((x) => x.Name).Distinct().ToArray();

            var unsupportedConfigurations = anxContentProject.Configurations.GetUniqueNames().Where((x) => !names.Contains(x)).ToArray();
            foreach (var configName in unsupportedConfigurations)
            {
                Console.WriteLine("Removed configuration \"" + configName + "\" because it's not supported by this solution.");
                foreach (var affectedConfig in anxContentProject.Configurations.Where((x) => x.Name == configName).ToArray())
                {
                    anxContentProject.Configurations.Remove(affectedConfig);
                }
            }
        }

        public override void ProcessFiles()
        {
            base.ProcessFiles();
            foreach (var item in anxContentProject.BuildItems)
            {
                HierarchyNode parentNode = this;

                var directory = Path.GetDirectoryName(item.SourceFilename);
                if (!string.IsNullOrEmpty(directory))
                {
                    var directories = directory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    string currentDir = "";
                    foreach (var dir in directories)
                    {
                        currentDir += dir + Path.DirectorySeparatorChar;
                        parentNode = this.CreateFolderNodes(currentDir, false);
                        
                    }
                }

                var projectItem = this.BuildProject.AddItem(ProjectFileConstants.Content, item.SourceFilename).FirstOrDefault();

                var asset = new AssetFileNode(this, new MsBuildProjectElement(this, projectItem));
                asset.Loading = true;
                try
                {
                    asset.AssetName = item.AssetName;
                    asset.ContentProcessor = item.ProcessorName;
                    asset.ContentImporter = item.ImporterName;

                    foreach (var pair in item.ProcessorParameters)
                    {
                        //The processor values are always saved as strings.
                        asset.ProcessorParameters.Add(pair.Key, (string)pair.Value);
                    }
                }
                finally
                {
                    asset.Loading = false;
                }

                parentNode.AddChild(asset);
            }
        }

        /// <summary>
        /// Return the value of a project property
        /// </summary>
        /// <param name="propertyName">Name of the property to get</param>
        /// <param name="resetCache">True to avoid using the cache</param>
        /// <returns>null if property does not exist, otherwise value of the property</returns>
        public override string GetProjectProperty(string propertyName, bool resetCache)
        {
            switch (propertyName)
            {
                case ProjectFileConstants.Name:
                    return this.anxContentProject.Name;
                case ProjectFileConstants.Configuration:
                    return VsUtilities.GetActiveConfigurationName((EnvDTE.Project)this.GetAutomationObject());
                case ProjectFileConstants.Platform:
                    return VsUtilities.GetActivePlatformName((EnvDTE.Project)this.GetAutomationObject());
            }
            return base.GetProjectProperty(propertyName, resetCache);
        }


        public override int GetGuidProperty(int propid, out Guid guid)
        {
            if (propid == (int)__VSHPROPID2.VSHPROPID_AddItemTemplatesGuid)
            {
                guid = GuidList.guidANXVisualStudio2012ContentProjectFactory;
                return VSConstants.S_OK;
            }
            return base.GetGuidProperty(propid, out guid);
        }

        public override void SetProjectProperty(string propertyName, string propertyValue)
        {
            switch (propertyName)
            {
                case ProjectFileConstants.Name:
                    this.anxContentProject.Name = propertyValue;
                    return;
                case ProjectFileConstants.Configuration:
                case ProjectFileConstants.Platform:
                    //Don't do anything, we don't save which configuration is currently active, for that we always use the ActiveConfiguration object from the IDE.
                    //The reason is, I don't know why the value should be saved in the first place, it always seems to depend on the active configuration and we just transfer the value
                    //into the project file.
                    return;
                case ProjectFileConstants.ProjectGuid:
                    //we don't save the guide in the project file, when we load up the project, we always get the guid set but we don't want to make the project file dirty.
                    bool isDirty = this.IsProjectFileDirty;
                    base.SetProjectProperty(propertyName, propertyValue);
                    this.SetProjectFileDirty(isDirty);
                    return;
            }

            base.SetProjectProperty(propertyName, propertyValue);
        }

        /*public void Save()
        {
            ((Microsoft.VisualStudio.Project.Automation.OAProject)this.GetAutomationObject()).Save(this.FileName);
            //VsShellUtilities.SaveFileIfDirty(this.Site, this.Url);
            // Save the project file and project file related properties.
            //Save(this.FileName, 1, 0);
        }*/

        private void Build(uint vsopts, string target, IEnumerable<string> files, string config, string platform, IVsOutputWindowPane output, Action<MSBuildResult, string> uiThreadCallback)
        {
            try
            {
                Configuration configuration;
                TargetPlatform targetPlatform = Utilities.ParseTargetPlatform(platform);
                if (!ContentProject.Configurations.TryGetConfiguration(config, targetPlatform, out configuration))
                {
                    uiThreadCallback(MSBuildResult.Failed, target);
                }
                else
                {
                    ErrorLoggingHelper loggingHelper = new ErrorLoggingHelper(this, ErrorListProvider, output);

                    try
                    {
                        this.BuildAppDomain.BuildContent(configuration, files, loggingHelper, (vsopts & (uint)VSConstants.VSStd2KCmdID.Debug) != 0, target);
                    }
                    catch (Exception exc)
                    {
                        try
                        {
                            loggingHelper.LogError(exc);
                        }
                        finally
                        {
                            uiThreadCallback(MSBuildResult.Failed, target);
                        }
                    }

                    //Do we have a build error?
                    if (ErrorListProvider.Tasks.Cast<Microsoft.VisualStudio.Shell.Task>().Where((x) => x.Priority == TaskPriority.High).Any())
                    {
                        uiThreadCallback(MSBuildResult.Failed, target);
                    }
                    else
                    {
                        uiThreadCallback(MSBuildResult.Successful, target);
                    }

                    if (ErrorListProvider.Tasks.Count > 0)
                        ErrorListProvider.Show();
                }
            }
            catch
            {
                uiThreadCallback(MSBuildResult.Failed, target);
            }
        }

        protected override int StartDebug()
        {
            string configName = this.GetProjectProperty(ProjectFileConstants.Configuration);
            string platform = this.GetProjectProperty(ProjectFileConstants.Platform);

            IVsCfgProvider cfgProvider;
            if (this.GetCfgProvider(out cfgProvider) == VSConstants.S_OK)
            {
                IVsCfg config;
                if (((ConfigProvider)cfgProvider).GetCfgOfName(configName, platform, out config) == VSConstants.S_OK)
                {
                    ((Config)config).DebugLaunch((uint)__VSDBGLAUNCHFLAGS.DBGLAUNCH_Selected);

                    return VSConstants.S_OK;
                }
            }

            return VSConstants.E_FAIL;
        }

        public override void PrepareBuild(uint vsopts, string config, string platform, bool cleanBuild)
        {
            ErrorListProvider.Tasks.Clear();

            base.PrepareBuild(vsopts, config, platform, cleanBuild);

            BuildAppDomain.PrepareBuild(cleanBuild);
        }

        public override void BuildAsync(uint vsopts, string config, string platform, IVsOutputWindowPane output, string target, IEnumerable<string> files, Action<MSBuildResult, string> uiThreadCallback)
        {
            System.Threading.Tasks.Task.Run(() =>
                    {
                        TriggerBuild(vsopts, target, config, platform, files, output, uiThreadCallback);
                    });
        }

        public override MSBuildResult InvokeMsBuild(uint vsopts, string target,  string config, string platform, IEnumerable<string> files)
        {
            return TriggerBuild(vsopts, target, config, platform, files);
        }

        private MSBuildResult TriggerBuild(uint vsopts, string target, string config, string platform, IEnumerable<string> files)
        {
            IVsOutputWindowPane pane;
            if (this.BuildLogger is IDEBuildLogger)
                pane = ((IDEBuildLogger)this.BuildLogger).OutputWindowPane;
            else
                pane = this.Package.GetOutputPane(Microsoft.VisualStudio.VSConstants.SID_SVsGeneralOutputWindowPane, null);

            return this.TriggerBuild(vsopts, target, config, platform, files, pane, null);
        }

        private MSBuildResult TriggerBuild(uint vsopts, string target, string config, string platform, IEnumerable<string> files, IVsOutputWindowPane output, Action<MSBuildResult, string> uiThreadCallback)
        {
            MSBuildResult result = MSBuildResult.Failed;
            const bool designTime = true;
            bool requiresUIThread = UIThread.Instance.IsUIThread; // we don't run tasks that require calling the STA thread, so unless we're ON it, we don't need it.

            IVsBuildManagerAccessor accessor = this.Site.GetService(typeof(SVsBuildManagerAccessor)) as IVsBuildManagerAccessor;
            BuildSubmission submission = null;

            if (!TryBeginBuild(designTime, requiresUIThread))
            {
                throw new InvalidOperationException("A build is already in progress.");
            }

            try
            {
                Build(vsopts, target, files, config, platform, output, (buildResult, buildTarget) => 
                { 
                    result = buildResult;
                    if (uiThreadCallback != null)
                        uiThreadCallback(buildResult, buildTarget);

                });
            }
            finally
            {
                EndBuild(vsopts, submission, designTime, requiresUIThread);
            }

            return result;
        }

        protected override void EndBuild(uint vsopts, BuildSubmission submission, bool designTime, bool requiresUIThread = false)
        {
            BuildAppDomain.EndBuild();

            base.EndBuild(vsopts, submission, designTime, requiresUIThread);

            if (this.PendingBuild != null)
            {
                this.PendingBuild = null;

                //A bit of a hack because I wasn't able to find out how to finish a build when it was started by our extension.
                //This causes several seconds delay until the build is actually marked as done.
                this.DTE.ExecuteCommand("Build.Cancel");
            }
            
        }

        public override int QueryStatusOnNode(Guid cmdGroup, uint cmd, IntPtr pCmdText, ref QueryStatusResult result)
        {
            if (cmdGroup == CommonConstants.Std97CmdGroupGuid)
            {
                switch ((VSConstants.VSStd97CmdID)cmd)
                {
                    case VSConstants.VSStd97CmdID.BuildCtx:
                    case VSConstants.VSStd97CmdID.RebuildCtx:
                        result |= QueryStatusResult.SUPPORTED | QueryStatusResult.ENABLED;
                        return VSConstants.S_OK;

                    case VSConstants.VSStd97CmdID.CleanCtx:
                        result |= QueryStatusResult.NOTSUPPORTED | QueryStatusResult.INVISIBLE;
                        return VSConstants.S_OK;
                }
            }
            else if (cmdGroup == Microsoft.VisualStudio.Project.VsMenus.guidStandardCommandSet2K)
            {
                switch ((VsCommands2K)cmd)
                {
                    case VsCommands2K.ECMD_PUBLISHSELECTION:
                        result |= QueryStatusResult.NOTSUPPORTED | QueryStatusResult.INVISIBLE;
                        return VSConstants.S_OK;

                    case VsCommands2K.ECMD_PUBLISHSLNCTX:
                        result |= QueryStatusResult.NOTSUPPORTED | QueryStatusResult.INVISIBLE;
                        return VSConstants.S_OK;
                }
            }

            return base.QueryStatusOnNode(cmdGroup, cmd, pCmdText, ref result);
        }

        public override CommonFileNode CreateNonCodeFileNode(ProjectElement item)
        {
            string fileName = item.GetMetadata(ProjectFileConstants.Include);
            var file = new AssetFileNode(this, item)
            {
                AssetName = Path.GetFileNameWithoutExtension(fileName),
            };

            return file;
        }

        public override string GetExistingFilesFilter()
        {
            using (var buildDomain = this.BuildAppDomain.Aquire())
            {
                return buildDomain.Proxy.GetExistingFilesFilter();
            }
        }

        public override Type GetProjectFactoryType()
        {
            return typeof(ContentProjectFactory);
        }

        public override Type GetEditorFactoryType()
        {
            return null;
        }

        public override string GetProjectName()
        {
            return this.GetProjectProperty(ProjectFileConstants.Name);
        }

        public override string GetFormatList()
        {
            return "Content Project Files (*.cproj)|*.cproj";
        }

        public override Type GetLibraryManagerType()
        {
            return null;
        }

        public override IProjectLauncher GetLauncher()
        {
            return new ContentProjectLauncher(this);
        }

        protected override NodeProperties CreatePropertiesObject()
        {
            return new ContentProjectNodeProperties(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.buildAppDomain != null)
                {
                    this.buildAppDomain.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /*public override System.Runtime.Versioning.FrameworkName TargetFrameworkMoniker
        {
            get
            {
                return ContentProject.DotNetFramework;
            }
            set
            {
                if (ContentProject.DotNetFramework != value)
                {
                    var oldFramework = ContentProject.DotNetFramework;
                    ContentProject.DotNetFramework = value;

                    UpdateTargetFramework(GetOuterHierarchy(), oldFramework.FullName, value.FullName);
                }
            }
        }*/
    }
}
