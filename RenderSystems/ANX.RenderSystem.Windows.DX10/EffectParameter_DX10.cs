using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using Dx10 = SharpDX.Direct3D10;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
	public class EffectParameter_DX10 : INativeEffectParameter
	{
		#region Public
		public Dx10.EffectVariable NativeParameter
		{
			get;
			internal set;
		}

		public string Name
		{
			get
			{
				return NativeParameter.Description.Name;
			}
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
            Matrix val = value;
            if (transpose)
            {
                Matrix.Transpose(ref val, out val);
            }

			NativeParameter.AsMatrix().SetMatrix(val);
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
