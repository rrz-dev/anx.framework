#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Graphics
{
    public sealed class SpriteFont
    {
        #region Private Members
        private Texture2D texture;
        private List<Rectangle> glyphs;
        private List<Rectangle> cropping;
        private List<char> characterMap;
        private int lineSpacing;
        private float horizontalSpacing;
        private List<Vector3> kerning;
        private char? defaultCharacter;
        private ReadOnlyCollection<Char> characters;

        #endregion // Private Members

        public ReadOnlyCollection<Char> Characters
        {
            get { return characters; }
        }

        public char? DefaultCharacter
        {
            get { return defaultCharacter; }
            set
            {
                if (value.HasValue && !this.characterMap.Contains(value.Value))
                {
                    throw new NotSupportedException("Character is not in used font");
                }
                this.defaultCharacter = value;
            }
        }

        public int LineSpacing
        {
            get { return lineSpacing; }
            set { lineSpacing = value; }
        }

        public float Spacing
        {
            get { return horizontalSpacing; }
            set { horizontalSpacing = value; }
        }


        internal SpriteFont(
            Texture2D texture, List<Rectangle> glyphs, List<Rectangle> cropping, List<char> charMap,
            int lineSpacing, float horizontalSpacing, List<Vector3> kerning, char? defaultCharacter)
        {
            this.texture = texture;
            this.glyphs = glyphs;
            this.cropping = cropping;
            this.characterMap = charMap;
            this.lineSpacing = lineSpacing;
            this.horizontalSpacing = horizontalSpacing;
            this.kerning = kerning;
            this.defaultCharacter = defaultCharacter;

            this.characters = new ReadOnlyCollection<char>(characterMap);
        }

        public Vector2 MeasureString(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            return InternalMeasure(ref text);
        }

        public Vector2 MeasureString(StringBuilder text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            String cachedText = text.ToString();
            return InternalMeasure(ref cachedText);
        }

        internal void DrawString(ref String text, SpriteBatch spriteBatch, Vector2 textPos, Color color, Vector2 scale, Vector2 origin, float rotation, float layerDepth, SpriteEffects effects)
        {
            Matrix transformation = Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(-origin.X * scale.X, -origin.Y * scale.Y, 0f);
            int horizontalFlipModifier = 1;
            float width = 0f;
            Vector2 topLeft = new Vector2();
            bool firstCharacterInLine = true;

            if ((effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally)
            {
                topLeft.X = width = this.InternalMeasure(ref text).X * scale.X;
                horizontalFlipModifier = -1;
            }

            if ((effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically)
            {
                topLeft.Y = (this.InternalMeasure(ref text).Y - this.lineSpacing) * scale.Y;
            }

            for (int i = 0; i < text.Length; i++)
            {
                char currentCharacter = text[i];
                switch (currentCharacter)
                {
                    case '\r':
                        break;

                    case '\n':
                        firstCharacterInLine = true;
                        topLeft.X = width;
                        if ((effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically)
                        {
                            topLeft.Y -= this.lineSpacing * scale.Y;
                        }
                        else
                        {
                            topLeft.Y += this.lineSpacing * scale.Y;
                        }
                        break;

                    default:
                        {
                            int characterIndex = GetIndexForCharacter(currentCharacter);
                            Vector3 kerning = this.kerning[characterIndex];
                            Rectangle glyphRect = this.glyphs[characterIndex];
                            Rectangle croppingRect = this.cropping[characterIndex];

                            if (firstCharacterInLine)
                            {
                                kerning.X = Math.Max(kerning.X, 0f);
                            }
                            else
                            {
                                topLeft.X += (this.Spacing * scale.X) * horizontalFlipModifier;
                            }
                            topLeft.X += (kerning.X * scale.X) * horizontalFlipModifier;

                            if ((effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically)
                            {
                                croppingRect.Y = (this.lineSpacing - glyphRect.Height) - croppingRect.Y;
                            }
                            if ((effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally)
                            {
                                croppingRect.X -= croppingRect.Width;
                            }
                            Vector2 position = Vector2.Transform(topLeft + (new Vector2(croppingRect.X, croppingRect.Y) * scale), transformation);
                            spriteBatch.Draw(this.texture, position + textPos, glyphRect, color, rotation, Vector2.Zero, scale, effects, layerDepth);
                            firstCharacterInLine = false;
                            topLeft.X += ((kerning.Y + kerning.Z) * scale.X) * horizontalFlipModifier;
                            break;
                        }
                }
            }
        }

        private Vector2 InternalMeasure(ref String text)
        {
            if (text.Length < 1)
            {
                return Vector2.Zero;
            }

            Vector2 size = Vector2.Zero;
            size.Y = this.lineSpacing;
            float maxWidth = 0f;
            int currentCharacter = 0;
            float z = 0f;
            bool firstCharacterInLine = true;

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                if (currentChar == '\r')
                {
                    continue;
                }

                if (currentChar == '\n')
                {
                    size.X += Math.Max(z, 0f);
                    z = 0f;
                    maxWidth = Math.Max(size.X, maxWidth);
                    size = Vector2.Zero;
                    size.Y = this.lineSpacing;
                    firstCharacterInLine = true;
                    currentCharacter++;
                }
                else
                {
                    int currentCharIndex = this.GetIndexForCharacter(currentChar);
                    Vector3 kerning = this.kerning[currentCharIndex];
                    if (firstCharacterInLine)
                    {
                        kerning.X = Math.Max(kerning.X, 0f);
                    }
                    else
                    {
                        size.X += this.Spacing + z;
                    }
                    size.X += kerning.X + kerning.Y;
                    z = kerning.Z;
                    size.Y = Math.Max(size.Y, (float)this.cropping[currentCharIndex].Height);
                    firstCharacterInLine = false;
                }
            }
            size.Y += currentCharacter * this.lineSpacing;
            size.X = Math.Max(Math.Max(z, 0) + size.X, maxWidth);
            return size;
        }

        private int GetIndexForCharacter(char character)
        {
            int currentIndex = 0;
            int upperBound = this.characterMap.Count - 1;
            char testChar;
            int searchPos;

            while (currentIndex <= upperBound)
            {
                searchPos = currentIndex + ((upperBound - currentIndex) >> 1);
                testChar = this.characterMap[searchPos];
                if (testChar == character)
                {
                    return searchPos;
                }
                else if (testChar > character)
                {
                    upperBound = searchPos - 1;
                }
                else
                {
                    currentIndex = searchPos + 1;
                }
            }

            if (this.defaultCharacter.HasValue)
            {
                return this.GetIndexForCharacter(this.defaultCharacter.Value);
            }

            throw new ArgumentException("character not found");
        }

    }
}
