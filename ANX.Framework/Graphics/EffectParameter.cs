#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using System.Runtime.InteropServices;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

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
