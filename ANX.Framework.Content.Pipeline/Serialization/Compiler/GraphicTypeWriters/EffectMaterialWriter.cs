using ANX.Framework.Content.Pipeline.Graphics;
using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler.GraphicTypeWriters
{
    [ContentTypeWriter]
    internal class EffectMaterialWriter : BuiltinTypeWriter<EffectMaterialContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(EffectMaterial).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, EffectMaterialContent value)
        {
            if (output == null)
                throw new ArgumentNullException("output");
            
            if (value == null)
                throw new ArgumentNullException("value");
            
            if (value.CompiledEffect == null)
                throw new InvalidContentException("The compiled effect is null", value.Identity);
            

            output.WriteExternalReference<CompiledEffectContent>(value.CompiledEffect);

            Dictionary<string, object> collectedData = new Dictionary<string, object>();
            foreach (var pair in value.OpaqueData)
            {
                if (!(pair.Key == "Effect") && !(pair.Key == "CompiledEffect"))
                {
                    collectedData.Add(pair.Key, pair.Value);
                }
            }

            foreach (var pair in value.Textures)
            {
                collectedData.Add(pair.Key, pair.Value);
            }

            output.WriteObject<Dictionary<string, object>>(collectedData);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(EffectMaterial), targetPlatform);
        }
    }
}
