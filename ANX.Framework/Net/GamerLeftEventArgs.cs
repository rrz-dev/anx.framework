using System;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class GamerLeftEventArgs : EventArgs
    {
        public NetworkGamer Gamer { get; private set; }

        public GamerLeftEventArgs(NetworkGamer gamer)
        {
            Gamer = gamer;
        }
    }
}
