#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

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
            return this.BuildAndLoadAsset<TInput, TOutput>(sourceAsset, processorName, null, null);
        }

        
        public abstract TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName);
    
        public ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName)
        {
            return this.BuildAsset<TInput, TOutput>(sourceAsset, processorName, null, null, null);
        }

        
        public abstract ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName);

        /// <summary>
        /// Converts a content item object using the specified content processor. 
        /// </summary>
        /// <typeparam name="TInput">Type of the input content.</typeparam>
        /// <typeparam name="TOutput">Type of the converted output.</typeparam>
        /// <param name="input">Source content to be converted.</param>
        /// <param name="processorName">Optional processor for this content.</param>
        /// <returns>Reference of the final converted content.</returns>
        public TOutput Convert<TInput, TOutput>(TInput input, string processorName)
        {
            return this.Convert<TInput, TOutput>(input, processorName, null);
        }

        /// <summary>
        /// Converts a content item object using the specified content processor. 
        /// </summary>
        /// <typeparam name="TInput">Type of the input content.</typeparam>
        /// <typeparam name="TOutput">Type of the converted output.</typeparam>
        /// <param name="input">Source content to be converted.</param>
        /// <param name="processorName">Optional processor for this content.</param>
        /// <param name="processorParameters">Optional paramters for the processor.</param>
        /// <returns>Reference of the final converted content.</returns>
        
        public abstract TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters);

    }
}
