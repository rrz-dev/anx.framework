using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.PrimitiveTypeWriters
{
	[ContentTypeWriter]
	internal class Int64Writer : BuiltinTypeWriter<long>
	{
		protected internal override void Write(ContentWriter output, long value)
		{
			output.Write(value);
		}

		[ContentTypeWriter]
		internal class UInt64Writer : BuiltinTypeWriter<ulong>
		{
			protected internal override void Write(ContentWriter output, ulong value)
			{
				output.Write(value);
			}
		}
	}
}
