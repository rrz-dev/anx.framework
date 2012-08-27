#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using System.IO;
using System.Runtime.InteropServices;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public class Effect : GraphicsResource, IGraphicsResource
    {
        #region Private Members
        private INativeEffect nativeEffect;
        private EffectTechniqueCollection techniqueCollection;
        private EffectTechnique currentTechnique;
        private EffectParameterCollection parameterCollection;
        private byte[] byteCode;
        private EffectSourceLanguage sourceLanguage;

        #endregion // Private Members

        protected Effect(Effect cloneSource)
            : this(cloneSource.GraphicsDevice, cloneSource.byteCode)
        {
        }

        public Effect(GraphicsDevice graphicsDevice, byte[] byteCode)
            : this(graphicsDevice, byteCode, EffectSourceLanguage.HLSL_FX)
        {
        }

        public Effect(GraphicsDevice graphicsDevice, byte[] byteCode, EffectSourceLanguage sourceLanguage)
            : base(graphicsDevice)
        {
            this.byteCode = new byte[byteCode.Length];
            Array.Copy(byteCode, this.byteCode, byteCode.Length);

            base.GraphicsDevice.ResourceCreated += GraphicsDevice_ResourceCreated;
            base.GraphicsDevice.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;

            CreateNativeEffect(sourceLanguage);

            this.currentTechnique = this.techniqueCollection[0];

            this.sourceLanguage = sourceLanguage;
        }

        ~Effect()
        {
            Dispose();
            base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
            base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
        }

        private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
        {
            if (nativeEffect != null)
            {
                nativeEffect.Dispose();
                nativeEffect = null;
            }

            CreateNativeEffect(this.sourceLanguage);
        }

        private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
        {
            if (nativeEffect != null)
            {
                nativeEffect.Dispose();
                nativeEffect = null;
            }
        }

        public virtual Effect Clone()
        {
            throw new NotImplementedException();
        }

        internal INativeEffect NativeEffect
        {
            get
            {
                if (nativeEffect == null)
                {
                    CreateNativeEffect(this.sourceLanguage);
                }

                return this.nativeEffect;
            }
        }

        public EffectTechnique CurrentTechnique
        {
            get
            {
                return this.currentTechnique;
            }
            set
            {
                this.currentTechnique = value;
            }
        }

        public EffectParameterCollection Parameters
        {
            get
            {
                return this.parameterCollection;
            }
        }

        public EffectTechniqueCollection Techniques
        {
            get
            {
                return this.techniqueCollection;
            }
        }

        public override void Dispose()
        {
            if (nativeEffect != null)
            {
                nativeEffect.Dispose();
                nativeEffect = null;
            }
        }

				protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
        {
            throw new NotImplementedException();
        }

        private void CreateNativeEffect(EffectSourceLanguage sourceLanguage)
        {
            IRenderSystemCreator creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();

            if (creator.IsLanguageSupported(sourceLanguage))
            {
                this.nativeEffect = creator.CreateEffect(GraphicsDevice, this, new MemoryStream(this.byteCode, false));

                this.techniqueCollection = new EffectTechniqueCollection(this, this.nativeEffect);
                this.parameterCollection = new EffectParameterCollection(this, this.nativeEffect);
            }
            else
            {
                throw new InvalidOperationException("couldn't create " + sourceLanguage.ToString() + " native effect using RenderSystem " + creator.Name);
            }
        }
    }
}
