using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
    [ContentTypeWriter]
    internal class DualTextureEffectWriter : BuiltinTypeWriter<DualTextureMaterialContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(DualTextureEffect).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, DualTextureMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            //Default values fetched from DualTextureEffect class
            output.WriteExternalReference<TextureContent>(value.Texture);
            output.WriteExternalReference<TextureContent>(value.Texture2);
            output.Write(value.DiffuseColor.GetValueOrDefault(Vector3.One));
            output.Write(value.Alpha.GetValueOrDefault(1f));
            output.Write(value.VertexColorEnabled.GetValueOrDefault(false));
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(DualTextureEffect), targetPlatform);
        }
    }
}
