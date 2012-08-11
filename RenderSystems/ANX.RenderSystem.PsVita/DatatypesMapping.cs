using System;
using Sce.PlayStation.Core.Graphics;
using ANX.Framework.Graphics;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	internal static class DatatypesMapping
	{
		#region SurfaceFormatToVitaPixelFormat
		public static PixelFormat SurfaceFormatToVitaPixelFormat(SurfaceFormat format)
		{
			switch (format)
			{
				case SurfaceFormat.Color:
					return PixelFormat.Rgba;

				case SurfaceFormat.Bgra4444:
					return PixelFormat.Rgba4444;

				case SurfaceFormat.Bgr565:
					return PixelFormat.Rgb565;

				case SurfaceFormat.Bgra5551:
					return PixelFormat.Rgba5551;

				default:
					throw new Exception("Can't convert surface format '" + format +
						"' to psvita pixel format!");
			}
		}
		#endregion
	}
}
