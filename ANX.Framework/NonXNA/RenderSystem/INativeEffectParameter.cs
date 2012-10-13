#region Using Statements
using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
    public interface INativeEffectParameter
    {
        string Name { get; }
        string Semantic { get; }
        int ColumnCount { get; }
        int RowCount { get; }
        EffectParameterClass ParameterClass { get; }
        EffectParameterType ParameterType { get; }

        #region GetValue
        bool GetValueBoolean();

        bool[] GetValueBooleanArray(int count);

        int GetValueInt32();

        int[] GetValueInt32Array(int count);

        Matrix GetValueMatrix();

        Matrix[] GetValueMatrixArray(int count);

        Matrix GetValueMatrixTranspose();

        Matrix[] GetValueMatrixTransposeArray(int count);

        Quaternion GetValueQuaternion();

        Quaternion[] GetValueQuaternionArray(int count);

        float GetValueSingle();

        float[] GetValueSingleArray(int count);

        string GetValueString();

        Texture2D GetValueTexture2D();

        Texture3D GetValueTexture3D();

        TextureCube GetValueTextureCube();

        Vector2 GetValueVector2();

        Vector2[] GetValueVector2Array(int count);

        Vector3 GetValueVector3();

        Vector3[] GetValueVector3Array(int count);

        Vector4 GetValueVector4();

        Vector4[] GetValueVector4Array(int count);
        #endregion

        #region SetValue
        void SetValue(bool value);

        void SetValue(bool[] value);

        void SetValue(int value);

        void SetValue(int[] value);

        void SetValue(Matrix value, bool transpose);

        void SetValue(Matrix[] value, bool transpose);

        void SetValue(Quaternion value);

        void SetValue(Quaternion[] value);

        void SetValue(float value);

        void SetValue(float[] value);

        void SetValue(Vector2 value);

        void SetValue(Vector2[] value);

        void SetValue(Vector3 value);

        void SetValue(Vector3[] value);

        void SetValue(Vector4 value);

        void SetValue(Vector4[] value);

        void SetValue(string value);

        void SetValue(Texture value);
        #endregion
    }
}
