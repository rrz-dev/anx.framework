#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D10;
using SharpDX.D3DCompiler;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using EffectTechnique = ANX.Framework.Graphics.EffectTechnique;

namespace ANX.RenderSystem.Windows.DX10
{
    public class Effect_DX10 : INativeEffect
    {
        #region Private Members
        private ShaderBytecode pixelShaderByteCode;
        private ShaderBytecode vertexShaderByteCode;
        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private ANX.Framework.Graphics.Effect managedEffect;
        private ShaderBytecode effectByteCode;
        private SharpDX.Direct3D10.Effect nativeEffect;

        #endregion

        public Effect_DX10(GraphicsDevice device, ANX.Framework.Graphics.Effect managedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
        {
            if (this.managedEffect == null)
            {
                throw new ArgumentNullException("managedEffect");
            }
            this.managedEffect = managedEffect;

            if (vertexShaderByteCode.CanSeek)
            {
                vertexShaderByteCode.Seek(0, SeekOrigin.Begin);
            }
            this.vertexShaderByteCode = ShaderBytecode.FromStream(vertexShaderByteCode);
            this.vertexShader = new VertexShader((SharpDX.Direct3D10.Device)device.NativeDevice, this.vertexShaderByteCode);

            if (pixelShaderByteCode.CanSeek)
            {
                pixelShaderByteCode.Seek(0, SeekOrigin.Begin);
            }
            this.pixelShaderByteCode = ShaderBytecode.FromStream(pixelShaderByteCode);
            this.pixelShader = new PixelShader((SharpDX.Direct3D10.Device)device.NativeDevice, this.pixelShaderByteCode);
        }

        public Effect_DX10(GraphicsDevice device, ANX.Framework.Graphics.Effect managedEffect, Stream effectByteCode)
        {
            if (managedEffect == null)
            {
                throw new ArgumentNullException("managedEffect");
            }
            this.managedEffect = managedEffect;

            if (effectByteCode.CanSeek)
            {
                effectByteCode.Seek(0, SeekOrigin.Begin);
            }
            this.effectByteCode = ShaderBytecode.FromStream(effectByteCode);
            this.nativeEffect = new SharpDX.Direct3D10.Effect(((GraphicsDeviceWindowsDX10)device.NativeDevice).NativeDevice, this.effectByteCode, EffectFlags.None);
        }

        public void Apply(GraphicsDevice graphicsDevice)
        {
            ((GraphicsDeviceWindowsDX10)graphicsDevice.NativeDevice).currentEffect = this;
        }

        internal SharpDX.Direct3D10.Effect NativeEffect
        {
            get
            {
                return this.nativeEffect;
            }
        }

        internal ShaderBytecode PixelShaderByteCode
        {
            get
            {
                return this.pixelShaderByteCode;
            }
        }

        internal ShaderBytecode VertexShaderByteCode
        {
            get
            {
                return this.vertexShaderByteCode;
            }
        }

        internal VertexShader VertexShader
        {
            get
            {
                return this.vertexShader;
            }
        }

        internal PixelShader PixelShader
        {
            get
            {
                return this.pixelShader;
            }
        }

        public static byte[] CompileVertexShader(string effectCode, string directory = "")
        {
            ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(effectCode, "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None, null, new IncludeHandler(directory), "unknown");
            byte[] bytecode = new byte[vertexShaderByteCode.BufferSize];
            vertexShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
        }

        public static byte[] CompilePixelShader(string effectCode, string directory = "")
        {
            ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(effectCode, "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None, null, new IncludeHandler(directory), "unknown");
            byte[] bytecode = new byte[pixelShaderByteCode.BufferSize];
            pixelShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
        }

        public static byte[] CompileFXShader(string effectCode, string directory = "")
        {
            ShaderBytecode effectByteCode = ShaderBytecode.Compile(effectCode, "fx_4_0", ShaderFlags.None, EffectFlags.None, null, new IncludeHandler(directory), "unknown");
            byte[] bytecode = new byte[effectByteCode.BufferSize];
            effectByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
        }

        public void Dispose()
        {
            if (this.pixelShaderByteCode != null)
            {
                this.pixelShaderByteCode.Dispose();
                this.pixelShaderByteCode = null;
            }

            if (this.pixelShader != null)
            {
                this.pixelShader.Dispose();
                this.pixelShader = null;
            }

            if (this.vertexShaderByteCode != null)
            {
                this.vertexShaderByteCode.Dispose();
                this.vertexShaderByteCode = null;
            }

            if (this.vertexShader != null)
            {
                this.vertexShader.Dispose();
                this.vertexShader = null;
            }

            if (this.effectByteCode != null)
            {
                this.effectByteCode.Dispose();
                this.effectByteCode = null;
            }

            if (this.nativeEffect != null)
            {
                this.nativeEffect.Dispose();
                this.nativeEffect = null;
            }
        }


        public IEnumerable<EffectTechnique> Techniques
        {
            get 
            {
                for (int i = 0; i < nativeEffect.Description.TechniqueCount; i++)
                {
                    EffectTechnique_DX10 teqDx10 = new EffectTechnique_DX10(this.managedEffect);
                    teqDx10.NativeTechnique = nativeEffect.GetTechniqueByIndex(i);

                    EffectTechnique teq = new EffectTechnique(this.managedEffect, teqDx10);
                    
                    yield return teq;
                }
            }
        }

        public IEnumerable<EffectParameter> Parameters
        {
            get
            {
                for (int i = 0; i < nativeEffect.Description.GlobalVariableCount; i++)
                {
                    EffectParameter_DX10 parDx10 = new EffectParameter_DX10();
                    parDx10.NativeParameter = nativeEffect.GetVariableByIndex(i);

                    EffectParameter par = new EffectParameter();
                    par.NativeParameter = parDx10;

                    yield return par;
                }
            }
        }
    }
}
