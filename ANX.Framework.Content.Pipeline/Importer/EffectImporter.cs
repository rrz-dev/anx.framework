#region Using Statements
using System.IO;
using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline
{
    [ContentImporter(new string[] { ".fx", ".fxg", ".hlsl", ".glsl" })]
    public class EffectImporter : ContentImporter<EffectContent>
    {
        public override EffectContent Import(string filename, ContentImporterContext context)
        {
            string fileExtension = Path.GetExtension(filename).ToLowerInvariant();
            EffectSourceLanguage sourceLanguage = EffectSourceLanguage.HLSL_FX;

            switch (fileExtension)
            {
                case ".fx":
                    sourceLanguage = EffectSourceLanguage.HLSL_FX;
                    break;
                case ".fxg":
                    sourceLanguage = EffectSourceLanguage.GLSL_FX;
                    break;
                case ".hlsl":
                    sourceLanguage = EffectSourceLanguage.HLSL;
                    break;
                case ".glsl":
                    sourceLanguage = EffectSourceLanguage.GLSL;
                    break;
                default:
                    throw new InvalidContentException("The EffectImporter is not able to import a file with extension '" + fileExtension + "'");
            }

            EffectContent content = new EffectContent()
            {
                EffectCode = System.IO.File.ReadAllText(filename),
                Identity = new ContentIdentity(filename, null, null),
                SourceLanguage = sourceLanguage,
            };

            return content;
        }
    }
}
