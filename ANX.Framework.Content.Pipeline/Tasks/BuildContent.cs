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

        public void Execute(IEnumerable<BuildItem> itemsToBuild)
        {
            foreach (BuildItem buildItem in itemsToBuild)
            {
                var importedObject = ImportAsset(buildItem);
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
    }
}
