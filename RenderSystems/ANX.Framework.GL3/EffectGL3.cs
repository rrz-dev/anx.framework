using System;
using System.Collections.Generic;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.RenderSystem.GL3.Helpers;
using OpenTK.Graphics.OpenGL;
using ANX.Framework.Content.Pipeline.Helpers.GL3;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
    /// <summary>
    /// Native OpenGL Effect implementation.
    /// http://wiki.delphigl.com/index.php/Tutorial_glsl
    /// </summary>
    public class EffectGL3 : INativeEffect
    {
        #region Private
        private Effect managedEffect;
        private ShaderData shaderData;
        private List<EffectParameter> parameters;
        private List<EffectTechnique> techniques;
        internal bool IsDisposed;

        internal EffectTechniqueGL3 CurrentTechnique
        {
            get
            {
                if (managedEffect.CurrentTechnique == null)
                    return null;

                return managedEffect.CurrentTechnique.NativeTechnique as EffectTechniqueGL3;
            }
        }
        #endregion

        #region Public
        #region Techniques
        public IEnumerable<EffectTechnique> Techniques
        {
            get
            {
                if (techniques.Count == 0)
                {
                    Compile();
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
                if (techniques.Count == 0)
                {
                    Compile();
                }

                return parameters;
            }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Private helper constructor for the basic initialization.
        /// </summary>
        private EffectGL3(Effect setManagedEffect)
        {
            GraphicsResourceManager.UpdateResource(this, true);

            parameters = new List<EffectParameter>();
            techniques = new List<EffectTechnique>();
            managedEffect = setManagedEffect;
        }

        ~EffectGL3()
        {
            GraphicsResourceManager.UpdateResource(this, false);
        }

        /// <summary>
        /// Create a new effect instance of separate streams.
        /// </summary>
        /// <param name="vertexShaderByteCode">The vertex shader code.</param>
        /// <param name="pixelShaderByteCode">The fragment shader code.</param>
        public EffectGL3(Effect setManagedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
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

        #region RecreateData
        internal void RecreateData()
        {
            Compile();
        }
        #endregion

        #region Compile
        private void Compile()
        {
            parameters.Clear();
            techniques.Clear();
            Dictionary<string, int> vertexShaders = new Dictionary<string, int>();
            Dictionary<string, int> fragmentShaders = new Dictionary<string, int>();
            List<string> parameterNames = new List<string>();

            #region Compile vertex shaders
            foreach (string vertexName in shaderData.VertexShaderCodes.Keys)
            {
                string vertexSource = shaderData.VertexGlobalCode + shaderData.VertexShaderCodes[vertexName];

                int vertexShader = GL.CreateShader(ShaderType.VertexShader);
                string vertexError = CompileShader(vertexShader, vertexSource);
                if (String.IsNullOrEmpty(vertexError) == false)
                    throw new InvalidDataException("Failed to compile the vertex shader '" + vertexName + "' cause of: " +
                        vertexError);

                vertexShaders.Add(vertexName, vertexShader);
            }
            #endregion

            #region Compile fragment shaders
            foreach (string fragmentName in shaderData.FragmentShaderCodes.Keys)
            {
                string fragmentSource = shaderData.FragmentGlobalCode + shaderData.FragmentShaderCodes[fragmentName];

                int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
                string fragmentError = CompileShader(fragmentShader, fragmentSource);
                if (String.IsNullOrEmpty(fragmentError) == false)
                    throw new InvalidDataException("Failed to compile the fragment shader '" + fragmentName + "' cause of: " +
                        fragmentError);

                fragmentShaders.Add(fragmentName, fragmentShader);
            }
            #endregion

            #region Compile programs
            foreach (string programName in shaderData.Techniques.Keys)
            {
                string vertexName = shaderData.Techniques[programName].Key;
                string fragmentName = shaderData.Techniques[programName].Value;

                int vertexShaderHandle = vertexShaders[vertexName];
                int fragmentShaderHandle = fragmentShaders[fragmentName];

                int programHandle = GL.CreateProgram();
                ErrorHelper.Check("CreateProgram");
                GL.AttachShader(programHandle, vertexShaderHandle);
                ErrorHelper.Check("AttachShader vertexShader");
                GL.AttachShader(programHandle, fragmentShaderHandle);
                ErrorHelper.Check("AttachShader fragmentShader");
                GL.LinkProgram(programHandle);

                int result;
                GL.GetProgram(programHandle, GetProgramParameterName.LinkStatus, out result);
                if (result == 0)
                {
                    string programError;
                    GL.GetProgramInfoLog(programHandle, out programError);
                    throw new InvalidDataException("Failed to link the shader program '" +
                        programName + "' because of: " + programError);
                }
                
                //After the program has been linked, the shaders don't have to be attached anymore as they won't do anything.
                //We also save some memory because the shader source code gets freed by this.
                GL.DetachShader(programHandle, vertexShaderHandle);
                GL.DetachShader(programHandle, fragmentShaderHandle);

                GL.DeleteShader(vertexShaderHandle);
                GL.DeleteShader(fragmentShaderHandle);

                EffectTechniqueGL3 technique = new EffectTechniqueGL3((EffectGL3)managedEffect.NativeEffect, programName, programHandle);
                techniques.Add(new EffectTechnique(managedEffect, technique));
                AddParametersFrom(programHandle, parameterNames, technique);
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

        #region AddParametersFrom
        private void AddParametersFrom(int programHandle, List<string> parameterNames, EffectTechniqueGL3 technique)
        {
            int uniformCount;
            GL.GetProgram(programHandle, GetProgramParameterName.ActiveUniforms, out uniformCount);
            ErrorHelper.Check("GetProgram ActiveUniforms");

            for (int index = 0; index < uniformCount; index++)
            {
                string name = GL.GetActiveUniformName(programHandle, index);
                ErrorHelper.Check("GetActiveUniformName name=" + name);

                if (parameterNames.Contains(name) == false)
                {
                    parameterNames.Add(name);
                    int uniformIndex = GL.GetUniformLocation(programHandle, name);
                    ErrorHelper.Check("GetUniformLocation name=" + name + " uniformIndex=" + uniformIndex);
                    parameters.Add(new EffectParameter(new EffectParameterGL3(technique, name, uniformIndex)));
                }
            }
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose the native shader data.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed == false)
            {
                IsDisposed = true;
                DisposeResource();
            }
        }

        internal void DisposeResource()
        {
            if (GraphicsDeviceWindowsGL3.IsContextCurrent == false)
            {
                return;
            }

            foreach (EffectTechnique technique in techniques)
            {
                int programHandle =
                    (technique.NativeTechnique as EffectTechniqueGL3).programHandle;

                GL.DeleteProgram(programHandle);
                ErrorHelper.Check("DeleteProgram");
                
                int result;
                GL.GetProgram(programHandle, GetProgramParameterName.DeleteStatus, out result);
                //If it isn't deleted, it means it's somehow still in use.
                if (result == 1)
                {
                    string deleteError;
                    GL.GetProgramInfoLog(programHandle, out deleteError);
                    throw new Exception("Failed to delete the shader program '" +
                        technique.Name + "' because of: " + deleteError);
                }
            }
            techniques.Clear();
            parameters.Clear();
        }
        #endregion
    }
}
