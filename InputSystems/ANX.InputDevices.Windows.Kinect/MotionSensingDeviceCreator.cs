using ANX.Framework.NonXNA;
using ANX.Framework.NonXNA.InputSystem;

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.InputDevices.Windows.Kinect
{
	public class MotionSensingDeviceCreator : IMotionSensingDeviceCreator
	{
		public string Name
		{
			get
			{
				return "Kinect";
			}
		}

		public int Priority
		{
			get
			{
				return 10000;
			}
		}

		public IMotionSensingDevice CreateDevice()
		{
			return new Kinect();
		}


        public string Provider
        {
            get { return "Microsoft.Research"; }
        }
    }
}
