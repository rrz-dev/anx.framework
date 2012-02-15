using System;
using ANX.Framework.NonXNA;

namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
			{
				AddInSystemFactory.Instance.SetPreferredSystem(
					AddInType.RenderSystem, "OpenGL3");
				//AddInType.RenderSystem, "DirectX11");

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

