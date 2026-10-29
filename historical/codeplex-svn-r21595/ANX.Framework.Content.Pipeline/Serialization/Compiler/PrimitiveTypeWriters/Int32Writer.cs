using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.PrimitiveTypeWriters
{
	[ContentTypeWriter]
	internal class Int32Writer : BuiltinTypeWriter<int>
	{
		protected internal override void Write(ContentWriter output, int value)
		{
			output.Write(value);
		}
	}

	[ContentTypeWriter]
	internal class UInt32Writer : BuiltinTypeWriter<uint>
	{
		protected internal override void Write(ContentWriter output, uint value)
		{
			output.Write(value);
		}
	}
}
