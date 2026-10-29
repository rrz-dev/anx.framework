using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx11 = SharpDX.Direct3D11;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
    [PercentageComplete(50)]
    [TestState(TestStateAttribute.TestState.Untested)]
    [Developer("Glatzemann")]
    public class EffectParameter_DX11 : INativeEffectParameter
	{
		#region Public
        public Dx11.EffectVariable NativeParameter { get; internal set; }

        public string Name
        {
            get { return NativeParameter.Description.Name; }
        }

        public string Semantic
        {
            get { return NativeParameter.Description.Semantic; }
        }

        public int ColumnCount
        {
            get { return NativeParameter.TypeInfo.Description.Columns; }
        }

        public int RowCount
        {
            get { return NativeParameter.TypeInfo.Description.Rows; }
        }

        public EffectParameterClass ParameterClass
        {
            get { return DxFormatConverter.Translate(NativeParameter.TypeInfo.Description.Class); }
        }

        public EffectParameterType ParameterType
        {
            get { return DxFormatConverter.Translate(NativeParameter.TypeInfo.Description.Type); }
        }

        public EffectAnnotationCollection Annotations
        {
            get { throw new NotImplementedException(); }
        }

        public EffectParameterCollection Elements
        {
            get { throw new NotImplementedException(); }
        }

        public EffectParameterCollection StructureMembers
        {
            get { throw new NotImplementedException(); }
        }
        #endregion

		#region SetValue (bool)
		public void SetValue(bool value)
		{
			NativeParameter.AsScalar().Set(value);
		}
		#endregion

		#region SetValue (bool[])
		public void SetValue(bool[] value)
		{
			NativeParameter.AsScalar().Set(value);
		}
		#endregion

		#region SetValue (int)
		public void SetValue(int value)
		{
			NativeParameter.AsScalar().Set(value);
		}
		#endregion

		#region SetValue (int[])
		public void SetValue(int[] value)
		{
			NativeParameter.AsScalar().Set(value);
		}
		#endregion

		#region SetValue (Matrix, transpose) (TODO)
		public void SetValue(Matrix value, bool transpose)
		{
			// TODO: handle transpose!
			NativeParameter.AsMatrix().SetMatrix(value);
		}
		#endregion

		#region SetValue (Matrix[], transpose) (TODO)
		public void SetValue(Matrix[] value, bool transpose)
		{
			// TODO: handle transpose!
			NativeParameter.AsMatrix().SetMatrix(value);
		}
		#endregion

		#region SetValue (Quaternion)
		public void SetValue(Quaternion value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Quaternion[])
		public void SetValue(Quaternion[] value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (float)
		public void SetValue(float value)
		{
			NativeParameter.AsScalar().Set(value);
		}
		#endregion

		#region SetValue (float[])
		public void SetValue(float[] value)
		{
			NativeParameter.AsScalar().Set(value);
		}
		#endregion

		#region SetValue (Vector2)
		public void SetValue(Vector2 value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Vector2[])
		public void SetValue(Vector2[] value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Vector3)
		public void SetValue(Vector3 value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Vector3[])
		public void SetValue(Vector3[] value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Vector4)
		public void SetValue(Vector4 value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Vector4[])
		public void SetValue(Vector4[] value)
		{
			NativeParameter.AsVector().Set(value);
		}
		#endregion

		#region SetValue (Texture)
		public void SetValue(Texture value)
		{
			if (value == null)
				throw new ArgumentNullException("value");

			var tex = value.NativeTexture as DxTexture2D;
			NativeParameter.AsShaderResource().SetResource(tex.NativeShaderResourceView);
		}
		#endregion

		#region SetValue (string) (TODO)
		public void SetValue(string value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Get (TODO)
		public bool GetValueBoolean()
		{
			throw new NotImplementedException();
		}

		public bool[] GetValueBooleanArray(int count)
		{
			throw new NotImplementedException();
		}

		public int GetValueInt32()
		{
			throw new NotImplementedException();
		}

		public int[] GetValueInt32Array(int count)
		{
			throw new NotImplementedException();
		}

		public Matrix GetValueMatrix()
		{
			throw new NotImplementedException();
		}

		public Matrix[] GetValueMatrixArray(int count)
		{
			throw new NotImplementedException();
		}

		public Matrix GetValueMatrixTranspose()
		{
			throw new NotImplementedException();
		}

		public Matrix[] GetValueMatrixTransposeArray(int count)
		{
			throw new NotImplementedException();
		}

		public Quaternion GetValueQuaternion()
		{
			throw new NotImplementedException();
		}

		public Quaternion[] GetValueQuaternionArray(int count)
		{
			throw new NotImplementedException();
		}

		public float GetValueSingle()
		{
			throw new NotImplementedException();
		}

		public float[] GetValueSingleArray(int count)
		{
			throw new NotImplementedException();
		}

		public string GetValueString()
		{
			throw new NotImplementedException();
		}

		public Texture2D GetValueTexture2D()
		{
			throw new NotImplementedException();
		}

		public Texture3D GetValueTexture3D()
		{
			throw new NotImplementedException();
		}

		public TextureCube GetValueTextureCube()
		{
			throw new NotImplementedException();
		}

		public Vector2 GetValueVector2()
		{
			throw new NotImplementedException();
		}

		public Vector2[] GetValueVector2Array(int count)
		{
			throw new NotImplementedException();
		}

		public Vector3 GetValueVector3()
		{
			throw new NotImplementedException();
		}

		public Vector3[] GetValueVector3Array(int count)
		{
			throw new NotImplementedException();
		}

		public Vector4 GetValueVector4()
		{
			throw new NotImplementedException();
		}

		public Vector4[] GetValueVector4Array(int count)
		{
			throw new NotImplementedException();
		}
		#endregion
    }
}
