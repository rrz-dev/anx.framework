#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
using System.IO;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

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

namespace ANX.RenderSystem.Windows.DX11
{
    public class Effect_DX11 : INativeEffect
    {
        private ShaderBytecode pixelShaderByteCode;
        private ShaderBytecode vertexShaderByteCode;
        private VertexShader vertexShader;
        private PixelShader pixelShader;
        private ANX.Framework.Graphics.Effect managedEffect;
        private ShaderBytecode effectByteCode;
        private SharpDX.Direct3D11.Effect nativeEffect;

        public Effect_DX11(GraphicsDevice device, ANX.Framework.Graphics.Effect managedEffect, Stream vertexShaderByteCode, Stream pixelShaderByteCode)
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
            this.vertexShader = new VertexShader((SharpDX.Direct3D11.Device)device.NativeDevice, this.vertexShaderByteCode);

            if (pixelShaderByteCode.CanSeek)
            {
                pixelShaderByteCode.Seek(0, SeekOrigin.Begin);
            }
            this.pixelShaderByteCode = ShaderBytecode.FromStream(pixelShaderByteCode);
            this.pixelShader = new PixelShader((SharpDX.Direct3D11.Device)device.NativeDevice, this.pixelShaderByteCode);
        }

        public Effect_DX11(GraphicsDevice device, ANX.Framework.Graphics.Effect managedEffect, Stream effectByteCode)
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
            this.nativeEffect = new SharpDX.Direct3D11.Effect(((GraphicsDeviceWindowsDX11)device.NativeDevice).NativeDevice.Device, this.effectByteCode);
        }

        public void Apply(GraphicsDevice graphicsDevice)
        {
            //TODO: dummy
            ((GraphicsDeviceWindowsDX11)graphicsDevice.NativeDevice).currentEffect = this;
        }

        internal SharpDX.Direct3D11.Effect NativeEffect
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

        public static byte[] CompileVertexShader(string effectCode)
        {
            ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(effectCode, "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            byte[] bytecode = new byte[vertexShaderByteCode.BufferSize];
            vertexShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
        }

        public static byte[] CompilePixelShader(string effectCode)
        {
            ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(effectCode, "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            byte[] bytecode = new byte[pixelShaderByteCode.BufferSize];
            pixelShaderByteCode.Data.Read(bytecode, 0, bytecode.Length);
            return bytecode;
        }

        public static byte[] CompileFXShader(string effectCode)
        {
            ShaderBytecode effectByteCode = ShaderBytecode.Compile(effectCode, "fx_5_0", ShaderFlags.None, EffectFlags.None);
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


        public IEnumerable<ANX.Framework.Graphics.EffectTechnique> Techniques
        {
            get
            {
                for (int i = 0; i < nativeEffect.Description.TechniqueCount; i++)
                {
                    EffectTechnique_DX11 teqDx11 = new EffectTechnique_DX11(this.managedEffect);
                    teqDx11.NativeTechnique = nativeEffect.GetTechniqueByIndex(i);

                    ANX.Framework.Graphics.EffectTechnique teq = new ANX.Framework.Graphics.EffectTechnique(this.managedEffect, teqDx11);

                    yield return teq;
                }
            }
        }

        public IEnumerable<ANX.Framework.Graphics.EffectParameter> Parameters
        {
            get
            {
                for (int i = 0; i < nativeEffect.Description.GlobalVariableCount; i++)
                {
                    EffectParameter_DX11 parDx11 = new EffectParameter_DX11();
                    parDx11.NativeParameter = nativeEffect.GetVariableByIndex(i);

                    ANX.Framework.Graphics.EffectParameter par = new ANX.Framework.Graphics.EffectParameter();
                    par.NativeParameter = parDx11;

                    yield return par;
                }
            }
        }
    }
}
