using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaRasterizerState : INativeRasterizerState
	{
		#region INativeRasterizerState Member

		public void Apply(Framework.Graphics.GraphicsDevice graphicsDevice)
		{
			throw new NotImplementedException();
		}

		public void Release()
		{
			throw new NotImplementedException();
		}

		public bool IsBound
		{
			get { throw new NotImplementedException(); }
		}

		public Framework.Graphics.CullMode CullMode
		{
			set { throw new NotImplementedException(); }
		}

		public float DepthBias
		{
			set { throw new NotImplementedException(); }
		}

		public Framework.Graphics.FillMode FillMode
		{
			set { throw new NotImplementedException(); }
		}

		public bool MultiSampleAntiAlias
		{
			set { throw new NotImplementedException(); }
		}

		public bool ScissorTestEnable
		{
			set { throw new NotImplementedException(); }
		}

		public float SlopeScaleDepthBias
		{
			set { throw new NotImplementedException(); }
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
