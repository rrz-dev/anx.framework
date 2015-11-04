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
    internal class SkinnedEffectWriter : BuiltinTypeWriter<SkinnedMaterialContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(SkinnedEffect).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, SkinnedMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            //Default values fetched from SkinnedEffect class
            output.WriteExternalReference<TextureContent>(value.Texture);
            output.Write(value.WeightsPerVertex.GetValueOrDefault(4));
            output.Write(value.DiffuseColor.GetValueOrDefault(Vector3.One));
            output.Write(value.EmissiveColor.GetValueOrDefault(Vector3.Zero));
            output.Write(value.SpecularColor.GetValueOrDefault(Vector3.One));
            output.Write(value.SpecularPower.GetValueOrDefault(16f));
            output.Write(value.Alpha.GetValueOrDefault(1f));
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(SkinnedEffect), targetPlatform);
        }
    }
}
