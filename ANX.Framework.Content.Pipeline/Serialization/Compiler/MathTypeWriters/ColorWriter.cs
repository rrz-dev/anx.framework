using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.MathTypeWriters
{
	[ContentTypeWriter]
	internal class ColorWriter : BuiltinTypeWriter<Color>
	{
		protected internal override void Write(ContentWriter output, Color value)
		{
			output.Write(value.PackedValue);
		}
	}
}
