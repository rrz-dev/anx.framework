#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Tasks;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public class AnxContentImporterContext : ContentImporterContext
    {
        private string intermediateDirectory;
        private string outputDirectory;

        public AnxContentImporterContext(BuildContent buildContent, BuildItem buildItem, ContentBuildLogger logger)
            : base(logger)
        {
            BuildContent = buildContent;
            BuildItem = buildItem;
        }

        public BuildContent BuildContent
        {
            get;
            set;
        }

        public BuildItem BuildItem
        {
            get;
            set;
        }

        public override string IntermediateDirectory
        {
            get
            {
                return intermediateDirectory;
            }
        }

        public override string OutputDirectory
        {
            get
            {
                return outputDirectory;
            }
        }

        public override void AddDependency(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
