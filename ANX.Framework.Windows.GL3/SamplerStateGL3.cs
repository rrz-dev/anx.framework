using System;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework.Windows.GL3
{
	/// <summary>
	/// Native Sampler State object for OpenGL.
	/// <para />
	/// Basically this is a wrapper class for setting the different values all
	/// at once, because OpenGL has no State objects like DirectX.
	/// </summary>
	public class SamplerStateGL3 : INativeSamplerState
	{
		#region Public
		/// <summary>
		/// Flag if the state object is bound to the device.
		/// </summary>
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
		/// <param name="index"></param>
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
