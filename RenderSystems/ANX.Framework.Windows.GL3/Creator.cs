using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.RenderSystem;
using OpenTK;

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
	/// OpenGL graphics creator.
	/// </summary>
	public class Creator : IRenderSystemCreator
	{
		#region Public
		/// <summary>
		/// Name of the Creator implementation.
		/// </summary>
		public string Name
		{
			get
			{
				return "OpenGL3";
			}
		}

		public int Priority
		{
			get { return 100; }
		}

		#endregion

		#region RegisterRenderSystemCreator
		public void RegisterCreator(AddInSystemFactory factory)
		{
			factory.AddCreator(this);
		}
		#endregion

		#region CreateGameHost
		public GameHost CreateGameHost(Game game)
		{
			return new WindowsGameHost(game);
		}
		#endregion

		#region CreateEffect
		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect, Stream byteCode)
		{
			return new EffectGL3(managedEffect, byteCode);
		}

		public INativeEffect CreateEffect(GraphicsDevice graphics, Effect managedEffect,
			Stream vertexShaderByteCode, Stream pixelShaderByteCode)
		{
			return new EffectGL3(managedEffect, vertexShaderByteCode, pixelShaderByteCode);
		}
		#endregion

		#region CreateGraphicsDevice
		INativeGraphicsDevice IRenderSystemCreator.CreateGraphicsDevice(
			PresentationParameters presentationParameters)
		{
			return new GraphicsDeviceWindowsGL3(presentationParameters);
		}
		#endregion

		#region CreateTexture
		/// <summary>
		/// Create a new native texture.
		/// </summary>
		/// <param name="graphics">Graphics device.</param>
		/// <param name="surfaceFormat">The format of the texture.</param>
		/// <param name="width">The width of the texture.</param>
		/// <param name="height">The height of the texture.</param>
		/// <param name="mipCount">The number of mipmaps in the texture.</param>
		/// <returns></returns>
		public INativeTexture2D CreateTexture(GraphicsDevice graphics,
			SurfaceFormat surfaceFormat, int width, int height, int mipCount)
		{
			return new Texture2DGL3(surfaceFormat, width, height, mipCount);
		}
		#endregion

		#region CreateIndexBuffer
		/// <summary>
		/// Create a native index buffer.
		/// </summary>
		/// <param name="graphics">The current graphics device.</param>
		/// <param name="size">The size of a single index element.</param>
		/// <param name="indexCount">The number of indices stored in the buffer.
		/// </param>
		/// <param name="usage">The usage type of the buffer.</param>
		/// <returns>Native OpenGL index buffer.</returns>
		public INativeBuffer CreateIndexBuffer(GraphicsDevice graphics,
			IndexElementSize size, int indexCount, BufferUsage usage)
		{
			return new IndexBufferGL3(size, indexCount, usage);
		}
		#endregion

		#region CreateVertexBuffer
		/// <summary>
		/// Create a native vertex buffer.
		/// </summary>
		/// <param name="graphics">The current graphics device.</param>
		/// <param name="size">The vertex declaration for the buffer.</param>
		/// <param name="indexCount">The number of vertices stored in the buffer.
		/// </param>
		/// <param name="usage">The usage type of the buffer.</param>
		/// <returns>Native OpenGL vertex buffer.</returns>
		public INativeBuffer CreateVertexBuffer(GraphicsDevice graphics,
			VertexDeclaration vertexDeclaration, int vertexCount,
			BufferUsage usage)
		{
			return new VertexBufferGL3(vertexDeclaration, vertexCount, usage);
		}
		#endregion

		#region CreateBlendState
		/// <summary>
		/// Create a new native blend state.
		/// </summary>
		/// <returns>Native Blend State.</returns>
		public INativeBlendState CreateBlendState()
		{
			return new BlendStateGL3();
		}
		#endregion

		#region CreateBlendState
		/// <summary>
		/// Create a new native rasterizer state.
		/// </summary>
		/// <returns>Native Rasterizer State.</returns>
		public INativeRasterizerState CreateRasterizerState()
		{
			return new RasterizerStateGL3();
		}
		#endregion

		#region CreateDepthStencilState
		/// <summary>
		/// Create a new native Depth Stencil State.
		/// </summary>
		/// <returns>Native Depth Stencil State.</returns>
		public INativeDepthStencilState CreateDepthStencilState()
		{
			return new DepthStencilStateGL3();
		}
		#endregion

		#region CreateSamplerState
		/// <summary>
		/// Create a new native sampler state.
		/// </summary>
		/// <returns>Native Sampler State.</returns>
		public INativeSamplerState CreateSamplerState()
		{
			return new SamplerStateGL3();
		}
		#endregion

		#region GetShaderByteCode
		/// <summary>
		/// Get the byte code of a pre defined shader.
		/// </summary>
		/// <param name="type">Pre defined shader type.</param>
		/// <returns>Byte code of the shader.</returns>
		public byte[] GetShaderByteCode(PreDefinedShader type)
		{
			if (type == PreDefinedShader.SpriteBatch)
			{
				return ShaderByteCode.SpriteBatchByteCode;
			}
			else if (type == PreDefinedShader.AlphaTestEffect)
			{
				return ShaderByteCode.AlphaTestEffectByteCode;
			}
			else if (type == PreDefinedShader.BasicEffect)
			{
				return ShaderByteCode.BasicEffectByteCode;
			}
			else if (type == PreDefinedShader.DualTextureEffect)
			{
				return ShaderByteCode.DualTextureEffectByteCode;
			}
			else if (type == PreDefinedShader.EnvironmentMapEffect)
			{
				return ShaderByteCode.EnvironmentMapEffectByteCode;
			}
			else if (type == PreDefinedShader.SkinnedEffect)
			{
				return ShaderByteCode.SkinnedEffectByteCode;
			}

			throw new NotImplementedException("ByteCode for '" + type.ToString() + "' is not yet available");
		}
		#endregion

		#region GetAdapterList
		/// <summary>
		/// Get a list of available graphics adapter information.
		/// </summary>
		/// <returns>List of graphics adapters.</returns>
		public ReadOnlyCollection<GraphicsAdapter> GetAdapterList()
		{
			var result = new List<GraphicsAdapter>();
			foreach (DisplayDevice device in DisplayDevice.AvailableDisplays)
			{
				var displayModeCollection = new DisplayModeCollection();
				foreach (string format in Enum.GetNames(typeof(SurfaceFormat)))
				{
					SurfaceFormat surfaceFormat =
						(SurfaceFormat)Enum.Parse(typeof(SurfaceFormat), format);

					// TODO: device.BitsPerPixel
					if (surfaceFormat != SurfaceFormat.Color)//adapter.Supports(surfaceFormat) == false)
					{
						continue;
					}

					var modes = new List<DisplayMode>();

					foreach (DisplayResolution res in device.AvailableResolutions)
					{
						float aspect = (float)res.Width / (float)res.Height;
						modes.Add(new DisplayMode
						{
							AspectRatio = aspect,
							Width = res.Width,
							Height = res.Height,
							TitleSafeArea = new Rectangle(0, 0, res.Width, res.Height),
							Format = surfaceFormat,
						});
					}

					displayModeCollection[surfaceFormat] = modes.ToArray();
				}

				GraphicsAdapter newAdapter = new GraphicsAdapter
				{
					SupportedDisplayModes = displayModeCollection,
					IsDefaultAdapter = device.IsPrimary,

					// TODO:
					DeviceId = 0,
					DeviceName = "",
					Revision = 0,
					SubSystemId = 0,
					VendorId = 0,
				};

				result.Add(newAdapter);
			}

			return new ReadOnlyCollection<GraphicsAdapter>(result);
		}
		#endregion

		#region CreateRenderTarget
		public INativeRenderTarget2D CreateRenderTarget(GraphicsDevice graphics,
			int width, int height, bool mipMap, SurfaceFormat preferredFormat,
			DepthFormat preferredDepthFormat, int preferredMultiSampleCount,
			RenderTargetUsage usage)
		{
			return new RenderTarget2DGL3(width, height, mipMap, preferredFormat,
				preferredDepthFormat, preferredMultiSampleCount, usage);
		}
		#endregion
	}
}
