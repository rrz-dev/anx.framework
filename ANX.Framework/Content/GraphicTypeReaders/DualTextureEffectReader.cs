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
    public class DualTextureEffectReader : ContentTypeReader<DualTextureEffect>
    {
        protected internal override DualTextureEffect Read(ContentReader input, DualTextureEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var effect = new DualTextureEffect(graphics);

            effect.Texture =input.ReadExternalReference<Texture2D>();
            effect.Texture2 = input.ReadExternalReference<Texture2D>();
            effect.DiffuseColor = input.ReadVector3();
            effect.Alpha = input.ReadSingle();
            effect.VertexColorEnabled = input.ReadBoolean();
            return effect;
        }
    }
}
