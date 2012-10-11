using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Content
{
    [PercentageComplete(100)]
    [Developer("GinieDP")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class AlphaTestEffectReader : ContentTypeReader<AlphaTestEffect>
	{
		protected internal override AlphaTestEffect Read(ContentReader input, AlphaTestEffect existingInstance)
		{
			var graphics = input.ResolveGraphicsDevice();
			var effect = new AlphaTestEffect(graphics)
			{
			    Texture = input.ReadExternalReference<Texture2D>(),
			    AlphaFunction = (CompareFunction)input.ReadInt32(),
			    ReferenceAlpha = input.ReadInt32(),
			    DiffuseColor = input.ReadVector3(),
			    Alpha = input.ReadSingle(),
			    VertexColorEnabled = input.ReadBoolean()
			};

		    return effect;
		}
	}
}