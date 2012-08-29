using System;
using ANX.Framework.NonXNA;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace AudioSample
{
    static class Program
    {
        static void Main(string[] args)
		{
			AddInSystemFactory.Instance.SetPreferredSystem(AddInType.SoundSystem, "OpenAL");
			//AddInSystemFactory.Instance.SetPreferredSystem(AddInType.SoundSystem, "XAudio");

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}

