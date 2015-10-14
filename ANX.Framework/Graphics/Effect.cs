using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public class Effect : GraphicsResource, IGraphicsResource
	{
		#region Private
		private INativeEffect nativeEffect;
        private readonly byte[] byteCode;
		private EffectSourceLanguage sourceLanguage;
        private EffectTechnique currentTechnique;
		#endregion

		#region Public
		internal INativeEffect NativeEffect
		{
			get
			{
				return this.nativeEffect;
			}
		}

        public EffectTechnique CurrentTechnique
        {
            get { return this.currentTechnique; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (!Techniques.Contains(value))
                    throw new InvalidOperationException("CurrentTechnique must be a technique of the same effect instance.");

                this.currentTechnique = value;
            }
        }

        public EffectParameterCollection Parameters { get; private set; }
        public EffectTechniqueCollection Techniques { get; private set; }
        #endregion

		protected Effect(Effect cloneSource)
			: this(cloneSource.GraphicsDevice, cloneSource.byteCode)
		{
            CreateNativeEffect(cloneSource.sourceLanguage);
		}

		public Effect(GraphicsDevice graphicsDevice, byte[] byteCode)
			: this(graphicsDevice, byteCode, EffectSourceLanguage.HLSL_FX)
		{
            CreateNativeEffect(EffectSourceLanguage.HLSL_FX);
		}

		public Effect(GraphicsDevice graphicsDevice, byte[] byteCode, EffectSourceLanguage sourceLanguage)
			: base(graphicsDevice)
		{
			this.byteCode = new byte[byteCode.Length];
			Array.Copy(byteCode, this.byteCode, byteCode.Length);

			base.GraphicsDevice.ResourceCreated += GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed += GraphicsDevice_ResourceDestroyed;

			CreateNativeEffect(sourceLanguage);

			this.CurrentTechnique = this.Techniques[0];

			this.sourceLanguage = sourceLanguage;
		}

		#region GraphicsDevice_ResourceCreated
		private void GraphicsDevice_ResourceCreated(object sender, ResourceCreatedEventArgs e)
		{
			if (nativeEffect != null)
			{
				nativeEffect.Dispose();
				nativeEffect = null;
			}

			CreateNativeEffect(this.sourceLanguage);
		}
		#endregion

		#region GraphicsDevice_ResourceDestroyed
		private void GraphicsDevice_ResourceDestroyed(object sender, ResourceDestroyedEventArgs e)
		{
			if (nativeEffect != null)
			{
				nativeEffect.Dispose();
				nativeEffect = null;
			}
		}
		#endregion

		#region Clone
		public virtual Effect Clone()
		{
		    return new Effect(this);
		}
		#endregion

        protected internal virtual void OnApply()
        {
        }

		#region Dispose
		protected override void Dispose(bool disposeManaged)
		{
			if (disposeManaged)
            {
                if (nativeEffect != null)
                {
                    nativeEffect.Dispose();
                    nativeEffect = null;
                }

                if (Parameters != null)
                {
                    foreach (var parameter in Parameters)
                        parameter.NativeParameter.Dispose();
                    Parameters = null;
                }

                if (Techniques != null)
                {
                    foreach (var technique in Techniques)
                        technique.NativeTechnique.Dispose();
                    Techniques = null;
                }

                base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
                base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
            }

            base.Dispose(disposeManaged);
		}
		#endregion

		#region CreateNativeEffect
		private void CreateNativeEffect(EffectSourceLanguage sourceLanguage)
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();

			if (creator.IsLanguageSupported(sourceLanguage))
			{
				this.nativeEffect = creator.CreateEffect(GraphicsDevice, this, new MemoryStream(this.byteCode, false));
				this.Techniques = new EffectTechniqueCollection(this.nativeEffect.Techniques);
				this.Parameters = new EffectParameterCollection(this.nativeEffect.Parameters);
			}
			else
				throw new InvalidOperationException("couldn't create " + sourceLanguage + " native effect using RenderSystem " +
					creator.Name);
		}
		#endregion
	}
}
