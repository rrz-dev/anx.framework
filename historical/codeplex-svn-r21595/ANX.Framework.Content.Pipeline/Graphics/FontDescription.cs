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

        [ContentSerializerIgnoreAttribute]
        public ICollection<char> Characters
        {
            get;
            set;
        }

        [ContentSerializerAttribute]
        public Nullable<char> DefaultCharacter 
        { 
            get; 
            set; 
        }

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

        public float Spacing 
        { 
            get; 
            set; 
        }

        public FontDescriptionStyle Style 
        { 
            get; 
            set; 
        }

        [ContentSerializerAttribute]
        public bool UseKerning 
        { 
            get; 
            set; 
        }
    }
}
