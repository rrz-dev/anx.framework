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
    [PercentageComplete(50)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Untested)]
    public sealed class GraphicsAdapter
    {
        public static ReadOnlyCollection<GraphicsAdapter> Adapters { get; private set; }

        public static GraphicsAdapter DefaultAdapter
        {
            get { return Adapters.First(a => a.IsDefaultAdapter); }
        }

        public static bool UseNullDevice { get; set; }
		public static bool UseReferenceDevice { get; set; }
		public int DeviceId { get; set; }
		public string DeviceName { get; set; }
		public bool IsDefaultAdapter { get; set; }
		public int Revision { get; set; }
		public int SubSystemId { get; set; }
		public int VendorId { get; set; }
        public string Description { get; set; }
		public DisplayMode CurrentDisplayMode { get; set; }
        public DisplayModeCollection SupportedDisplayModes { get; set; }
		public IntPtr MonitorHandle { get; set; }

		public bool IsWideScreen
		{
			get
			{
				throw new NotImplementedException();
			}
		}

        static GraphicsAdapter()
		{
			var creator = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>();
			Adapters = new ReadOnlyCollection<GraphicsAdapter>(creator.GetAdapterList());
        }

        public bool IsProfileSupported(GraphicsProfile graphicsProfile)
        {
            throw new NotImplementedException();
        }

        public bool QueryBackBufferFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat,
			int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat,
			out int selectedMultiSampleCount)
        {
            throw new NotImplementedException();
        }

        public bool QueryRenderTargetFormat(GraphicsProfile graphicsProfile, SurfaceFormat format, DepthFormat depthFormat,
			int multiSampleCount, out SurfaceFormat selectedFormat, out DepthFormat selectedDepthFormat,
			out int selectedMultiSampleCount)
        {
            throw new NotImplementedException();
        }
    }
}
