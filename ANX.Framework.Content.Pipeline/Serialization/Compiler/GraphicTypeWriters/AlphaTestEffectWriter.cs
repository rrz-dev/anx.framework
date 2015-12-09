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
    internal class AlphaTestEffectWriter : BuiltinTypeWriter<AlphaTestMaterialContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(AlphaTestEffect).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, AlphaTestMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            //Default values fetched from AlphaTestEffect class
            output.WriteExternalReference<TextureContent>(value.Texture);
            output.Write((int)value.AlphaFunction.GetValueOrDefault(CompareFunction.Greater));
            output.Write(value.ReferenceAlpha.GetValueOrDefault(0));
            output.Write(value.DiffuseColor.GetValueOrDefault(Vector3.One));
            output.Write(value.Alpha.GetValueOrDefault(1f));
            output.Write(value.VertexColorEnabled.GetValueOrDefault(false));
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(AlphaTestEffect), targetPlatform);
        }
    }
}
