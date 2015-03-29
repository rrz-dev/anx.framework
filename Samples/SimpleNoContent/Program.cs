using System;

namespace SimpleNoContent
{
    static class Program
    {
        [MTAThread]
        static void Main()
        {
#if WINDOWSMETRO
            var host = new ANX.PlatformSystem.Metro.WindowsGameHost(() => new Game1());
            Windows.ApplicationModel.Core.CoreApplication.Run(host);
#else
            using (Game1 game = new Game1())
            {
                game.Run();
            }
#endif
        }
    }
}
