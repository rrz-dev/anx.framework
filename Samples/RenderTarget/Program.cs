using System;
using ANX.Framework.NonXNA;

namespace RenderTarget
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main(string[] args)
        {
			//AddInSystemFactory.Instance.PreferredRenderSystem = "OpenGL3";
            //AddInSystemFactory.Instance.PreferredRenderSystem = "DirectX11";

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

