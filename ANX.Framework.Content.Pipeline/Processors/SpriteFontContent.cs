using System;
using System.Collections.Generic;
using ANX.Framework.Content.Pipeline.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Processors
{
    public sealed class SpriteFontContent
    {
		[ContentSerializer(ElementName = "Texture", AllowNull = false)]
		internal Texture2DContent Texture { get; set; }

		[ContentSerializer(ElementName = "Glyphs", AllowNull = false)]
		internal List<Rectangle> Glyphs { get; set; }

		[ContentSerializer(ElementName = "Cropping", AllowNull = false)]
		internal List<Rectangle> Cropping { get; set; }

		[ContentSerializer(ElementName = "CharacterMap", AllowNull = false)]
		internal List<char> CharacterMap { get; set; }

		[ContentSerializer(ElementName = "LineSpacing", AllowNull = false)]
		internal int LineSpacing { get; set; }

		[ContentSerializer(ElementName = "Spacing", AllowNull = false)]
		internal float Spacing { get; set; }

		[ContentSerializer(ElementName = "Kerning", AllowNull = false)]
		internal List<Vector3> Kerning { get; private set; }

		[ContentSerializer(ElementName = "DefaultCharacter", AllowNull = true)]
		internal char? DefaultCharacter { get; set; }

		internal SpriteFontContent()
		{
			Texture = new Texture2DContent();
			Glyphs = new List<Rectangle>();
			Cropping = new List<Rectangle>();
			CharacterMap = new List<char>();
			Kerning = new List<Vector3>();
		}
    }
}
