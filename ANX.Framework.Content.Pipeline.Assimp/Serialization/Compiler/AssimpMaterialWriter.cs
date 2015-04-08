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
    internal class AssimpMaterialWriter : ContentTypeWriter<AssimpMaterialContent>
    {
        protected override void Write(ContentWriter output, AssimpMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");

            if (value == null)
                throw new ArgumentNullException("value");

            //Writes as a BasicEffect.
            output.WriteExternalReference(value.Texture);
            output.Write(value.DiffuseColor.GetValueOrDefault(Vector3.One));
            output.Write(value.EmissiveColor.GetValueOrDefault(Vector3.Zero));
            output.Write(value.SpecularColor.GetValueOrDefault(Vector3.One));
            output.Write(value.SpecularPower.GetValueOrDefault(16f));
            output.Write(value.Alpha.GetValueOrDefault(1f));
            output.Write(value.VertexColorEnabled.GetValueOrDefault(false));
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(BasicEffectReader).AssemblyQualifiedName;
        }
    }
}
