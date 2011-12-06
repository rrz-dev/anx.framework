using System;
using ANX.Framework.NonXNA;

namespace Kinect
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        static void Main(string[] args)
        {
            AddInSystemFactory.Instance.PreferredInputSystem = "Kinect";

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

