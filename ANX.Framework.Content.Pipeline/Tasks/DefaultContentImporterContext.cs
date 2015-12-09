using ANX.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class DefaultContentImporterContext : ContentImporterContext
    {
        string intermediateDirectory;
        string outputDirectory;

        public DefaultContentImporterContext(ContentBuildLogger logger, string intermediateDirectory, string outputDirectory)
            : base(logger)
        {
            this.intermediateDirectory = intermediateDirectory;
            this.outputDirectory = outputDirectory;
        }

        public override string IntermediateDirectory
        {
            get { return intermediateDirectory; }
        }

        public override string OutputDirectory
        {
            get { return outputDirectory; }
        }

        public override void AddDependency(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
