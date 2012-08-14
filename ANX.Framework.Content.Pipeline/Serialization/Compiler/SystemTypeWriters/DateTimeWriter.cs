using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.SystemTypeWriters
{
	[ContentTypeWriter]
	internal class DateTimeWriter : BuiltinTypeWriter<DateTime>
	{
		protected internal override void Write(ContentWriter output, DateTime value)
		{
			DateTimeKind kind = value.Kind;
			if (kind == DateTimeKind.Local)
			{
				value = value.ToUniversalTime();
			}
			output.Write(value.Ticks | (long)kind << 62);
		}
	}
}
