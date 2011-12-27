using System;
using ANX.Framework.NonXNA;

namespace RecordingSample
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            //This is technically unessasary, because there is only a reference to the RecordingSystem...
            AddInSystemFactory.Instance.PreferredInputSystem = "Recording";
            
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}
