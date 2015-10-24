#region Using Statements
using ANX.Framework.Graphics;
using System;
#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface INativeGraphicsAdapter
    {
        IntPtr MonitorHandle { get; }
        bool IsDefaultAdapter { get; }
        int Revision { get; }
        int SubSystemId { get; }
        int DeviceId { get; }
        int VendorId { get; }
        string DeviceName { get; }
        string Description { get; }
        DisplayMode CurrentDisplayMode { get; }
        DisplayModeCollection SupportedDisplayModes { get; }

        bool IsProfileSupported(GraphicsProfile graphicsProfile);

        bool QueryBackBufferFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat,
            int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat,
            out int selectedMultiSampleCount);

        bool QueryRenderTargetFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat,
            int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat,
            out int selectedMultiSampleCount);
    }
}
