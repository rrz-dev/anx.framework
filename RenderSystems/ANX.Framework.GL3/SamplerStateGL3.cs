using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.GL3
{
	/// <summary>
	/// Native Sampler State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// <para />
	/// Info for OpenGL filter states:
	/// http://gregs-blog.com/2008/01/17/opengl-texture-filter-parameters-explained/
	/// 
	/// Info for OGL 3.3 sampler objects (sadly not implemented in OpenTK yet):
	/// http://www.sinanc.org/blog/?p=215
	/// </summary>
	[PercentageComplete(10)]
	[TestState(TestStateAttribute.TestState.Untested)]
	public class SamplerStateGL3 : INativeSamplerState
	{
		#region Public
		#region IsBound
		/// <summary>
		/// Flag if the state object is bound to the device.
		/// </summary>
		public bool IsBound
		{
			get;
			private set;
		}
		#endregion

		#region AddressU
		public TextureAddressMode AddressU
		{
			set;
			private get;
		}
		#endregion
		
		#region AddressV
		public TextureAddressMode AddressV
		{
			set;
			private get;
		}
		#endregion
		
		#region AddressW
		public TextureAddressMode AddressW
		{
			set;
			private get;
		}
		#endregion
		
		#region Filter
		public TextureFilter Filter
		{
			set;
			private get;
		}
		#endregion
		
		#region MaxAnisotropy
		public int MaxAnisotropy
		{
			set;
			private get;
		}
		#endregion
		
		#region MaxMipLevel
		public int MaxMipLevel
		{
			set;
			private get;
		}
		#endregion
		
		#region MipMapLevelOfDetailBias
		public float MipMapLevelOfDetailBias
		{
			set;
			private get;
		}
		#endregion
		#endregion

		#region Constructor
		/// <summary>
		/// Create a new sampler state object.
		/// </summary>
		internal SamplerStateGL3()
		{
			IsBound = false;
		}
		#endregion

		#region Apply
		/// <summary>
		/// Apply the sampler state.
		/// </summary>
		/// <param name="graphicsDevice">Graphics device.</param>
		/// <param name="index">The index of which sampler should be modified.</param>
		public void Apply(GraphicsDevice graphicsDevice, int index)
		{
			IsBound = true;

			// TODO: set stuff
		}
		#endregion

		#region Release
		/// <summary>
		/// Release the sampler state.
		/// </summary>
		public void Release()
		{
			IsBound = false;
		}
		#endregion

		#region Dispose
		/// <summary>
		/// Dispose the sampler state object.
		/// </summary>
		public void Dispose()
		{
		}
		#endregion
	}
}
