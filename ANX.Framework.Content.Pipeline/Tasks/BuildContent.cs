#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ANX.Framework.Content.Pipeline.Serialization.Compiler;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class BuildContent
    {
        private ImporterManager importerManager;
        private ProcessorManager processorManager;
        private ContentCompiler contentCompiler;

        public BuildContent()
        {
            OutputDirectory = Environment.CurrentDirectory;
            TargetPlatform = TargetPlatform.Windows;
            CompressContent = false;
            TargetProfile = GraphicsProfile.HiDef;
        }

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
        }

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
        }

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

        public ContentBuildLogger BuildLogger
        {
            get;
            set;
        }

        public void Execute(IEnumerable<BuildItem> itemsToBuild)
        {
            foreach (BuildItem buildItem in itemsToBuild)
            {
                var importedObject = ImportAsset(buildItem);

                if (String.IsNullOrEmpty(buildItem.ProcessorName))
                {
                    buildItem.ProcessorName = ImporterManager.GetDefaultProcessor(buildItem.ImporterName);
                    if (string.IsNullOrEmpty(buildItem.ProcessorName))
                    {
                        buildItem.ProcessorName = ProcessorManager.GetProcessorForType(importedObject.GetType());
                    }
                }

                var buildedItem = Process(buildItem, importedObject);

                SerializeAsset(buildItem, buildedItem);
            }
        }

        private object ImportAsset(BuildItem item)
        {
            IContentImporter instance = this.ImporterManager.GetInstance(item.ImporterName);
            ContentImporterContext context = new AnxContentImporterContext(this, item, BuildLogger);
            BuildLogger.LogMessage("building {0} of type {1}", new object[]
            {
                item.SourceFilename,
                instance.GetType()
            });
            return instance.Import(item.SourceFilename, context);
        }

        private object Process(BuildItem item, object importedObject)
        {
            if (String.IsNullOrEmpty(item.ProcessorName) == false)
            {
                IContentProcessor instance = this.ProcessorManager.GetInstance(item.ProcessorName);
                SetProcessorParameters(instance, item.ProcessorParameters);
                ContentProcessorContext context = new AnxContentProcessorContext(item, BuildLogger, TargetPlatform, TargetProfile, "");
                context.OutputDirectory = OutputDirectory;
                context.OutputFilename = item.OutputFilename;
                return instance.Process(importedObject, context);
            }
            else
            {
                return importedObject;
            }
        }

        private void SerializeAsset(BuildItem item, object assetData)
        {
            string outputFilename = Path.Combine(OutputDirectory, item.OutputFilename);

            BuildLogger.LogMessage("serializing {0}", new object[] { item.OutputFilename });
            using (Stream stream = new FileStream(outputFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                this.ContentCompiler.Compile(stream, assetData, TargetPlatform, TargetProfile, CompressContent, OutputDirectory, outputFilename);
            }
            //this.rebuiltFiles.Add(outputFilename);
        }

        private void SetProcessorParameters(IContentProcessor instance, OpaqueDataDictionary parameters)
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

            throw new NotImplementedException();
        }
    }
}
