using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.PrimitiveTypeWriters
{
	[ContentTypeWriter]
	internal class Int16Writer : BuiltinTypeWriter<short>
	{
		protected internal override void Write(ContentWriter output, short value)
		{
			output.Write(value);
		}
	}

	[ContentTypeWriter]
	internal class UInt16Writer : BuiltinTypeWriter<ushort>
	{
		protected internal override void Write(ContentWriter output, ushort value)
		{
			output.Write(value);
		}
	}
}
