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
    public class BuildItem
    {
        public readonly OpaqueDataDictionary ProcessorParameters = new OpaqueDataDictionary();

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

        [TypeConverter(typeof(ImporterConverter))]
        public string ImporterName
        {
            get;
            set;
        }

        [TypeConverter(typeof(ProcessorConverter))]
        public string ProcessorName
        {
            get;
            set;
        }

        public string OutputFilename
        {
            get;
            set;
        }
    }
}
