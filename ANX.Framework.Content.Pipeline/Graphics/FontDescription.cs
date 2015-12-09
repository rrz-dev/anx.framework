#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Graphics
{
    public class FontDescription : ContentItem
    {
        private List<char> _characters = new List<char>();

        internal FontDescription()
        {

        }

        public FontDescription(string fontName, float size, float spacing)
        {
            FontName = fontName;
            Size = size;
            Spacing = spacing;
        }

        public FontDescription(string fontName, float size, float spacing, FontDescriptionStyle fontStyle)
        {
            FontName = fontName;
            Size = size;
            Spacing = spacing;
            Style = fontStyle;
        }

        public FontDescription(string fontName, float size, float spacing, FontDescriptionStyle fontStyle, bool useKerning)
        {
            FontName = fontName;
            Size = size;
            Spacing = spacing;
            Style = fontStyle;
            UseKerning = useKerning;
        }


        [ContentSerializer(AllowNull = false)]
        public string FontName
        {
            get;
            set;
        }

        public float Size
        {
            get;
            set;
        }

        [ContentSerializer(Optional = true)]
        public float Spacing
        {
            get;
            set;
        }

        [ContentSerializer(Optional = true)]
        public bool UseKerning
        {
            get;
            set;
        }

        public FontDescriptionStyle Style
        {
            get;
            set;
        }

        [ContentSerializer(Optional = true)]
        public Nullable<char> DefaultCharacter
        {
            get;
            set;
        }

        [ContentSerializer(CollectionItemName = "CharacterRegion")]
        internal CharacterRegion[] CharacterRegions
        {
            get
            {
                if (_characters.Count == 0)
                    return new CharacterRegion[0];

                List<CharacterRegion> regions = new List<CharacterRegion>();
                _characters.Sort();

                //Find gaps in the characters and put them into groups.
                char start = _characters[0];
                char previousCharacter = start;
                for (int i = 1; i < _characters.Count; i++)
                {
                    if (_characters[i] != previousCharacter + 1)
                    {
                        regions.Add(new CharacterRegion() { Start = start, End = previousCharacter});
                        start = _characters[i];
                    }
                    previousCharacter = _characters[i];
                }

                regions.Add(new CharacterRegion() { Start = start, End = previousCharacter });

                return regions.ToArray();
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _characters.Clear();
                foreach (CharacterRegion region in value)
                {
                    if (region.End < region.Start)
                        throw new ArgumentException(string.Format("The end value is lower than the start value for the character region {0}:{1}", region.Start, region.End));

                    for (char c = region.Start; c <= region.End; c++)
                        _characters.Add(c);
                }
            }
        }

        [ContentSerializerIgnore]
        public ICollection<char> Characters
        {
            get { return _characters; }
        }
    }
}
