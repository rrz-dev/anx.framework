using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class CompiledBuildItem
    {
        public object Item
        {
            get;
            private set;
        }

        public BuildItem OriginalBuildItem
        {
            get;
            private set;
        }

        public string OutputFile
        {
            get;
            private set;
        }

        public bool Successfull
        {
            get;
            private set;
        }

        public CompiledBuildItem(object item, BuildItem originalBuildItem, string outputFile, bool successfull)
        {
            this.Item = item;
            this.OriginalBuildItem = originalBuildItem;
            this.OutputFile = outputFile;
            this.Successfull = successfull;
        }
    }
}
