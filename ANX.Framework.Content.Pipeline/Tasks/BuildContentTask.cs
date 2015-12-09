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
using System.Diagnostics;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class BuildContentTask
    {
        public delegate void PrepareAssetBuild(BuildContentTask sender, BuildItem item, out ContentImporterContext importerContext, out ContentProcessorContext processorContext);

        private ImporterManager importerManager;
        private ProcessorManager processorManager;
        private ContentCompiler contentCompiler;
        private MultiContentBuildLogger buildLogger = new MultiContentBuildLogger();

        public BuildContentTask()
        {
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

        public CompiledBuildItem Execute(BuildItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            return Execute(new [] { item }).First();
        }

        public CompiledBuildItem[] Execute(IEnumerable<BuildItem> itemsToBuild)
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

                ContentImporterContext importerContext;
                ContentProcessorContext processorContext;
                PrepareAssetBuildCallback(this, buildItem, out importerContext, out processorContext);

                if (string.IsNullOrEmpty(buildItem.ProcessorName))
                    throw new ArgumentNullException(string.Format("Asset \"{0}\" has no processor.", buildItem.AssetName));

                //Has to be done before the cache is checked to make it has all the data it needs.
                if (string.IsNullOrEmpty(buildItem.ImporterName))
                    buildItem.ImporterName = this.ImporterManager.GuessImporterByFileExtension(Path.GetExtension(buildItem.SourceFilename));

                foreach (var parameter in this.ProcessorManager.GetProcessorParameters(buildItem.ProcessorName))
                {
                    if (!buildItem.ProcessorParameters.ContainsKey(parameter.PropertyName))
                        buildItem.ProcessorParameters.Add(parameter.PropertyName, parameter.DefaultValue);
                    else
                    {
                        //Make sure the value is of the type it should be.
                        buildItem.ProcessorParameters[parameter.PropertyName] = TypeHelper.ConvertFromInvariantString(buildItem.ProcessorParameters[parameter.PropertyName], TypeHelper.GetType(parameter.PropertyType));
                    }
                }

                Uri outputFilename = new Uri(processorContext.OutputFilename, UriKind.Absolute);
                if (BuildCache != null)
                {
                    try
                    {
                        if (BuildCache.IsValid(buildItem, outputFilename))
                        {
                            result.Add(new CompiledBuildItem(null, buildItem, outputFilename.LocalPath, true));
                            continue;
                        }
                    }
                    catch (Exception exc)
                    {
                        BuildLogger.LogWarning(null, new ContentIdentity() { SourceTool = "BuildCache" }, exc.Message);
                        Debugger.Break();
                    }
                }

                var absoluteFilename = MakeAbsolute(buildItem.SourceFilename);

                object importedObject = null;
                CompiledBuildItem compiled = null;

                try
                {
                    importedObject = ImportAsset(buildItem, absoluteFilename, importerContext);
                }
                catch (Exception exc)
                {
                    LogException(exc, absoluteFilename);
                    Debugger.Break();
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

                    try
                    {
                        compiledItem = Process(buildItem, absoluteFilename, importedObject, processorContext);
                    }
                    catch (Exception exc)
                    {
                        LogException(exc, absoluteFilename);
                        Debugger.Break();
                    }

                    if (compiledItem != null)
                    {
                        try
                        {
                            SerializeAsset(buildItem, compiledItem, processorContext.OutputDirectory, outputFilename.LocalPath);

                            compiled = new CompiledBuildItem(compiledItem, buildItem, outputFilename.LocalPath, true);
                        }
                        catch (Exception exc)
                        {
                            LogException(exc, absoluteFilename);
                            Debugger.Break();
                        }
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
                        try
                        {
                            BuildCache.Refresh(compiled.OriginalBuildItem, outputFilename);
                        }
                        catch (Exception exc)
                        {
                            BuildLogger.LogWarning(null, new ContentIdentity() { SourceTool = "BuildCache" }, exc.Message);
                            Debugger.Break();
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
            IContentImporter importer = this.ImporterManager.GetInstance(item.ImporterName);
            buildLogger.LogMessage("importing {0} with importer {1}", item.SourceFilename.ToString(), importer.GetType());

            object result = importer.Import(absoluteFilename, context);

            if (result == null)
            {
                var identity = new ContentIdentity();
                identity.SourceFilename = absoluteFilename;

                buildLogger.LogWarning("", identity, "importer \"{0}\" didn't return a value.", item.ImporterName);
            }

            return result;
        }

        private object Process(BuildItem item, string absoluteFilename, object importedObject, ContentProcessorContext context)
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

        private void SerializeAsset(BuildItem item, object assetData, string outputDirectory, string outputFilename)
        {
            string dir =Path.GetDirectoryName(outputFilename);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            buildLogger.LogMessage("serializing {0}", new object[] { item.AssetName });
            using (Stream stream = new FileStream(outputFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                this.ContentCompiler.Compile(stream, assetData, TargetPlatform, TargetProfile, CompressContent, outputDirectory, outputFilename);
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
