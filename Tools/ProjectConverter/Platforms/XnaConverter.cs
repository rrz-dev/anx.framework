using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectConverter.Platforms
{
    public class XnaConverter : AbstractXna2AnxConverter
    {
        private readonly string[] anxNamespaces = new string[] { "ANX.Framework",
                                                                 "ANX.Framework.Avatar",
                                                                 "ANX.Framework.Content.Pipeline.AudioImporters",
                                                                 "ANX.Framework.Content.Pipeline",
                                                                 "ANX.Framework.Content.Pipeline.EffectImporter",
                                                                 "ANX.Framework.Content.Pipeline.FBXImporter",
                                                                 "ANX.Framework.Content.Pipeline.TextureImporter",
                                                                 "ANX.Framework.Content.Pipeline.VideoImporters",
                                                                 "ANX.Framework.Content.Pipeline.XImporter",
                                                                 "ANX.Framework.Game",
                                                                 "ANX.Framework.GamerServices",
                                                                 "ANX.Framework.Graphics",
                                                                 "ANX.Framework.Input",
                                                                 "ANX.Framework.Input.Touch",
                                                                 "ANX.Framework.Net",
                                                                 "ANX.Framework.Storage",
                                                                 "ANX.Framework.Video",
                                                                 "ANX.Framework.Xact",
                                                               };

        public override string Name
        {
            get { return "xna2anx"; }
        }

        protected override void ConvertUsingDirectives(string file, ref string target)
        {
            string content = System.IO.File.ReadAllText(System.IO.Path.Combine(CurrentProject.FullSourceDirectoryPath, file));
            ConvertUsingDirectivesImpl(ref content, ref target, "ANX.Framework", "Microsoft.XNA.Framework", anxNamespaces);
        }
    }
}
