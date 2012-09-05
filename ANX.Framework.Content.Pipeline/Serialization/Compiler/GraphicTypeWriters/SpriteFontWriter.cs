using System.Reflection;
using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
	[ContentTypeWriter]
	internal class SpriteFontWriter : BuiltinTypeWriter<SpriteFontContent>
	{
		protected override Assembly RuntimeAssembly
		{
			get
			{
				return typeof(SpriteFont).Assembly;
			}
		}

		protected internal override void Write(ContentWriter output, SpriteFontContent value)
		{
			output.WriteObject(value.Texture);
			output.WriteObject(value.Glyphs);
			output.WriteObject(value.Cropping);
			output.WriteObject(value.CharacterMap);
			output.Write(value.LineSpacing);
			output.Write(value.Spacing);
			output.WriteObject(value.Kerning);
			output.Write(value.DefaultCharacter.HasValue);
			if (value.DefaultCharacter.HasValue)
				output.Write(value.DefaultCharacter.Value);
		}

		public override string GetRuntimeType(TargetPlatform targetPlatform)
		{
			return ContentTypeWriter.GetStrongTypeName(typeof(SpriteFont), targetPlatform);
		}
	}
}
