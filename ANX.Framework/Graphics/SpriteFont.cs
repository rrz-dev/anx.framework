using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
	[Developer("Glatzemann, AstrorEnales")]
    public sealed class SpriteFont
    {
        #region Private
        private Texture2D texture;
        private Rectangle[] glyphs;
        private Rectangle[] cropping;
        private List<char> characterMap;
        private Vector3[] kernings;
		private char? defaultCharacter;
		Vector2 topLeft;
		Vector2 position;
        #endregion

		#region Public
		public ReadOnlyCollection<Char> Characters { get; private set; }
		public int LineSpacing { get; set; }
		public float Spacing { get; set; }

        public char? DefaultCharacter
        {
            get { return defaultCharacter; }
            set
            {
                if (value.HasValue && this.characterMap.Contains(value.Value) == false)
                    throw new NotSupportedException("Character is not in used font");

                this.defaultCharacter = value;
            }
        }
        #endregion

		#region Constructor
		internal SpriteFont(Texture2D texture, List<Rectangle> glyphs, List<Rectangle> cropping, List<char> charMap,
			int lineSpacing, float horizontalSpacing, List<Vector3> kerning, char? defaultCharacter)
        {
            this.texture = texture;
            this.glyphs = glyphs.ToArray();
			this.cropping = cropping.ToArray();
            this.characterMap = charMap;
            this.LineSpacing = lineSpacing;
			this.Spacing = horizontalSpacing;
			this.kernings = kerning.ToArray();
            this.defaultCharacter = defaultCharacter;

			Characters = new ReadOnlyCollection<char>(characterMap);
        }
		#endregion

		#region MeasureString
		public Vector2 MeasureString(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            return InternalMeasure(text);
        }

        public Vector2 MeasureString(StringBuilder text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            return InternalMeasure(text.ToString());
        }
		#endregion

		#region DrawString
		internal void DrawString(string text, SpriteBatch spriteBatch, Vector2 textPos, Color color, Vector2 scale,
			Vector2 origin, float rotation, float layerDepth, SpriteEffects effects)
        {
			Matrix rotationMatrix;
			Matrix.CreateRotationZ(rotation, out rotationMatrix);
			Matrix translationMatrix;
			Matrix.CreateTranslation(-origin.X * scale.X, -origin.Y * scale.Y, 0f, out translationMatrix);
            Matrix transformation;
			Matrix.Multiply(ref rotationMatrix, ref translationMatrix, out transformation);

			topLeft.X = topLeft.Y = 0f;
            int horizontalFlipModifier = 1;
            float width = 0f;
			bool flipVertically = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
			bool flipHorizontally = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;

			if (flipHorizontally)
            {
                topLeft.X = width = InternalMeasure(text).X * scale.X;
                horizontalFlipModifier = -1;
            }

			if (flipVertically)
                topLeft.Y = (InternalMeasure(text).Y - LineSpacing) * scale.Y;

			bool firstCharacterInLine = true;
			for (int i = 0; i < text.Length; i++)
			{
				char currentCharacter = text[i];
				if (currentCharacter == '\r')
					continue;

				if (currentCharacter == '\n')
				{
					firstCharacterInLine = true;
					topLeft.X = width;
					float factor = LineSpacing * scale.Y;
					topLeft.Y += flipVertically ? -factor : factor;
					continue;
				}

				int characterIndex = GetIndexForCharacter(currentCharacter);
				Vector3 kerning = kernings[characterIndex];
				Rectangle croppingRect = cropping[characterIndex];

				if (firstCharacterInLine)
					kerning.X = Math.Max(kerning.X, 0f);
				else
					topLeft.X += (Spacing * scale.X) * horizontalFlipModifier;

				topLeft.X += (kerning.X * scale.X) * horizontalFlipModifier;

				if (flipVertically)
					croppingRect.Y = (LineSpacing - glyphs[characterIndex].Height) - croppingRect.Y;

				if (flipHorizontally)
					croppingRect.X -= croppingRect.Width;

				position.X = topLeft.X + (croppingRect.X * scale.X);
				position.Y = topLeft.Y + (croppingRect.Y * scale.Y);
				Vector2 result;
				Vector2.Transform(ref position, ref transformation, out result);
				result.X += textPos.X;
				result.Y += textPos.Y;
				spriteBatch.DrawOptimizedText(texture, result, ref glyphs[characterIndex], ref color, rotation, scale,
					effects, layerDepth);
				firstCharacterInLine = false;
				topLeft.X += ((kerning.Y + kerning.Z) * scale.X) * horizontalFlipModifier;
			}
        }
		#endregion

		#region DrawString
		internal void DrawString(string text, SpriteBatch spriteBatch, Vector2 textPos, Color color)
		{
			topLeft.X = topLeft.Y = 0f;
			bool firstCharacterInLine = true;
			for (int i = 0; i < text.Length; i++)
			{
				char currentCharacter = text[i];
				if (currentCharacter == '\r')
					continue;

				if (currentCharacter == '\n')
				{
					firstCharacterInLine = true;
					topLeft.X = 0f;
					topLeft.Y += LineSpacing;
					continue;
				}

				int characterIndex = GetIndexForCharacter(currentCharacter);
				Vector3 kerning = kernings[characterIndex];
				Rectangle croppingRect = cropping[characterIndex];

				if (firstCharacterInLine)
					kerning.X = Math.Max(kerning.X, 0f);
				else
					topLeft.X += Spacing;

				topLeft.X += kerning.X;

				position.X = topLeft.X + croppingRect.X + textPos.X;
				position.Y = topLeft.Y + croppingRect.Y + textPos.Y;
				spriteBatch.DrawOptimizedText(texture, position, ref glyphs[characterIndex], ref color);
				firstCharacterInLine = false;
				topLeft.X += kerning.Y + kerning.Z;
			}
		}
		#endregion

		#region InternalMeasure
		private Vector2 InternalMeasure(string text)
        {
            if (text.Length < 1)
                return Vector2.Zero;

            Vector2 size = Vector2.Zero;
            size.Y = this.LineSpacing;
            float maxWidth = 0f;
            int currentCharacter = 0;
            float z = 0f;
            bool firstCharacterInLine = true;

			for (int i = 0; i < text.Length; i++)
			{
				char currentChar = text[i];
				if (currentChar == '\r')
					continue;

				if (currentChar == '\n')
				{
					size.X += Math.Max(z, 0f);
					z = 0f;
					maxWidth = Math.Max(size.X, maxWidth);
					size = Vector2.Zero;
					size.Y = LineSpacing;
					firstCharacterInLine = true;
					currentCharacter++;
					continue;
				}

				int currentCharIndex = GetIndexForCharacter(currentChar);
				Vector3 kerning = kernings[currentCharIndex];
				if (firstCharacterInLine)
					kerning.X = Math.Max(kerning.X, 0f);
				else
					size.X += this.Spacing + z;

				size.X += kerning.X + kerning.Y;
				z = kerning.Z;
				size.Y = Math.Max(size.Y, (float)cropping[currentCharIndex].Height);
				firstCharacterInLine = false;
			}

            size.Y += currentCharacter * LineSpacing;
            size.X = Math.Max(Math.Max(z, 0) + size.X, maxWidth);
            return size;
		}
		#endregion

		#region GetIndexForCharacter
		private int GetIndexForCharacter(char character)
		{
			int currentIndex = 0;
			int upperBound = this.characterMap.Count - 1;
			char testChar;
			int searchPos;

			while (currentIndex <= upperBound)
			{
				searchPos = currentIndex + ((upperBound - currentIndex) >> 1);
				testChar = characterMap[searchPos];
				if (testChar == character)
					return searchPos;
				else if (testChar > character)
					upperBound = searchPos - 1;
				else
					currentIndex = searchPos + 1;
			}

			if (defaultCharacter.HasValue)
				return GetIndexForCharacter(defaultCharacter.Value);

			throw new ArgumentException("character not found");
		}
		#endregion
    }
}
