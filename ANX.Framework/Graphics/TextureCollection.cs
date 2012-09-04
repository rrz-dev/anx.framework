using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
	public sealed class TextureCollection
	{
		private Texture[] textures;

		public Texture this[int index]
		{
			get
			{
				return textures[index];
			}
			set
			{
				textures[index] = value;
				var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
				creator.SetTextureSampler(index, value);
			}
		}

		internal TextureCollection(int maxNumberOfTextures)
		{
			textures = new Texture[maxNumberOfTextures];
		}
	}
}
