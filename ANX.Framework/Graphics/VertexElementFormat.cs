using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public enum VertexElementFormat
    {
        Single = 0,
        Vector2 = 1,
        Vector3 = 2,
        Vector4 = 3,
        Color = 4,
        Byte4 = 5,
        Short2 = 6,
        Short4 = 7,
        NormalizedShort2 = 8,
        NormalizedShort4 = 9,
        HalfVector2 = 10,
        HalfVector4 = 11,
    }
}
