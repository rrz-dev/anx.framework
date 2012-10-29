#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    internal class EnumWriter<T> : ContentTypeWriter<T> where T : struct, IConvertible
    {
        private Type underlyingType = Enum.GetUnderlyingType(typeof(T));
        private ContentTypeWriter underlyingTypeWriter;
        
        protected override void Initialize(ContentCompiler compiler)
        {
            this.underlyingTypeWriter = compiler.GetTypeWriter(this.underlyingType);
        }

        protected internal override void Write(ContentWriter output, T value)
        {
            object value2 = value.ToType(this.underlyingType, CultureInfo.InvariantCulture);
            output.WriteRawObject<object>(value2, this.underlyingTypeWriter);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return string.Concat(new object[]
			{
				typeof(ContentTypeReader).Namespace,
				'.',
				"EnumReader`1[[",
				this.GetRuntimeType(targetPlatform),
				"]]"
			});
        }
    }
}
