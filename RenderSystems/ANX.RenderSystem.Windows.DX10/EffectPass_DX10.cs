using ANX.Framework.NonXNA;
using SharpDX.Direct3D10;
using System;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX10
{
    public class EffectPass_DX10 : INativeEffectPass
	{
        private Framework.Graphics.Effect parentEffect;

        public string Name
        {
            get;
            private set;
        }

        private EffectPass NativeEffectPass
        {
            get;
            set;
        }

        public SharpDX.D3DCompiler.ShaderBytecode Signature
        {
            get;
            private set;
        }

        internal EffectPass_DX10(Framework.Graphics.Effect parentEffect, EffectPass nativePass)
		{
            this.NativeEffectPass = nativePass;
            this.parentEffect = parentEffect;

            var description = nativePass.Description;

            this.Name = description.Name;
            this.Signature = description.Signature;

            var annotations = new Framework.Graphics.EffectAnnotation[description.AnnotationCount];
            for (int i = 0; i < annotations.Length; i++)
                annotations[i] = new Framework.Graphics.EffectAnnotation(new DxEffectAnnotation(nativePass.GetAnnotationByIndex(i)));
            
            this.Annotations = new Framework.Graphics.EffectAnnotationCollection(annotations);
		}

        public Framework.Graphics.EffectAnnotationCollection Annotations
        {
            get;
            private set;
        }

        public void Apply()
        {
            //TODO: Don't set every state every time, use NativeEffectPass.ComputeStateBlockMask to prevent unnecessary state changes.
            NativeEffectPass.Apply();
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
                this.NativeEffectPass.Dispose();

                foreach (var annotation in this.Annotations)
                    annotation.NativeAnnotation.Dispose();
            }
        }
    }
}
