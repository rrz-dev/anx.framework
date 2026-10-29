using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public interface INativeEffectAnnotation
    {
        int ColumnCount { get; }
        string Name { get; }
        EffectParameterClass ParameterClass { get; }
        EffectParameterType ParameterType { get; }
        int RowCount { get; }
        string Semantic { get; }

        bool GetValueBoolean();
        int GetValueInt32();
        Matrix GetValueMatrix();
        float GetValueSingle();
        string GetValueString();
        Vector2 GetValueVector2();
        Vector3 GetValueVector3();
        Vector4 GetValueVector4();
    }
}
