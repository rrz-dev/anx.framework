using System;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
	public interface INativeBuffer : IDisposable
	{
        void SetData<T>(T[] data) where T : struct;
		void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct;

        void GetData<T>(T[] data) where T : struct;
		void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct;
	}
}
