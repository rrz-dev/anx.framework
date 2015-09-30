using System;
using System.IO;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
	public interface INativeIndexBuffer : INativeBuffer
	{
		void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct;

        void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct;
	}
}
