#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    public abstract class ContentProcessorContext
    {
        public ContentProcessorContext()
        {
            throw new NotImplementedException();
        }

        public abstract string BuildConfiguration { get; }
        public abstract string IntermediateDirectory { get; }
        public abstract ContentBuildLogger Logger { get; }
        public abstract string OutputDirectory { get; }
        public abstract string OutputFilename { get; }
        public abstract OpaqueDataDictionary Parameters { get; }
        public abstract TargetPlatform TargetPlatform { get; }
        public abstract GraphicsProfile TargetProfile { get; }

        public abstract void AddDependency(string filename);
        public abstract void AddOutputFile(string filename);

        public TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName)
        {
            throw new NotImplementedException();
        }

        public abstract TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName);
    
        public ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName)
        {
            throw new NotImplementedException();
        }

        public abstract ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName);
    
        public TOutput Convert<TInput, TOutput>(TInput input, string processorName)
        {
            throw new NotImplementedException();
        }

        public abstract TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters);

    }
}
