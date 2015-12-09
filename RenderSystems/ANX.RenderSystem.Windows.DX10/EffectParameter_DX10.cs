using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx10 = SharpDX.Direct3D10;
using ANX.Framework.NonXNA.Development;
using SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    [PercentageComplete(80)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann, KorsarNek")]
    public class EffectParameter_DX10 : DxEffectAnnotation, INativeEffectParameter
	{
		#region Public
        public Dx10.EffectVariable NativeParameter { get; private set; }

        public EffectParameter_DX10(Dx10.EffectVariable nativeParameter)
            : base(nativeParameter)
        {
            this.NativeParameter = nativeParameter;

            var description = nativeParameter.Description;
            var typeDescription = nativeParameter.TypeInfo.Description;

            var annotations = new EffectAnnotation[description.AnnotationCount];
            for (int i = 0; i < annotations.Length; i++)
                annotations[i] = new EffectAnnotation(new DxEffectAnnotation(nativeParameter.GetAnnotationByIndex(i)));
            this.Annotations = new EffectAnnotationCollection(annotations);

            var elements = new EffectParameter[typeDescription.Elements];
            for (int i = 0; i < elements.Length; i++)
                elements[i] = new EffectParameter(new EffectParameter_DX10(nativeParameter.GetElement(i)));
            this.Elements = new EffectParameterCollection(elements);

            var members = new EffectParameter[typeDescription.Members];
            for (int i = 0; i < members.Length; i++)
                members[i] = new EffectParameter(new EffectParameter_DX10(nativeParameter.GetMemberByIndex(i)));
            this.StructureMembers = new EffectParameterCollection(members);

            this.ShaderResource = nativeParameter.AsShaderResource();
        }

        public EffectAnnotationCollection Annotations
        {
            get;
            private set;
        }

        public EffectParameterCollection Elements
        {
            get;
            private set;
        }

        public EffectParameterCollection StructureMembers
        {
            get;
            private set;
        }
        #endregion

        protected EffectShaderResourceVariable ShaderResource
        {
            get;
            private set;
        }

		#region SetValue (TODO)
		public void SetValue(bool value)
		{
			Scalar.Set(value);
		}

		public void SetValue(bool[] value)
		{
            Scalar.Set(value);
		}

		public void SetValue(int value)
		{
            Scalar.Set(value);
		}

		public void SetValue(int[] value)
		{
            Scalar.Set(value);
		}

		public void SetValue(Matrix value, bool transpose)
		{
            if (transpose)
                Matrix.SetMatrixTranspose(value);
            else
                Matrix.SetMatrix(value);
		}

		public void SetValue(Matrix[] value, bool transpose)
		{
            if (transpose)
                Matrix.SetMatrixTranspose(value);
            else
                Matrix.SetMatrix(value);
		}

		public void SetValue(Quaternion value)
		{
			Vector.Set(value);
		}

		public void SetValue(Quaternion[] value)
		{
            Vector.Set(value);
		}

		public void SetValue(float value)
		{
            Vector.Set(value);
		}

		public void SetValue(float[] value)
		{
			Scalar.Set(value);
		}

		public void SetValue(Vector2 value)
		{
            Vector.Set(value);
		}

		public void SetValue(Vector2[] value)
		{
            Vector.Set(value);
		}

		public void SetValue(Vector3 value)
		{
            Vector.Set(value);
		}

		public void SetValue(Vector3[] value)
		{
            Vector.Set(value);
		}

		public void SetValue(Vector4 value)
		{
            Vector.Set(value);
		}

		public void SetValue(Vector4[] value)
		{
            Vector.Set(value);
		}

		public void SetValue(Texture value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			ShaderResource.SetResource(((DxTexture2D)value.NativeTexture).NativeShaderResourceView);
		}

		public void SetValue(string value)
		{
            throw new NotImplementedException();
		}
		#endregion

		#region Get (TODO)

		public bool[] GetValueBooleanArray(int count)
		{
            return Scalar.GetBoolArray(0, count);
		}

		public int[] GetValueInt32Array(int count)
		{
            return Scalar.GetIntArray(count);
		}

		public Matrix[] GetValueMatrixArray(int count)
		{
            return Matrix.GetMatrixArray<Matrix>(count);
		}

        public Matrix GetValueMatrixTranspose()
        {
            return Matrix.GetMatrixTranspose<Matrix>();
        }

		public Matrix[] GetValueMatrixTransposeArray(int count)
		{
            return Matrix.GetMatrixTransposeArray<Matrix>(count);
		}

		public Quaternion GetValueQuaternion()
		{
            return Vector.GetVector<Quaternion>();
		}

		public Quaternion[] GetValueQuaternionArray(int count)
		{
            return Vector.GetVectorArray<Quaternion>(count);
		}

		public float[] GetValueSingleArray(int count)
		{
            return Scalar.GetFloatArray(count);
		}

		public ANX.Framework.Graphics.Texture2D GetValueTexture2D()
		{
			throw new NotImplementedException();
		}

        public ANX.Framework.Graphics.Texture3D GetValueTexture3D()
		{
			throw new NotImplementedException();
		}

		public TextureCube GetValueTextureCube()
		{
			throw new NotImplementedException();
		}

		public Vector2[] GetValueVector2Array(int count)
		{
            return Vector.GetVectorArray<Vector2>(count);
		}

		public Vector3[] GetValueVector3Array(int count)
		{
            return Vector.GetVectorArray<Vector3>(count);
		}

		public Vector4[] GetValueVector4Array(int count)
		{
            return Vector.GetVectorArray<Vector4>(count);
		}
		#endregion

        protected override void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                ShaderResource.Dispose();

                foreach (var annotation in this.Annotations)
                    annotation.NativeAnnotation.Dispose();

                foreach (var element in this.Elements)
                    element.NativeParameter.Dispose();

                foreach (var member in this.StructureMembers)
                    member.NativeParameter.Dispose();
            }

            base.Dispose(disposeManaged);
        }
    }
}
