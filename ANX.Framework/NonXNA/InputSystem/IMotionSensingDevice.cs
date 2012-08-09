#if XNAEXT

#region Using Statements
using System;
using ANX.Framework.Input;
using ANX.Framework.Input.MotionSensing;
using ANX.Framework.Graphics;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.NonXNA
{
    public interface IMotionSensingDevice : IDisposable
    {
        GraphicsDevice GraphicsDevice { get; set; }

        MotionSensingDeviceType DeviceType { get; }

        MotionSensingDeviceState GetState();
    }
}

#endif