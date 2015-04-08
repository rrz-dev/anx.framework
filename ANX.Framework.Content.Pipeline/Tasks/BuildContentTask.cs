using ANX.Framework.Content.Pipeline.Serialization.Compiler;
using ANX.Framework.Graphics;
using ANX.Framework.Content.Pipeline.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class BuildContentTask
    {
        public delegate void PrepareAssetBuild(BuildContentTask sender, BuildItem item, out ContentImporterContext importerContext, out ContentProcessorContext processorContext);

        private ImporterManager importerManager;
        private ProcessorManager processorManager;
        private ContentCompiler contentCompiler;
        private MultiContentBuildLogger buildLogger = new MultiContentBuildLogger("BuildContent main logger");

        public BuildContentTask()
        {
            OutputDirectory = Environment.CurrentDirectory;
            TargetPlatform = TargetPlatform.Windows;
            CompressContent = false;
            TargetProfile = GraphicsProfile.HiDef;
        }

        public PrepareAssetBuild PrepareAssetBuildCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="ImporterManagerAdapter"/> that will be used to build the content.
        /// </summary>
        /// <remarks>The instance is automatically created when none is set.</remarks>
        public ImporterManager ImporterManager
        {
            get
            {
                if (this.importerManager == null)
                {
                    this.importerManager = new ImporterManager();
                }

                return this.importerManager;
            }
            set
            {
                this.importerManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ProcessorManagerAdapter"/> that will be used to build the content.
        /// </summary>
        /// <remarks>The instance is automatically created when none is set.</remarks>
        public ProcessorManager ProcessorManager
        {
            get
            {
                if (this.processorManager == null)
                {
                    this.processorManager = new ProcessorManager();
                }

                return this.processorManager;
            }
            set
            {
                this.processorManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ContentCompilerAdapter"/> that will be used to build the content.
        /// </summary>
        /// <remarks>The instance is automatically created when none is set.</remarks>
        public ContentCompiler ContentCompiler
        {
            get
            {
                if (this.contentCompiler == null)
                {
                    this.contentCompiler = new ContentCompiler();
                }

                return this.contentCompiler;
            }
            set
            {
                this.contentCompiler = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IBuildCache"/> that will be used check if assets have to be rebuild.
        /// </summary>
        public IBuildCache BuildCache
        {
            get;
            set;
        }

        public string OutputDirectory
        {
            get;
            set;
        }

        public TargetPlatform TargetPlatform
        {
            get;
            set;
        }

        public bool CompressContent
        {
            get;
            set;
        }

        public GraphicsProfile TargetProfile
        {
            get;
            set;
        }

        public Uri BaseDirectory
        {
            get;
            set;
        }

        public MultiContentBuildLogger BuildLogger
        {
            get { return buildLogger; }
        }

        public Uri GetOutputFileName(BuildItem buildItem)
        {
            return new Uri(Path.Combine(OutputDirectory, buildItem.AssetName) + ".xnb", UriKind.Absolute);
        }

        public CompiledBuildItem Execute(BuildItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return Execute(new [] { item }).First();
        }

        public CompiledBuildItem[] Execute(IEnumerable<BuildItem> itemsToBuild, bool throwExceptions = false)
        {
            if (itemsToBuild == null)
                throw new ArgumentNullException("itemsToBuild");

            if (PrepareAssetBuildCallback == null)
                throw new InvalidOperationException("The property PrepareAssetBuildCallback must be set to execute a build content task.");

            List<CompiledBuildItem> result = new List<CompiledBuildItem>();
            foreach (BuildItem buildItem in itemsToBuild)
            {
                if (buildItem == null)
                    throw new ArgumentNullException("An element of the parameter itemsToBuild is null.");

                Uri outputFilename = GetOutputFileName(buildItem);
                if (BuildCache != null)
                {
                    if (throwExceptions)
                    {
                        if (CheckIsValid(buildItem, outputFilename))
                            continue;
                    }
                    else
                    {
                        try
                        {
                            if (CheckIsValid(buildItem, outputFilename))
                                continue;
                        }
                        catch (Exception exc)
                        {
                            BuildLogger.LogWarning(null, new ContentIdentity() { SourceTool = "BuildCache" }, exc.Message);
                        }
                    }
                }

                ContentImporterContext importerContext;
                ContentProcessorContext processorContext;
                PrepareAssetBuildCallback(this, buildItem, out importerContext, out processorContext);

                var absoluteFilename = MakeAbsolute(buildItem.SourceFilename);

                object importedObject = null;
                CompiledBuildItem compiled = null;

                if (throwExceptions)
                {
                    importedObject = ImportAsset(buildItem, absoluteFilename, importerContext);
                }
                else
                {
                    try
                    {
                        importedObject = ImportAsset(buildItem, absoluteFilename, importerContext);
                    }
                    catch (Exception exc)
                    {
                        LogException(exc, absoluteFilename);
                    }
                }

                if (importedObject != null)
                {
                    if (String.IsNullOrEmpty(buildItem.ProcessorName))
                    {
                        buildItem.ProcessorName = ImporterManager.GetDefaultProcessor(buildItem.ImporterName);
                        if (string.IsNullOrEmpty(buildItem.ProcessorName))
                        {
                            buildItem.ProcessorName = ProcessorManager.GetProcessorForType(importedObject.GetType());
                        }
                    }

                    object compiledItem = null;

                    if (throwExceptions)
                    {
                        compiledItem = Process(buildItem, absoluteFilename, importedObject, processorContext);
                    }
                    else
                    {
                        try
                        {
                            compiledItem = Process(buildItem, absoluteFilename, importedObject, processorContext);
                        }
                        catch (Exception exc)
                        {
                            LogException(exc, absoluteFilename);
                        }
                    }

                    if (compiledItem != null)
                    {
                        SerializeAsset(buildItem, compiledItem, outputFilename.LocalPath);

                        compiled = new CompiledBuildItem(compiledItem, buildItem, outputFilename.LocalPath, true);
                    }
                }

                if (compiled == null)
                {
                    compiled = new CompiledBuildItem(null, buildItem, null, false);
                }

                result.Add(compiled);

                if (compiled.Successfull)
                {
                    BuildLogger.LogMessage("---\"{0}\" successfully build.", buildItem.AssetName);

                    if (BuildCache != null)
                    {
                        if (throwExceptions)
                        {
                            BuildCache.Refresh(compiled.OriginalBuildItem, outputFilename);
                        }
                        else
                        {
                            try
                            {
                                BuildCache.Refresh(compiled.OriginalBuildItem, outputFilename);
                            }
                            catch (Exception exc)
                            {
                                BuildLogger.LogWarning(null, new ContentIdentity() { SourceTool = "BuildCache" }, exc.Message);
                            }
                        }
                    }
                }
                else
                {
                    BuildLogger.LogMessage("---Build of \"{0}\" not successfull.", buildItem.AssetName);
                }
            }

            return result.ToArray();
        }

        private bool CheckIsValid(BuildItem buildItem, Uri outputFilename)
        {
            if (BuildCache.IsValid(buildItem, outputFilename))
            {
                BuildLogger.LogMessage("---\"{0}\" still valid, skipping build.", buildItem.SourceFilename);
                return true;
            }
            return false;
        }

        private string MakeAbsolute(string filename)
        {
            if (filename == null || BaseDirectory == null)
                return filename;
            
            var uri = new Uri(filename, UriKind.RelativeOrAbsolute);

            if (uri.IsAbsoluteUri)
                return filename;

            return new Uri(BaseDirectory, uri).OriginalString;
        }

        private object ImportAsset(BuildItem item, string absoluteFilename, ContentImporterContext context)
        {
            string importerName = item.ImporterName;
            if (string.IsNullOrEmpty(importerName))
            {
                importerName = ImporterManager.GuessImporterByFileExtension(Path.GetExtension(item.SourceFilename));
            }

            IContentImporter importer = this.ImporterManager.GetInstance(importerName);
            buildLogger.LogMessage("importing {0} with importer {1}", Uri.EscapeUriString(item.SourceFilename.ToString()), importer.GetType());

            object result = importer.Import(absoluteFilename, context);

            if (result == null)
            {
                var identity = new ContentIdentity();
                identity.SourceFilename = absoluteFilename;

                buildLogger.LogWarning("", identity, "importer \"{0}\" didn't return a value.", importerName);
            }

            return result;
        }

        private object Process(BuildItem item, string absoluteFilename, object importedObject, ContentProcessorContext context)
        {
            if (String.IsNullOrEmpty(item.ProcessorName) == false)
            {
                IContentProcessor instance = this.ProcessorManager.GetInstance(item.ProcessorName);
                SetProcessorParameters(instance, item.ProcessorParameters);

                buildLogger.LogMessage("building with processor {0}", instance.GetType());

                if (!instance.InputType.IsAssignableFrom(importedObject.GetType()))
                {
                    ContentIdentity identity = null;
                    if (importedObject is ContentItem)
                    {
                        identity = ((ContentItem)importedObject).Identity;
                    }
                    
                    if (identity == null)
                    {
                        identity = new ContentIdentity();
                        identity.SourceFilename = absoluteFilename;
                    }

                    buildLogger.LogWarning("", identity, "The input type of the processor \"{0}\" is not assignable from the output type of the importer \"{1}\".", item.ProcessorName, item.ImporterName);
                    return null;
                }

                return instance.Process(importedObject, context);
            }
            else
            {
                return importedObject;
            }
        }

        private void SerializeAsset(BuildItem item, object assetData, string outputFilename)
        {
            string dir =Path.GetDirectoryName(outputFilename);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            buildLogger.LogMessage("serializing {0}", new object[] { item.AssetName });
            using (Stream stream = new FileStream(outputFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                this.ContentCompiler.Compile(stream, assetData, TargetPlatform, TargetProfile, CompressContent, OutputDirectory, outputFilename);
            }
        }

        private void SetProcessorParameters(IContentProcessor instance, IDictionary<string, object> parameters)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (parameters.Count == 0)
            {
                return;
            }

            Type processorType = instance.GetType();

            foreach (var keyPair in parameters)
            {
                var property = processorType.GetProperty(keyPair.Key);
                if (property != null)
                {
                    if (keyPair.Value != null && property.PropertyType != keyPair.Value.GetType())
                    {
                        TypeConverter converter = property.GetConverter();
                        if (converter == null)
                            continue;

                        object value;
                        try
                        {
                            value = converter.ConvertFrom(keyPair.Value);
                        }
                        catch (NotSupportedException exc)
                        {
                            throw new ContentLoadException(string.Format("Unable to convert processor parameter \"{0}\" from {1} to {2}.", keyPair.Key, keyPair.Value.GetType(), property.PropertyType), exc);
                        }

                        property.SetValue(instance, value, null);
                    }
                    else
                    {
                        property.SetValue(instance, keyPair.Value, null);
                    }
                }
            }
        }

        private void LogException(Exception exc, string filename)
        {
            var identity = new ContentIdentity();
            identity.SourceFilename = filename;

            buildLogger.LogWarning(exc.HelpLink, identity, exc.Message);
        }
    }
}
