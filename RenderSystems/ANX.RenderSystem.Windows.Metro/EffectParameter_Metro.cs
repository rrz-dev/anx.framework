using System;
using ANX.Framework;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.Metro
{
	public class EffectParameter_Metro : INativeEffectParameter
	{
		//private EffectVariable nativeEffectVariable;

		//public EffectVariable NativeParameter
		//{
		//    get
		//    {
		//        return this.nativeEffectVariable;
		//    }
		//    internal set
		//    {
		//        this.nativeEffectVariable = value;
		//    }
		//}

		public void SetValue(bool value)
		{
			//nativeEffectVariable.AsScalar().Set(value);
			throw new NotImplementedException();
		}

		public void SetValue(bool[] value)
		{
			//nativeEffectVariable.AsScalar().Set(value);
			throw new NotImplementedException();
		}

		public void SetValue(int value)
		{
			//nativeEffectVariable.AsScalar().Set(value);
			throw new NotImplementedException();
		}

		public void SetValue(int[] value)
		{
			//nativeEffectVariable.AsScalar().Set(value);
			throw new NotImplementedException();
		}

		public void SetValue(Matrix value)
		{
			//SharpDX.Matrix m = new SharpDX.Matrix(value.M11, value.M12, value.M13, value.M14, value.M21, value.M22, value.M23, value.M24, value.M31, value.M32, value.M33, value.M34, value.M41, value.M42, value.M43, value.M44);
			//nativeEffectVariable.AsMatrix().SetMatrix(m);
			throw new NotImplementedException();
		}

		public void SetValue(Matrix[] value)
		{
			int count = value.Length;
			SharpDX.Matrix[] m = new SharpDX.Matrix[count];
			Matrix anxMatrix;
			for (int i = 0; i < count; i++)
			{
				anxMatrix = value[i];
				m[i] = new SharpDX.Matrix(anxMatrix.M11, anxMatrix.M12, anxMatrix.M13, anxMatrix.M14,
																	anxMatrix.M21, anxMatrix.M22, anxMatrix.M23, anxMatrix.M24,
																	anxMatrix.M31, anxMatrix.M32, anxMatrix.M33, anxMatrix.M34,
																	anxMatrix.M41, anxMatrix.M42, anxMatrix.M43, anxMatrix.M44);
			}

			//nativeEffectVariable.AsMatrix().SetMatrix(m);
			throw new NotImplementedException();
		}

		public void SetValue(Quaternion value)
		{
			SharpDX.Vector4 q = new SharpDX.Vector4(value.X, value.Y, value.Z, value.W);
			//nativeEffectVariable.AsVector().Set(q);
			throw new NotImplementedException();
		}

		public void SetValue(Quaternion[] value)
		{
			int count = value.Length;
			SharpDX.Vector4[] q = new SharpDX.Vector4[count];
			for (int i = 0; i < count; i++)
			{
				q[i] = new SharpDX.Vector4(value[i].X, value[i].Y, value[i].Z, value[i].W);
			}
			//nativeEffectVariable.AsVector().Set(q);
			throw new NotImplementedException();
		}

		public void SetValue(float value)
		{
			//nativeEffectVariable.AsScalar().Set(value);
			throw new NotImplementedException();
		}

		public void SetValue(float[] value)
		{
			//nativeEffectVariable.AsScalar().Set(value);
			throw new NotImplementedException();
		}

		public void SetValue(Vector2 value)
		{
			SharpDX.Vector2 v = new SharpDX.Vector2(value.X, value.Y);
			//nativeEffectVariable.AsVector().Set(v);
			throw new NotImplementedException();
		}

		public void SetValue(Vector2[] value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(Vector3 value)
		{
			SharpDX.Vector3 v = new SharpDX.Vector3(value.X, value.Y, value.Z);
			//nativeEffectVariable.AsVector().Set(v);
			throw new NotImplementedException();
		}

		public void SetValue(Vector3[] value)
		{
			throw new NotImplementedException();
		}

		public void SetValue(Vector4 value)
		{
			SharpDX.Vector4 v = new SharpDX.Vector4(value.X, value.Y, value.Z, value.W);
			//nativeEffectVariable.AsVector().Set(v);
			throw new NotImplementedException();
		}

		public void SetValue(Vector4[] value)
		{
			int count = value.Length;
			SharpDX.Vector4[] q = new SharpDX.Vector4[count];
			for (int i = 0; i < count; i++)
			{
				q[i] = new SharpDX.Vector4(value[i].X, value[i].Y, value[i].Z, value[i].W);
			}
			//nativeEffectVariable.AsVector().Set(q);
			throw new NotImplementedException();
		}

		public void SetValue(Texture value)
		{
			Texture2D_Metro tex = value.NativeTexture as Texture2D_Metro;

			//nativeEffectVariable.AsShaderResource().SetResource(tex.NativeShaderResourceView);
			throw new NotImplementedException();
		}

		public void SetValue(Matrix value, bool transpose)
		{
			throw new NotImplementedException();
		}

		public void SetValue(Matrix[] value, bool transpose)
		{
			throw new NotImplementedException();
		}

		public string Name
		{
			get
			{
				//return nativeEffectVariable.Description.Name;
				throw new NotImplementedException();
			}
		}

		#region INativeEffectParameter Member


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

		public void SetValue(string value)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
