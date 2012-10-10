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
    public class BasicEffectReader : ContentTypeReader<BasicEffect>
    {
        protected internal override BasicEffect Read(ContentReader input, BasicEffect existingInstance)
        {
            var graphics = input.ResolveGraphicsDevice();
            var effect = new BasicEffect(graphics);
            Texture2D texture = input.ReadExternalReference<Texture2D>();
            // TODO: enable parameter setup when basic effect is implemented
            //if (texture != null)
            //{
            //    effect.Texture = texture;
            //    effect.TextureEnabled = true;
            //}
            /*effect.DiffuseColor = */input.ReadVector3();
            /*effect.EmissiveColor = */input.ReadVector3();
            /*effect.SpecularColor = */input.ReadVector3();
            /*effect.SpecularPower = */input.ReadSingle();
            /*effect.Alpha = */input.ReadSingle();
            /*effect.VertexColorEnabled = */input.ReadBoolean();
            return effect;
        }
    }
}
