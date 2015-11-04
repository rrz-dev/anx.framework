using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Helpers;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Design;
using ANX.Framework.Graphics;
using ANX.Framework.VisualStudio;
using ANX.Framework.VisualStudio.Nodes;
using ContentBuilder;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ANX.Framework.Build
{
    public sealed class BuildAppDomain : IDisposable
    {
        public class RemoteProxy : MarshalByRefObject
        {
            private List<string> processorNames = new List<string>();
            private ProcessorManager processorManager = null;
            private bool processorNamesInvalid = true;

            private ImporterManager importerManager = null;
            private bool importerManagerInvalid = true;
            private List<string> importerNames = new List<string>();

            public override object InitializeLifetimeService()
            {
                return null;
            }

            public void LoadProjectAssemblies(IEnumerable<string> assemblies, IEnumerable<Uri> searchPaths, IDictionary<string, Uri> redirectAssemblies, Action<string, Exception> errorCallback)
            {
                if (assemblies == null)
                    throw new ArgumentNullException("assemblies");

                if (searchPaths == null)
                    throw new ArgumentNullException("searchPaths");

                if (redirectAssemblies == null)
                    throw new ArgumentNullException("redirectAssemblies");

                foreach (string assembly in assemblies)
                {
                    try
                    {
                        Assembly assemblyInstance = null;
                        if (File.Exists(assembly))
                        {
                            string assemblyName = Path.GetFileNameWithoutExtension(assembly);
                            if (IsAssemblyAlreadyLoaded(assemblyName))
                                continue;

                            assemblyInstance = LoadFrom(assembly, redirectAssemblies);
                        }
                        else
                        {
                            if (IsAssemblyAlreadyLoaded(assembly))
                                continue;

                            Uri assemblyUri;
                            if (Uri.TryCreate(assembly.Split(',').First() + ".dll", UriKind.RelativeOrAbsolute, out assemblyUri))
                            {
                                foreach (Uri path in searchPaths)
                                {
                                    Uri temp = new Uri(path, assemblyUri);
                                    if (File.Exists(temp.LocalPath))
                                    {
                                        assemblyInstance = LoadFrom(temp.LocalPath, redirectAssemblies);
                                        break;
                                    }
                                }

                                if (assemblyInstance == null)
                                    assemblyInstance = Assembly.Load(assembly);
                            }
                            else
                            {
                                assemblyInstance = Assembly.Load(assembly);
                            }
                        }

                        ClassPreloader.Preload(assemblyInstance);
                    }
                    catch (Exception exc)
                    {
                        if (errorCallback != null)
                            errorCallback(assembly, exc);
                        else
                            throw;
                    }
                }

                this.processorManager = null;
                this.processorNamesInvalid = true;
                this.importerManager = null;
                this.importerManagerInvalid = true;
            }

            private bool IsAssemblyAlreadyLoaded(string assemblyName)
            {
                bool isFullName = assemblyName.Contains(',');
                var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                if (isFullName)
                {
                    foreach (var loadedAssembly in loadedAssemblies)
                        if (loadedAssembly.FullName == assemblyName)
                        {
                            return true;
                        }
                }
                else
                {
                    foreach (var loadedAssembly in loadedAssemblies)
                        if (loadedAssembly.GetName().Name == assemblyName)
                        {
                            return true;
                        }
                }

                return false;
            }

            private Assembly LoadFrom(string assembly, IDictionary<string, Uri> redirectAssemblies)
            {
                string assemblyName = assembly;
                if (Path.IsPathRooted(assembly))
                    assemblyName = Path.GetFileNameWithoutExtension(assembly);
                else if (assembly.Contains(','))
                    assemblyName = assembly.Split(',').First();

                Uri redirectedPath;
                if (redirectAssemblies.TryGetValue(assemblyName, out redirectedPath))
                {
                    return Assembly.LoadFrom(redirectedPath.LocalPath);
                }
                else
                {
                    return Assembly.LoadFrom(assembly);
                }
            }

            public string GetAssemblyRuntimeVersion(string assemblyPath)
            {
                if (File.Exists(assemblyPath))
                {
                    return Assembly.ReflectionOnlyLoadFrom(assemblyPath).ImageRuntimeVersion;
                }

                return null;
            }

            public IEnumerable<AssemblyName> GetReferencedAssemblies(string assembly)
            {
                if (File.Exists(assembly))
                {
                    return Assembly.ReflectionOnlyLoadFrom(assembly).GetReferencedAssemblies();
                }
                else
                {
                    return Assembly.ReflectionOnlyLoad(assembly).GetReferencedAssemblies();
                }
            }

            public string GetExistingFilesFilter()
            {
                ImporterManager importerManager = new ImporterManager();

                SortedDictionary<string, List<string>> supportedFileExtensions = new SortedDictionary<string, List<string>>();
                List<string> allExtensions = new List<string>();

                foreach (var valuePair in importerManager.AvailableImporters)
                {
                    var attribute = valuePair.Value.GetCustomAttribute<ContentImporterAttribute>(true);
                    IEnumerable<string> fileExtensions = attribute.FileExtensions;
                    string category = attribute.Category;

                    if (fileExtensions.Count() == 0)
                        continue;

                    if (string.IsNullOrWhiteSpace(category))
                    {
                        category = "Other Files";
                    }

                    category = category.Trim();

                    List<string> extensions;
                    if (!supportedFileExtensions.TryGetValue(category, out extensions))
                    {
                        extensions = new List<string>();
                        supportedFileExtensions.Add(category, extensions);
                    }

                    foreach (var fileExtension in fileExtensions)
                    {
                        if (!string.IsNullOrWhiteSpace(fileExtension))
                        {
                            string usableExtension = "*" + fileExtension;
                            if (!extensions.Contains(usableExtension))
                            {
                                extensions.Add(usableExtension);
                                if (!allExtensions.Contains(usableExtension))
                                {
                                    allExtensions.Add(usableExtension);
                                }
                            }
                        }
                    }
                }

                string allExtensionsJoined = string.Join(";", allExtensions);
                string filter = "Content Project Files|" + allExtensionsJoined;

                foreach (var valuePair in supportedFileExtensions)
                {
                    string extensionsJoined = string.Join(";", valuePair.Value);
                    filter += "|" + valuePair.Key + "|" + extensionsJoined;
                }

                return filter;
            }

            public IEnumerable<string> GetProcessorNames()
            {
                if (processorNamesInvalid)
                {
                    processorNames.Clear();

                    if (processorManager == null)
                    {
                        processorManager = new ProcessorManager();
                    }

                    foreach (var value in processorManager.AvailableProcessors)
                    {
                        processorNames.Add(value.Key);
                    }

                    processorNamesInvalid = false;
                }

                return processorNames;
            }

            public IEnumerable<string> GetImporterNames()
            {
                if (importerManagerInvalid)
                {
                    importerNames.Clear();

                    if (importerManager == null)
                    {
                        importerManager = new ImporterManager();
                    }

                    foreach (var value in importerManager.AvailableImporters)
                    {
                        importerNames.Add(value.Key);
                    }

                    importerManagerInvalid = false;
                }

                return importerNames;
            }

            public string GetImporterName(string displayName)
            {
                if (importerManager == null)
                {
                    importerManager = new ImporterManager();
                }

                return importerManager.GetImporterName(displayName);
            }

            public string GetImporterDisplayName(string importer)
            {
                if (importerManager == null)
                {
                    importerManager = new ImporterManager();
                }

                return importerManager.GetImporterDisplayName(importer);
            }

            public string GetProcessorName(string displayName)
            {
                if (processorManager == null)
                {
                    processorManager = new ProcessorManager();
                }

                return processorManager.GetProcessorName(displayName);
            }

            public string GetProcessorDisplayName(string processor)
            {
                if (processorManager == null)
                {
                    processorManager = new ProcessorManager();
                }

                return processorManager.GetProcessorDisplayName(processor);
            }

            public string GetProcessorTypeName(string processor)
            {
                if (processorManager == null)
                {
                    processorManager = new ProcessorManager();
                }

                return processorManager.GetInstance(processor).GetType().AssemblyQualifiedName;
            }

            public string GuessImporterByFileExtension(string fileName)
            {
                if (importerManager == null)
                {
                    importerManager = new ImporterManager();
                }

                return importerManager.GuessImporterByFileExtension(fileName);
            }

            public string GetDefaultProcessorForImporter(string importer)
            {
                if (importerManager == null)
                    importerManager = new ImporterManager();

                return importerManager.GetDefaultProcessor(importer);
            }

            public ICollection<ANX.Framework.VisualStudio.ProcessorParameter> GetProcessorParameters(string processor)
            {
                if (processorManager == null)
                    processorManager = new ProcessorManager();

                List<ANX.Framework.VisualStudio.ProcessorParameter> result = new List<ANX.Framework.VisualStudio.ProcessorParameter>();

                if (!string.IsNullOrEmpty(processor))
                {
                    ANX.Framework.Content.Pipeline.ProcessorParameterCollection processorParams = processorManager.GetProcessorParameters(processor);
                    foreach (var parameter in processorParams)
                    {
                        string defaultValueText = null;
                        string valueText = null;

                        object processorInstance = processorManager.GetInstance(processor);
                        Type processorType = processorInstance.GetType();
                        Type propertyType = TypeHelper.GetType(parameter.PropertyType);

                        PropertyInfo property = processorType.GetProperty(parameter.PropertyName, propertyType);

                        TypeConverter converter = property.GetConverter();
                        if (converter != null)
                        {
                            defaultValueText = (string)converter.ConvertTo(parameter.DefaultValue, typeof(string));
                            valueText = (string)converter.ConvertTo(property.GetValue(processorInstance), typeof(string));
                        }

                        result.Add(new ANX.Framework.VisualStudio.ProcessorParameter(parameter.PropertyName, parameter.DisplayName, parameter.PropertyType, valueText, defaultValueText, parameter.Description));
                    }
                }

                return result;
            }

            public string GetConverterTypeName(string containerType, string propertyName)
            {
                Type type = TypeHelper.GetType(containerType);
                PropertyInfo property = type.GetProperty(propertyName);

                TypeConverter converter = property.GetConverter();
                if (converter != null)
                    return converter.GetType().AssemblyQualifiedName;

                return null;
            }

            private object CreateInstance(Type targetType, Type typeParam)
            {
                var parameterTypes = new Type[] { typeof(Type) };

                ConstructorInfo constructor = targetType.GetConstructor(parameterTypes);
                if (constructor != null)
                {
                    return TypeDescriptor.CreateInstance(null, targetType, parameterTypes, new object[] { typeParam });
                }
                return TypeDescriptor.CreateInstance(null, targetType, null, null);
            }

            public IEnumerable<string> GetAssemblyLocations()
            {
                List<string> locations = new List<string>();
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.CodeBase == null)
                    {
                        locations.Add(assembly.Location);
                    }
                    else
                    {
                        locations.Add(new Uri(assembly.CodeBase).LocalPath);
                    }
                }

                return locations;
            }

            /// <summary>
            /// Returns a proxy for an editor.
            /// The returned value should be wrapped by <see cref="UITypeEditorWrapper"/>
            /// </summary>
            /// <param name="editorBaseType">The wanted base type for the editor. Currently only supports <see cref="UITypeEditor"/>.</param>
            /// <param name="typeName"></param>
            /// <param name="propertyName"></param>
            /// <param name="converterProxy">Must be a proxy to the <see cref="WrappedConverter"/> class.</param>
            /// <returns></returns>
            public IProxy GetEditor(Type editorBaseType, string typeName, string propertyName, IProxy converterProxy)
            {
                if (editorBaseType != typeof(UITypeEditor))
                {
                    return null;
                }

                Type type = TypeHelper.GetType(typeName);

                PropertyInfo property = type.GetProperty(propertyName);

                UITypeEditor editor;
                var attributes = property.GetCustomAttributes<EditorAttribute>(true);
                foreach (var attribute in attributes)
                {
                    if (attribute.EditorBaseTypeName == editorBaseType.AssemblyQualifiedName)
                    {
                        Type editorType = TypeHelper.GetType(attribute.EditorTypeName);
                        if (editorType != null && typeof(UITypeEditor).IsAssignableFrom(editorType))
                        {
                            editor = (UITypeEditor)this.CreateInstance(editorType, property.PropertyType);
                            if (editor != null)
                            {
                                return UITypeEditorWrapper.CreateProxy(editor, converterProxy);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }

                editor = (UITypeEditor)TypeDescriptor.GetEditor(property.PropertyType, editorBaseType);

                if (editor != null)
                {
                    return UITypeEditorWrapper.CreateProxy(editor, converterProxy);
                }
                else
                {
                    return null;
                }
            }

            public string[] GetPlatformDisplayNames()
            {
                return Utilities.GetTargetPlatformDisplayNames();
            }

            public string[] GetGraphicsProfilesNames()
            {
                return Enum.GetNames(typeof(GraphicsProfile));
            }

            public bool IsUpDoDate(string projectHome, IEnumerable<BuildItem> buildItems, Configuration activeConfiguration, ErrorLoggingHelper loggingHelper)
            {
                BuildContentTask task = new BuildContentTask();
                task.TargetPlatform = activeConfiguration.Platform;
                task.TargetProfile = activeConfiguration.Profile;

                var buildCache = new AnxBuildCache(task.ImporterManager, task.ProcessorManager, task.ContentCompiler);
                buildCache.ProjectHome = new Uri(projectHome, UriKind.Absolute);

                task.BuildCache = buildCache;

                string intermediateDirectory = Path.Combine(projectHome, "obj", CreateSafeFileName(activeConfiguration.Platform.ToDisplayName()), CreateSafeFileName(activeConfiguration.Name)) + Path.DirectorySeparatorChar;

                var buildCacheUri = new Uri(new Uri(intermediateDirectory, UriKind.Absolute), new Uri("build.cache", UriKind.Relative));

                try
                {
                    buildCache.LoadCache(buildCacheUri);
                }
                catch
                {
                    return false;
                }

                foreach (var buildItem in buildItems)
                {
                    if (!buildCache.IsValid(buildItem, new Uri(BuildHelper.GetOutputFileName(activeConfiguration.OutputDirectory, projectHome, buildItem), UriKind.Relative)))
                        return false;
                }

                return true;
            }

            private string CreateSafeFileName(string text)
            {
                foreach (var unsafeChar in Path.GetInvalidFileNameChars())
                    text = text.Replace(unsafeChar, '_');

                return text;
            }
        }

        //pretty much the same as if one would use lock(appDomain) but as a public api.
        public sealed class BuildDomainAquire : IDisposable
        {
            BuildAppDomain appDomain;

            internal BuildDomainAquire(BuildAppDomain appDomain)
            {
                Monitor.Enter(appDomain);
                this.appDomain = appDomain;
            }

            public RemoteProxy Proxy
            {
                get
                {
                    return this.appDomain.proxyInstance;
                }
            }

            public void Unload()
            {
                this.appDomain.Unload();
            }

            public void Initialize(string identifier)
            {
                appDomain.Initialize(identifier);
            }

            public T CreateInstanceAndUnwrap<T>()
            {
                return appDomain.CreateInstanceAndUnwrap<T>();
            }

            public List<Uri> SearchPaths
            {
                get { return appDomain.SearchPaths; }
            }

            public Dictionary<string, Uri> Redirects
            {
                get { return appDomain.Redirects; }
            }

            public String MakeRelativeToSearchPaths(String path)
            {
                if (path == null)
                    throw new ArgumentNullException("path");

                Uri uri = new Uri(path);
                if (!uri.IsAbsoluteUri)
                    throw new ArgumentException("uri must be absolute.");

                return uri.MakeRelativeUri(SearchPaths).OriginalString;
            }

            public Uri MakeRelativeToSearchPaths(Uri uri)
            {
                if (uri == null)
                    throw new ArgumentNullException("uri");

                if (!uri.IsAbsoluteUri)
                    throw new ArgumentException("uri must be absolute.");

                return uri.MakeRelativeUri(SearchPaths);
            }

            public String MakeAbsoluteFromSearchPaths(String path)
            {
                if (path == null)
                    throw new ArgumentNullException("path");

                Uri uri;
                if (!Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out uri))
                    return path;

                if (uri.IsAbsoluteUri)
                {
                    return path;
                }

                foreach (Uri searchPath in SearchPaths)
                {
                    Uri tempPath = new Uri(searchPath, uri);
                    if (File.Exists(tempPath.LocalPath))
                        return tempPath.OriginalString;
                }

                return uri.OriginalString;
            }

            public Uri MakeAbsoluteFromSearchPaths(Uri uri)
            {
                if (uri == null)
                    throw new ArgumentNullException("uri");

                if (uri.IsAbsoluteUri)
                {
                    return uri;
                }

                foreach (Uri path in SearchPaths)
                {
                    Uri tempPath = new Uri(path, uri);
                    if (File.Exists(tempPath.LocalPath))
                        return tempPath;
                }

                return uri;
            }

            public Uri[] ShadowCopyDirectories
            {
                get { return appDomain.ShadowCopyDirectories; }
                set { appDomain.ShadowCopyDirectories = value; }
            }

            public void AddShadowCopyDirectory(Uri path)
            {
                appDomain.AddShadowCopyDirectory(path);
            }

            public void RemoveShadowCopyDirectory(Uri path)
            {
                appDomain.RemoveShadowCopyDirectory(path);
            }

            public void Release()
            {
                Monitor.Exit(appDomain);
            }

            void IDisposable.Dispose()
            {
                this.Release();
            }
        }

        object buildLock = new object();
        bool disposed = false;
        AppDomain appDomain;
        RemoteProxy proxyInstance;
        List<Uri> searchPaths = new List<Uri>();
        //string shadowCopyPath;
        Uri[] sourceShadowCopyDirectories = new Uri[0];
        Dictionary<string, Uri> redirects = new Dictionary<string, Uri>();
        ServiceHost loggerServiceHost;
        ContentProjectNode projectNode;
        Process buildProcess;

        public BuildAppDomain(ContentProjectNode projectNode)
        {
            this.SearchPaths.Add(new Uri(Path.GetDirectoryName(typeof(BuildAppDomain.RemoteProxy).Assembly.Location), UriKind.Absolute));
            this.Redirects.Add(typeof(Color).Assembly.GetName().Name, new Uri(typeof(Color).Assembly.CodeBase, UriKind.Absolute)); //ANX.Framework
            this.Redirects.Add(typeof(IContentProcessor).Assembly.GetName().Name, new Uri(typeof(IContentProcessor).Assembly.CodeBase, UriKind.Absolute)); //ANX.Framework.Content.Pipeline

            this.projectNode = projectNode;
        }

        public BuildDomainAquire Aquire()
        {
            if (disposed)
                throw new ObjectDisposedException("BuildAppDomain");

            return new BuildDomainAquire(this);
        }

        public bool IsDisposed
        {
            get { return disposed; }
        }

        private List<Uri> SearchPaths
        {
            get 
            {
                return searchPaths;
            }
        }

        private Dictionary<string, Uri> Redirects
        {
            get
            {
                return redirects;
            }
        }

        public bool IsBuildRunning
        {
            get
            {
                return this.buildProcess != null && !this.buildProcess.HasExited;
            }
        }

        private void AddShadowCopyDirectory(Uri path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            if (!path.IsAbsoluteUri)
                throw new ArgumentException("path must be absolute.");

            var newDirs = new Uri[sourceShadowCopyDirectories.Length + 1];
            Array.Copy(sourceShadowCopyDirectories, newDirs, sourceShadowCopyDirectories.Length);
            newDirs[newDirs.Length - 1] = path;

            ShadowCopyDirectories = newDirs;
        }

        private void RemoveShadowCopyDirectory(Uri path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            if (!path.IsAbsoluteUri)
                throw new ArgumentException("path must be absolute.");

            int index = Array.IndexOf(sourceShadowCopyDirectories, path);
            if (index == -1)
                return;

            var newDirs = new Uri[sourceShadowCopyDirectories.Length - 1];
            Array.Copy(sourceShadowCopyDirectories, newDirs, index);
            if (newDirs.Length - index > 0)
                Array.Copy(sourceShadowCopyDirectories, index + 1, newDirs, index, newDirs.Length - index);

            ShadowCopyDirectories = newDirs;
        }

        //Disable obsolete code warning
        //Settings the shadow copy path for an appDomain is declared obsolete,  it says I should use the shadow copy path in the appDomainSetup.
        //Doing that would require to recreate the appDomain whenever a new reference is added and to batch the adding of references.
        //Doing it this way was just the easier way.
        #pragma warning disable 618

        private Uri[] ShadowCopyDirectories
        {
            get
            {
                return sourceShadowCopyDirectories.ToArray();
            }
            set
            {
                if (value == null)
                {
                    sourceShadowCopyDirectories = new Uri[0];
                    appDomain.SetShadowCopyPath("");
                }
                else
                {
                    if (!value.All((x) => x != null && x.IsAbsoluteUri))
                    {
                        throw new ArgumentException("All Uri's must be not null and absolute.");
                    }

                    sourceShadowCopyDirectories = value;
                    appDomain.SetShadowCopyPath(string.Join(";", sourceShadowCopyDirectories.Where((x) => x != null).Select((x) => x.LocalPath)));
                }
            }
        }

        //Not behind the lock because we only use that has been given in the parameters and we're not using any data from foreign assemblies.
        //Another reason is, when we start debugging, some project properties get refreshed and we need access to the lock for that.
        public void BuildContent(Configuration activeConfiguration, IEnumerable<string> files, ErrorLoggingHelper loggingHelper, bool debug, string target)
        {
            if (activeConfiguration == null)
                throw new ArgumentNullException("activeConfiguration");

            if (loggingHelper == null)
                throw new ArgumentNullException("loggingHelper");

            string projectHome = this.projectNode.ProjectHome;
            List<string> arguments = new List<string>();

            VisualStudioContentBuildLogger logger = new VisualStudioContentBuildLogger(loggingHelper);

            arguments.Add(this.projectNode.AbsoluteProjectFilePath);
            arguments.Add("-c:" + activeConfiguration.Name);
            arguments.Add("-t:" + activeConfiguration.Platform);
            arguments.Add("-cd:" + projectHome);

            if (target == MsBuildTarget.Rebuild)
                arguments.Add("-ForceRebuild");
            else if (target == MsBuildTarget.Clean)
                arguments.Add("-CleanCache");

            if (files != null && files.Count() > 0)
            {
                arguments.Add("-DontAddProjectBuildItems");

                foreach (string file in files)
                {
                    if (file.StartsWith(projectHome))
                        arguments.Add(file.Substring(projectHome.Length));
                    else
                        arguments.Add(file);
                }
            }

            StartContentBuilder(arguments, debug, logger);
        }

        private void StartContentBuilder(IList<string> arguments, bool debug, IContentBuildLogger logger, int? millisecondsTimeOut = null)
        {
            if (buildProcess != null)
            {
                if (buildProcess.HasExited)
                    this.EndBuild();
                else
                    throw new InvalidOperationException("There is still a build process running.");
            }

            lock (buildLock)
            {
                string workingDir = Path.GetDirectoryName(this.GetType().Assembly.Location);
                string exePath = Path.Combine(workingDir, "ContentBuilder.exe");

                Uri loggerUri = new Uri("net.pipe://localhost/VisualStudio/" + Process.GetCurrentProcess().Id + "/");

                if (loggerServiceHost != null)
                    loggerServiceHost.Close();

                loggerServiceHost = new ServiceHost(logger, loggerUri);
                try
                {
                    loggerServiceHost.AddServiceEndpoint(typeof(IContentBuildLogger), new NetNamedPipeBinding(), "ContentBuildLogger");

                    loggerServiceHost.Open();

                    arguments.Add("-l:" + new Uri(loggerUri, new Uri("ContentBuildLogger", UriKind.RelativeOrAbsolute)).OriginalString);

                    if (debug)
                        arguments.Add("-Debug");

                    //Prepare the arguments to be used for process start.
                    var args = arguments.Select((x) => "\"" + Regex.Replace(x, "(\\\\*)(\\\\$|\")", "$1$1\\$2") + "\"");

                    if (debug)
                    {
                        //May have to change the debugger interface for newer versions of visual studio.
                        var debugger = this.projectNode.Site.GetService(typeof(SVsShellDebugger)) as IVsDebugger4;

                        var targetInfo = new VsDebugTargetInfo4()
                        {
                            bstrArg = string.Join(" ", args),
                            bstrCurDir = workingDir,
                            bstrExe = exePath,
                            guidLaunchDebugEngine = Microsoft.VisualStudio.VSConstants.DebugEnginesGuids.ManagedOnly_guid,
                            project = this.projectNode,
                            dlo = (uint)DEBUG_LAUNCH_OPERATION.DLO_CreateProcess,
                        };

                        VsDebugTargetProcessInfo[] result = new VsDebugTargetProcessInfo[1];
                        debugger.LaunchDebugTargets4(1, new VsDebugTargetInfo4[] { targetInfo }, result);

                        buildProcess = Process.GetProcessById((int)result[0].dwProcessId);
                    }
                    else
                    {
                        buildProcess = Process.Start(new ProcessStartInfo(exePath, string.Join(" ", args))
                        {
                            CreateNoWindow = true,
                            UseShellExecute = false,
                            WorkingDirectory = workingDir,
                        });
                    }

                    if (millisecondsTimeOut.HasValue)
                        buildProcess.WaitForExit(millisecondsTimeOut.Value);
                    else
                        buildProcess.WaitForExit();
                }
                catch
                {
                    if (loggerServiceHost != null)
                        loggerServiceHost.Abort();

                    throw;
                }
            }
        }

        public void EndBuild()
        {
            lock (buildLock)
            {
                if (buildProcess != null)
                {
                    buildProcess.Close();
                    buildProcess = null;
                }

                if (loggerServiceHost != null)
                {
                    loggerServiceHost.Close();
                    loggerServiceHost = null;
                }
            }
        }

        public void PrepareBuild(bool cleanBuild)
        {
            if (cleanBuild)
                return;

            lock (buildLock)
            {
                ErrorLoggingHelper loggingHelper = new ErrorLoggingHelper(this.projectNode, this.projectNode.ErrorListProvider, null);
                //Test if ANX.Framework or ANX.Framework.Content.Pipeline are referenced, because we must use our own version.
                foreach (var reference in this.projectNode.GetReferenceContainer().EnumReferences())
                {
                    if (Redirects.ContainsKey(reference.Caption))
                    {
                        loggingHelper.LogMessage("", null, reference.Url, ErrorLoggingHelper.MessageImportance.Info, PackageResources.GetString(PackageResources.AnxFrameworkAssembliesRedirected), reference.Caption);
                    }
                }
            }
        }

        #pragma warning restore 618

        private void Unload()
        {
            EndBuild();

            if (appDomain != null)
            {
                proxyInstance = null;
                AppDomain.Unload(appDomain);
                appDomain = null;
            }
        }

        private void Initialize(string identifier)
        {
            if (appDomain != null)
            {
                throw new InvalidOperationException("The AppDomain is already initialized.");
            }
 
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = Path.GetDirectoryName(typeof(BuildAppDomain.RemoteProxy).Assembly.Location);
            
            //if (SearchPath != null)
            //    setup.ApplicationBase = SearchPath.OriginalString;

            setup.ApplicationName = "ANX Project " + identifier;
            setup.ShadowCopyFiles = "true"; //Shadow copying so that files like dll's of referenced projects can still be updated.

            //I can't shadow copy everything, otherwise I wouldn't be able to use the remoteProxy in the other appDomain because if the location of the loaded assembly differs from our own, I can't cast the proxy
            //in our appDomain.
            setup.ShadowCopyDirectories = string.Join(";", ShadowCopyDirectories.Select((x) => x.LocalPath));
            //setup.CachePath = Path.Combine(Path.GetTempPath(), "ANX Project ShadowCopies");

            //I might need to setup a custom shadow copy path, because the default path is restricted by space. But if I do that, I would also have to take care of the cleanup.
            //this.shadowCopyPath = Path.Combine(setup.CachePath, setup.ApplicationName);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            appDomain = AppDomain.CreateDomain("ANX Project " + identifier, null, setup);
            appDomain.AssemblyResolve += BuildDomainAssemblyResolver.appDomain_AssemblyResolve;
            //appDomain.TypeResolve += BuildDomainAssemblyResolver.appDomain_AssemblyResolve;
            //appDomain.Load(typeof(ContentProject).Assembly.FullName); //ANX.Framework.Build

            proxyInstance = (RemoteProxy)appDomain.CreateInstanceAndUnwrap(typeof(BuildAppDomain.RemoteProxy).Assembly.FullName, typeof(BuildAppDomain.RemoteProxy).FullName);
        }

        [Serializable]
        static class BuildDomainAssemblyResolver
        {
            //Help placing project assemblies from the loadFrom context into the load context, only that way we can find the contained types later by using Type.GetType(string).
            public static Assembly appDomain_AssemblyResolve(object sender, ResolveEventArgs args)
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.FullName == args.Name)
                        return assembly;
                }
                
                return null;
            }
        }

        //Help placing our own assemblies into the load context of the newly created appDomain
        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where((x) => x.FullName == args.Name).FirstOrDefault();
            if (assembly != null)
                return assembly;

            return null;
        }

        private T CreateInstanceAndUnwrap<T>()
        {
            return (T)appDomain.CreateInstanceAndUnwrap(typeof(T).Assembly.FullName, typeof(T).FullName);
        }

        public void Dispose()
        {
            Unload();

            disposed = true;

            /*if (Directory.Exists(shadowCopyPath))
            {
                //If the directory is still used by another process, the delete should silently fail.
                try
                {
                    Directory.Delete(shadowCopyPath, true);
                }
                catch
                { }
            }*/
        }
    }
}
