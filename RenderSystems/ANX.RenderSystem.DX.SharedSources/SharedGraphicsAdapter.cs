using System;
using SharpDX.D3DCompiler;
using System.IO;
using System.Linq;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA;
using SharpDX.DXGI;
using System.Collections.Generic;

#if DX10
using Dx = SharpDX.Direct3D10;
#elif DX11
using Dx = SharpDX.Direct3D11;
#endif

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if DX10
namespace ANX.RenderSystem.Windows.DX10
#elif DX11
namespace ANX.RenderSystem.Windows.DX11
#endif
{
    public partial class DirectXGraphicsAdapter : INativeGraphicsAdapter
    {
        Output _output;
        Adapter _adapter;
        bool _isDefaultAdapter;

        #region Public
        public IntPtr MonitorHandle
        {
            get { return _output.Description.MonitorHandle; }
        }

        public bool IsDefaultAdapter
        {
            get { return _isDefaultAdapter; }
        }

        public int Revision
        {
            get { return _adapter.Description.Revision; }
        }

        public int SubSystemId
        {
            get { return _adapter.Description.SubsystemId; }
        }

        public int DeviceId
        {
            get { return _adapter.Description.DeviceId; }
        }

        public int VendorId
        {
            get { return _adapter.Description.VendorId; }
        }

        public string DeviceName
        {
            get { return _output.Description.DeviceName; }
        }

        public string Description
        {
            get { return _adapter.Description.Description; }
        }

        public DisplayMode CurrentDisplayMode
        {
            get
            {
                //Display mode can change when changing resolutions.
                //The output description will be automatically updated it seems, we only have the return the correct display mode.
                return new DisplayMode(_output.Description.DesktopBounds.Width, _output.Description.DesktopBounds.Height, SurfaceFormat.Color);
            }
        }

        public DisplayModeCollection SupportedDisplayModes
        {
            get;
            private set;
        }

        public Output Output
        {
            get { return _output; }
        }

        public Adapter Adapter
        {
            get { return _adapter; }
        }

        public ModeDescription CurrentModeDescription
        {
            get
            {
                return _output.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced).Where(
                    (x) => 
                        {
                            var bounds = _output.Description.DesktopBounds;
                            return x.Width == bounds.Width && x.Height == bounds.Height; //TODO: Check refreshRate too.
                        }).FirstOrDefault();
            }
        }
        #endregion

        #region Constructors
        public DirectXGraphicsAdapter(Adapter adapter, Output output, bool isDefaultAdapter)
        {
            this._adapter = adapter;
            this._output = output;
            this._isDefaultAdapter = isDefaultAdapter;

            List<DisplayMode> supportedModes = new List<DisplayMode>();
            var modeList = output.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);
            foreach (ModeDescription modeDescription in modeList)
            {
                var displayMode = new DisplayMode(modeDescription.Width, modeDescription.Height, DxFormatConverter.Translate(modeDescription.Format));
                supportedModes.Add(displayMode);
            }
            
            this.SupportedDisplayModes = new DisplayModeCollection(supportedModes);
        }
        #endregion

        public ModeDescription GetClosestMatchingMode(Dx.Device device, int width, int height)
        {
            ModeDescription closestMatch;
            _output.GetClosestMatchingMode(device, new ModeDescription(width, height, new Rational(0, 0), Format.R8G8B8A8_UNorm), out closestMatch);

            return closestMatch;
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