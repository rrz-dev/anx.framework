#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    public class SkinnedEffectReader : ContentTypeReader<SkinnedEffect>
    {
        protected internal override SkinnedEffect Read(ContentReader input, SkinnedEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var effect = new SkinnedEffect(graphics);

            effect.Texture = input.ReadExternalReference<Texture2D>();
            effect.WeightsPerVertex = input.ReadInt32();
            effect.DiffuseColor = input.ReadVector3();
            effect.EmissiveColor = input.ReadVector3();
            effect.SpecularColor = input.ReadVector3();
            effect.SpecularPower = input.ReadSingle();
            effect.Alpha = input.ReadSingle();

            return effect;
        }
    }
}
