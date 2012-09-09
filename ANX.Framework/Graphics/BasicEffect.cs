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
    public class BasicEffect : Effect, IEffectMatrices, IEffectLights, IEffectFog
	{
		#region Private
		private Matrix world;
		private Matrix view;
		private Matrix projection;
		private bool preferPerPixelLighting;
		private bool isFogEnabled;
		private bool isLightingEnabled;
		private bool isVertexColorEnabled;
		private bool isTextureEnabled;
		private Vector3 diffuseColor;
		private Vector3 specularColor;
		private Vector3 fogColor;
		private Vector3 emissiveColor;
		private Vector3 ambientLightColor;
		#endregion

		#region Public
		public float FogEnd { get; set; }
		public float FogStart { get; set; }
		public Texture2D Texture { get; set; }
		public float SpecularPower { get; set; }
		public float Alpha { get; set; }
		public DirectionalLight DirectionalLight0 { get; private set; }
		public DirectionalLight DirectionalLight1 { get; private set; }
		public DirectionalLight DirectionalLight2 { get; private set; }

		public bool PreferPerPixelLighting
		{
			get { return preferPerPixelLighting; }
			set
			{
				preferPerPixelLighting = value;
				SelectTechnique();
			}
		}

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

		public Vector3 AmbientLightColor
		{
			get { return ambientLightColor; }
			set { ambientLightColor = value; }
		}

		public bool LightingEnabled
		{
			get { return isLightingEnabled; }
			set
			{
				isLightingEnabled = value;
				SelectTechnique();
			}
		}

		public Vector3 FogColor
		{
			get { return fogColor; }
			set { fogColor = value; }
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

		public bool TextureEnabled
		{
			get { return isTextureEnabled; }
			set
			{
				isTextureEnabled = value;
				SelectTechnique();
			}
		}

		public Vector3 DiffuseColor
		{
			get { return diffuseColor; }
			set { diffuseColor = value; }
		}

		public Vector3 EmissiveColor
		{
			get { return emissiveColor; }
			set { emissiveColor = value; }
		}

		public Vector3 SpecularColor
		{
			get { return specularColor; }
			set { specularColor = value; }
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
		public BasicEffect(GraphicsDevice graphics)
			: base(graphics, GetByteCode(), GetSourceLanguage())
		{
			world = Matrix.Identity;
			view = Matrix.Identity;
			projection = Matrix.Identity;
			FogStart = 0f;
			FogEnd = 1f;
			Alpha = 1f;
			diffuseColor = Vector3.One;
			ambientLightColor = Vector3.One;
			emissiveColor = Vector3.One;
			specularColor = Vector3.One;
			SpecularPower = 16f;
			CreateLights(null);
			DirectionalLight0.Enabled = true;

			SelectTechnique();
        }

        protected BasicEffect(BasicEffect cloneSource)
            : base(cloneSource)
		{
			world = cloneSource.world;
			view = cloneSource.view;
			projection = cloneSource.projection;
			preferPerPixelLighting = cloneSource.preferPerPixelLighting;
			isFogEnabled = cloneSource.isFogEnabled;
			isLightingEnabled = cloneSource.isLightingEnabled;
			isVertexColorEnabled = cloneSource.isVertexColorEnabled;
			isTextureEnabled = cloneSource.isTextureEnabled;
			diffuseColor = cloneSource.diffuseColor;
			specularColor = cloneSource.specularColor;
			fogColor = cloneSource.fogColor;
			emissiveColor = cloneSource.emissiveColor;
			ambientLightColor = cloneSource.ambientLightColor;
			FogEnd = cloneSource.FogEnd;
			FogStart = cloneSource.FogStart;
			Texture = cloneSource.Texture;
			SpecularPower = cloneSource.SpecularPower;
			Alpha = cloneSource.Alpha;

			CreateLights(cloneSource);
			SelectTechnique();
		}
		#endregion

		#region GetByteCode
		private static byte[] GetByteCode()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetShaderByteCode(PreDefinedShader.BasicEffect);
		}
		#endregion

		#region GetSourceLanguage
		private static EffectSourceLanguage GetSourceLanguage()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetStockShaderSourceLanguage;
		}
		#endregion

		#region EnableDefaultLighting
		public void EnableDefaultLighting()
		{
			LightingEnabled = true;
			ambientLightColor = new Vector3(0.05333332f, 0.09882354f, 0.1819608f);

			DirectionalLight0.Direction = new Vector3(-0.5265408f, -0.5735765f, -0.6275069f);
			DirectionalLight0.DiffuseColor = new Vector3(1f, 0.9607844f, 0.8078432f);
			DirectionalLight0.SpecularColor = DirectionalLight0.DiffuseColor;
			DirectionalLight0.Enabled = true;

			DirectionalLight1.Direction = new Vector3(0.7198464f, 0.3420201f, 0.6040227f);
			DirectionalLight1.DiffuseColor = new Vector3(0.9647059f, 0.7607844f, 0.4078432f);
			DirectionalLight1.SpecularColor = Vector3.Zero;
			DirectionalLight1.Enabled = true;

			DirectionalLight2.Direction = new Vector3(0.4545195f, -0.7660444f, 0.4545195f);
			DirectionalLight2.DiffuseColor = new Vector3(0.3231373f, 0.3607844f, 0.3937255f);
			DirectionalLight2.SpecularColor = DirectionalLight2.DiffuseColor;
			DirectionalLight2.Enabled = true;
		}
		#endregion

		#region Clone
		public override Effect Clone()
        {
            return new BasicEffect(this);
		}
		#endregion

		#region CreateLights
		private void CreateLights(BasicEffect cloneSource)
		{
			DirectionalLight0 = new DirectionalLight(Parameters["DirLight0Direction"], Parameters["DirLight0DiffuseColor"],
				null, (cloneSource != null) ? cloneSource.DirectionalLight0 : null);
			DirectionalLight1 = new DirectionalLight(Parameters["DirLight1Direction"], Parameters["DirLight1DiffuseColor"],
				null, (cloneSource != null) ? cloneSource.DirectionalLight1 : null);
			DirectionalLight2 = new DirectionalLight(Parameters["DirLight2Direction"], Parameters["DirLight2DiffuseColor"],
				null, (cloneSource != null) ? cloneSource.DirectionalLight2 : null);
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

			SetLightingMatrices();
			SetMaterialColor();

			Parameters["FogColor"].SetValue(fogColor);
			Parameters["SpecularPower"].SetValue(SpecularPower);
			Parameters["SpecularColor"].SetValue(specularColor);

			if (Texture != null)
				Parameters["Texture"].SetValue(Texture);

			if (isFogEnabled)
				SetFogVector(ref worldView);
			else
				Parameters["FogVector"].SetValue(Vector4.Zero);

			SelectTechnique();
		}
		#endregion

		#region SelectTechnique
		private void SelectTechnique()
        {
			string techniqueName = "BasicEffect";

			if (isLightingEnabled)
			{
				if (preferPerPixelLighting)
					techniqueName += "PixelLighting";
				else
				{
					bool isLight1Disabled = DirectionalLight1 == null || DirectionalLight1.Enabled == false;
					bool isLight2Disabled = DirectionalLight2 == null || DirectionalLight2.Enabled == false;
					if (isLight1Disabled && isLight2Disabled)
						techniqueName += "OneLight";
					else
						techniqueName += "VertexLighting";
				}
			}

			if (isTextureEnabled)
				techniqueName += "Texture";

			if (isVertexColorEnabled)
				techniqueName += "VertexColor";

			if (isFogEnabled == false)
				techniqueName += "NoFog";

			CurrentTechnique = Techniques[techniqueName];
        }
		#endregion

		#region SetLightingMatrices
		private void SetLightingMatrices()
		{
			Matrix worldInverse;
			Matrix.Invert(ref world, out worldInverse);
			Matrix worldInverseTranspose;
			Matrix.Transpose(ref worldInverse, out worldInverseTranspose);

			Parameters["World"].SetValue(world);
			Parameters["WorldInverseTranspose"].SetValue(worldInverseTranspose);

			Matrix viewInverse;
			Matrix.Invert(ref view, out viewInverse);
			Parameters["EyePosition"].SetValue(viewInverse.Translation);
		}
		#endregion

		#region SetMaterialColor
		private void SetMaterialColor()
		{
			Vector4 diffuse;
			if (isLightingEnabled)
			{
				diffuse.X = diffuseColor.X * Alpha;
				diffuse.Y = diffuseColor.Y * Alpha;
				diffuse.Z = diffuseColor.Z * Alpha;
				diffuse.W = Alpha;
				Vector3 emissive;
				emissive.X = (emissiveColor.X + ambientLightColor.X * diffuseColor.X) * Alpha;
				emissive.Y = (emissiveColor.Y + ambientLightColor.Y * diffuseColor.Y) * Alpha;
				emissive.Z = (emissiveColor.Z + ambientLightColor.Z * diffuseColor.Z) * Alpha;
				Parameters["DiffuseColor"].SetValue(diffuse);
				Parameters["EmissiveColor"].SetValue(emissive);
				return;
			}

			diffuse.X = (diffuseColor.X + emissiveColor.X) * Alpha;
			diffuse.Y = (diffuseColor.Y + emissiveColor.Y) * Alpha;
			diffuse.Z = (diffuseColor.Z + emissiveColor.Z) * Alpha;
			diffuse.W = Alpha;
			Parameters["DiffuseColor"].SetValue(diffuse);
		}
		#endregion

		#region SetFogVector
		private void SetFogVector(ref Matrix worldView)
		{
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
    }
}
