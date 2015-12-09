using ANX.ContentCompiler.GUI.Converters;
using ANX.Framework.Content.Pipeline.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ANX.ContentCompiler.GUI.Nodes
{
    class BuildItemNodeProperties : NodeProperties
    {
        public BuildItemNodeProperties(BuildItem buildItem)
        {
            if (buildItem == null)
                throw new ArgumentNullException("buildItem");

            this.BuildItem = buildItem;
        }

        internal BuildItem BuildItem
        {
            get;
            private set;
        }

        public string AssetName
        {
            get { return BuildItem.AssetName; }
            set 
            {
                if (value != null)
                {
                    foreach (var c in Path.GetInvalidFileNameChars())
                    {
                        if (value.Contains(c))
                            return;
                    }
                }

                BuildItem.AssetName = value; 
            }
        }

        public string SourceFilename
        {
            get { return BuildItem.SourceFilename; }
        }

        [TypeConverter(typeof(ImporterConverter))]
        public string Importer
        {
            get { return BuildItem.ImporterName; }
            set { BuildItem.ImporterName = value; }
        }

        [TypeConverter(typeof(ProcessorConverter))]
        public string Processor
        {
            get { return BuildItem.ProcessorName; }
            set { BuildItem.ProcessorName = value; }
        }
    }
}
