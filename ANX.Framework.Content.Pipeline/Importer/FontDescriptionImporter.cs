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
using ANX.Framework.Content.Pipeline.Serialization.Intermediate;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Importer
{
    /// <summary>Provides methods for reading .spritefont files for use in the Content Pipeline.</summary>
    [PercentageComplete(90)]
    [Developer("SilentWarrior/Eagle Eye Studios, KorsarNek")]
    [TestState(TestStateAttribute.TestState.InProgress)] //Works but there should be a check whether the characters are supported
    [ContentImporter("Spritefont", ".spritefont", DefaultProcessor = "FontDescriptionProcessor", DisplayName = "FontDescription Importer - ANX Framework", Category = "Font Files")]
    public class FontDescriptionImporter : ContentImporter<FontDescription>
    {
        /// <summary>
        /// Called by the Framework when importing a .spritefont file to be used as a game asset. This is the method called by the Framework when an asset is to be imported into an object that can be recognized by the Content Pipeline.
        /// </summary>
        /// <param name="filename">Name of a game asset file.</param>
        /// <param name="context">Contains information for importing a game asset, such as a logger interface.</param>
        public override FontDescription Import(string filename, ContentImporterContext context)
        {
            using (XmlReader reader = XmlReader.Create(filename))
            {
                FontDescription fontDescription = IntermediateSerializer.Deserialize<FontDescription>(reader, filename);
                fontDescription.Identity = new ContentIdentity(new FileInfo(filename).FullName, "FontDescriptionImporter");

                return fontDescription;
            }
        }
    }
}
