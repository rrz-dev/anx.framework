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
		private EffectGL3 parentEffect;
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
		internal EffectParameterGL3(EffectGL3 setParentEffect,
			string setName, int setUniformIndex)
		{
			parentEffect = setParentEffect;
			Name = setName;
			UniformIndex = setUniformIndex;
		}
		#endregion

		#region SetValue (Matrix)
		/// <summary>
		/// Set a matrix value to the effect parameter.
		/// </summary>
		/// <param name="value">Value for the parameter</param>
		public void SetValue(Matrix value)
		{
			GL.UseProgram(parentEffect.programHandle);
			ErrorHelper.Check("UseProgram");

			OpenTK.Matrix4 matrix = new OpenTK.Matrix4(
				value.M11, value.M12, value.M13, value.M14,
				value.M21, value.M22, value.M23, value.M24,
				value.M31, value.M32, value.M33, value.M34,
				value.M41, value.M42, value.M43, value.M44);

			GL.UniformMatrix4(UniformIndex, false, ref matrix);
			ErrorHelper.Check("UniformMatrix4");
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
			GL.UseProgram(parentEffect.programHandle);
			ErrorHelper.Check("UseProgram");
			if (textureCache == null ||
				textureCache != value)
			{
				// TODO: multiple texture units
				TextureUnit textureUnit = TextureUnit.Texture0;
				GL.Enable(EnableCap.Texture2D);
				ErrorHelper.Check("Enable");
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
	}
}
