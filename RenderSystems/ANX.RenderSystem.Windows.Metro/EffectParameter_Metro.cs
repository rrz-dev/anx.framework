using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.Windows.Metro.Shader;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class EffectParameter_Metro : INativeEffectParameter
	{
		#region Private
		private Effect_Metro parentEffect;
		private ExtendedShaderParameter nativeParameter;
		#endregion

		#region Public
		public string Name
		{
			get
			{
				return nativeParameter.Name;
			}
		}

        public string Semantic
        {
            get
            {
                return nativeParameter.Semantic;
            }
        }
		#endregion

		#region Constructor
		public EffectParameter_Metro(Effect_Metro setParentEffect,
				ExtendedShaderParameter setNativeParameter)
		{
			parentEffect = setParentEffect;
			nativeParameter = setNativeParameter;
		}
		#endregion

		#region SetValue (int)
		public void SetValue(int value)
		{
			var bytes = BitConverter.GetBytes(value);
			parentEffect.paramBuffer.SetParameter(Name, bytes);
		}
		#endregion

		#region SetValue (int[])
		public void SetValue(int[] value)
		{
			parentEffect.paramBuffer.SetParameter(Name, value);
		}
		#endregion

		#region SetValue (Matrix)
		public void SetValue(Matrix value)
		{
			value = Matrix.Transpose(value);
			parentEffect.paramBuffer.SetParameter(Name, ref value);
		}
		#endregion

		#region SetValue (Matrix[])
		public void SetValue(Matrix[] value)
		{
			Matrix[] transposedMatrices = new Matrix[value.Length];
			for (int index = 0; index < value.Length; index++)
			{
				transposedMatrices[index] = Matrix.Transpose(value[index]);
			}
			parentEffect.paramBuffer.SetParameter(Name, transposedMatrices);
		}
		#endregion

		#region SetValue (Quaternion)
		public void SetValue(Quaternion value)
		{
			parentEffect.paramBuffer.SetParameter(Name, ref value);
		}
		#endregion

		#region SetValue (Quaternion[])
		public void SetValue(Quaternion[] value)
		{
			parentEffect.paramBuffer.SetParameter(Name, value);
		}
		#endregion

		#region SetValue (float)
		public void SetValue(float value)
		{
			var bytes = BitConverter.GetBytes(value);
			parentEffect.paramBuffer.SetParameter(Name, bytes);
		}
		#endregion

		#region SetValue (float[])
		public void SetValue(float[] value)
		{
			parentEffect.paramBuffer.SetParameter(Name, value);
		}
		#endregion

		#region SetValue (Vector2)
		public void SetValue(Vector2 value)
		{
			parentEffect.paramBuffer.SetParameter(Name, ref value);
		}
		#endregion

		#region SetValue (Vector2[])
		public void SetValue(Vector2[] value)
		{
			parentEffect.paramBuffer.SetParameter(Name, value);
		}
		#endregion

		#region SetValue (Vector3)
		public void SetValue(Vector3 value)
		{
			parentEffect.paramBuffer.SetParameter(Name, ref value);
		}
		#endregion

		#region SetValue (Vector3[])
		public void SetValue(Vector3[] value)
		{
			parentEffect.paramBuffer.SetParameter(Name, value);
		}
		#endregion

		#region SetValue (Vector4)
		public void SetValue(Vector4 value)
		{
			parentEffect.paramBuffer.SetParameter(Name, ref value);
		}
		#endregion

		#region SetValue (Vector4[])
		public void SetValue(Vector4[] value)
		{
			parentEffect.paramBuffer.SetParameter(Name, value);
		}
		#endregion

		#region SetValue (Texture)
		public void SetValue(Texture value)
		{
			Texture2D_Metro tex = value.NativeTexture as Texture2D_Metro;
			var context = NativeDxDevice.Current.NativeContext;

			int textureIndex = -1;
			foreach (var parameter in parentEffect.shader.Parameters)
			{
				if (parameter.IsTexture)
				{
					textureIndex++;

					if (parameter.Name == Name)
					{
						context.PixelShader.SetShaderResource(textureIndex, tex.NativeShaderResourceView);
						break;
					}
				}
			}
		}
		#endregion

		#region SetValue (Matrix, transpose)
		public void SetValue(Matrix value, bool transpose)
		{
			if (transpose == false)
				value = Matrix.Transpose(value);

			parentEffect.paramBuffer.SetParameter(Name, ref value);
		}
		#endregion

		#region SetValue (Matrix[], transpose)
		public void SetValue(Matrix[] value, bool transpose)
		{
			if (transpose)
			{
				parentEffect.paramBuffer.SetParameter(Name, value);
			}
			else
			{
				Matrix[] transposedMatrices = new Matrix[value.Length];
				for (int index = 0; index < value.Length; index++)
				{
					transposedMatrices[index] = Matrix.Transpose(value[index]);
				}
				parentEffect.paramBuffer.SetParameter(Name, transposedMatrices);
			}
		}
		#endregion

		#region SetValue (TODO)
		public void SetValue(bool value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(bool[] value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(string value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValue (TODO)
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
