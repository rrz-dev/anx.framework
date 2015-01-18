using System;

#if DX10
using Dx = SharpDX.Direct3D10;
#elif DX11
using Dx = SharpDX.Direct3D11;
#endif

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#elif DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
    class BufferHelper
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void ValidateCopyResource(Dx.Buffer source, Dx.Buffer destination)
        {
            if (source.Description.SizeInBytes != destination.Description.SizeInBytes)
                throw new InvalidOperationException("source and destination must have the same size.");
        }
    }
}