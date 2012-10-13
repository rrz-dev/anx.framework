using ANX.Framework.NonXNA.Development;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

#if XNAEXT
namespace ANX.Framework.Input.MotionSensing
{
    [PercentageComplete(100)]
    [TestState(TestStateAttribute.TestState.Tested)]
    public enum MotionSensingDeviceType
    {
        Kinect,
    }
}
#endif