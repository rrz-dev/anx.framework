#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;
using System.IO;
using ANX.Framework.NonXNA;

#endregion // Using Statements

#region License

//
// This file is part of the ANX.Framework created by the "ANX.Framework developer group".
//
// This file is released under the Ms-PL license.
//
//
//
// Microsoft Public License (Ms-PL)
//
// This license governs use of the accompanying software. If you use the software, you accept this license. 
// If you do not accept the license, do not use the software.
//
// 1.Definitions
//   The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning 
//   here as under U.S. copyright law.
//   A "contribution" is the original software, or any additions or changes to the software.
//   A "contributor" is any person that distributes its contribution under this license.
//   "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//
// 2.Grant of Rights
//   (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations 
//       in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to 
//       reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution
//       or any derivative works that you create.
//   (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in 
//       section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed
//       patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution 
//       in the software or derivative works of the contribution in the software.
//
// 3.Conditions and Limitations
//   (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//   (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your 
//       patent license from such contributor to the software ends automatically.
//   (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution 
//       notices that are present in the software.
//   (D) If you distribute any portion of the software in source code form, you may do so only under this license by including
//       a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or 
//       object code form, you may only do so under a license that complies with this license.
//   (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees,
//       or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the
//       extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a 
//       particular purpose and non-infringement.

#endregion // License

namespace ANX.Framework
{
    public class GraphicsDeviceManager : IGraphicsDeviceManager, IDisposable, IGraphicsDeviceService
    {
        #region Private Members
        private Game game;
        private GraphicsDevice graphicsDevice;
        private int backBufferWidth = DefaultBackBufferWidth;
        private int backBufferHeight = DefaultBackBufferHeight;
        private SurfaceFormat backBufferFormat = SurfaceFormat.Color;
        private DepthFormat depthStencilFormat = DepthFormat.Depth24;
        private GraphicsProfile graphicsProfile;
        private bool isFullScreen;
        private bool multiSampling;
        private GraphicsDeviceInformation currentGraphicsDeviceInformation;

        #endregion // Private Members

        public static readonly int DefaultBackBufferWidth = 800;
        public static readonly int DefaultBackBufferHeight = 480;

        public event EventHandler<EventArgs> Disposed;
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
        public event EventHandler<PreparingDeviceSettingsEventArgs> PreparingDeviceSettings;

        public GraphicsDeviceManager(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException("game");
            }

            this.game = game;

            if (game.Services.GetService(typeof(IGraphicsDeviceManager)) != null)
            {
                throw new ArgumentException("The GraphicsDeviceManager was already registered to the game class");
            }
            game.Services.AddService(typeof(IGraphicsDeviceManager), this);

            if (game.Services.GetService(typeof(IGraphicsDeviceService)) != null)
            {
                throw new ArgumentException("The GraphicsDeviceService was already registered to the game class");
            }
            game.Services.AddService(typeof(IGraphicsDeviceService), this);

            game.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            game.Window.ScreenDeviceNameChanged += new EventHandler<EventArgs>(Window_ScreenDeviceNameChanged);
            game.Window.OrientationChanged += new EventHandler<EventArgs>(Window_OrientationChanged);

            this.graphicsProfile = FetchGraphicsProfile();
        }

        void Window_OrientationChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Window_ScreenDeviceNameChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool BeginDraw()
        {
            return true;
        }

        public void CreateDevice()
        {
            ApplyChanges();
        }

        private void CreateDevice(GraphicsDeviceInformation deviceInformation)
        {
            if (this.graphicsDevice != null)
            {
                System.Diagnostics.Debugger.Break(); // Test this!!!
                this.graphicsDevice.Dispose();
                this.graphicsDevice = null;
            }

            //TODO: validate graphics device

            this.graphicsDevice = new GraphicsDevice(deviceInformation.Adapter, deviceInformation.GraphicsProfile, deviceInformation.PresentationParameters);
            GraphicsResourceTracker.Instance.UpdateGraphicsDeviceReference(this.graphicsDevice);

            //TODO: hookup events
            this.graphicsDevice.DeviceResetting += new EventHandler<EventArgs>(graphicsDevice_DeviceResetting);
            this.graphicsDevice.DeviceReset += new EventHandler<EventArgs>(graphicsDevice_DeviceReset);

            OnDeviceCreated(this, EventArgs.Empty);
        }

        void graphicsDevice_DeviceReset(object sender, EventArgs e)
        {
            OnDeviceReset(this, EventArgs.Empty);
        }

        void graphicsDevice_DeviceResetting(object sender, EventArgs e)
        {
            OnDeviceResetting(this, EventArgs.Empty);
        }

        public void EndDraw()
        {
            this.graphicsDevice.Present();
        }

        public void ApplyChanges()
        {
            GraphicsDeviceInformation graphicsDeviceInformation = FindBestDevice(true);
            OnPreparingDeviceSettings(this, new PreparingDeviceSettingsEventArgs(graphicsDeviceInformation));

            if (graphicsDevice != null)
            {
                if (this.CanResetDevice(graphicsDeviceInformation))
                {
                    OnDeviceResetting(this, EventArgs.Empty);

                    this.graphicsDevice.Reset(graphicsDeviceInformation.PresentationParameters, graphicsDeviceInformation.Adapter);

                    OnDeviceReset(this, EventArgs.Empty);
                }
                else
                {
                    //graphicsDevice.Dispose();
                    //graphicsDevice = null;
                    // Dispose could not be used here because all references to graphicsDevice get dirty!
                    
                    graphicsDevice.Recreate(graphicsDeviceInformation.PresentationParameters);
                }
            }

            if (graphicsDevice == null)
            {
                CreateDevice(graphicsDeviceInformation);
            }

            this.currentGraphicsDeviceInformation = graphicsDeviceInformation;
        }

        public void ToggleFullScreen()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        protected GraphicsDeviceInformation FindBestDevice(bool anySuitableDevice)
        {
            //TODO: implement FindBestDevice

            GraphicsDeviceInformation deviceInformation = new GraphicsDeviceInformation();

            deviceInformation.PresentationParameters.DeviceWindowHandle = game.Window.Handle;
            deviceInformation.PresentationParameters.BackBufferFormat = backBufferFormat;
            deviceInformation.PresentationParameters.BackBufferWidth = backBufferWidth;
            deviceInformation.PresentationParameters.BackBufferHeight = backBufferHeight;
            deviceInformation.PresentationParameters.DepthStencilFormat = depthStencilFormat;
            
            return deviceInformation;
        }

        protected virtual bool CanResetDevice(GraphicsDeviceInformation newDeviceInfo)
        {
            if (newDeviceInfo.Adapter == currentGraphicsDeviceInformation.Adapter &&
                newDeviceInfo.GraphicsProfile == currentGraphicsDeviceInformation.GraphicsProfile &&
//                newDeviceInfo.PresentationParameters.DepthStencilFormat == currentGraphicsDeviceInformation.PresentationParameters.DepthStencilFormat &&
                newDeviceInfo.PresentationParameters.BackBufferFormat == currentGraphicsDeviceInformation.PresentationParameters.BackBufferFormat)
            {
                return true;
            }

            //TODO: implement CanResetDevice completly

            return false;
        }

        protected virtual void RankDevices(List<GraphicsDeviceInformation> foundDevices)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnDeviceCreated(Object sender, EventArgs args)
        {
            if (DeviceCreated != null)
            {
                DeviceCreated(sender, args);
            }
        }

        protected virtual void OnDeviceDisposing(Object sender, EventArgs args)
        {
            if (DeviceDisposing != null)
            {
                DeviceDisposing(sender, args);
            }
        }

        protected virtual void OnDeviceReset(Object sender, EventArgs args)
        {
            if (DeviceReset != null)
            {
                DeviceReset(sender, args);
            }
        }

        protected virtual void OnDeviceResetting(Object sender, EventArgs args)
        {
            if (DeviceResetting != null)
            {
                DeviceResetting(sender, args);
            }
        }

        protected virtual void OnPreparingDeviceSettings(Object sender, PreparingDeviceSettingsEventArgs args)
        {
            if (PreparingDeviceSettings != null)
            {
                PreparingDeviceSettings(sender, args);
            }
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return this.graphicsDevice; }
        }

        public GraphicsProfile GraphicsProfile
        {
            get 
            { 
                return this.currentGraphicsDeviceInformation.GraphicsProfile; 
            }
            set 
            { 
                throw new NotImplementedException(); 
            }
        }

        public DepthFormat PreferredDepthStencilFormat
        {
            get 
            {
                return this.currentGraphicsDeviceInformation.PresentationParameters.DepthStencilFormat; 
            }
            set 
            { 
                this.depthStencilFormat = value; 
            }
        }

        public SurfaceFormat PreferredBackBufferFormat
        {
            get 
            { 
                return this.currentGraphicsDeviceInformation.PresentationParameters.BackBufferFormat; 
            }
            set 
            { 
                this.backBufferFormat = value; 
            }
        }

        public int PreferredBackBufferWidth
        {
            get 
            { 
                return this.currentGraphicsDeviceInformation.PresentationParameters.BackBufferWidth; 
            }
            set 
            { 
                this.backBufferWidth = value; 
            }
        }

        public int PreferredBackBufferHeight
        {
            get 
            { 
                return this.currentGraphicsDeviceInformation.PresentationParameters.BackBufferHeight; 
            }
            set 
            { 
                this.backBufferHeight = value; 
            }
        }

        public bool IsFullScreen
        {
            get 
            { 
                return this.currentGraphicsDeviceInformation.PresentationParameters.IsFullScreen; 
            }
            set 
            { 
                throw new NotImplementedException(); 
            }
        }

        public bool SynchronizeWithVerticalRetrace
        {
            get 
            {
                if (graphicsDevice != null && graphicsDevice.NativeDevice != null)
                {
                    return graphicsDevice.NativeDevice.VSync;
                }

                return true;
            }
            set 
            {
                graphicsDevice.NativeDevice.VSync = value;
            }
        }

        public bool PreferMultiSampling
        {
            get 
            { 
                return this.currentGraphicsDeviceInformation.PresentationParameters.MultiSampleCount > 0; 
            }
            set { throw new NotImplementedException(); }
        }

        public DisplayOrientation SupportedOrientations
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        private GraphicsProfile FetchGraphicsProfile()
        {
            Stream manifestResourceStream = this.game.GetType().Assembly.GetManifestResourceStream("Microsoft.Xna.Framework.RuntimeProfile");
            
            if (manifestResourceStream != null)
            {
                using (StreamReader reader = new StreamReader(manifestResourceStream))
                {
                    if (reader.ReadToEnd().Contains("HiDef"))
                    {
                        return GraphicsProfile.HiDef;
                    }
                }
            }

            return GraphicsProfile.Reach;
        }
    }
}
