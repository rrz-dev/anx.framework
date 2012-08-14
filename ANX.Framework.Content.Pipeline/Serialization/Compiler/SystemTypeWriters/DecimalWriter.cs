using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.SystemTypeWriters
{
	[ContentTypeWriter]
	internal class DecimalWriter : BuiltinTypeWriter<decimal>
	{
		protected internal override void Write(ContentWriter output, decimal value)
		{
			int[] bits = decimal.GetBits(value);
			for (int i = 0; i < bits.Length; i++)
			{
				output.Write(bits[i]);
			}
		}
	}
}
