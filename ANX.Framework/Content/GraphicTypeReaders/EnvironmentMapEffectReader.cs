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
    public class EnvironmentMapEffectReader : ContentTypeReader<EnvironmentMapEffect>
    {
        protected internal override EnvironmentMapEffect Read(ContentReader input, EnvironmentMapEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var effect = new EnvironmentMapEffect(graphics);
            effect.Texture = input.ReadExternalReference<Texture2D>();
            effect.EnvironmentMap = input.ReadExternalReference<TextureCube>();
            effect.EnvironmentMapAmount = input.ReadSingle();
            effect.EnvironmentMapSpecular = input.ReadVector3();
            effect.FresnelFactor = input.ReadSingle();
            effect.DiffuseColor = input.ReadVector3();
            effect.EmissiveColor = input.ReadVector3();
            effect.Alpha = input.ReadSingle();
            return effect;
        }
    }
}
