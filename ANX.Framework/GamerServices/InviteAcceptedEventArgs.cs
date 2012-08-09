#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    public class InviteAcceptedEventArgs : EventArgs
    {
        public InviteAcceptedEventArgs(SignedInGamer gamer, bool isCurrentSession)
        {
            this.Gamer = gamer;
            this.IsCurrentSession = isCurrentSession;
        }

        public SignedInGamer Gamer
        {
            get;
            private set;
        }

        public bool IsCurrentSession
        {
            get;
            private set;
        }
    }
}
