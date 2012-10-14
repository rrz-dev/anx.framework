using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public enum VertexElementUsage
    {
        Position = 0,
        Color = 1,
        TextureCoordinate = 2,
        Normal = 3,
        Binormal = 4,
        Tangent = 5,
        BlendIndices = 6,
        BlendWeight = 7,
        Depth = 8, 
        Fog = 9,
        PointSize = 10,
        Sample = 11,
        TessellateFactor = 12,
    }
}
