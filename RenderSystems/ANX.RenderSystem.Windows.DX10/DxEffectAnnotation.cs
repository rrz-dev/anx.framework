using ANX.Framework.NonXNA.RenderSystem;
using SharpDX.Direct3D10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.RenderSystem.Windows.DX10
{
    public class DxEffectAnnotation : INativeEffectAnnotation
    {
        private readonly EffectVariable nativeEffectVariable;

        public DxEffectAnnotation(SharpDX.Direct3D10.EffectVariable nativeEffectVariable)
        {
            this.nativeEffectVariable = nativeEffectVariable;

            var description = nativeEffectVariable.Description;
            var typeDescription = nativeEffectVariable.TypeInfo.Description;

            this.Name = description.Name;
            this.ColumnCount = typeDescription.Columns;
            this.ParameterClass = typeDescription.Class.ToParameterClass();
            this.ParameterType = typeDescription.Type.ToParameterType();
            this.RowCount = typeDescription.Rows;
            this.Semantic = description.Semantic;

            this.Matrix = nativeEffectVariable.AsMatrix();
            this.Scalar = nativeEffectVariable.AsScalar();
            this.String = nativeEffectVariable.AsString();
            this.Vector = nativeEffectVariable.AsVector();
        }

        public int ColumnCount
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public Framework.Graphics.EffectParameterClass ParameterClass
        {
            get;
            private set;
        }

        public Framework.Graphics.EffectParameterType ParameterType
        {
            get;
            private set;
        }

        public int RowCount
        {
            get;
            private set;
        }

        public string Semantic
        {
            get;
            private set;
        }

        protected EffectScalarVariable Scalar
        {
            get;
            private set;
        }

        protected EffectMatrixVariable Matrix
        {
            get;
            private set;
        }

        protected EffectStringVariable String
        {
            get;
            private set;
        }

        protected EffectVectorVariable Vector
        {
            get;
            private set;
        }

        public bool GetValueBoolean()
        {
            return this.Scalar.GetBool();
        }

        public int GetValueInt32()
        {
            return this.Scalar.GetInt();
        }

        public Framework.Matrix GetValueMatrix()
        {
            return this.Matrix.GetMatrix<Framework.Matrix>();
        }

        public float GetValueSingle()
        {
            return this.Scalar.GetFloat();
        }

        public string GetValueString()
        {
            return this.String.GetString();
        }

        public Framework.Vector2 GetValueVector2()
        {
            return this.Vector.GetVector<Framework.Vector2>();
        }

        public Framework.Vector3 GetValueVector3()
        {
            return this.Vector.GetVector<Framework.Vector3>();
        }

        public Framework.Vector4 GetValueVector4()
        {
            return this.Vector.GetVector<Framework.Vector4>();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                this.Matrix.Dispose();
                this.Scalar.Dispose();
                this.String.Dispose();
                this.Vector.Dispose();

                this.nativeEffectVariable.Dispose();
            }
        }
    }
}
