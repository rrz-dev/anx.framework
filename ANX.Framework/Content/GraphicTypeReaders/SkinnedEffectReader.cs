#region Using Statements
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    public class SkinnedEffectReader : ContentTypeReader<SkinnedEffect>
    {
        protected internal override SkinnedEffect Read(ContentReader input, SkinnedEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var effect = new SkinnedEffect(graphics)
            {
                Texture = input.ReadExternalReference<Texture2D>(),
                WeightsPerVertex = input.ReadInt32(),
                DiffuseColor = input.ReadVector3(),
                EmissiveColor = input.ReadVector3(),
                SpecularColor = input.ReadVector3(),
                SpecularPower = input.ReadSingle(),
                Alpha = input.ReadSingle()
            };

            return effect;
        }
    }
}
