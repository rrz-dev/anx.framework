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
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public class SignedOutEventArgs : EventArgs
    {
        public SignedOutEventArgs(SignedInGamer gamer)
        {
            this.Gamer = gamer;
        }

        public SignedInGamer Gamer { get; private set; }
    }
}
