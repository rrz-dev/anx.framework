#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework
{
    [Flags]
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public enum DisplayOrientation
    {
        Default = 0,
        LandscapeLeft = 1,
        LandscapeRight = 2,
        Portrait = 4,
    }
}
