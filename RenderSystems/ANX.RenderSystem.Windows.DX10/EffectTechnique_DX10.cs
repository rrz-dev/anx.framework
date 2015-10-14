#region Using Statements
using System;
using System.Collections.Generic;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

using Dx10 = SharpDX.Direct3D10;

namespace ANX.RenderSystem.Windows.DX10
{
	public class EffectTechnique_DX10 : INativeEffectTechnique
	{
		private readonly Effect parentEffect;
        private readonly EffectPass[] effectPasses;

		public EffectTechnique_DX10(Effect parentEffect, Dx10.EffectTechnique nativeTechnique)
		{
            if (parentEffect == null)
            {
                throw new ArgumentNullException("parentEffect");
            }

			this.parentEffect = parentEffect;
			NativeTechnique = nativeTechnique;

            var description = NativeTechnique.Description;

            this.Name = description.Name;

            var passCounts = description.PassCount;
            this.effectPasses = new EffectPass[passCounts];

            for (int i = 0; i < passCounts; i++)
            {
                this.effectPasses[i] = new EffectPass(new EffectPass_DX10(this.parentEffect, NativeTechnique.GetPassByIndex(i)));
            }
            
            var annotationCount = description.AnnotationCount;
            var annotations = new EffectAnnotation[annotationCount];
            for (int i = 0; i < annotationCount; i++)
                annotations[i] = new EffectAnnotation(new DxEffectAnnotation(nativeTechnique.GetAnnotationByIndex(i)));

            this.Annotations = new EffectAnnotationCollection(annotations);
		}

        public Dx10.EffectTechnique NativeTechnique { get; private set; }

        public string Name
        {
            get;
            private set;
        }

		public IEnumerable<EffectPass> Passes
		{
			get
			{
                return this.effectPasses;
			}
		}

        public EffectAnnotationCollection Annotations
        {
            get;
            private set;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                if (NativeTechnique != null)
                {
                    NativeTechnique.Dispose();
                    NativeTechnique = null;
                }

                foreach (var pass in this.effectPasses)
                    pass.NativeEffectPass.Dispose();

                foreach (var annotation in this.Annotations)
                    annotation.NativeAnnotation.Dispose();
            }
        }
    }
}
