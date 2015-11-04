using ANX.Framework.Content.Pipeline;
using ANX.Framework.Content.Pipeline.Tasks;
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Tasks
{
    public class DefaultContentProcessorContext : ContentProcessorContext
    {
        BuildContentTask task;
        OpaqueDataDictionary parameters = new OpaqueDataDictionary();
        string buildConfiguration;
        string intermediateDirectory;
        string outputFilename;
        string outputDirectory;

        public DefaultContentProcessorContext(BuildContentTask task, string buildConfiguration, string intermediateDirectory, string outputDirectory, string outputFilename)
        {
            this.task = task;
            this.buildConfiguration = buildConfiguration;
            this.intermediateDirectory = intermediateDirectory;
            this.outputFilename = outputFilename;
            this.outputDirectory = outputDirectory;
        }

        public override string BuildConfiguration
        {
            get { return buildConfiguration; }
        }

        public override string IntermediateDirectory
        {
            get { return intermediateDirectory; }
        }

        public override ContentBuildLogger Logger
        {
            get { return task.BuildLogger; }
        }

        public override string OutputDirectory
        {
            get { return outputDirectory; }
        }

        public override string OutputFilename
        {
            get { return outputFilename; }
        }

        public override OpaqueDataDictionary Parameters
        {
            get { return parameters; }
        }

        public override TargetPlatform TargetPlatform
        {
            get { return task.TargetPlatform; }
        }

        public override GraphicsProfile TargetProfile
        {
            get { return task.TargetProfile; }
        }

        public override void AddDependency(string filename)
        {
            throw new NotImplementedException();
        }

        public override void AddOutputFile(string filename)
        {
            throw new NotImplementedException();
        }

        public override TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName)
        {
            if (sourceAsset == null)
                throw new ArgumentNullException("sourceAsset");

            if (string.IsNullOrEmpty(sourceAsset.Filename))
                throw new ArgumentNullException("sourceAsset.Filename");

            var buildItem = new BuildItem()
            {
                AssetName = Path.GetFileNameWithoutExtension(sourceAsset.Filename),
                ImporterName = importerName,
                ProcessorName = processorName,
                SourceFilename = sourceAsset.Filename
            };

            if (processorParameters != null)
            {
                foreach (var pair in processorParameters)
                {
                    buildItem.ProcessorParameters.Add(pair.Key, pair.Value);
                }
            }

            var result = task.Execute(buildItem);
            if (!result.Successfull)
                throw new InvalidOperationException(string.Format("Unable to build asset \"{0}\".", sourceAsset.Filename));

            return (TOutput)result.Item;
        }

        public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName)
        {
            if (sourceAsset == null)
                throw new ArgumentNullException("sourceAsset");

            if (string.IsNullOrEmpty(sourceAsset.Filename))
                throw new ArgumentNullException("sourceAsset.Filename");

            var buildItem = new BuildItem()
            {
                AssetName = assetName,
                ImporterName = importerName,
                ProcessorName = processorName,
                SourceFilename = sourceAsset.Filename
            };

            if (processorParameters != null)
            {
                foreach (var pair in processorParameters)
                {
                    buildItem.ProcessorParameters.Add(pair.Key, pair.Value);
                }
            }

            var result = task.Execute(buildItem);
            if (!result.Successfull)
                throw new InvalidOperationException(string.Format("Unable to build asset \"{0}\".", sourceAsset.Filename));

            return new ExternalReference<TOutput>(result.OutputFile);
        }

        public override TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters)
        {
            var processorManager = task.ProcessorManager;

            if (string.IsNullOrWhiteSpace(processorName))
                processorName = processorManager.GetProcessorForType(typeof(TInput), typeof(TOutput));
            else
                processorManager.ValidateProcessorTypes(processorName, typeof(TInput), typeof(TOutput));

            var processor = processorManager.GetInstance(processorName);

            if (processorParameters != null)
                processorManager.SetProcessorParameters(processor, processorParameters);

            return (TOutput)processor.Process(input, this);
        }
    }
}
