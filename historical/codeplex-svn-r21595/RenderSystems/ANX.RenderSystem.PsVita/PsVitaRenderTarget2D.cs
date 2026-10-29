using System;
using ANX.Framework.NonXNA.RenderSystem;
using VitaRenderTarget = Sce.PlayStation.Core.Graphics.RenderTarget;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.RenderSystem.PsVita
{
	public class PsVitaRenderTarget2D : PsVitaTexture2D, INativeRenderTarget2D
	{
		// TODO
		public PsVitaRenderTarget2D()
			: base(Framework.Graphics.SurfaceFormat.Color, 100, 100, 0)
		{
		}

		public override void Dispose()
		{
		}
	}
}
