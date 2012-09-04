using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    [Developer("Glatzemann")]
    public interface IEffectFog
    {
        Vector3 FogColor { get; set; }
        bool FogEnabled { get; set; }
        float FogEnd { get; set; }
        float FogStart { get; set; }
    }
}
