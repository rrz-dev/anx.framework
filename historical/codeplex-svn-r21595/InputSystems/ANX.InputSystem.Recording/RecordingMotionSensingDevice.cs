#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
#if XNAEXT
using ANX.Framework.Input.MotionSensing;
#endif

#endregion

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputSystem.Recording
{
#if XNAEXT

    /// <summary>
    /// Wrapper aroung another IMotionSensingDevice, will record all inputs and allows playback.
    /// </summary>
    public class RecordingMotionSensingDevice : RecordableDevice, IMotionSensingDevice
    {
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MotionSensingDeviceType DeviceType
        {
            get { throw new NotImplementedException(); }
        }

        public MotionSensingDeviceState GetState()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
#endif
}
