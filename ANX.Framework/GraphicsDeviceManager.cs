#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.Graphics;

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

        public static readonly int DefaultBackBufferWidth = 800;
        public static readonly int DefaultBackBufferHeight = 600;   //TODO: this is 480 in the original XNA

        public event EventHandler<EventArgs> Disposed;
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
        public event EventHandler<PreparingDeviceSettingsEventArgs> PreparingDeviceSettings;

        #endregion // Private Members

        public GraphicsDeviceManager(Game game)
        {
            //TODO: check arguments for null

            this.game = game;

            //TODO: check for duplicates
            game.Services.AddService(typeof(IGraphicsDeviceManager), this);
            game.Services.AddService(typeof(IGraphicsDeviceService), this);

        }

        public bool BeginDraw()
        {
            return true;
        }

        public void CreateDevice()
        {
            if (this.graphicsDevice != null)
            {
                this.graphicsDevice.Dispose();
                this.graphicsDevice = null;
            }

            //TODO: validate graphics device

            PresentationParameters presentationParameters = new PresentationParameters();
            presentationParameters.DeviceWindowHandle = game.Window.Handle;
            presentationParameters.BackBufferWidth = DefaultBackBufferWidth;   //TODO: set real default sizes
            presentationParameters.BackBufferHeight = DefaultBackBufferHeight;
            this.graphicsDevice = new GraphicsDevice(presentationParameters);

            //TODO: hookup events
        }

        public void EndDraw()
        {
            this.graphicsDevice.Present();
        }

        public void ApplyChanges()
        {
            throw new NotImplementedException();
        }

        public void ToggleFullScreen()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        protected GraphicsDeviceInformation FindBestDevice(bool anySuitableDevice)
        {
            throw new NotImplementedException();
        }

        protected virtual bool CanResetDevice(GraphicsDeviceInformation newDeviceInfo)
        {
            throw new NotImplementedException();
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

        protected virtual void Dispose(bool disposing
        )
        {
            throw new NotImplementedException();
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return this.graphicsDevice; }
        }

        public GraphicsProfile GraphicsProfile
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public DepthFormat PreferredDepthStencilFormat
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public SurfaceFormat PreferredBackBufferFormat
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int PreferredBackBufferWidth
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int PreferredBackBufferHeight
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool IsFullScreen
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool SynchronizeWithVerticalRetrace
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool PreferMultiSampling
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public DisplayOrientation SupportedOrientations
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
