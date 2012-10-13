using System;
using ANX.Framework.NonXNA.Development;
using ANX.Framework.NonXNA.RenderSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("AstrorEnales")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class EffectAnnotation
    {
        private readonly INativeEffectAnnotation nativeAnnotation;

		public int ColumnCount
		{
			get { return nativeAnnotation.ColumnCount; }
		}

		public string Name
		{
			get { return nativeAnnotation.Name; }
		}

		public EffectParameterClass ParameterClass
		{
			get { return nativeAnnotation.ParameterClass; }
		}

		public EffectParameterType ParameterType
		{
			get { return nativeAnnotation.ParameterType; }
		}

		public int RowCount
		{
			get { return nativeAnnotation.RowCount; }
		}

		public string Semantic
		{
			get { return nativeAnnotation.Semantic; }
		}

        internal EffectAnnotation(INativeEffectAnnotation setNativeAnnotation)
        {
            nativeAnnotation = setNativeAnnotation;
        }

        public bool GetValueBoolean()
        {
            return nativeAnnotation.GetValueBoolean();
        }

        public int GetValueInt32()
        {
            return nativeAnnotation.GetValueInt32();
        }

        public Matrix GetValueMatrix()
        {
            return nativeAnnotation.GetValueMatrix();
        }

        public float GetValueSingle()
        {
            return nativeAnnotation.GetValueSingle();
        }

        public string GetValueString()
        {
            return nativeAnnotation.GetValueString();
        }

        public Vector2 GetValueVector2()
        {
            return nativeAnnotation.GetValueVector2();
        }

        public Vector3 GetValueVector3()
        {
            return nativeAnnotation.GetValueVector3();
        }

        public Vector4 GetValueVector4()
        {
            return nativeAnnotation.GetValueVector4();
        }
    }
}
