#region Using Statements
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Tasks
{
    [Serializable]
    public class BuildItem : ICloneable
    {
        public BuildItem()
        {
            ProcessorParameters = new OpaqueDataDictionary();
        }

        protected BuildItem(BuildItem original)
        {
            this.SourceFilename = original.SourceFilename;
            this.AssetName = original.AssetName;
            this.ImporterName = original.ImporterName;
            this.ProcessorName = original.ProcessorName;
            this.ProcessorParameters = new OpaqueDataDictionary(original.ProcessorParameters);
        }

        public String SourceFilename
        {
            get;
            set;
        }

        public String AssetName
        {
            get;
            set;
        }

        public string ImporterName
        {
            get;
            set;
        }

        public string ProcessorName
        {
            get;
            set;
        }

        public OpaqueDataDictionary ProcessorParameters
        {
            get;
            private set;
        }

        public virtual object Clone()
        {
            return new BuildItem(this);
        }
    }
}
