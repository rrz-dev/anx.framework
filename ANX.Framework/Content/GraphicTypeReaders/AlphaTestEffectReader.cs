using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [Developer("GinieDP")]
    public class AlphaTestEffectReader : ContentTypeReader<AlphaTestEffect>
	{
		protected internal override AlphaTestEffect Read(ContentReader input, AlphaTestEffect existingInstance)
		{
			var graphics = input.ResolveGraphicsDevice();
			var effect = new AlphaTestEffect(graphics);

			effect.Texture = input.ReadExternalReference<Texture2D>();
			effect.AlphaFunction = (CompareFunction)input.ReadInt32();
			effect.ReferenceAlpha = input.ReadInt32();
			effect.DiffuseColor = input.ReadVector3();
			effect.Alpha = input.ReadSingle();
			effect.VertexColorEnabled = input.ReadBoolean();
			return effect;
		}
	}
}