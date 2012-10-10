// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using ANX.Framework.NonXNA.Development;
namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public enum EffectParameterType
    {
        Void,
        Bool,
        Int32,
        Single,
        String,
        Texture,
        Texture1D,
        Texture2D,
        Texture3D,
        TextureCube
    }
}
