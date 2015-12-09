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
    internal class BasicEffectWriter : BuiltinTypeWriter<BasicMaterialContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(BasicEffect).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, BasicMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            //Default values fetched from BasicEffect class
            output.WriteExternalReference(value.Texture);
            output.Write(value.DiffuseColor.GetValueOrDefault(Vector3.One));
            output.Write(value.EmissiveColor.GetValueOrDefault(Vector3.Zero));
            output.Write(value.SpecularColor.GetValueOrDefault(Vector3.One));
            output.Write(value.SpecularPower.GetValueOrDefault(16f));
            output.Write(value.Alpha.GetValueOrDefault(1f));
            output.Write(value.VertexColorEnabled.GetValueOrDefault(false));
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(BasicEffect), targetPlatform);
        }
    }
}
