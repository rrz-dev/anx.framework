using System;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using OpenTK.Graphics.OpenGL;

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

namespace ANX.Framework.Windows.GL3
{
	/// <summary>
	/// Native OpenGL implementation of an effect parameter.
	/// </summary>
	public class EffectParameterGL3 : INativeEffectParameter
	{
		#region Private
		private EffectTechniqueGL3 parentTechnique;
		#endregion

		#region Public
		/// <summary>
		/// The name of the effect parameter.
		/// </summary>
		public string Name
		{
			get;
			private set;
		}

		/// <summary>
		/// The index of the uniform.
		/// </summary>
		public int UniformIndex
		{
			get;
			private set;
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Create a ne effect parameter object.
		/// </summary>
		internal EffectParameterGL3(EffectTechniqueGL3 setParentTechnique,
			string setName, int setUniformIndex)
		{
			parentTechnique = setParentTechnique;
			Name = setName;
			UniformIndex = setUniformIndex;
		}
		#endregion

		#region SetValue
		#region SetValue (Matrix)
		/// <summary>
		/// Set a matrix value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Matrix value, bool transpose)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");

			OpenTK.Matrix4 matrix = new OpenTK.Matrix4(
				value.M11, value.M12, value.M13, value.M14,
				value.M21, value.M22, value.M23, value.M24,
				value.M31, value.M32, value.M33, value.M34,
				value.M41, value.M42, value.M43, value.M44);

			GL.UniformMatrix4(UniformIndex, transpose, ref matrix);
			ErrorHelper.Check("UniformMatrix4");
		}
		#endregion

		#region SetValue (Matrix[])
		/// <summary>
		/// Set a Matrix array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Matrix[] value, bool transpose)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			float[] array = new float[value.Length * 16];
			for (int index = 0; index < value.Length; index++)
			{
				array[(index * 16)] = value[index].M11;
				array[(index * 16) + 1] = value[index].M12;
				array[(index * 16) + 2] = value[index].M13;
				array[(index * 16) + 3] = value[index].M14;
				array[(index * 16) + 4] = value[index].M21;
				array[(index * 16) + 5] = value[index].M22;
				array[(index * 16) + 6] = value[index].M23;
				array[(index * 16) + 7] = value[index].M24;
				array[(index * 16) + 8] = value[index].M31;
				array[(index * 16) + 9] = value[index].M32;
				array[(index * 16) + 10] = value[index].M33;
				array[(index * 16) + 11] = value[index].M34;
				array[(index * 16) + 12] = value[index].M41;
				array[(index * 16) + 13] = value[index].M42;
				array[(index * 16) + 14] = value[index].M43;
				array[(index * 16) + 15] = value[index].M44;
			}
			GL.UniformMatrix4(UniformIndex, array.Length, transpose, array);
			ErrorHelper.Check("UniformMatrix4v");
		}
		#endregion

		#region SetValue (Texture)
		private Texture textureCache = null;
		/// <summary>
		/// Set a texture value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Texture value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			if (textureCache == null ||
				textureCache != value)
			{
				// TODO: multiple texture units
				TextureUnit textureUnit = TextureUnit.Texture0;
				GL.ActiveTexture(textureUnit);
				ErrorHelper.Check("ActiveTexture");
				int handle = (value.NativeTexture as Texture2DGL3).NativeHandle;
				GL.BindTexture(TextureTarget.Texture2D, handle);
				ErrorHelper.Check("BindTexture");
				int unitIndex = (int)(textureUnit - TextureUnit.Texture0);
				GL.Uniform1(UniformIndex, 1, ref unitIndex);
				ErrorHelper.Check("Uniform1");
			}
		}
		#endregion

		#region SetValue (int)
		/// <summary>
		/// Set an int value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(int value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform1(UniformIndex, value);
			ErrorHelper.Check("Uniform1i");
		}
		#endregion

		#region SetValue (int[])
		/// <summary>
		/// Set an int array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(int[] value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform1(UniformIndex, value.Length, value);
			ErrorHelper.Check("Uniform1iv");
		}
		#endregion
		
		#region SetValue (float)
		/// <summary>
		/// Set a float value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(float value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform1(UniformIndex, value);
			ErrorHelper.Check("Uniform1f");
		}
		#endregion

		#region SetValue (float[])
		/// <summary>
		/// Set a float array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(float[] value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform1(UniformIndex, value.Length, value);
			ErrorHelper.Check("Uniform1fv");
		}
		#endregion

		#region SetValue (Vector2)
		/// <summary>
		/// Set a Vector2 value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Vector2 value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform2(UniformIndex, value.X, value.Y);
			ErrorHelper.Check("Uniform2f");
		}
		#endregion

		#region SetValue (Vector2[])
		/// <summary>
		/// Set a Vector2 array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Vector2[] value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			float[] array = new float[value.Length * 2];
			for(int index = 0; index < value.Length; index++)
			{
				array[(index * 2)] = value[index].X;
				array[(index * 2) + 1] = value[index].Y;
			}
			GL.Uniform2(UniformIndex, array.Length, array);
			ErrorHelper.Check("Uniform2fv");
		}
		#endregion

		#region SetValue (Vector3)
		/// <summary>
		/// Set a Vector3 value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Vector3 value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform3(UniformIndex, value.X, value.Y, value.Z);
			ErrorHelper.Check("Uniform3f");
		}
		#endregion

		#region SetValue (Vector3[])
		/// <summary>
		/// Set a Vector3 array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Vector3[] value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			float[] array = new float[value.Length * 3];
			for (int index = 0; index < value.Length; index++)
			{
				array[(index * 3)] = value[index].X;
				array[(index * 3) + 1] = value[index].Y;
				array[(index * 3) + 2] = value[index].Z;
			}
			GL.Uniform3(UniformIndex, array.Length, array);
			ErrorHelper.Check("Uniform3fv");
		}
		#endregion

		#region SetValue (Vector4)
		/// <summary>
		/// Set a Vector4 value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Vector4 value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			GL.Uniform4(UniformIndex, value.X, value.Y, value.Z, value.W);
			ErrorHelper.Check("Uniform4f");
		}
		#endregion

		#region SetValue (Vector4[])
		/// <summary>
		/// Set a Vector4 array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Vector4[] value)
		{
			GL.UseProgram(parentTechnique.programHandle);
			ErrorHelper.Check("UseProgram");
			float[] array = new float[value.Length * 4];
			for (int index = 0; index < value.Length; index++)
			{
				array[(index * 4)] = value[index].X;
				array[(index * 4) + 1] = value[index].Y;
				array[(index * 4) + 2] = value[index].Z;
				array[(index * 4) + 3] = value[index].W;
			}
			GL.Uniform4(UniformIndex, array.Length, array);
			ErrorHelper.Check("Uniform4fv");
		}
		#endregion

		#region SetValue (bool) (TODO)
		/// <summary>
		/// Set a bool value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(bool value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetValue (bool[]) (TODO)
		/// <summary>
		/// Set a bool array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(bool[] value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetValue (Quaternion) (TODO)
		/// <summary>
		/// Set a Quaternion value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Quaternion value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetValue (Quaternion[]) (TODO)
		/// <summary>
		/// Set a Quaternion array value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Quaternion[] value)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region SetValue (string, TODO)
		public void SetValue(string value)
		{
			throw new NotImplementedException();
		}
		#endregion
		#endregion

		#region GetValue
		#region GetValueBoolean
		public bool GetValueBoolean()
		{
			int result;
			GL.GetUniform(parentTechnique.programHandle, UniformIndex, out result);
			ErrorHelper.Check("GetUniform");
			return result == 1;
		}
		#endregion

		#region GetValueBooleanArray (TODO)
		public bool[] GetValueBooleanArray(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueInt32
		public int GetValueInt32()
		{
			int result;
			GL.GetUniform(parentTechnique.programHandle, UniformIndex, out result);
			ErrorHelper.Check("GetUniform");
			return result;
		}
		#endregion

		#region GetValueInt32Array (TODO)
		public int[] GetValueInt32Array(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueMatrix (TODO)
		public Matrix GetValueMatrix()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueMatrixArray (TODO)
		public Matrix[] GetValueMatrixArray(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueMatrixTranspose (TODO)
		public Matrix GetValueMatrixTranspose()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueMatrixTransposeArray (TODO)
		public Matrix[] GetValueMatrixTransposeArray(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueQuaternion (TODO)
		public Quaternion GetValueQuaternion()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueQuaternionArray (TODO)
		public Quaternion[] GetValueQuaternionArray(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueSingle
		public float GetValueSingle()
		{
			float result;
			GL.GetUniform(parentTechnique.programHandle, UniformIndex, out result);
			ErrorHelper.Check("GetUniform");
			return result;
		}
		#endregion

		#region GetValueSingleArray (TODO)
		public float[] GetValueSingleArray(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueString (TODO)
		public string GetValueString()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueTexture2D (TODO)
		public Texture2D GetValueTexture2D()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueTexture3D (TODO)
		public Texture3D GetValueTexture3D()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueTextureCube (TODO)
		public TextureCube GetValueTextureCube()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueVector2
		public Vector2 GetValueVector2()
		{
			float[] result = new float[2];
			unsafe
			{
				fixed (float* ptr = &result[0])
				{
					GL.GetUniform(parentTechnique.programHandle, UniformIndex, ptr);
				}
			}
			ErrorHelper.Check("GetUniform");
			return new Vector2(result[0], result[1]);
		}
		#endregion

		#region GetValueVector2Array (TODO)
		public Vector2[] GetValueVector2Array(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueVector3
		public Vector3 GetValueVector3()
		{
			float[] result = new float[3];
			unsafe
			{
				fixed (float* ptr = &result[0])
				{
					GL.GetUniform(parentTechnique.programHandle, UniformIndex, ptr);
				}
			}
			ErrorHelper.Check("GetUniform");
			return new Vector3(result[0], result[1], result[2]);
		}
		#endregion

		#region GetValueVector3Array (TODO)
		public Vector3[] GetValueVector3Array(int count)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region GetValueVector4
		public Vector4 GetValueVector4()
		{
			float[] result = new float[4];
			unsafe
			{
				fixed (float* ptr = &result[0])
				{
					GL.GetUniform(parentTechnique.programHandle, UniformIndex, ptr);
				}
			}
			ErrorHelper.Check("GetUniform");
			return new Vector4(result[0], result[1], result[2], result[3]);
		}
		#endregion

		#region GetValueVector4Array (TODO)
		public Vector4[] GetValueVector4Array(int count)
		{
			throw new NotImplementedException();
		}
		#endregion
		#endregion
	}
}
