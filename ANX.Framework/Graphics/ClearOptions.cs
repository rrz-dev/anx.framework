#region Using Statements
using System;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [Flags]
    public enum ClearOptions
    {
        DepthBuffer = 2,
        Stencil = 4,
        Target = 1
    }
}
