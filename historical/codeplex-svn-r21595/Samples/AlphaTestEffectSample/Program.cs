using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace AlphaTestEffectSample
{
    static class Program
    {
        static void Main(string[] args)
		{
            AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "DirectX10");
			//AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "DirectX11");
            //AddInSystemFactory.Instance.SetPreferredSystem(AddInType.RenderSystem, "OpenGL3");

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}

