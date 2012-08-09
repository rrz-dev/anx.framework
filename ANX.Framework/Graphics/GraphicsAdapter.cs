#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using ANX.Framework.NonXNA;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    public sealed class GraphicsAdapter
    {
        private static List<GraphicsAdapter> adapters;
        private DisplayModeCollection supportedDisplayModes;

        static GraphicsAdapter()
        {
            adapters = new List<GraphicsAdapter>();

            IRenderSystemCreator renderSystemCreator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
            adapters.AddRange(renderSystemCreator.GetAdapterList());
        }

        public static ReadOnlyCollection<GraphicsAdapter> Adapters
        {
            get
            {
                return new ReadOnlyCollection<GraphicsAdapter>(adapters);
            }
        }

        public static GraphicsAdapter DefaultAdapter
        {
            get
            {
                GraphicsAdapter defaultAdapter = adapters.Where(a => a.IsDefaultAdapter).First<GraphicsAdapter>();
                return defaultAdapter;
            }
        }

        public static bool UseNullDevice
        {
            get;
            set;
        }

        public static bool UseReferenceDevice
        {
            get;
            set;
        }

        public DisplayMode CurrentDisplayMode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Description
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int DeviceId
        {
            get;
            set;
        }

        public string DeviceName
        {
            get;
            set;
        }

        public bool IsDefaultAdapter
        {
            get;
            set;
        }

        public bool IsWideScreen
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IntPtr MonitorHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Revision
        {
            get;
            set;
        }

        public int SubSystemId
        {
            get;
            set;
        }

        public DisplayModeCollection SupportedDisplayModes
        {
            get
            {
                return this.supportedDisplayModes;
            }
            set
            {
                this.supportedDisplayModes = value;
            }
        }

        public int VendorId
        {
            get;
            set;
        }

        public bool IsProfileSupported(GraphicsProfile graphicsProfile)
        {
            throw new NotImplementedException();
        }

        public bool QueryBackBufferFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat, int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat, out int selectedMultiSampleCount)
        {
            throw new NotImplementedException();
        }

        public bool QueryRenderTargetFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat, int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat, out int selectedMultiSampleCount)
        {
            throw new NotImplementedException();
        }

    }
}
