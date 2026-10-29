#region Using Statements
using System;
using System.Reflection;
using ANX.Framework.Content.Pipeline.Processors;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content.Pipeline.Serialization.Compiler
{
    [ContentTypeWriter]
    internal class EffectWriter : BuiltinTypeWriter<CompiledEffectContent>
    {
        protected override Assembly RuntimeAssembly
        {
            get
            {
                return typeof(Effect).Assembly;
            }
        }

        protected internal override void Write(ContentWriter output, CompiledEffectContent value)
        {
            if (output.TargetPlatform != TargetPlatform.Windows)
            {
                throw new InvalidOperationException("currently only HLSL windows effects are supported by EffectWriter");
            }

            byte[] effectCode = value.GetEffectCode();
            output.Write((byte)value.SourceLanguage);       // ANX Extensions !!!
            output.Write(effectCode.Length);
            output.Write(effectCode);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(Effect), targetPlatform);
        }
    }
}
