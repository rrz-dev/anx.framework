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
    public class ContentWithNullable
    {
        public Vector3? NullableValue { get; set; }
    }

    public class ContentWithNullableWriter : ContentTypeWriter<ContentWithNullable>
    {
        protected override void Write(ContentWriter output, ContentWithNullable value)
        {
            output.WriteObject(value.NullableValue);
        }

        public override string GetRuntimeReader(Microsoft.Xna.Framework.Content.Pipeline.TargetPlatform targetPlatform)
        {
            return typeof(ContentWithNullableReader).AssemblyQualifiedName;
        }
    }

    // The reader must be in the ANX Namespace
    public class ContentWithNullableReader : ANX.Framework.Content.ContentTypeReader<ContentWithNullable>
    {
        protected override ContentWithNullable Read(ANX.Framework.Content.ContentReader input, ContentWithNullable existingInstance)
        {
            existingInstance = new ContentWithNullable();
            existingInstance.NullableValue = input.ReadObject<ANX.Framework.Vector3?>();
            return existingInstance;
        }
    }
}
