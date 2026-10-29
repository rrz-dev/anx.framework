using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaSamplerState : INativeSamplerState
	{
		#region Public
		public bool IsBound
		{
			get;
			private set;
		}

		public TextureAddressMode AddressU
		{
			set;
			private get;
		}

		public TextureAddressMode AddressV
		{
			set;
			private get;
		}

		public TextureAddressMode AddressW
		{
			set;
			private get;
		}

		public TextureFilter Filter
		{
			set;
			private get;
		}

		public int MaxAnisotropy
		{
			set;
			private get;
		}

		public int MaxMipLevel
		{
			set;
			private get;
		}

		public float MipMapLevelOfDetailBias
		{
			set;
			private get;
		}
		#endregion

		#region Constructor
		internal PsVitaSamplerState()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		public void Apply(GraphicsDevice graphicsDevice, int index)
		{
			IsBound = true;

			// TODO: set stuff
		}
		#endregion

		#region Release
		public void Release()
		{
			IsBound = false;
		}
		#endregion

		#region Dispose
		public void Dispose()
		{
		}
		#endregion
	}
}
