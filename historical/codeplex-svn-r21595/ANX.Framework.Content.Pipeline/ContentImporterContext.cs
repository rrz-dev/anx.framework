#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public abstract class ContentImporterContext
    {
        public ContentImporterContext(ContentBuildLogger logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            this.Logger = logger;
        }

        public abstract string IntermediateDirectory { get; }
        public abstract string OutputDirectory { get; }
        public abstract void AddDependency(string filename);

        public ContentBuildLogger Logger
        {
            get;
            protected set;
        }
    }
}
