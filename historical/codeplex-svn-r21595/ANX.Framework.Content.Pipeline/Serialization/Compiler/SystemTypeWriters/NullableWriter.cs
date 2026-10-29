using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.SystemTypeWriters
{
	[ContentTypeWriter]
	internal class NullableWriter<T> : BuiltinTypeWriter<T?> where T : struct
	{
		private ContentTypeWriter underlyingTypeWriter;

		protected override void Initialize(ContentCompiler compiler)
		{
			underlyingTypeWriter = compiler.GetTypeWriter(typeof(T));
		}

		protected internal override void Write(ContentWriter output, T? value)
		{
			output.Write(value.HasValue);
			if (value.HasValue)
			{
				output.WriteRawObject(value.Value, underlyingTypeWriter);
			}
		}

		protected internal override void Write(ContentWriter output, object value)
		{
			if (value == null)
			{
				output.Write(false);
				return;
			}

			base.Write(output, value);
		}
	}
}
