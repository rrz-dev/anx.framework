#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Execute(IEnumerable<BuildItem> itemsToBuild)
        {
            foreach (BuildItem buildItem in itemsToBuild)
            {
                var importedObject = ImportAsset(buildItem);

                if (String.IsNullOrEmpty(buildItem.BuildRequest.ProcessorName))
                {
                    buildItem.BuildRequest.ProcessorName = ProcessorManager.GetProcessorForType(importedObject.GetType());
                }

                var buildedItem = Process(buildItem, importedObject);
            }
        }

        private object ImportAsset(BuildItem item)
        {
            IContentImporter instance = this.ImporterManager.GetInstance(item.BuildRequest.ImporterName);
            ContentImporterContext context = new AnxContentImporterContext(this, item, null); //this.buildLogger);
            //this.buildLogger.LogMessage(Resources.BuildLogImporting, new object[]
            //{
            //    item.BuildRequest.SourceFilename,
            //    instance.GetType()
            //});
            return instance.Import(item.BuildRequest.SourceFilename, context);
        }

        private object Process(BuildItem item, object importedObject)
        {
            if (String.IsNullOrEmpty(item.BuildRequest.ProcessorName) == false)
            {
                IContentProcessor instance = this.ProcessorManager.GetInstance(item.BuildRequest.ProcessorName);
                ContentProcessorContext context = new AnxContentProcessorContext();
                return instance.Process(importedObject, context);
            }
            else
            {
                return importedObject;
            }
        }
    }
}
