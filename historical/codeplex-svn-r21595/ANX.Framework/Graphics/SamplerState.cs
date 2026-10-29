#region Using Statements
using System;
using System.Runtime.InteropServices;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	[PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
	public class SamplerState : GraphicsResource
	{
		#region Constants
		public static readonly SamplerState AnisotropicClamp;
		public static readonly SamplerState AnisotropicWrap;
		public static readonly SamplerState LinearClamp;
		public static readonly SamplerState LinearWrap;
		public static readonly SamplerState PointClamp;
		public static readonly SamplerState PointWrap;
		#endregion

		#region Private
		private INativeSamplerState nativeSamplerState;

		private TextureAddressMode addressU;
		private TextureAddressMode addressV;
		private TextureAddressMode addressW;
		private TextureFilter filter;
		private int maxAnisotropy;
		private int maxMipLevel;
		private float mipMapLevelOfDetailBias;
		#endregion

		#region Public
		internal INativeSamplerState NativeSamplerState
		{
			get
			{
				return this.nativeSamplerState;
			}
		}

		public TextureAddressMode AddressU
		{
			get
			{
				return this.addressU;
			}
			set
			{
				ThrowIfBound();
				this.addressU = value;
				this.nativeSamplerState.AddressU = value;
			}
		}

		public TextureAddressMode AddressV
		{
			get
			{
				return this.addressV;
			}
			set
			{
				ThrowIfBound();
				this.addressV = value;
				this.nativeSamplerState.AddressV = value;
			}
		}

		public TextureAddressMode AddressW
		{
			get
			{
				return this.addressW;
			}
			set
			{
				ThrowIfBound();
				this.addressW = value;
				this.nativeSamplerState.AddressW = value;
			}
		}

		public TextureFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				ThrowIfBound();
				this.filter = value;
				this.nativeSamplerState.Filter = value;
			}
		}

		public int MaxAnisotropy
		{
			get
			{
				return this.maxAnisotropy;
			}
			set
			{
				ThrowIfBound();
				this.maxAnisotropy = value;
				this.nativeSamplerState.MaxAnisotropy = value;
			}
		}

		public int MaxMipLevel
		{
			get
			{
				return this.maxMipLevel;
			}
			set
			{
				ThrowIfBound();
				this.maxMipLevel = value;
				this.nativeSamplerState.MaxMipLevel = value;
			}
		}

		public float MipMapLevelOfDetailBias
		{
			get
			{
				return this.mipMapLevelOfDetailBias;
			}
			set
			{
				ThrowIfBound();
				this.mipMapLevelOfDetailBias = value;
				this.nativeSamplerState.MipMapLevelOfDetailBias = value;
			}
		}
		#endregion

		#region Constructor
		public SamplerState()
		{
			CreateNative();
			AddressU = TextureAddressMode.Wrap;
			AddressV = TextureAddressMode.Wrap;
			AddressW = TextureAddressMode.Wrap;
			Filter = TextureFilter.Linear;
			MaxAnisotropy = 0;
			MaxMipLevel = 0;
			MipMapLevelOfDetailBias = 0f;
		}

		private SamplerState(TextureFilter filter, TextureAddressMode addressMode, string name)
		{
			CreateNative();
			AddressU = addressMode;
			AddressV = addressMode;
			AddressW = addressMode;
			Filter = filter;
			MaxAnisotropy = 0;
			MaxMipLevel = 0;
			MipMapLevelOfDetailBias = 0f;
			Name = name;
		}

		static SamplerState()
		{
			PointWrap = new SamplerState(TextureFilter.Point, TextureAddressMode.Wrap, "SamplerState.PointWrap");
			PointClamp = new SamplerState(TextureFilter.Point, TextureAddressMode.Clamp, "SamplerState.PointClamp");
			LinearWrap = new SamplerState(TextureFilter.Linear, TextureAddressMode.Wrap, "SamplerState.LinearWrap");
			LinearClamp = new SamplerState(TextureFilter.Linear, TextureAddressMode.Clamp, "SamplerState.LinearClamp");
			AnisotropicWrap = new SamplerState(TextureFilter.Anisotropic, TextureAddressMode.Wrap,
				"SamplerState.AnisotropicWrap");
			AnisotropicClamp = new SamplerState(TextureFilter.Anisotropic, TextureAddressMode.Clamp,
				"SamplerState.AnisotropicClamp");
		}
		#endregion

		#region Dispose
		public override void Dispose()
		{
			if (this.nativeSamplerState != null)
			{
				this.nativeSamplerState.Dispose();
				this.nativeSamplerState = null;
			}
		}

		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool disposeManaged)
		{
			base.Dispose(disposeManaged);
		}
		#endregion

		#region CreateNative
		private void CreateNative()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			nativeSamplerState = creator.CreateSamplerState();
		}
		#endregion

		#region ThrowIfBound
		private void ThrowIfBound()
		{
			if (nativeSamplerState.IsBound)
				throw new InvalidOperationException("You are not allowed to change SamplerState properties while it is " +
					"bound to the GraphicsDevice.");
		}
		#endregion
	}
}
