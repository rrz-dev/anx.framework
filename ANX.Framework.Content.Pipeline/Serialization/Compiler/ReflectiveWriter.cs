using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    class ReflectiveWriter : ContentTypeWriter
    {
        public ReflectiveWriter(Type targetType) : base(targetType)
        {

        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return" ANX.Framework.Content.ReflectiveReader";
        }

        protected internal override void Write(ContentWriter output, object value)
        {
            throw new NotImplementedException();
        }
    }
}
