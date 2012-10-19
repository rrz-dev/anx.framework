using System;
using System.IO;
using System.Xml;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.NonXNA.Development;

namespace ANX.Framework.Content.Pipeline.Importer
{
    /// <summary>Provides methods for reading .spritefont files for use in the Content Pipeline.</summary>
    [PercentageComplete(20)]
    [Developer("SilentWarrior/Eagle Eye Studios")]
    [TestState(TestStateAttribute.TestState.Untested)]
    [ContentImporter("Spritefont", ".spritefont", DefaultProcessor = "FontDescriptionProcessor")]
    public class FontDescriptionImporter : ContentImporter<FontDescription>
    {
        /// <summary>Called by the Framework when importing a .spritefont file to be used as a game asset. This is the method called by the Framework when an asset is to be imported into an object that can be recognized by the Content Pipeline.</summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        public override FontDescription Import(string filename, ContentImporterContext context)
        {
            FontDescription fontDescription = null;
            using (XmlReader xmlReader = XmlReader.Create(filename))
            {
                fontDescription = Deserialize(xmlReader, filename);
            }
            fontDescription.Identity = new ContentIdentity(new FileInfo(filename).FullName, "FontDescriptionImporter");
            return fontDescription;
        }

        private FontDescription Deserialize(XmlReader reader, string filename)
        {
            throw new NotImplementedException();
        }
    }
}
