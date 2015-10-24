using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ANX.RenderSystem.GL3
{
    public class GraphicsAdapterGL3 : INativeGraphicsAdapter
    {
        SurfaceFormat surfaceFormat;

        public IntPtr MonitorHandle
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDefaultAdapter
        {
            get { return this.DisplayDevice.IsPrimary; }
        }

        public int Revision
        {
            get { throw new NotImplementedException(); }
        }

        public int SubSystemId
        {
            get { throw new NotImplementedException(); }
        }

        public int DeviceId
        {
            get { throw new NotImplementedException(); }
        }

        public int VendorId
        {
            get { throw new NotImplementedException(); }
        }

        public string DeviceName
        {
            get { throw new NotImplementedException(); }
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public DisplayDevice DisplayDevice
        {
            get;
            private set;
        }

        public GraphicsAdapterGL3(DisplayDevice device, SurfaceFormat surfaceFormat)
        {
            this.DisplayDevice = device;
            this.surfaceFormat = surfaceFormat;

            List<DisplayMode> modes = new List<DisplayMode>();
            foreach (DisplayResolution res in device.AvailableResolutions)
                modes.Add(new DisplayMode(res.Width, res.Height, surfaceFormat));

            this.SupportedDisplayModes = new DisplayModeCollection(modes);
        }

        public Framework.Graphics.DisplayMode CurrentDisplayMode
        {
            get 
            {
                return new DisplayMode(this.DisplayDevice.Width, this.DisplayDevice.Height, surfaceFormat);
            }
        }

        public Framework.Graphics.DisplayModeCollection SupportedDisplayModes
        {
            get;
            private set;
        }

        public bool IsProfileSupported(Framework.Graphics.GraphicsProfile graphicsProfile)
        {
            throw new NotImplementedException();
        }

        public bool QueryBackBufferFormat(Framework.Graphics.GraphicsProfile graphicsProfile, Framework.Graphics.SurfaceFormat format, Framework.Graphics.DepthFormat depthFormat, int multiSampleCount, out Framework.Graphics.SurfaceFormat selectedFormat, out Framework.Graphics.DepthFormat selectedDepthFormat, out int selectedMultiSampleCount)
        {
            throw new NotImplementedException();
        }

        public bool QueryRenderTargetFormat(Framework.Graphics.GraphicsProfile graphicsProfile, Framework.Graphics.SurfaceFormat format, Framework.Graphics.DepthFormat depthFormat, int multiSampleCount, out Framework.Graphics.SurfaceFormat selectedFormat, out Framework.Graphics.DepthFormat selectedDepthFormat, out int selectedMultiSampleCount)
        {
            throw new NotImplementedException();
        }
    }
}
