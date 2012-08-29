using System;
using ANX.Framework.NonXNA;

namespace TextRendering
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main(string[] args)
			{
				//AddInSystemFactory.Instance.SetPreferredSystem(
				//  AddInType.RenderSystem, "OpenGL3");
				//AddInSystemFactory.Instance.SetPreferredSystem(
				//  AddInType.RenderSystem, "DirectX11");

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}

