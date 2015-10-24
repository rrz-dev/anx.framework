using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.Graphics
{
    [PercentageComplete(100)]
    [Developer("Glatzemann, KorsarNek")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class GraphicsAdapter
    {
        private static ReadOnlyCollection<GraphicsAdapter> _adapters;

        public static ReadOnlyCollection<GraphicsAdapter> Adapters
        {
            get
            {
                if (_adapters == null)
                {
                    var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
                    _adapters = new ReadOnlyCollection<GraphicsAdapter>(creator.GetAdapterList().Select((x) => new GraphicsAdapter(x)).ToArray());
                }
                return _adapters;
            }
        }

        public static GraphicsAdapter DefaultAdapter
        {
            get { return Adapters.First(a => a.IsDefaultAdapter); }
        }

        public static bool UseNullDevice { get; set; }
        public static bool UseReferenceDevice { get; set; }

        private INativeGraphicsAdapter nativeAdapter;

        #region Public
        public int DeviceId
        {
            get { return nativeAdapter.DeviceId; }
        }

        public string DeviceName
        {
            get { return nativeAdapter.DeviceName; }
        }

        public bool IsDefaultAdapter
        {
            get { return nativeAdapter.IsDefaultAdapter; }
        }

        public int Revision
        {
            get { return nativeAdapter.Revision; }
        }

        public int SubSystemId
        {
            get { return nativeAdapter.SubSystemId; }
        }

        public int VendorId
        {
            get { return nativeAdapter.VendorId; }
        }

        public string Description
        {
            get { return nativeAdapter.Description; }
        }

        public DisplayMode CurrentDisplayMode
        {
            get { return nativeAdapter.CurrentDisplayMode; }
        }

        public DisplayModeCollection SupportedDisplayModes
        {
            get { return nativeAdapter.SupportedDisplayModes; }
        }

        public IntPtr MonitorHandle
        {
            get { return nativeAdapter.MonitorHandle; }
        }

        public bool IsWideScreen
        {
            get
            {
                return this.CurrentDisplayMode.AspectRatio > (16 / 10);
            }
        }
        #endregion

        internal INativeGraphicsAdapter NativeAdapter
        {
            get { return nativeAdapter; }
        }

        internal GraphicsAdapter(INativeGraphicsAdapter nativeAdapter)
        {
            if (nativeAdapter == null)
                throw new ArgumentNullException("nativeAdapter");

            this.nativeAdapter = nativeAdapter;
        }

        public bool IsProfileSupported(GraphicsProfile graphicsProfile)
        {
            return nativeAdapter.IsProfileSupported(graphicsProfile);
        }

        public bool QueryBackBufferFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat,
            int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat,
            out int selectedMultiSampleCount)
        {
            return nativeAdapter.QueryBackBufferFormat(graphicsProfile, format, depthFormat, multiSampleCount, out selectedFormat, out selectedDepthFormat, out selectedMultiSampleCount);
        }

        public bool QueryRenderTargetFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat,
            int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat,
            out int selectedMultiSampleCount)
        {
            return nativeAdapter.QueryRenderTargetFormat(graphicsProfile, format, depthFormat, multiSampleCount, out selectedFormat, out selectedDepthFormat, out selectedMultiSampleCount);
        }
    }
}
