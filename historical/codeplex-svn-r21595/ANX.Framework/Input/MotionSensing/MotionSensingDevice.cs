#region Using Statements
using System;
using ANX.Framework.NonXNA;
using ANX.Framework.Graphics;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if XNAEXT

namespace ANX.Framework.Input.MotionSensing
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Untested)]
    public static class MotionSensingDevice
    {
        private static readonly IMotionSensingDevice motionSensingDevice;

        public static GraphicsDevice GraphicsDevice
        {
            get { return motionSensingDevice.GraphicsDevice; }
            set { motionSensingDevice.GraphicsDevice = value; }
        }

        public static MotionSensingDeviceType DeviceType
        {
            get { return motionSensingDevice.DeviceType; }
        }

        static MotionSensingDevice()
        {
            var creator = AddInSystemFactory.Instance.GetDefaultCreator<IInputSystemCreator>();
            motionSensingDevice = creator.MotionSensingDevice;
        }

        public static MotionSensingDeviceState GetState() 
        {
            return motionSensingDevice.GetState();
        }
    }
}

#endif
