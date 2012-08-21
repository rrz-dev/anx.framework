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
    public class AnxContentProcessorContext : ContentProcessorContext
    {
        private string buildConfiguration;
        private string intermediateDirectory;
        private ContentBuildLogger contentBuildLogger;
        private string outputDirectory;
        private string outputFilename;
        private OpaqueDataDictionary parameters;
        private TargetPlatform targetPlatform;
        private GraphicsProfile targetProfile;

        public override string BuildConfiguration
        {
            get 
            {
                return buildConfiguration;
            }
        }

        public override string IntermediateDirectory
        {
            get 
            { 
                return intermediateDirectory; 
            }
        }

        public override ContentBuildLogger Logger
        {
            get 
            { 
                return contentBuildLogger; 
            }
        }

        public override string OutputDirectory
        {
            get 
            { 
                return outputDirectory; 
            }
        }

        public override string OutputFilename
        {
            get 
            { 
                return outputFilename; 
            }
        }

        public override OpaqueDataDictionary Parameters
        {
            get 
            { 
                return parameters; 
            }
        }

        public override TargetPlatform TargetPlatform
        {
            get 
            { 
                return targetPlatform; 
            }
        }

        public override GraphicsProfile TargetProfile
        {
            get 
            { 
                return targetProfile; 
            }
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
            throw new NotImplementedException();
        }

        public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName)
        {
            throw new NotImplementedException();
        }

        public override TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters)
        {
            throw new NotImplementedException();
        }
    }
}
