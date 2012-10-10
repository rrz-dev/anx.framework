#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public enum AvatarMouth
    {
        Neutral,
        Sad,
        Angry,
        Confused,
        Laughing,
        Shocked,
        Happy,
        PhoneticO,
        PhoneticAi,
        PhoneticEe,
        PhoneticFv,
        PhoneticW,
        PhoneticL,
        PhoneticDth
    }
}
