#region Using Statements
using System;
using ANX.Framework.Graphics;
using System.Collections.Generic;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface INativeEffect : IDisposable
    {
        IEnumerable<EffectTechnique> Techniques { get; }
        
        IEnumerable<EffectParameter> Parameters { get; }
    }
}
