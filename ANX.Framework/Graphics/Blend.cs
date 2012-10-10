// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public enum Blend
    {
        One,
        Zero,
        SourceColor,
        InverseSourceColor,
        SourceAlpha,
        InverseSourceAlpha,
        DestinationColor,
        InverseDestinationColor,
        DestinationAlpha,
        InverseDestinationAlpha,
        BlendFactor,
        InverseBlendFactor,
        SourceAlphaSaturation,
    }
}
