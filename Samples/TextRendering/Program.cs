using System;
using ANX.Framework.NonXNA;

namespace TextRendering
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main(string[] args)
        {
            AddInSystemFactory.Instance.PreferredRenderSystem = "OpenGL3";

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

