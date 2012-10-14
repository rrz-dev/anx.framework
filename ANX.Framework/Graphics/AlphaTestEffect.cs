using System;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
	[TestState(TestStateAttribute.TestState.Tested)]
	[Developer("AstrorEnales")]
    public class AlphaTestEffect : Effect, IEffectMatrices, IEffectFog, IGraphicsResource
	{
		#region Private
		private Matrix world;
		private Matrix view;
		private Matrix projection;
		private Vector3 fogColor;
		private Vector3 diffuseColor;
		private bool isVertexColorEnabled;
		private bool isFogEnabled;
		private CompareFunction alphaFunction;
		#endregion

		#region Public
		public float Alpha { get; set; }
		public float FogEnd { get; set; }
		public float FogStart { get; set; }
		public Texture2D Texture { get; set; }
		public int ReferenceAlpha { get; set; }

		public CompareFunction AlphaFunction
		{
			get { return alphaFunction; }
			set
			{
				alphaFunction = value;
				SelectTechnique();
			}
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

		public Vector3 DiffuseColor
		{
			get { return diffuseColor; }
			set { diffuseColor = value; }
		}

		public Vector3 FogColor
		{
			get { return fogColor; }
			set { fogColor = value; }
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
		#endregion

		#region Constructor
		public AlphaTestEffect(GraphicsDevice device)
			: base(device, GetByteCode(), GetSourceLanguage())
		{
			Alpha = 1f;
			isFogEnabled = false;
			isVertexColorEnabled = false;
			FogEnd = 1f;
			alphaFunction = CompareFunction.Greater;
			world = Matrix.Identity;
			view = Matrix.Identity;
			projection = Matrix.Identity;
			diffuseColor = Vector3.One;
			SelectTechnique();
        }

        protected AlphaTestEffect(AlphaTestEffect cloneSource)
            : base(cloneSource)
		{
			world = cloneSource.world;
			view = cloneSource.view;
			projection = cloneSource.projection;
			fogColor = cloneSource.fogColor;
			diffuseColor = cloneSource.diffuseColor;
			isFogEnabled = cloneSource.isFogEnabled;
			isVertexColorEnabled = cloneSource.isVertexColorEnabled;
			alphaFunction = cloneSource.alphaFunction;
			Alpha = cloneSource.Alpha;
			FogEnd = cloneSource.FogEnd;
			FogStart = cloneSource.FogStart;
			ReferenceAlpha = cloneSource.ReferenceAlpha;
			Texture = cloneSource.Texture;
			SelectTechnique();
		}
		#endregion

		#region GetByteCode
		private static byte[] GetByteCode()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			return creator.GetShaderByteCode(PreDefinedShader.AlphaTestEffect);
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
			return new AlphaTestEffect(this);
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

			SetAlphaTestValue();

			if (isFogEnabled)
				SetFogVector(ref worldView);
			else
				Parameters["FogVector"].SetValue(Vector4.Zero);
		}
		#endregion

		#region SetAlphaTestValue
		private void SetAlphaTestValue()
		{
			Vector4 alphaTestValue = Vector4.Zero;
			float num = (float)ReferenceAlpha / 255f;
			switch (alphaFunction)
			{
				case CompareFunction.Never:
					alphaTestValue.Z = -1f;
					alphaTestValue.W = -1f;
					break;

				case CompareFunction.Less:
					alphaTestValue.X = num - 0.00196078443f;
					alphaTestValue.Z = 1f;
					alphaTestValue.W = -1f;
					break;

				case CompareFunction.LessEqual:
					alphaTestValue.X = num + 0.00196078443f;
					alphaTestValue.Z = 1f;
					alphaTestValue.W = -1f;
					break;

				case CompareFunction.Equal:
					alphaTestValue.X = num;
					alphaTestValue.Y = 0.00196078443f;
					alphaTestValue.Z = 1f;
					alphaTestValue.W = -1f;
					break;

				case CompareFunction.GreaterEqual:
					alphaTestValue.X = num - 0.00196078443f;
					alphaTestValue.Z = -1f;
					alphaTestValue.W = 1f;
					break;

				case CompareFunction.Greater:
					alphaTestValue.X = num + 0.00196078443f;
					alphaTestValue.Z = -1f;
					alphaTestValue.W = 1f;
					break;

				case CompareFunction.NotEqual:
					alphaTestValue.X = num;
					alphaTestValue.Y = 0.00196078443f;
					alphaTestValue.Z = -1f;
					alphaTestValue.W = 1f;
					break;

				default:
					alphaTestValue.Z = 1f;
					alphaTestValue.W = 1f;
					break;
			}
			Parameters["AlphaTest"].SetValue(alphaTestValue);
		}
		#endregion

		#region SelectTechnique
		private void SelectTechnique()
		{
			string extra = (alphaFunction == CompareFunction.Equal || alphaFunction == CompareFunction.NotEqual) ?
				"EqNe" : "LtGt";

			if (isVertexColorEnabled)
			{
				if (isFogEnabled)
					CurrentTechnique = Techniques["AlphaTestVertexColor" + extra];
				if (isFogEnabled == false)
					CurrentTechnique = Techniques["AlphaTestVertexColorNoFog" + extra];
			}
			else
			{
				if (isFogEnabled)
					CurrentTechnique = Techniques["AlphaTest" + extra];
				if (isFogEnabled == false)
					CurrentTechnique = Techniques["AlphaTestNoFog" + extra];
			}
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
