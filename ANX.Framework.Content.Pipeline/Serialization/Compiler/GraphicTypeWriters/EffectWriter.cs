#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Content.Pipeline.Processors;
using System.Reflection;
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
            //TODO: implement
            //if (output.TargetPlatform == TargetPlatform.WindowsPhone)
            //{
            //    throw new InvalidContentException(Resources.MobileNoEffects);
            //}
            byte[] effectCode = value.GetEffectCode();
            output.Write(effectCode.Length);
            output.Write(effectCode);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return ContentTypeWriter.GetStrongTypeName(typeof(Effect), targetPlatform);
        }
    }
}
