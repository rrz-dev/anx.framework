using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaSamplerState : INativeSamplerState
	{
		#region INativeSamplerState Member

		public void Apply(Framework.Graphics.GraphicsDevice graphicsDevice, int index)
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

		public Framework.Graphics.TextureAddressMode AddressU
		{
			set { throw new NotImplementedException(); }
		}

		public Framework.Graphics.TextureAddressMode AddressV
		{
			set { throw new NotImplementedException(); }
		}

		public Framework.Graphics.TextureAddressMode AddressW
		{
			set { throw new NotImplementedException(); }
		}

		public Framework.Graphics.TextureFilter Filter
		{
			set { throw new NotImplementedException(); }
		}

		public int MaxAnisotropy
		{
			set { throw new NotImplementedException(); }
		}

		public int MaxMipLevel
		{
			set { throw new NotImplementedException(); }
		}

		public float MipMapLevelOfDetailBias
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
