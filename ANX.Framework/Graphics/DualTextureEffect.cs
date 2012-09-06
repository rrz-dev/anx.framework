using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.InProgress)]
	[Developer("AstrorEnales")]
    public class DualTextureEffect : Effect, IEffectMatrices, IEffectFog
	{
		#region Private
		private Matrix world;
		private Matrix view;
		private Matrix projection;
		private Vector3 fogColor;
		private Vector3 diffuseColor;
		private bool isVertexColorEnabled;
		private bool isFogEnabled;
		#endregion

		#region Public
		public Matrix Projection
		{
			get { return projection; }
			set { projection = value; }
		}

		public Matrix View
		{
			get { return view; }
			set { view = value; }
		}

		public Matrix World
		{
			get { return world; }
			set { world = value; }
		}

		public Vector3 FogColor
		{
			get { return fogColor; }
			set { fogColor = value; }
		}

		public float Alpha { get; set; }
		public float FogEnd { get; set; }
		public float FogStart { get; set; }
		public Texture2D Texture { get; set; }
		public Texture2D Texture2 { get; set; }

		public Vector3 DiffuseColor
		{
			get { return diffuseColor; }
			set { diffuseColor = value; }
		}

		public bool FogEnabled
		{
			get { return isFogEnabled; }
			set
			{
				isFogEnabled = value;
				SelectTechnique();
			}
		}

		public bool VertexColorEnabled
		{
			get { return isVertexColorEnabled; }
			set
			{
				isVertexColorEnabled = value;
				SelectTechnique();
			}
		}
		#endregion

		#region Constructor
		public DualTextureEffect(GraphicsDevice graphics)
			: base(graphics, GetByteCode(), GetSourceLanguage())
        {
			diffuseColor = Vector3.One;
			Alpha = 1f;
			FogStart = 0f;
			FogEnd = 1f;
			FogEnabled = false;
			VertexColorEnabled = false;
			world = Matrix.Identity;
			view = Matrix.Identity;
			projection = Matrix.Identity;
        }

        protected DualTextureEffect(DualTextureEffect cloneSource)
            : base(cloneSource)
		{
			diffuseColor = cloneSource.diffuseColor;
			Alpha = cloneSource.Alpha;
			FogStart = cloneSource.FogStart;
			FogEnd = cloneSource.FogEnd;
			FogEnabled = cloneSource.FogEnabled;
			VertexColorEnabled = cloneSource.VertexColorEnabled;
			world = cloneSource.world;
			view = cloneSource.view;
			projection = cloneSource.projection;
			Texture = cloneSource.Texture;
			Texture2 = cloneSource.Texture2;
        }
		#endregion

		#region GetByteCode
		private static byte[] GetByteCode()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetShaderByteCode(PreDefinedShader.DualTextureEffect);
		}
		#endregion

		#region GetSourceLanguage
		private static EffectSourceLanguage GetSourceLanguage()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetStockShaderSourceLanguage;
		}
		#endregion

		#region Clone
		public override Effect Clone()
        {
            return new DualTextureEffect(this);
		}
		#endregion

		#region PreBindSetParameters
		internal override void PreBindSetParameters()
		{
			Matrix worldView;
			Matrix.Multiply(ref world, ref view, out worldView);
			Matrix wvp;
			Matrix.Multiply(ref worldView, ref projection, out wvp);
			Parameters["WorldViewProj"].SetValue(wvp);

			Parameters["DiffuseColor"].SetValue(new Vector4(diffuseColor * Alpha, Alpha));
			Parameters["FogColor"].SetValue(fogColor);

			if (Texture != null)
				Parameters["Texture"].SetValue(Texture);
			if (Texture2 != null)
				Parameters["Texture2"].SetValue(Texture2);

			if (FogStart == FogEnd)
			{
				Parameters["FogVector"].SetValue(new Vector4(0f, 0f, 0f, 1f));
				return;
			}

			float fogFactor = 1f / (FogStart - FogEnd);
			Vector4 value;
			value.X = worldView.M13 * fogFactor;
			value.Y = worldView.M23 * fogFactor;
			value.Z = worldView.M33 * fogFactor;
			value.W = (worldView.M43 + FogStart) * fogFactor;
			Parameters["FogVector"].SetValue(value);
		}
		#endregion

		#region SelectTechnique
		private void SelectTechnique()
		{
			if (isVertexColorEnabled)
			{
				if (isFogEnabled)
					CurrentTechnique = Techniques["DualTextureEffectVertexColor"];
				if (isFogEnabled == false)
					CurrentTechnique = Techniques["DualTextureEffectNoFogVertexColor"];
			}
			else
			{
				if (isFogEnabled)
					CurrentTechnique = Techniques["DualTextureEffect"];
				if (isFogEnabled == false)
					CurrentTechnique = Techniques["DualTextureEffectNoFog"];
			}
		}
		#endregion
	}
}
