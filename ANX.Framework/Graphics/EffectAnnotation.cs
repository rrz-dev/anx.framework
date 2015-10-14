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
        internal INativeEffectAnnotation NativeAnnotation
        {
            get;
            private set;
        }

		public int ColumnCount
		{
			get { return NativeAnnotation.ColumnCount; }
		}

		public string Name
		{
			get { return NativeAnnotation.Name; }
		}

		public EffectParameterClass ParameterClass
		{
			get { return NativeAnnotation.ParameterClass; }
		}

		public EffectParameterType ParameterType
		{
			get { return NativeAnnotation.ParameterType; }
		}

		public int RowCount
		{
			get { return NativeAnnotation.RowCount; }
		}

		public string Semantic
		{
			get { return NativeAnnotation.Semantic; }
		}

        internal EffectAnnotation(INativeEffectAnnotation setNativeAnnotation)
        {
            NativeAnnotation = setNativeAnnotation;
        }

        public bool GetValueBoolean()
        {
            return NativeAnnotation.GetValueBoolean();
        }

        public int GetValueInt32()
        {
            return NativeAnnotation.GetValueInt32();
        }

        public Matrix GetValueMatrix()
        {
            return NativeAnnotation.GetValueMatrix();
        }

        public float GetValueSingle()
        {
            return NativeAnnotation.GetValueSingle();
        }

        public string GetValueString()
        {
            return NativeAnnotation.GetValueString();
        }

        public Vector2 GetValueVector2()
        {
            return NativeAnnotation.GetValueVector2();
        }

        public Vector3 GetValueVector3()
        {
            return NativeAnnotation.GetValueVector3();
        }

        public Vector4 GetValueVector4()
        {
            return NativeAnnotation.GetValueVector4();
        }
    }
}
