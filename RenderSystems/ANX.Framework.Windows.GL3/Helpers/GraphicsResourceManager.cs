using System;
using System.Collections.Generic;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.Windows.GL3.Helpers
{
	internal static class GraphicsResourceManager
	{
		#region Private
		private static List<Texture2DGL3> allTextures;

		private static List<EffectGL3> allEffects;

		private static List<VertexBufferGL3> allVertexBuffers;

		private static List<IndexBufferGL3> allIndexBuffers;
		#endregion
		
		#region Constructor
		static GraphicsResourceManager()
		{
			allTextures = new List<Texture2DGL3>();
			allEffects = new List<EffectGL3>();
			allVertexBuffers = new List<VertexBufferGL3>();
			allIndexBuffers = new List<IndexBufferGL3>();
		}
		#endregion

		#region UpdateResource
		public static void UpdateResource<T>(T resource, bool register)
		{
			IList<T> list = null;

			if (typeof(T) == typeof(Texture2DGL3))
			{
				list = (IList<T>)allTextures;
			}
			else if (typeof(T) == typeof(EffectGL3))
			{
				list = (IList<T>)allEffects;
			}
			else if (typeof(T) == typeof(VertexBufferGL3))
			{
				list = (IList<T>)allVertexBuffers;
			}
			else if (typeof(T) == typeof(IndexBufferGL3))
			{
				list = (IList<T>)allIndexBuffers;
			}

			lock (list)
			{
				if (register)
				{
					list.Add(resource);
				}
				else
				{
					list.Remove(resource);
				}
			}
		}
		#endregion

		#region DisposeAllResources
		public static void DisposeAllResources()
		{
			#region Textures
			lock (allTextures)
			{
				foreach (Texture2DGL3 texture in allTextures)
				{
					if (texture.IsDisposed == false)
					{
						texture.DisposeResource();
					}
				}
			}
			#endregion

			#region Effects
			lock (allEffects)
			{
				foreach (EffectGL3 effect in allEffects)
				{
					if (effect.IsDisposed == false)
					{
						effect.DisposeResource();
					}
				}
			}
			#endregion

			#region VertexBuffers
			lock (allVertexBuffers)
			{
				foreach (VertexBufferGL3 buffer in allVertexBuffers)
				{
					if (buffer.IsDisposed == false)
					{
						buffer.DisposeResource();
					}
				}
			}
			#endregion

			#region IndexBuffers
			lock (allIndexBuffers)
			{
				foreach (IndexBufferGL3 buffer in allIndexBuffers)
				{
					if (buffer.IsDisposed == false)
					{
						buffer.DisposeResource();
					}
				}
			}
			#endregion
		}
		#endregion

		#region RecreateAllResources
		public static void RecreateAllResources()
		{
			#region Textures
			lock (allTextures)
			{
				foreach (Texture2DGL3 texture in allTextures)
				{
					if (texture.IsDisposed == false)
					{
						texture.RecreateData();
					}
				}
			}
			#endregion

			#region Effects
			lock (allEffects)
			{
				foreach (EffectGL3 effect in allEffects)
				{
					if (effect.IsDisposed == false)
					{
						effect.RecreateData();
					}
				}
			}
			#endregion

			#region VertexBuffers
			lock (allVertexBuffers)
			{
				foreach (VertexBufferGL3 buffer in allVertexBuffers)
				{
					if (buffer.IsDisposed == false)
					{
						buffer.RecreateData();
					}
				}
			}
			#endregion

			#region IndexBuffers
			lock (allIndexBuffers)
			{
				foreach (IndexBufferGL3 buffer in allIndexBuffers)
				{
					if (buffer.IsDisposed == false)
					{
						buffer.RecreateData();
					}
				}
			}
			#endregion

			if (BlendStateGL3.Current != null)
			{
				BlendStateGL3.Current.Apply(null);
			}
			if (RasterizerStateGL3.Current != null)
			{
				RasterizerStateGL3.Current.Apply(null);
			}
			if (DepthStencilStateGL3.Current != null)
			{
				DepthStencilStateGL3.Current.Apply(null);
			}
		}
		#endregion
	}
}
