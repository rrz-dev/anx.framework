using ANX.Framework.NonXNA;
using SharpDX.Direct3D11;
using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
    public class EffectPass_DX11 : INativeEffectPass
    {
		private EffectPass nativePass;
        private Framework.Graphics.Effect parentEffect;

        public string Name
        {
            get 
            {
				return nativePass.Description.Name;
            }
        }

        public SharpDX.D3DCompiler.ShaderBytecode Signature
        {
            get;
            private set;
        }

        internal EffectPass_DX11(Framework.Graphics.Effect parentEffect, EffectPass setNativePass)
		{
			nativePass = setNativePass;
            this.parentEffect = parentEffect;

            this.Signature = nativePass.Description.Signature;
		}

        public Framework.Graphics.EffectAnnotationCollection Annotations
        {
            get { throw new System.NotImplementedException(); }
        }

        public void Apply()
        {
            var deviceContext = ((GraphicsDeviceDX)parentEffect.GraphicsDevice.NativeDevice).NativeDevice;

            nativePass.Apply(deviceContext);
            ((GraphicsDeviceDX)this.parentEffect.GraphicsDevice.NativeDevice).currentPass = this;
            parentEffect.OnApply();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                if (nativePass != null)
                {
                    nativePass.Dispose();
                    nativePass = null;
                }
            }
        }
    }
}
