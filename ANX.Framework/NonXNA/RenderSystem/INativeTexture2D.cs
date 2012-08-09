using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
	public interface INativeTexture2D : INativeTexture
	{
		void SaveAsJpeg(Stream stream, int width, int height);
		void SaveAsPng(Stream stream, int width, int height);

		void GetData<T>(int level, Nullable<Rectangle> rect, T[] data,
			int startIndex, int elementCount) where T : struct;

		void SetData<T>(int level, Nullable<Rectangle> rect, T[] data,
			int startIndex, int elementCount) where T : struct;
	}
}
