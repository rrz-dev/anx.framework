#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    internal class SpriteFontReader : ContentTypeReader<SpriteFont>
    {
        protected internal override SpriteFont Read(ContentReader input, SpriteFont existingInstance)
        {
            Texture2D texture = input.ReadObject<Texture2D>();
            List<Rectangle> glyphs = input.ReadObject<List<Rectangle>>();
            List<Rectangle> cropping = input.ReadObject<List<Rectangle>>();
            List<Char> characterMap = input.ReadObject<List<Char>>();
            int verticalLineSpacing = input.ReadInt32();
            float horizontalSpacing = input.ReadSingle();
            List<Vector3> kerning = input.ReadObject<List<Vector3>>();
            char? defaultCharacter = input.ReadObject<char?>();

            SpriteFont spriteFont = new SpriteFont(
                texture, glyphs, cropping, characterMap, 
                verticalLineSpacing, horizontalSpacing, kerning, defaultCharacter);
            return spriteFont;
        }
    }
}
