#region Using Statements
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class BasicEffectReader : ContentTypeReader<BasicEffect>
    {
        protected internal override BasicEffect Read(ContentReader input, BasicEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var texture = input.ReadExternalReference<Texture2D>();
            var effect = new BasicEffect(graphics)
            {
                DiffuseColor = input.ReadVector3(),
                EmissiveColor = input.ReadVector3(),
                SpecularColor = input.ReadVector3(),
                SpecularPower = input.ReadSingle(),
                Alpha = input.ReadSingle(),
                VertexColorEnabled = input.ReadBoolean()
            };

            if (texture != null)
            {
                effect.Texture = texture;
                effect.TextureEnabled = true;
            }

            return effect;
        }
    }
}
