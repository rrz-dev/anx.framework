#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    internal class ListWriter<T> : BuiltinTypeWriter<List<T>>
    {
        private ContentTypeWriter elementWriter;

        public override bool CanDeserializeIntoExistingObject
        {
            get
            {
                return true;
            }
        }

        protected override void Initialize(ContentCompiler compiler)
        {
            this.elementWriter = compiler.GetTypeWriter(typeof(T));
        }

        protected internal override void Write(ContentWriter output, List<T> value)
        {
            output.Write(value.Count);
            foreach (T current in value)
            {
                output.WriteObject<T>(current, this.elementWriter);
            }
        }

        protected internal override bool ShouldCompressContent(TargetPlatform targetPlatform, object value)
        {
            return this.elementWriter.ShouldCompressContent(targetPlatform, null);
        }
    }
}
