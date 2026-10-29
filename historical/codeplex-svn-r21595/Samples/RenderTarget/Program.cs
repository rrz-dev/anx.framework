using System;
using ANX.Framework.NonXNA;

namespace RenderTarget
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main(string[] args)
			{
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}

