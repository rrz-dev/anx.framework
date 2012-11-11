#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.NonXNA.Development;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
    /// <summary>Provides methods for reading .spritefont files for use in the Content Pipeline.</summary>
    [PercentageComplete(90)]
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [TestState(TestStateAttribute.TestState.InProgress)] //Works but there should be a check whether the characters are supported
    [ContentImporter("Spritefont", ".spritefont", DefaultProcessor = "FontDescriptionProcessor", DisplayName = "FontDescription Importer - ANX Framework")]
    public class FontDescriptionImporter : ContentImporter<FontDescription>
    {
        private static ContentBuildLogger _logger;

        /// <summary>Called by the Framework when importing a .spritefont file to be used as a game asset. This is the method called by the Framework when an asset is to be imported into an object that can be recognized by the Content Pipeline.</summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        public override FontDescription Import(string filename, ContentImporterContext context)
        {
            _logger = context.Logger;
            var doc = XDocument.Load(filename);
            FontDescription fontDescription = DeserializeFont(doc);
            fontDescription.Identity = new ContentIdentity(new FileInfo(filename).FullName,
                                                           "FontDescriptionImporter");

            return fontDescription;
        }

        
        private static FontDescription DeserializeFont(XContainer xDoc)
        {
            //Check if either the XnaContent or the AnxContent attribute exists.
            var tag = xDoc.Element("XnaContent");
            if (tag == null)
            {
                tag = xDoc.Element("AnxContent");
                if (tag == null)
                    throw new InvalidContentException(
                        "The Xml file does not contain a valid XnaContent or AnxContent tag.");
            }

            //Check for asset element to ensure this is XNA content
            var assetNode = tag.Element("Asset");
            if (assetNode == null)
                throw new InvalidContentException("No valid Asset element in xml source.");

            //Only accept assets with type FontDescription
            var xAttribute = assetNode.Attribute("Type");
            if (xAttribute == null || xAttribute.Value != "Graphics:FontDescription")
                throw new InvalidContentException("This is not a font description!");

            //Check if the font name was specified
            var fontNameElement = assetNode.Element("FontName");
            if (fontNameElement == null)
                throw new InvalidContentException("No font name specified in xml source.");
            var fontName = fontNameElement.Value; // if there is a font name, store it

            //Same as above, except with a conversion of the size.
            var sizeElement = assetNode.Element("Size");
            if (sizeElement == null)
                throw new InvalidContentException("No font size specified in xml source.");
            var fontSize = Convert.ToInt32(sizeElement.Value);

            //Check and convert the spacing
            var spacingElement = assetNode.Element("Spacing");
            if (spacingElement == null)
                throw new InvalidContentException("Spacing is not defined in xml source.");
            var fontSpacing = Convert.ToInt32(spacingElement.Value);

            //Check for style element and try to parse it.
            var styleElement = assetNode.Element("Style");
            if (styleElement == null)
                throw new InvalidContentException("No style specified in xml source.");
            FontDescriptionStyle fontStyle;
            Enum.TryParse(styleElement.Value, out fontStyle);

            //Check for default character element and try to parse it
            var defaultCharElement = assetNode.Element("DefaultCharacter");
            char? defaultChar = null;
            if (defaultCharElement != null)
            {
                char c;
                if (Char.TryParse(defaultCharElement.Value, out c))
                {
                    defaultChar = c;
                }
            }

            //Get the character regions element to iterate through the character regions
            var charRegionsElement = assetNode.Element("CharacterRegions");
            if (charRegionsElement == null)
                throw new InvalidContentException("No character region specified in xml source");
            var fontCharRegions = charRegionsElement.Elements("CharacterRegion");

            var characters = new List<char>(); //The list that will be passed to the processor
            foreach (var fontCharRegion in fontCharRegions) 
            {
                //for each char declaration get the start and end elements
                var startElement = fontCharRegion.Element("Start");
                if (startElement == null)
                    throw new InvalidContentException("Character region is invalid. Start element is missing!");

                var endElement = fontCharRegion.Element("End");
                if (endElement == null)
                    throw new InvalidContentException("Character region is invalid. Start element is missing!");

                int startChar;
                int endChar;
                
                //Convert the chars back to hex values, silly xml importer reads them as chars, int value '32' equals an empty string or hex value #32
                startChar = String.IsNullOrEmpty(startElement.Value) ? 32 : Convert.ToInt32(Convert.ToChar(startElement.Value));
                endChar = Convert.ToInt32(Convert.ToChar(endElement.Value));
                
                _logger.LogMessage("Start: '" + startElement.Value + "' (" + startChar + ")");
                _logger.LogMessage("End: '" + endElement.Value + "' (" + endChar + ")");
                //Check if the chars are within ASCII range
                if ((startChar >= endChar) ||
                    (startChar < 0) || (startChar > 0xFFFF) ||
                    (endChar < 0) || (endChar > 0xFFFF))
                {
                    throw new ArgumentException("Invalid character range " +
                                                startElement.Value + " - " + endElement.Value);
                }
                //add each char from the range to the list
                for (var i = startChar; i <= endChar; i++)
                {
                    characters.Add(Convert.ToChar(i));
                }
                //Debugger.Break();
                //TODO: We need a platform independet solution for checking the characters supported by a font! For now we pass it.
            }

            _logger.LogMessage("Import of SpriteFont finished.");
            var result = new FontDescription(fontName, fontSize, fontSpacing, fontStyle)
                                         {
                                             Characters = characters, 
                                             DefaultCharacter = defaultChar  //Currently disabled because the ContentLoader does not like this (Bad XNB)
                                         };

            return result;
        }
    }
}
