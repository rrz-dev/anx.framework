using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.MathTypeWriters
{
	[ContentTypeWriter]
	internal class RectangleWriter : BuiltinTypeWriter<Rectangle>
	{
		protected internal override void Write(ContentWriter output, Rectangle value)
		{
			output.Write(value.X);
			output.Write(value.Y);
			output.Write(value.Width);
			output.Write(value.Height);
		}
	}
}
