#region Using Statements
using System;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if XNAEXT

namespace ANX.Framework.Input.MotionSensing
{
    public class MotionSensingDevice
    {
        private static IMotionSensingDevice motionSensingDevice;

        static MotionSensingDevice()
        {
            motionSensingDevice = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>().MotionSensingDevice;
        }

        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                if (motionSensingDevice != null)
                {
                    return motionSensingDevice.GraphicsDevice;
                }

                return null;
            }
            set
            {
                if (motionSensingDevice != null)
                {
                    motionSensingDevice.GraphicsDevice = value;
                }
            }
        }

        public static MotionSensingDeviceType DeviceType
        {
            get
            {
                return motionSensingDevice.DeviceType; 
            }
        }

        public static MotionSensingDeviceState GetState() 
        {
            return motionSensingDevice.GetState();
        }
    }
}

#endif
