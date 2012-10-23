using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectConverter.Platforms
{
    public class AnxConverter : AbstractXna2AnxConverter
    {
        private readonly string[] xnaNamespaces = new string[] { "Microsoft.XNA.Framework",
                                                                 "Microsoft.XNA.Framework.Avatar",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline.AudioImporters",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline.EffectImporter",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline.FBXImporter",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline.TextureImporter",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline.VideoImporters",
                                                                 "Microsoft.XNA.Framework.Content.Pipeline.XImporter",
                                                                 "Microsoft.XNA.Framework.Game",
                                                                 "Microsoft.XNA.Framework.GamerServices",
                                                                 "Microsoft.XNA.Framework.Graphics",
                                                                 "Microsoft.XNA.Framework.Input",
                                                                 "Microsoft.XNA.Framework.Input.Touch",
                                                                 "Microsoft.XNA.Framework.Net",
                                                                 "Microsoft.XNA.Framework.Storage",
                                                                 "Microsoft.XNA.Framework.Video",
                                                                 "Microsoft.XNA.Framework.Xact",
                                                               };

        public override string Name
        {
            get { return "anx2xna"; }
        }

        protected override void ConvertUsingDirectives(string file, ref string target)
        {
            string content = System.IO.File.ReadAllText(System.IO.Path.Combine(CurrentProject.FullSourceDirectoryPath, file));
            ConvertUsingDirectivesImpl(ref content, ref target, "Microsoft.XNA.Framework", "ANX.Framework", xnaNamespaces);
        }
    }
}
