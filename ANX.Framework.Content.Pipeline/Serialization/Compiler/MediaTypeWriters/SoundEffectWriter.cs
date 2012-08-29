using System;
using ANX.Framework.Audio;
using ANX.Framework.Content.Pipeline.Processors;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    internal class SoundEffectWriter : BuiltinTypeWriter<SoundEffectContent>
	{
		protected internal override void Write(ContentWriter output, SoundEffectContent value)
		{
			if (output == null)
				throw new ArgumentNullException("ouput");
			if (value == null)
				throw new ArgumentNullException("value");

			output.Write(value.Format.Count);
			output.Write(value.Format.ToArray());

			output.Write(value.Data.Count);
			output.Write(value.Data.ToArray());

			output.Write(value.LoopStart);
			output.Write(value.LoopLength);
			output.Write(value.Duration);
		}

		protected internal override bool ShouldCompressContent(TargetPlatform targetPlatform, object value)
		{
			return false;
		}

		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return ContentTypeWriter.GetStrongTypeName(typeof(SoundEffect), targetPlatform);
		}
	}
}
