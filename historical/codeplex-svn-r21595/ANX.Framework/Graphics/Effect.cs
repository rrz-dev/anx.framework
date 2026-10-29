using System;
using System.IO;
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
		#endregion

		#region Public
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

        public EffectTechnique CurrentTechnique { get; set; }
        public EffectParameterCollection Parameters { get; private set; }
        public EffectTechniqueCollection Techniques { get; private set; }
        #endregion

		#region Constructor
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

			this.CurrentTechnique = this.Techniques[0];

			this.sourceLanguage = sourceLanguage;
		}

		~Effect()
		{
			Dispose();
			base.GraphicsDevice.ResourceCreated -= GraphicsDevice_ResourceCreated;
			base.GraphicsDevice.ResourceDestroyed -= GraphicsDevice_ResourceDestroyed;
		}
		#endregion

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

		#region PreBindSetParameters
		/// <summary>
		/// This is used by the built in effects to set all their parameters only once and not everytime the properties change.
		/// </summary>
		internal virtual void PreBindSetParameters()
		{
		}
		#endregion

		#region Dispose
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
			try
			{
			    Dispose();
			}
			finally
			{
				base.Dispose(false);
			}
		}
		#endregion

		#region CreateNativeEffect
		private void CreateNativeEffect(EffectSourceLanguage sourceLanguage)
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();

			if (creator.IsLanguageSupported(sourceLanguage))
			{
				this.nativeEffect = creator.CreateEffect(GraphicsDevice, this, new MemoryStream(this.byteCode, false));
				this.Techniques = new EffectTechniqueCollection(this, this.nativeEffect);
				this.Parameters = new EffectParameterCollection(this, this.nativeEffect);
			}
			else
				throw new InvalidOperationException("couldn't create " + sourceLanguage + " native effect using RenderSystem " +
					creator.Name);
		}
		#endregion
	}
}
