#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Graphics;
using System.Reflection;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    internal class TextureWriter : BuiltinTypeWriter<TextureContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(Texture).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, TextureContent value)
        {
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(Texture), targetPlatform);
        }
    }
}
