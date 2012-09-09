using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Untested)]
    public sealed class DirectionalLight
	{
		#region Private
		private Vector3 diffuseColor;
        private Vector3 direction;
        private Vector3 specularColor;
        private EffectParameter directionParameter;
        private EffectParameter diffuseColorParameter;
		private EffectParameter specularColorParameter;
		#endregion

		#region Public
		public bool Enabled { get; set; }

		public Vector3 DiffuseColor
		{
			get { return diffuseColor; }
			set
			{
				diffuseColor = value;
				SetValueIfPossible(diffuseColorParameter, ref value);
			}
		}

		public Vector3 Direction
		{
			get { return direction; }
			set
			{
				direction = value;
				SetValueIfPossible(directionParameter, ref value);
			}
		}

		public Vector3 SpecularColor
		{
			get { return diffuseColor; }
			set
			{
				specularColor = value;
				SetValueIfPossible(specularColorParameter, ref value);
			}
		}
		#endregion

		#region Constructor
		public DirectionalLight(EffectParameter directionParameter, EffectParameter diffuseColorParameter,
			EffectParameter specularColorParameter, DirectionalLight cloneSource)
        {
            this.directionParameter = directionParameter;
            this.diffuseColorParameter = diffuseColorParameter;
            this.specularColorParameter = specularColorParameter;

			if (cloneSource != null)
			{
				diffuseColor = cloneSource.diffuseColor;
				direction = cloneSource.direction;
				Enabled = cloneSource.Enabled;
				specularColor = cloneSource.specularColor;
			}
			else
			{
				diffuseColor = Vector3.One;
				direction = Vector3.Down;
				specularColor = Vector3.Zero;
			}
		}
		#endregion

		#region SetValueIfPossible
		private void SetValueIfPossible(EffectParameter parameter, ref Vector3 value)
		{
			if (Enabled && parameter != null)
				parameter.SetValue(value);
		}
		#endregion
	}
}
