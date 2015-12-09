#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Content.Pipeline;
using System.IO;
using System.Reflection;
using ANX.Framework.Graphics;
using System.Diagnostics;
using ANX.Framework.Content.Pipeline.Helpers;
using Microsoft.Win32;
using System.ServiceModel;
using System.Threading;
using ANX.Framework.Content.Pipeline.Tasks.References;
using ANX.Framework.Content;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ContentBuilder
{
    class Program
    {
        private static ICommunicationObject remoteLogger = null;

        static void Main(string[] args)
        {
            //Welcome on the console, but we don't output it to the build logger.
            Console.WriteLine(String.Format("ANX.Framework {0} v{1}", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version));

            #region local var declarations

            BuildContentTask buildContentTask = new BuildContentTask();
            buildContentTask.BuildLogger.Childs.Add(new ConsoleContentBuildLogger());

            string projectFile = null;
            List<string> buildItems = new List<string>();
            string configurationName = null;
            string loggerName = null;
            List<string> referencedAssemblies = new List<string>();
            string outputDirectory = null;
            string intermediateDirectory = null;
            string targetPlatformName = null;
            string graphicsProfileName = null;
            string currentDirectory = null;
            bool dontAddProjectBuildItems = false;
            bool forceRebuild = false;
            bool cleanCache = false;
            bool debug = false;
            bool upToDateCheck = false;

            #endregion

            #region parse parameters

            foreach (string arg in args)
            {
                if (!arg.StartsWith("/") && !arg.StartsWith("-"))
                {
                    if (IsPathInvalid(arg))
                    {
                        buildContentTask.BuildLogger.LogWarning(null, null, Resources.ParameterPathIsInvalid, arg);
                        continue;
                    }

                    if (Path.GetExtension(arg) == ".cproj")
                    {
                        projectFile = arg;
                    }
                    else
                    {
                        buildItems.Add(arg);
                    }
                }
                else
                {
                    string paramName = arg.Substring(1);

                    if (paramName.Equals("DontAddProjectBuildItems", StringComparison.OrdinalIgnoreCase))
                    {
                        dontAddProjectBuildItems = true;
                    }
                    else if (paramName.Equals("ForceRebuild", StringComparison.OrdinalIgnoreCase))
                    {
                        forceRebuild = true;
                    }
                    else if (paramName.Equals("CleanCache", StringComparison.OrdinalIgnoreCase))
                    {
                        cleanCache = true;
                    }
                    else if (paramName.Equals("Debug", StringComparison.OrdinalIgnoreCase))
                    {
                        debug = true;
                    }
                    else if (paramName.Equals("UpToDate", StringComparison.OrdinalIgnoreCase))
                    {
                        upToDateCheck = true;
                    }
                    else
                    {
                        // parse argument
                        string parameterChar1 = arg.Substring(1, 1).ToLowerInvariant();
                        string parameterChar2 = arg.Substring(2, 1).ToLowerInvariant();

                        if (parameterChar1 == "o" && parameterChar2 == "d")
                        {
                            // output dir
                            outputDirectory = arg.Substring(4);
                        }
                        //current directory
                        else if (parameterChar1 == "c" && parameterChar2 == "d")
                        {
                            currentDirectory = arg.Substring(4);
                        }
                        //target platform
                        else if (parameterChar1 == "t")
                        {
                            string[] argParts = arg.Split(new char[] { '=', ':' }, StringSplitOptions.RemoveEmptyEntries);

                            targetPlatformName = argParts[1];
                        }
                        //configuration
                        else if (parameterChar1 == "c")
                        {
                            configurationName = arg.Substring(3);
                        }
                        //logger name
                        else if (parameterChar1 == "l")
                        {
                            loggerName = arg.Substring(3);
                        }
                        //reference
                        else if (parameterChar1 == "r")
                        {
                            referencedAssemblies.Add(arg.Substring(3));
                        }
                        //graphicsProfile
                        else if (parameterChar1 == "g")
                        {
                            graphicsProfileName = arg.Substring(3);
                        }
                        //intermediate directory
                        else if (parameterChar1 == "i")
                        {
                            intermediateDirectory = arg.Substring(3);
                        }
                    }
                }
            }

            #endregion

            if (!string.IsNullOrWhiteSpace(loggerName))
            {
                Console.WriteLine("Trying to connect with \"{0}\".", loggerName);

                ChannelFactory<IContentBuildLogger> factory = new ChannelFactory<IContentBuildLogger>(new NetNamedPipeBinding(), new EndpointAddress(new Uri(loggerName)));

                buildContentTask.BuildLogger.Childs.Add(factory.CreateChannel());

                Console.WriteLine("Connection established.");

                remoteLogger = factory;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(projectFile) && buildItems.Count == 0)
                {
                    buildContentTask.BuildLogger.LogImportantMessage(Resources.NoItemsSpecified);
                    Exit(ExitCode.NothingToDo);
                }

                TargetPlatform platform = ParseEnum<TargetPlatform>(targetPlatformName, TargetPlatform.Windows);
                GraphicsProfile graphics = ParseEnum<GraphicsProfile>(graphicsProfileName, GraphicsProfile.Reach);

                List<BuildItem> itemsToBuild = new List<BuildItem>();
                if (!string.IsNullOrWhiteSpace(projectFile))
                {
                    if (File.Exists(projectFile))
                    {
                        ContentProject project = ContentProject.Load(projectFile);

                        if (!dontAddProjectBuildItems)
                            itemsToBuild.AddRange(project.BuildItems);
                        else
                        {
                            //If the project BuildItems should not be added, we look for relative filenames and if they are part of the project, we use the BuildItem of the project, otherwise we try to add them the usual way. 
                            var absoluteBuildItems = buildItems.Where((x) =>
                                {
                                    var uri = new Uri(x, UriKind.RelativeOrAbsolute);
                                    return uri.IsAbsoluteUri;
                                }).ToArray();

                            var relativeBuildItems = buildItems.Except(absoluteBuildItems).ToArray();

                            var containedBuildItems = project.BuildItems.Where((x) => relativeBuildItems.Contains(x.SourceFilename));
                            var notContainedBuildItems = absoluteBuildItems.Concat(relativeBuildItems.Except(containedBuildItems.Select((x) => x.SourceFilename)));

                            itemsToBuild.AddRange(containedBuildItems);
                            itemsToBuild.AddRange(notContainedBuildItems.Select((x) => new BuildItem() { SourceFilename = x, AssetName = Path.GetFileNameWithoutExtension(x) }));
                        }

                        if (project.Configurations.Count == 0)
                            throw new InvalidOperationException(Resources.ProjectDoesntContainConfiguration);

                        Configuration config;
                        if (!project.Configurations.TryGetConfiguration(configurationName, platform, out config))
                        {
                            config = project.Configurations[0];
                            configurationName = config.Name;
                        }

                        buildContentTask.CompressContent = config.CompressContent;

                        if (string.IsNullOrWhiteSpace(graphicsProfileName))
                            graphics = config.Profile;

                        if (string.IsNullOrWhiteSpace(outputDirectory))
                            outputDirectory = config.OutputDirectory;

                        referencedAssemblies.AddRange(project.References.OfType<AssemblyReference>().Select((x) => x.AssemblyPath));
                        referencedAssemblies.AddRange(project.References.OfType<ProjectReference>().Select((x) => x.AssemblyPath));
                        referencedAssemblies.AddRange(project.References.OfType<GACReference>().Select((x) => x.AssemblyName));

                        if (string.IsNullOrWhiteSpace(currentDirectory))
                            currentDirectory = Path.Combine(Path.GetDirectoryName(projectFile), config.OutputDirectory);

                        if (string.IsNullOrWhiteSpace(currentDirectory))
                            currentDirectory = Path.GetDirectoryName(projectFile);

                        if (string.IsNullOrWhiteSpace(intermediateDirectory))
                        {
                            intermediateDirectory = Path.Combine(Path.GetDirectoryName(projectFile), "obj", BuildHelper.CreateSafeFileName(config.Platform.ToDisplayName()), BuildHelper.CreateSafeFileName(config.Name));
                        }
                    }
                    else
                    {
                        buildContentTask.BuildLogger.LogMessage(Resources.FileNotFound_Skipping, projectFile);

                        itemsToBuild.AddRange(buildItems.Select((x) => new BuildItem() { SourceFilename = x, AssetName = Path.GetFileNameWithoutExtension(x) }));
                    }
                }

                buildContentTask.BuildLogger.LogMessage(Resources.FinishedLoadingBuildItems);

                Uri currentDirectoryUri, intermediateDirectoryUri, outputDirectoryUri;
                HandleDirectories(ref currentDirectory, ref intermediateDirectory, ref outputDirectory, out currentDirectoryUri, out intermediateDirectoryUri, out outputDirectoryUri);

                //TODO: Change name to project to check against.
                var buildCacheUri = new Uri(new Uri(intermediateDirectory), new Uri("build.cache", UriKind.Relative));

                if (cleanCache)
                {
                    if (File.Exists(buildCacheUri.LocalPath))
                        File.Delete(buildCacheUri.LocalPath);

                    Exit(ExitCode.OK);
                }

                LoadReferences(referencedAssemblies, currentDirectory, buildContentTask.BuildLogger);

                buildContentTask.BuildLogger.LogMessage(Resources.FinishedLoadingAssemblies);

                buildContentTask.TargetPlatform = platform;
                buildContentTask.TargetProfile = graphics;
                buildContentTask.TargetPlatform = platform;
                buildContentTask.BaseDirectory = new Uri(currentDirectory, UriKind.Absolute);

                buildContentTask.PrepareAssetBuildCallback = (BuildContentTask task, BuildItem item, out ContentImporterContext importerContext, out ContentProcessorContext processorContext) =>
                {
                    if (String.IsNullOrEmpty(item.AssetName))
                        item.AssetName = Path.GetFileNameWithoutExtension(item.SourceFilename);

                    //Make sure the path is always relative (if possible); is important for the cache.
                    if (Path.IsPathRooted(item.SourceFilename) && item.SourceFilename.StartsWith(currentDirectory))
                        item.SourceFilename = item.SourceFilename.Substring(currentDirectory.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                    importerContext = new DefaultContentImporterContext(task.BuildLogger, intermediateDirectory, outputDirectory);
                    processorContext = new DefaultContentProcessorContext(task, configurationName, intermediateDirectory, outputDirectory, BuildHelper.GetOutputFileName(outputDirectory, currentDirectory, item));
                };



                itemsToBuild.ForEach((x) =>
                {
                    if (string.IsNullOrEmpty(x.ImporterName))
                    {
                        x.ImporterName = buildContentTask.ImporterManager.GuessImporterByFileExtension(x.SourceFilename);
                        buildContentTask.BuildLogger.LogMessage(string.Format(Resources.MissedImporterUsingDefault, x.AssetName, x.ImporterName));
                    }

                    if (string.IsNullOrEmpty(x.ProcessorName) && !string.IsNullOrEmpty(x.ImporterName))
                    {
                        x.ProcessorName = buildContentTask.ImporterManager.GetDefaultProcessor(x.ImporterName);
                        buildContentTask.BuildLogger.LogMessage(string.Format(Resources.MissedProcessorUsingDefault, x.AssetName, x.ProcessorName));
                    }
                });

                var buildCache = new AnxBuildCache(buildContentTask.ImporterManager, buildContentTask.ProcessorManager, buildContentTask.ContentCompiler);
                buildCache.ProjectHome = currentDirectoryUri;
                if (File.Exists(buildCacheUri.LocalPath))
                {
                    try
                    {
                        buildCache.LoadCache(buildCacheUri);
                    }
                    catch (Exception exc)
                    {
                        buildContentTask.BuildLogger.LogWarning(null, new ContentIdentity("build.cache", "BuildCache"), exc.Message);
                    }
                }

                if (upToDateCheck)
                {
                    buildContentTask.BuildLogger.LogMessage("Starting up to date check.");

                    foreach (var buildItem in itemsToBuild)
                        if (!buildCache.IsValid(buildItem, new Uri(BuildHelper.GetOutputFileName(outputDirectory, currentDirectory, buildItem))))
                        {
                            buildContentTask.BuildLogger.LogMessage("{0} is not up to date.", buildItem.SourceFilename);
                            Exit(ExitCode.NotUpToDate);
                        }

                    buildContentTask.BuildLogger.LogMessage("All content files are up to date.");
                    Exit(ExitCode.OK);
                }

                if (forceRebuild)
                {
                    foreach (var item in itemsToBuild)
                        buildCache.Remove(new Uri(item.SourceFilename, UriKind.RelativeOrAbsolute));
                }

                buildContentTask.BuildCache = buildCache;

                if (!string.IsNullOrWhiteSpace(outputDirectory) && !Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);

                if (!string.IsNullOrWhiteSpace(intermediateDirectory) && !Directory.Exists(intermediateDirectory))
                    Directory.CreateDirectory(intermediateDirectory);

                try
                {
                    buildContentTask.Execute(itemsToBuild);
                }
                catch (Exception exc)
                {
                    buildContentTask.BuildLogger.LogWarning(null, null, exc.Message + "\n" + exc.StackTrace);
                    Debugger.Break();
                }

                try
                {
                    buildCache.SaveCache(buildCacheUri);
                }
                catch (Exception exc)
                {
                    buildContentTask.BuildLogger.LogWarning(null, new ContentIdentity("build.cache", "BuildCache"), exc.Message);
                }

                Exit(ExitCode.OK);
            }
            catch (Exception exc)
            {
                Console.WriteLine("{0} thrown: {1}", exc.GetType(), exc.Message);

                if (remoteLogger != null)
                    remoteLogger.Abort();

                throw;
            }
        }

        private static void Exit(ExitCode exitCode)
        {
            if (remoteLogger != null)
                remoteLogger.Close();

            //Force exit in case some other thread is still doing work.
            Environment.Exit((int)exitCode);
        }

        static string TrailingSlash(string uri)
        {
            if (uri.EndsWith(new string(Path.DirectorySeparatorChar, 1)) || uri.EndsWith(new string(Path.AltDirectorySeparatorChar, 1)))
                return uri;
            else
                return uri + Path.DirectorySeparatorChar;
        }

        static T ParseEnum<T>(string name, T defaultValue) where T : struct
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                T result;
                if (Enum.TryParse<T>(name, true, out result))
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException(string.Format(Resources.PlatformNotSetable, name));
                }
            }
            else
                return defaultValue;
        }

        private static bool IsPathInvalid(string path)
        {
            foreach (var c in Path.GetInvalidPathChars())
            {
                if (path.Contains(c))
                {
                    return true;
                }
            }

            string fileName = Path.GetFileName(path);
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (fileName.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }

        private static void LoadReferences(IEnumerable<string> referencedAssemblies, string currentDirectory, ContentBuildLogger buildLogger)
        {
            List<Uri> searchPaths = new List<Uri>();

            Uri anxFrameworkPath;
            if (BuildHelper.TryGetAnxFrameworkPath(out anxFrameworkPath))
            {
                searchPaths.Add(anxFrameworkPath);
            }
            searchPaths.Add(new Uri(currentDirectory));

            Uri ownDirectory = new Uri(TrailingSlash(Path.GetDirectoryName(typeof(Program).Assembly.Location)), UriKind.Absolute);
            Dictionary<string, Uri> redirectPaths = new Dictionary<string, Uri>();
            redirectPaths.Add("ANX.Framework", new Uri(ownDirectory, new Uri("ANX.Framework.dll", UriKind.Relative)));
            redirectPaths.Add("ANX.Framework.Content.Pipeline", new Uri(ownDirectory, new Uri("ANX.Framework.Content.Pipeline.dll", UriKind.Relative)));

            buildLogger.LoggerRootDirectory = currentDirectory;

            //Use SearchPaths to find assemblies.
            foreach (var assemblyNameOrPath in referencedAssemblies)
            {
                try
                {
                    Uri reference = new Uri(assemblyNameOrPath, UriKind.RelativeOrAbsolute);
                    if (!reference.IsAbsoluteUri)
                    {
                        foreach (Uri path in searchPaths)
                        {
                            Uri tempPath = new Uri(path, reference);
                            if (File.Exists(tempPath.LocalPath))
                            {
                                string assemblyName = Path.GetFileNameWithoutExtension(tempPath.OriginalString);
                                Uri redirectPath;
                                if (redirectPaths.TryGetValue(assemblyName, out redirectPath))
                                {
                                    reference = redirectPath;
                                    buildLogger.LogMessage(Resources.RedirectedAssembly, assemblyNameOrPath, redirectPath.OriginalString);
                                }
                                else
                                {
                                    reference = tempPath;
                                }
                                break;
                            }
                        }
                    }

                    if (File.Exists(reference.OriginalString))
                        Assembly.LoadFrom(reference.LocalPath);
                    else
                        Assembly.Load(reference.OriginalString);
                }
                catch (Exception exc)
                {
                    buildLogger.LogWarning(null, new ContentIdentity(assemblyNameOrPath), exc.Message);
                }
            }
        }

        private static void HandleDirectories(ref string currentDirectory, ref string intermediateDirectory, ref string outputDirectory, out Uri currentDirectoryUri, out Uri intermediateDirectoryUri, out Uri outputDirectoryUri)
        {
            if (!string.IsNullOrWhiteSpace(currentDirectory))
            {
                Environment.CurrentDirectory = currentDirectory;
                if (string.IsNullOrWhiteSpace(intermediateDirectory))
                    intermediateDirectory = currentDirectory;
            }
            else
            {
                currentDirectory = Environment.CurrentDirectory;
            }
            currentDirectory = TrailingSlash(currentDirectory);
            currentDirectoryUri = new Uri(currentDirectory, UriKind.Absolute);


            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                outputDirectory = currentDirectory;
            }

            outputDirectoryUri = new Uri(outputDirectory, UriKind.RelativeOrAbsolute);
            if (!outputDirectoryUri.IsAbsoluteUri)
            {
                outputDirectoryUri = new Uri(currentDirectoryUri, outputDirectory);
                outputDirectory = outputDirectoryUri.LocalPath;
            }

            if (string.IsNullOrWhiteSpace(intermediateDirectory))
                intermediateDirectory = Path.Combine(outputDirectory, "intermediate");

            intermediateDirectory = TrailingSlash(intermediateDirectory);
            intermediateDirectoryUri = new Uri(intermediateDirectory, UriKind.Absolute);
        }
    }
}
