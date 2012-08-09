#region Using Statements
using System;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface INativeBlendState : IDisposable
    {
        void Apply(GraphicsDevice graphicsDevice);

        void Release();

        bool IsBound { get; }

        Color BlendFactor { set; }

        int MultiSampleMask { set; }

        BlendFunction AlphaBlendFunction { set; }

        BlendFunction ColorBlendFunction { set; }

        Blend AlphaDestinationBlend { set; }

        Blend ColorDestinationBlend { set; }

        ColorWriteChannels ColorWriteChannels { set; }

        ColorWriteChannels ColorWriteChannels1 { set; }

        ColorWriteChannels ColorWriteChannels2 { set; }

        ColorWriteChannels ColorWriteChannels3 { set; }

        Blend AlphaSourceBlend { set; }

        Blend ColorSourceBlend { set; }
    }
}
