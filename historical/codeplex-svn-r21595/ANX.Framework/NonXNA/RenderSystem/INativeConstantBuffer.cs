#region Using Statements
using System;
using System.IO;
using ANX.Framework.Graphics;

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA.RenderSystem
{
#if XNAEXT
	
    public interface INativeConstantBuffer : INativeBuffer
	{
		void GetData<T>(T data) where T : struct;

		void SetData<T>(GraphicsDevice graphicsDevice, T data) where T : struct;
	}

#endif
}
