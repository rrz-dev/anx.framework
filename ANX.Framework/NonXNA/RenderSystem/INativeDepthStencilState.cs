#region Using Statements
using System;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface INativeDepthStencilState : IDisposable
    {
        void Apply(GraphicsDevice graphicsDevice);

        void Release();

        bool IsBound { get; }

        StencilOperation CounterClockwiseStencilDepthBufferFail { set; }

        StencilOperation CounterClockwiseStencilFail { set; }

        CompareFunction CounterClockwiseStencilFunction { set; }

        StencilOperation CounterClockwiseStencilPass { set; }

        bool DepthBufferEnable { set; }

        CompareFunction DepthBufferFunction { set; }

        bool DepthBufferWriteEnable { set; }

        int ReferenceStencil { set; }

        StencilOperation StencilDepthBufferFail { set; }

        bool StencilEnable { set; }

        StencilOperation StencilFail { set; }

        CompareFunction StencilFunction { set; }

        int StencilMask { set; }

        StencilOperation StencilPass { set; }

        int StencilWriteMask { set; }

        bool TwoSidedStencilMode { set; }
    }
}
