#region Using Statements
using System;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface INativeRasterizerState : IDisposable
    {
        void Apply(GraphicsDevice graphicsDevice);

        void Release();

        bool IsBound { get; }

        CullMode CullMode { set; }

        float DepthBias { set; }

        FillMode FillMode { set; }

        bool MultiSampleAntiAlias { set; }

        bool ScissorTestEnable { set; }

        float SlopeScaleDepthBias { set; }
    }
}
