#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Net
{
    public enum NetworkSessionType
    {
        Local = 0,
        SystemLink = 1,
        PlayerMatch = 2,
        Ranked = 3,
        LocalWithLeaderboards = 4,
    }
}
