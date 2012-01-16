using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
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
	/// Native OpenGL Effect implementation.
	/// 
	/// http://wiki.delphigl.com/index.php/Tutorial_glsl
	/// </summary>
	public class EffectGL3 : INativeEffect
	{
		#region Private
		/// <summary>
		/// The managed effect instance of this shader.
		/// </summary>
		private Effect managedEffect;

		/// <summary>
		/// The loaded shader data from the shader file.
		/// </summary>
		private ShaderData shaderData;

		/// <summary>
		/// The available techniques of this shader.
		/// </summary>
		private List<EffectTechnique> techniques;

		/// <summary>
		/// The current native technique.
		/// </summary>
		internal EffectTechniqueGL3 CurrentTechnique
		{
			get
			{
				return managedEffect.CurrentTechnique.NativeTechnique as EffectTechniqueGL3;
			}
		}

		/// <summary>
		/// The active uniforms of this technique.
		/// </summary>
		internal List<EffectParameter> parameters;
		#endregion

		#region Public
		#region Techniques
		public IEnumerable<EffectTechnique> Techniques
		{
			get
			{
				if (techniques.Count == 0)
				{
					CompileTechniques();
				}

				return techniques;
			}
		}
		#endregion

		#region Parameters
		public IEnumerable<EffectParameter> Parameters
		{
			get
			{
				return parameters;
			}
		}
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Private helper constructor for the basic initialization.
		/// </summary>
		/// <param name="setManagedEffect"></param>
		private EffectGL3(Effect setManagedEffect)
		{
			parameters = new List<EffectParameter>();
			techniques = new List<EffectTechnique>();
			managedEffect = setManagedEffect;
		}

		/// <summary>
		/// Create a new effect instance of separate streams.
		/// </summary>
		/// <param name="vertexShaderByteCode">The vertex shader code.</param>
		/// <param name="pixelShaderByteCode">The fragment shader code.</param>
		public EffectGL3(Effect setManagedEffect, Stream vertexShaderByteCode,
			Stream pixelShaderByteCode)
			: this(setManagedEffect)
		{
// TODO: this is probably not right!
			throw new NotImplementedException("TODO: implement effect constructor with vertex and fragment streams, check HOWTO...");
			//CreateShader(ShaderHelper.LoadShaderCode(vertexShaderByteCode),
			//  ShaderHelper.LoadShaderCode(pixelShaderByteCode));
		}

		/// <summary>
		/// Create a new effect instance of one streams.
		/// </summary>
		/// <param name="byteCode">The byte code of the shader.</param>
		public EffectGL3(Effect setManagedEffect, Stream byteCode)
			: this(setManagedEffect)
		{
			string source = ShaderHelper.LoadShaderCode(byteCode);
			shaderData = ShaderHelper.ParseShaderCode(source);
		}
		#endregion

		#region CompileTechniques
		private void CompileTechniques()
		{
			parameters.Clear();
			techniques.Clear();
			Dictionary<string, int> vertexShaders = new Dictionary<string, int>();
			Dictionary<string, int> fragmentShaders = new Dictionary<string, int>();
			List<string> parameterNames = new List<string>();

			#region Compile vertex shaders
			foreach (string vertexName in shaderData.VertexShaderCodes.Keys)
			{
				string vertexSource = shaderData.VertexGlobalCode +
					shaderData.VertexShaderCodes[vertexName];

				int vertexShader = GL.CreateShader(ShaderType.VertexShader);
				string vertexError = CompileShader(vertexShader, vertexSource);
				if (String.IsNullOrEmpty(vertexError) == false)
				{
					throw new InvalidDataException("Failed to compile the vertex " +
						"shader '" + vertexName + "' because of: " + vertexError);
				}

				vertexShaders.Add(vertexName, vertexShader);
			}
			#endregion

			#region Compile fragment shaders
			foreach (string fragmentName in shaderData.FragmentShaderCodes.Keys)
			{
				string fragmentSource = shaderData.FragmentGlobalCode +
					shaderData.FragmentShaderCodes[fragmentName];

				int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
				string vertexError = CompileShader(fragmentShader, fragmentSource);
				if (String.IsNullOrEmpty(vertexError) == false)
				{
					throw new InvalidDataException("Failed to compile the fragment " +
						"shader '" + fragmentName + "' because of: " + vertexError);
				}

				fragmentShaders.Add(fragmentName, fragmentShader);
			}
			#endregion

			#region Compile programs
			foreach (string programName in shaderData.Techniques.Keys)
			{
				string vertexName = shaderData.Techniques[programName].Key;
				string fragmentName = shaderData.Techniques[programName].Value;

				int programHandle = GL.CreateProgram();
				ErrorHelper.Check("CreateProgram");
				GL.AttachShader(programHandle, vertexShaders[vertexName]);
				ErrorHelper.Check("AttachShader vertexShader");
				GL.AttachShader(programHandle, fragmentShaders[fragmentName]);
				ErrorHelper.Check("AttachShader fragmentShader");
				GL.LinkProgram(programHandle);

				int result;
				GL.GetProgram(programHandle, ProgramParameter.LinkStatus, out result);
				if (result == 0)
				{
					string programError;
					GL.GetProgramInfoLog(programHandle, out programError);
					throw new InvalidDataException("Failed to link the shader program '" +
						programName + "' because of: " + programError);
				}

				EffectTechniqueGL3 technique = new EffectTechniqueGL3(programName, programHandle);
				techniques.Add(new EffectTechnique(managedEffect, technique));

				int uniformCount;
				GL.GetProgram(programHandle, ProgramParameter.ActiveUniforms,
					out uniformCount);
				ErrorHelper.Check("GetProgram ActiveUniforms");

				for (int index = 0; index < uniformCount; index++)
				{
					string name = GL.GetActiveUniformName(programHandle, index);
					ErrorHelper.Check("GetActiveUniformName name=" + name);

					if (parameterNames.Contains(name) == false)
					{
						parameterNames.Add(name);
						int uniformIndex = GL.GetUniformLocation(programHandle, name);
						ErrorHelper.Check("GetUniformLocation name=" + name +
							" uniformIndex=" + uniformIndex);
						parameters.Add(new EffectParameter()
						{
							NativeParameter =
								new EffectParameterGL3(technique, name, uniformIndex),
						});
					}
				}
			}
			#endregion
		}
		#endregion

		#region CompileShader
		private string CompileShader(int shader, string source)
		{
			GL.ShaderSource(shader, source);
			GL.CompileShader(shader);

			int result;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out result);
			if (result == 0)
			{
				string error = "";
				GL.GetShaderInfoLog(shader, out error);

				GL.DeleteShader(shader);

				return error;
			}

			return null;
		}
		#endregion

		#region Apply (TODO)
		public void Apply(GraphicsDevice graphicsDevice)
		{
			if (GraphicsDeviceWindowsGL3.activeEffect != this)
			{
				GL.Enable(EnableCap.Blend);
				GL.UseProgram(CurrentTechnique.programHandle);
				GraphicsDeviceWindowsGL3.activeEffect = this;
				ErrorHelper.Check("UseProgram");
			}
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the native shader data.
		/// </summary>
		public void Dispose()
		{
			foreach (EffectTechnique technique in techniques)
			{
				int programHandle = (technique.NativeTechnique as EffectTechniqueGL3).programHandle;

				GL.DeleteProgram(programHandle);
				ErrorHelper.Check("DeleteProgram");

				int result;
				GL.GetProgram(programHandle, ProgramParameter.DeleteStatus, out result);
				if (result == 0)
				{
					string deleteError;
					GL.GetProgramInfoLog(programHandle, out deleteError);
					throw new Exception("Failed to delete the shader program '" + technique.Name +
						"' because of: " + deleteError);
				}
			}

			techniques.Clear();
		}
		#endregion
	}
}
