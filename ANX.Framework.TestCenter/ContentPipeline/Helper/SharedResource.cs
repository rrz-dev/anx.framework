#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.TestCenter.ContentPipeline.Helper
{
    public class ContentWithSharedResource
    {
        public class SharedResource
        {
            public int Value { get; set; }
        }

        public SharedResource ResourceA { get; set; }
        public SharedResource ResourceB { get; set; }
    }

    public class ContentWithSharedResourceWriter : ContentTypeWriter<ContentWithSharedResource>
    {
        protected override void Write(ContentWriter output, ContentWithSharedResource value)
        {
            output.WriteSharedResource<ContentWithSharedResource.SharedResource>(value.ResourceA);
            output.WriteSharedResource<ContentWithSharedResource.SharedResource>(value.ResourceB);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(ContentWithSharedResourceReader).AssemblyQualifiedName;
        }
    }

    public class SharedResourceWriter : ContentTypeWriter<ContentWithSharedResource.SharedResource>
    {
        protected override void Write(ContentWriter output, ContentWithSharedResource.SharedResource value)
        {
            output.Write(value.Value);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(SharedResourceReader).AssemblyQualifiedName;
        }
    }




    // The reader must be in the ANX Namespace
    public class ContentWithSharedResourceReader : ANX.Framework.Content.ContentTypeReader<ContentWithSharedResource>
    {
        protected override ContentWithSharedResource Read(ANX.Framework.Content.ContentReader input, ContentWithSharedResource existingInstance)
        {
            existingInstance = new ContentWithSharedResource();
            input.ReadSharedResource<ContentWithSharedResource.SharedResource>((value) => {
                existingInstance.ResourceA = value;
            });
            input.ReadSharedResource<ContentWithSharedResource.SharedResource>((value) =>
            {
                existingInstance.ResourceB = value;
            });
            return existingInstance;
        }
    }

    public class SharedResourceReader : ANX.Framework.Content.ContentTypeReader<ContentWithSharedResource.SharedResource>
    {
        protected override ContentWithSharedResource.SharedResource
            Read(ANX.Framework.Content.ContentReader input, ContentWithSharedResource.SharedResource existingInstance)
        {
            existingInstance = new ContentWithSharedResource.SharedResource();
            existingInstance.Value = input.ReadInt32();
            return existingInstance;
        }
    }
}
