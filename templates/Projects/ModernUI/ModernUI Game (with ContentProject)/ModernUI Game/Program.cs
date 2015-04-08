using System;

namespace $safeprojectname$
{
    static class Program
    {
        static void Main(string[] args)
        {
            var host = new ANX.PlatformSystem.Metro.WindowsGameHost(() => new Game1());
            Windows.ApplicationModel.Core.CoreApplication.Run(host);
        }
    }
}
