#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class EffectParameter
		{
				#region Public
				private INativeEffectParameter nativeParameter;

        public INativeEffectParameter NativeParameter
        {
            get
            {
                return this.nativeParameter;
            }
            set
            {
                this.nativeParameter = value;
            }
				}
				public EffectAnnotationCollection Annotations
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public int ColumnCount
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public EffectParameterCollection Elements
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public string Name
				{
					get
					{
						return this.NativeParameter.Name;
					}
				}

				public EffectParameterClass ParameterClass
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public EffectParameterType ParameterType
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public int RowCount
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public string Semantic
				{
					get
					{
						throw new NotImplementedException();
					}
				}

				public EffectParameterCollection StructureMembers
				{
					get
					{
						throw new NotImplementedException();
					}
				}
				#endregion

				#region GetValue
				public bool GetValueBoolean()
				{
					return nativeParameter.GetValueBoolean();
        }

        public bool[] GetValueBooleanArray(int count)
				{
					return nativeParameter.GetValueBooleanArray(count);
        }

        public int GetValueInt32()
				{
					return nativeParameter.GetValueInt32();
        }

        public Int32[] GetValueInt32Array(int count)
        {
					return nativeParameter.GetValueInt32Array(count);
        }

        public Matrix GetValueMatrix()
				{
					return nativeParameter.GetValueMatrix();
        }

        public Matrix[] GetValueMatrixArray(int count)
				{
					return nativeParameter.GetValueMatrixArray(count);
        }

        public Matrix GetValueMatrixTranspose()
				{
					return nativeParameter.GetValueMatrixTranspose();
        }

        public Matrix[] GetValueMatrixTransposeArray(int count)
				{
					return nativeParameter.GetValueMatrixTransposeArray(count);
        }

        public Quaternion GetValueQuaternion()
				{
					return nativeParameter.GetValueQuaternion();
        }

        public Quaternion[] GetValueQuaternionArray(int count)
				{
					return nativeParameter.GetValueQuaternionArray(count);
        }

        public float GetValueSingle()
				{
					return nativeParameter.GetValueSingle();
        }

        public float[] GetValueSingleArray(int count)
				{
					return nativeParameter.GetValueSingleArray(count);
        }

        public string GetValueString()
				{
					return nativeParameter.GetValueString();
        }

        public Texture2D GetValueTexture2D()
				{
					return nativeParameter.GetValueTexture2D();
        }

        public Texture3D GetValueTexture3D()
				{
					return nativeParameter.GetValueTexture3D();
        }

        public TextureCube GetValueTextureCube()
				{
					return nativeParameter.GetValueTextureCube();
        }

        public Vector2 GetValueVector2()
				{
					return nativeParameter.GetValueVector2();
        }

        public Vector2[] GetValueVector2Array(int count)
				{
					return nativeParameter.GetValueVector2Array(count);
        }

        public Vector3 GetValueVector3()
				{
					return nativeParameter.GetValueVector3();
        }

        public Vector3[] GetValueVector3Array(int count)
				{
					return nativeParameter.GetValueVector3Array(count);
        }

        public Vector4 GetValueVector4()
				{
					return nativeParameter.GetValueVector4();
        }

        public Vector4[] GetValueVector4Array(int count)
        {
					return nativeParameter.GetValueVector4Array(count);
        }
				#endregion

				#region SetValue
				public void SetValue([MarshalAs(UnmanagedType.U1)] bool value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(bool[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(int value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(int[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Matrix value)
				{
						nativeParameter.SetValue(value, false);
        }

        public void SetValue(Matrix[] value)
        {
						nativeParameter.SetValue(value, false);
        }

        public void SetValue(Quaternion value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Quaternion[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(float value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(float[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(string value)
				{
					nativeParameter.SetValue(value);
        }

        public void SetValue(Texture value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector2 value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector2[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector3 value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector3[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector4 value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector4[] value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValueTranspose(Matrix value)
				{
					nativeParameter.SetValue(value, true);
        }

        public void SetValueTranspose(Matrix[] value)
				{
					nativeParameter.SetValue(value, true);
				}
				#endregion
    }
}
