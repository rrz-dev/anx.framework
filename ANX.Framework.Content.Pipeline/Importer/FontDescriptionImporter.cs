#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    [PercentageComplete(40)]
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [TestState(TestStateAttribute.TestState.InProgress)] //Works except for the character list, because it seems there is no managed solution for getting a fonts characters
    [ContentImporter("Spritefont", ".spritefont", DefaultProcessor = "FontDescriptionProcessor")]
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
            var tag = xDoc.Element("XnaContent");
            if (tag == null)
            {
                tag = xDoc.Element("AnxContent");
                if (tag == null)
                    throw new InvalidContentException(
                        "The Xml file does not contain a valid XnaContent or AnxContent tag.");
            }

            var assetNode = tag.Element("Asset");
            if (assetNode == null)
                throw new InvalidContentException("No valid Asset element in xml source.");
            var xAttribute = assetNode.Attribute("Type");
            if (xAttribute == null || xAttribute.Value != "Graphics:FontDescription")
                throw new InvalidContentException("This is not a valid font description!");

            var fontNameElement = assetNode.Element("FontName");
            if (fontNameElement == null)
                throw new InvalidContentException("No font name specified in xml source.");
            var fontName = fontNameElement.Value;

            var sizeElement = assetNode.Element("Size");
            if (sizeElement == null)
                throw new InvalidContentException("No font size specified in xml source.");
            var fontSize = Convert.ToInt32(sizeElement.Value);

            var spacingElement = assetNode.Element("Spacing");
            if (spacingElement == null)
                throw new InvalidContentException("Spacing is not defined in xml source.");
            var fontSpacing = Convert.ToInt32(spacingElement.Value);

            var styleElement = assetNode.Element("Style");
            if (styleElement == null)
                throw new InvalidContentException("No style specified in xml source.");
            FontDescriptionStyle fontStyle;
            Enum.TryParse(styleElement.Value, out fontStyle);

            var charRegionsElement = assetNode.Element("CharacterRegions");
            if (charRegionsElement == null)
                throw new InvalidContentException("No character region specified in xml source");
            var fontCharRegions = charRegionsElement.Elements("CharacterRegion");

            var characters = new List<char>();
            foreach (var fontCharRegion in fontCharRegions)
            {
                var startElement = fontCharRegion.Element("Start");
                if (startElement == null)
                    throw new InvalidContentException("Character region is invalid. Start element is missing!");

                var endElement = fontCharRegion.Element("End");
                if (endElement == null)
                    throw new InvalidContentException("Character region is invalid. Start element is missing!");

                char startChar;
                char endChar;
                _logger.LogMessage("Start: " + startElement.Value);
                _logger.LogMessage("End: " + endElement.Value);
                Debugger.Break();
                startChar = Convert.ToChar(startElement.Value);
                endChar = Convert.ToChar(endElement.Value);

                Debugger.Break();
                //TODO: We need a platform independet solution for getting the characters supported by a font!

            }

            var result = new FontDescription(fontName, fontSize, fontSpacing, fontStyle)
                                         {
                                             Characters = characters
                                         };

            return result;
        }
    }
}
