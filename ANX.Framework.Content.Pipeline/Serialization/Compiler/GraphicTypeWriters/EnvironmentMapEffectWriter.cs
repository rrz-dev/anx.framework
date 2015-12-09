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
    internal class EnvironmentMapEffectWriter : BuiltinTypeWriter<EnvironmentMapMaterialContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(EnvironmentMapEffect).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, EnvironmentMapMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            //Default values fetched from EnvironmentMapEffect class
            output.WriteExternalReference<TextureContent>(value.Texture);
            output.WriteExternalReference<TextureContent>(value.EnvironmentMap);
            output.Write(value.EnvironmentMapAmount.GetValueOrDefault(1f));
            output.Write(value.EnvironmentMapSpecular.GetValueOrDefault(Vector3.Zero));
            output.Write(value.FresnelFactor.GetValueOrDefault(1f));
            output.Write(value.DiffuseColor.GetValueOrDefault(Vector3.One));
            output.Write(value.EmissiveColor.GetValueOrDefault(Vector3.Zero));
            output.Write(value.Alpha.GetValueOrDefault(1f));
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(EnvironmentMapEffect), targetPlatform);
        }
    }
}
