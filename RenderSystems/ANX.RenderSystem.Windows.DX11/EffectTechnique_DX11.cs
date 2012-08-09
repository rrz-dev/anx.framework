#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using SharpDX.Direct3D11;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.DX11
{
    public class EffectTechnique_DX11 : INativeEffectTechnique
    {
        private EffectTechnique nativeTechnique;
        private ANX.Framework.Graphics.Effect parentEffect;

        internal EffectTechnique_DX11(ANX.Framework.Graphics.Effect parentEffect)
        {
            if (parentEffect == null)
            {
                throw new ArgumentNullException("parentEffect");
            }

            this.parentEffect = parentEffect;
        }

        public EffectTechnique NativeTechnique
        {
            get
            {
                return this.nativeTechnique;
            }
            internal set
            {
                this.nativeTechnique = value;
            }
        }

        public string Name
        {
            get 
            {
                return nativeTechnique.Description.Name;
            }
        }


        public IEnumerable<ANX.Framework.Graphics.EffectPass> Passes
        {
            get 
            {
                for (int i = 0; i < nativeTechnique.Description.PassCount; i++)
                {
                    EffectPass_DX11 passDx11 = new EffectPass_DX11();
                    passDx11.NativePass = nativeTechnique.GetPassByIndex(i);

                    ANX.Framework.Graphics.EffectPass pass = new ANX.Framework.Graphics.EffectPass(this.parentEffect);

                    yield return pass;
                }
            }
        }
    }
}
