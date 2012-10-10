#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(10)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public struct LeaderboardIdentity
    {
        public static LeaderboardIdentity Create(LeaderboardKey key)
        {
            throw new NotSupportedException("Games for Windows LIVE is not supported in ANX");
        }

        public static LeaderboardIdentity Create(LeaderboardKey key, int gameMode)
        {
            throw new NotSupportedException("Games for Windows LIVE is not supported in ANX");
        }

        public int GameMode 
        { 
            get; 
            set; 
        }

        public string Key 
        { 
            get; 
            set; 
        }
    }
}
