#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;

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

        public Int32[] GetValueInt32Array(int count)
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

        public void SetValue(bool value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(bool[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(int value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(int[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Matrix value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Matrix[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Quaternion value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Quaternion[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(float value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(float[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Texture value)
        {
            nativeParameter.SetValue(value);
        }

        public void SetValue(Vector2 value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Vector2[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Vector3 value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Vector3[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Vector4 value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(Vector4[] value)
        {
            throw new NotImplementedException();
        }

        public void SetValueTranspose(Matrix value)
        {
            throw new NotImplementedException();
        }

        public void SetValueTranspose(Matrix[] value)
        {
            throw new NotImplementedException();
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
    }
}
