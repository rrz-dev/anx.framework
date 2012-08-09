#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [Flags]
    public enum ColorWriteChannels
    {
        All = 15,
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 4,
        Alpha = 8,
    }
}
